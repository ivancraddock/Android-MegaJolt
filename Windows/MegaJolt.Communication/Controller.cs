#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MegaJolt.Communication.Commands;
using MegaJolt.Communication.Responses;
using Timer = System.Threading.Timer;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class Controller : IDisposable
    {
        #region Controller(ICommunicationPort, IPersistedStorage)
        public Controller(ICommunicationPort communicationPort, IPersistedStorage persistedStorage)
        {
            CommunicationPort = communicationPort;
            CommunicationPortLock = new object();
            
            RefreshInterval = 150;

            GlobalConfiguration = new GlobalConfiguration(this);
            IgnitionMap = new IgnitionMap(this, persistedStorage);
            CurrentState = new CurrentState(this);
        }
        #endregion Controller

        #region EVENTS
        public event EventHandler<LogEventArgs> LogMessage;
        public event EventHandler StateChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region CommunicationPort Property
        private ICommunicationPort CommunicationPort { get; set; }
        #endregion CommunicationPort
        #region CommunicationPortLock Property
        private object CommunicationPortLock { get; set; }
        #endregion CommunicationPortLock
        #region IsDisposed Property
        public bool IsDisposed { get; set; }
        #endregion IsDisposed

        #region IsConnected Property
        public bool IsConnected { get { return CommunicationPort != null && CommunicationPort.IsConnected; } }
        #endregion IsConnected
        #region ComPort Property
        public string ComPort { get; set; }
        #endregion ComPort
        #region RefreshInterval Property
        private int refreshInterval;
        public int RefreshInterval
        {
            get { return refreshInterval; }
            set
            {
                if (refreshInterval == value) return;
                StopUpdateTimer();
                refreshInterval = value;
                if (IsConnected) StartUpdateTimer();
            }
        }
        #endregion RefreshInterval
        #region RefreshInProgress Property
        private bool RefreshInProgress { get; set; }
        #endregion RefreshInProgress

        #region UpdateStateTimer Property
        private Timer UpdateStateTimer { get; set; }
        #endregion UpdateStateTimer

        #region GlobalConfiguration Property
        public GlobalConfiguration GlobalConfiguration { get; private set; }
        #endregion GlobalConfiguration
        #region IgnitionMap Property
        public IgnitionMap IgnitionMap { get; private set; }
        #endregion IgnitionMap
        #region CurrentState Property
        public CurrentState CurrentState { get; private set; }
        #endregion CurrentState
        #endregion PROPERTIES

        #region METHODS
        #region Dispose()
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose
        #region Dispose(bool)
        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing && !IsDisposed)
                {
                    Close();
                    CommunicationPort.Dispose();
                    CommunicationPort = null;
                }
                IsDisposed = true;
            }
        }
        #endregion Dispose

        #region Log(LogLevel, string, object[])
        internal void Log(LogLevel level, string messageFormat, params object[] args)
        {
            if (LogMessage == null || string.IsNullOrEmpty(messageFormat) || level == LogLevel.Off) return;
            LogMessage(this, new LogEventArgs(level, string.Format(messageFormat, args)));
        }
        #endregion Log
        #region BytesToString(IEnumerable<byte>)
        private string BytesToString(IEnumerable<byte> bytes)
        {
            if (!bytes.Any()) return string.Empty;
            return string.Join(" ", bytes.Select(item => item.ToString("X").PadLeft(2, '0')).ToArray());
        }
        #endregion BytesToString

        #region Open()
        public bool Open()
        {
            if (CommunicationPort.IsConnected) return true;

            lock (CommunicationPortLock)
            {
                Log(LogLevel.Information, "Opening communicaton port: {0}", ComPort);
                bool success = CommunicationPort.Open(ComPort);
                if (success)
                {
                    OnStateChanged();

                    GetGlobalConfiguration();
                    GetIgnitionConfiguration();
                    StartUpdateTimer();
                }
                return success;
            }
        }
        #endregion Open
        #region Close()
        public void Close()
        {
            lock (CommunicationPortLock)
            {
                //Make sure the port is not already closed.
                if (!CommunicationPort.IsConnected) return;

                //Make sure we are not currently updating the state and
                //stop the timer so we don't try to update the state after we close the port.
                while (RefreshInProgress)
                {
                    ManualResetEvent manualEvent = new ManualResetEvent(false);
                    manualEvent.WaitOne(100);
                    manualEvent.Dispose();
                }
                StopUpdateTimer();

                //Close the serial port.
                Log(LogLevel.Information, "Closing communicaton port");
                CommunicationPort.Close();

                //Make sure the current state is cleared.
                CurrentState.Reset();
                OnStateChanged();
            }
        }
        #endregion Close
        #region StartUpdateTimer()
        private void StartUpdateTimer()
        {
            if (RefreshInterval <= 0) return;
            Log(LogLevel.Information, "Starting the refresh timer: {0}ms", RefreshInterval);
            UpdateStateTimer = new Timer(GetCurrentSate, null, RefreshInterval, RefreshInterval);
        }
        #endregion StartUpdateTimer
        #region StopUpdateTimer()
        private void StopUpdateTimer()
        {
            if (UpdateStateTimer != null)
            {
                Log(LogLevel.Information, "Stopping the refresh timer.");
                UpdateStateTimer.Dispose();
                UpdateStateTimer = null;
            }
        }
        #endregion StopUpdateTimer

        #region GetGlobalConfiguration()
        public void GetGlobalConfiguration()
        {
            GetGlobalConfigurationResponse response = (GetGlobalConfigurationResponse)SendCommand(new GetGlobalConfigurationCommand());
            if (response != null) GlobalConfiguration.Populate(response);
        }
        #endregion GetGlobalConfiguration
        #region GetIgnitionConfiguration()
        public void GetIgnitionConfiguration()
        {
            GetIgnitionConfigurationResponse response = (GetIgnitionConfigurationResponse)SendCommand(new GetIgnitionConfigurationCommand());
            if (response != null) IgnitionMap.Populate(response);
        }
        #endregion GetIgnitionConfiguration
        #region GetCurrentSate(object)
        public void GetCurrentSate(object userState)
        {
            if (RefreshInProgress) return;
            RefreshInProgress = true;

            GetStateResponse response = (GetStateResponse)SendCommand(new GetStateCommand());
            if (response != null) CurrentState.Populate(response, GlobalConfiguration.EdisType);

            RefreshInProgress = false;
        }
        #endregion GetCurrentSate

        #region UpdateGlobalConfiguration()
        public void UpdateGlobalConfiguration()
        {
            SendCommand(new UpdateGlobalConfigurationCommand(GlobalConfiguration));
        }
        #endregion UpdateGlobalConfiguration
        #region UpdateIgnitionConfiguration()
        public void UpdateIgnitionConfiguration()
        {
            Response response = SendCommand(new UpdateIgnitionConfigurationCommand(IgnitionMap));
            if (response != null) IgnitionMap.SavedState = IgnitionMap.SavedState ^ SavedStates.SavedToController;
        }
        #endregion UpdateIgnitionConfiguration
        #region FlashIgnitionConfiguration()
        public void FlashIgnitionConfiguration()
        {
            Response response = SendCommand(new FlashIgnitionConfigurationCommand());
            if (response != null) IgnitionMap.SavedState = IgnitionMap.SavedState ^ SavedStates.WrittenToFlash;
        }
        #endregion FlashIgnitionConfiguration

        #region SendCommand(Command)
        private Response SendCommand(Command command)
        {
            if (!Open()) return null;
            try
            {
                List<byte> bytes = new List<byte>() { command.CommandNumber };
                bytes.AddRange(command.Data);

                lock (CommunicationPortLock)
                {
                    Log(LogLevel.Information, "Sending Command: 0x{0}", BytesToString(bytes));
                    CommunicationPort.Write(bytes);
                    Log(LogLevel.Information, "Sent");

                    if (command.ResponseLength == 0)
                    {
                        Log(LogLevel.Information, "Command expects no response.");
                        return new EmptyResponse();
                    }

                    byte[] buffer = CommunicationPort.Read(command.ResponseLength).ToArray();
                    Log(LogLevel.Information, "Response received: 0x{0}", BytesToString(buffer));
                    return command.BuildResponse(buffer);
                }
            }
            catch (InvalidOperationException)
            {
                StopUpdateTimer();
                RefreshInProgress = false;
                Close();
                return null;
            }
        }
        #endregion SendCommand

        #region OnStateChanged()
        protected virtual void OnStateChanged()
        {
            if (StateChanged == null) return;
            StateChanged(this, EventArgs.Empty);
        }
        #endregion OnStateChanged
        #endregion METHODS
    }
}
