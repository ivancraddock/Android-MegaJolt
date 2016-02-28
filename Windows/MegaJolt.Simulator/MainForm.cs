#region USING STATEMENTS
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MegaJolt.Simulator.Properties;

#endregion USING STATEMENTS

namespace MegaJolt.Simulator
{
    public partial class MainForm : Form
    {
        #region MainForm()
        public MainForm()
        {
            InitializeComponent();

            base.CreateHandle();
            Settings setttings = Settings.Default;

            //Window Settings
            StartPosition = FormStartPosition.Manual;
            if (setttings.Height > base.MinimumSize.Height)
                Height = setttings.Height;
            if (setttings.Width > base.MinimumSize.Width)
                Width = setttings.Width;
            Left = setttings.Left > int.MinValue
                       ? setttings.Left
                       : (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            Top = setttings.Top > int.MinValue
                       ? setttings.Top
                       : (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            if (setttings.WindowState != FormWindowState.Minimized)
                WindowState = setttings.WindowState;

            //Ignition Configuration Settings
            RpmBins = setttings.RpmBins ?? new byte[10];
            LoadBins = setttings.LoadBins ?? new byte[10];
            IgnitionMap = setttings.IgnitionMap ?? new byte[100];
            UserOutputTypes = setttings.UserOutputTypes;
            UserOutputModeConfigurations = setttings.UserOutputModeConfigurations;
            UserOutput1Threshold = setttings.UserOutput1Threshold;
            UserOutput2Threshold = setttings.UserOutput2Threshold;
            UserOutput3Threshold = setttings.UserOutput3Threshold;
            UserOutput4Threshold = setttings.UserOutput4Threshold;
            RevLimitThreshold = setttings.RevLimitThreshold;
            ShiftLightThreshold = setttings.ShiftLightThreshold;
            AdvanceCorrectionBins = setttings.AdvanceCorrectionBins ?? new byte[10];
            AdvanceCorrectionValues = setttings.AdvanceCorrectionValues ?? new byte[10];
            AuxiliaryInputPeakHoldDecay = setttings.AuxiliaryInputPeakHoldDecay;

            //Application Settings
            LogMessagesCheckBox.Checked = setttings.LogMessages;
            LogPropertyChangesCheckBox.Checked = setttings.LogPropertyChanges;
            LogEventsCheckBox.Checked = setttings.LogEvents;

            //Global Settings
            VersionComboBox.SelectedIndex = VersionComboBox.FindString(setttings.Version);
            EdisTypeComboBox.SelectedIndex = EdisTypeComboBox.FindString(setttings.EdisType);
            LoadTypeComboBox.SelectedIndex = LoadTypeComboBox.FindString(setttings.LoadType);
            CrankingAdvanceNumericUpDown.Value = setttings.CrankingAdvance;
            TriggerWheelOffsetNumericUpDown.Value = setttings.TriggerWheelOffset;
            PipNoiseFilterNumericUpDown.Value = setttings.PipNoiseFilter;
            AspirationTypeComboBox.SelectedIndex = AspirationTypeComboBox.FindString(setttings.AspirationType);

            //Current State Settings
            IgnitionConfigurationComboBox.SelectedIndex = IgnitionConfigurationComboBox.FindString(setttings.IgnitionConfiguration);
            AuxiliaryInputNumericUpDown.Value = setttings.AuxiliaryInput;
            LoadNumericUpDown.Value = setttings.Load;
            RpmNumericUpDown.Value = setttings.Rpm;
            DynamicStateCheckBox.Checked = setttings.DynamicState;
            UserOutput1CheckBox.Checked = setttings.UserOutput1;
            UserOutput2CheckBox.Checked = setttings.UserOutput2;
            UserOutput3CheckBox.Checked = setttings.UserOutput3;
            UserOutput4CheckBox.Checked = setttings.UserOutput4;
        }
        #endregion MainForm

        #region FIELDS
        private const byte TpsLoadType = 1;
        private const byte MapLoadType = 2;
        #endregion FIELDS

        #region PROPERTIES
        private TcpListener Listener { get; set; }
        private Socket Socket { get; set; }
        private NetworkStream Stream { get; set; }
        private bool LogMessages { get; set; }
        private bool LogPropertyChanges { get; set; }
        private bool LogEvents { get; set; }

        //Get Version Command
        private byte[] Version { get; set; }

        //Get Global Settings Command
        private byte EdisType { get; set; }
        private byte LoadType { get; set; }
        private byte CrankingAdvance { get; set; }
        private byte PipNoiseFilter { get; set; }
        private byte TriggerWheelOffset { get; set; }
        private bool Boosted { get; set; }

        //Get Current State Command
        private byte IgnitionAdvance { get; set; }
        private ushort RpmValue { get; set; }
        private ushort RawRpmCount { get; set; }
        private byte RpmBin { get; set; }
        private byte LoadBin { get; set; }
        private byte LoadValue { get; set; }
        private byte ControllerState { get; set; }
        private byte AuxiliaryInput { get; set; }
        private byte AdvanceCorrectionBin { get; set; }
        private byte AdvanceCorrectionValue { get; set; } 
       
        //Get & Set Ignition Configuration Command
        private byte[] RpmBins { get; set; }
        private byte[] LoadBins { get; set; }
        private byte[] IgnitionMap { get; set; }
        private byte UserOutputTypes { get; set; }
        private byte UserOutputModeConfigurations { get; set; }
        private byte UserOutput1Threshold { get; set; }
        private byte UserOutput2Threshold { get; set; }
        private byte UserOutput3Threshold { get; set; }
        private byte UserOutput4Threshold { get; set; }
        private byte RevLimitThreshold { get; set; }
        private byte ShiftLightThreshold { get; set; }
        private byte[] AdvanceCorrectionBins { get; set; }
        private byte[] AdvanceCorrectionValues { get; set; }
        private ushort AuxiliaryInputPeakHoldDecay { get; set; }
        #endregion PROPERTIES

        #region METHODS
        #region LogPropertyChange(string, params object[])
        private void LogPropertyChange(string format, params object[] args)
        {
            if (!LogPropertyChanges) return;
            PublishLog(format, args);
        }
        #endregion LogPropertyChange
        #region LogMessage(string, params object[])
        private void LogMessage(string format, params object[] args)
        {
            if (!LogMessages) return;
            PublishLog(format, args);
        }
        #endregion LogMessage
        #region LogEvent(string, params object[])
        private void LogEvent(string format, params object[] args)
        {
            if (!LogEvents) return;
            PublishLog(format, args);
        }
        #endregion LogEvent
        #region PublishLog(string, object[])
        private void PublishLog(string format, params object[] args)
        {
            string message = string.Format(format, args);
            LogMessagesTextBox.Invoke(new Action(() => LogMessagesTextBox.AppendText(string.Format("{0}: {1}{2}", DateTime.Now, message, Environment.NewLine))));
        }
        #endregion PublishLog

        #region SetMaxForLoad()
        private void SetMaxForLoad()
        {
            if (LoadType == MapLoadType)
                LoadNumericUpDown.Maximum = Boosted ? 255 : 102;
            else 
                LoadNumericUpDown.Maximum = 100;
        }
        #endregion SetMaxForLoad
        #region SetControllerStateFlag(string, byte)
        private void SetControllerStateFlag(string flagName, byte mask)
        {
            ControllerState = (byte)(ControllerState | mask);
            LogMessage("{0}: Set (0x{1})", flagName, ControllerState.ToString("X").PadLeft(2, '0'));
        }
        #endregion SetControllerStateFlag
        #region ClearControllerStateFlag(string, byte)
        private void ClearControllerStateFlag(string flagName, byte mask)
        {
            ControllerState = (byte)(ControllerState & mask);
            LogMessage("{0}: Cleared (0x{1})", flagName, ControllerState.ToString("X").PadLeft(2, '0'));
        }
        #endregion ClearControllerStateFlag

        #region SendIgnitionConfigurationResponse()
        private void SendIgnitionConfigurationResponse()
        {
            byte[] response = new byte[150];
            response.Initialize();

            Array.Copy(RpmBins, response, 10);
            Array.Copy(LoadBins, 0, response, 10, 10);
            Array.Copy(IgnitionMap, 0, response, 20, 100);
            response[120] = UserOutputTypes;
            response[121] = UserOutputModeConfigurations;
            response[122] = UserOutput1Threshold;
            response[123] = UserOutput2Threshold;
            response[124] = UserOutput3Threshold;
            response[125] = UserOutput4Threshold;
            response[126] = RevLimitThreshold;
            response[127] = ShiftLightThreshold;
            Array.Copy(AdvanceCorrectionBins, 0, response, 128, 10);
            Array.Copy(AdvanceCorrectionValues, 0, response, 138, 10);
            response[148] = (byte)((AuxiliaryInputPeakHoldDecay >> 8) & 0xFF);
            response[149] = (byte)(AuxiliaryInputPeakHoldDecay & 0xFF);

            SendResponse(response);
        }
        #endregion SendIgnitionConfigurationResponse
        #region SendVersionResponse()
        private void SendVersionResponse()
        {
            SendResponse(Version);
        }
        #endregion SendVersionResponse
        #region SendGlobalConfigurationResponse()
        private void SendGlobalConfigurationResponse()
        {
            byte[] response = new byte[64];
            response.Initialize();
            response[0] = EdisType;
            response[1] = PipNoiseFilter;
            response[2] = CrankingAdvance;
            response[3] = TriggerWheelOffset;

            SendResponse(response);
        }
        #endregion SendGlobalConfigurationResponse
        #region SendStateResponse()
        private void SendStateResponse()
        {
            ushort rpmValue = RpmValue;
            byte loadValue = LoadValue;
            bool dynamicState = DynamicStateCheckBox.Checked;
            Random rnd = new Random();

            if (dynamicState)
            {
                RpmValue = (ushort)rnd.Next((ushort)(rpmValue * 0.995), (ushort)(rpmValue * 1.005));
                LoadValue = (byte)rnd.Next((byte)(loadValue * 0.98), (byte)(loadValue * 1.02));
                UpdateRpmValues();
                UpdateLoadValues();
            }

            byte[] response = new byte[9];
            response[0] = IgnitionAdvance;
            response[1] = (byte)((RawRpmCount >> 8) & 0xFF);
            response[2] = (byte)(RawRpmCount & 0xFF);
            response[3] = (byte)((RpmBin << 4) + LoadBin);
            response[4] = LoadValue;
            response[5] = ControllerState;
            response[6] = AuxiliaryInput;
            response[7] = AdvanceCorrectionBin;
            response[8] = AdvanceCorrectionValue;

            SendResponse(response);

            if (dynamicState)
            {
                RpmValue = rpmValue;
                LoadValue = loadValue;
                UpdateRpmValues();
                UpdateLoadValues();
            }
        }
        #endregion SendStateResponse
        #region SendResponse(byte[])
        private void SendResponse(byte[] response)
        {
            LogMessage("Sending response: 0x" + string.Join(" ", response.Select(item => item.ToString("X").PadLeft(2, '0')).ToArray()));
            Stream.Write(response, 0, response.Length);
            LogMessage("Sent");
        }
        #endregion SendResponse

        #region ReadCommandData(int)
        private byte[] ReadCommandData(StreamReader reader, int count)
        {
            char[] buffer = new char[count];
            reader.Read(buffer, 0, count);
            byte[] bytes = new byte[count];
            for (int x = 0; x < count; x++)
            {
                bytes[x] = (byte) buffer[x];
            }
            LogMessage("Command data read: 0x" + string.Join(" ", bytes.Select(item => item.ToString("X").PadLeft(2, '0')).ToArray()));
            return bytes;
        }
        #endregion ReadCommandData
        #region UpdateGlobalConfiguration(byte[])
        private void UpdateGlobalConfiguration(byte[] data)
        {
            if (data[0] == 4)
                EdisTypeComboBox.SelectedIndex = 0;
            else if (data[0] == 6)
                EdisTypeComboBox.SelectedIndex = 1;
            else
                EdisTypeComboBox.SelectedIndex = 2;

            PipNoiseFilterNumericUpDown.Value = data[1];
            CrankingAdvanceNumericUpDown.Value = data[2];
            TriggerWheelOffsetNumericUpDown.Value = unchecked((sbyte) data[3]);
        }
        #endregion UpdateGlobalConfiguration
        #region UpdateIgnitionConfiguration(byte[])
        private void UpdateIgnitionConfiguration(byte[] data)
        {
            Array.Copy(data, RpmBins, 10);
            Array.Copy(data, 10, LoadBins, 0, 10);
            Array.Copy(data, 20, IgnitionMap, 0, 100);
            UserOutputTypes = data[120];
            UserOutputModeConfigurations = data[121];
            UserOutput1Threshold = data[122];
            UserOutput2Threshold = data[123];
            UserOutput3Threshold = data[124];
            UserOutput4Threshold = data[125];
            RevLimitThreshold = data[126];
            ShiftLightThreshold = data[127];
            Array.Copy(data, 128, AdvanceCorrectionBins, 0, 10);
            Array.Copy(data, 138, AdvanceCorrectionValues, 0, 10);
            AuxiliaryInputPeakHoldDecay = (ushort)((data[148] << 8) + data[149]);

            UpdateRpmValues();
            UpdateLoadValues();
        }
        #endregion UpdateIgnitionConfiguration
        #region UpdateIgnitionCell(byte[])
        private void UpdateIgnitionCell(byte[] data)
        {
            int rpmBin = data[0] >> 4 & 0x0F;
            int loadBin = data[0] & 0x0F;
            IgnitionMap[(loadBin*10) + rpmBin] = data[1];
        }
        #endregion UpdateIgnitionCell
        #region UpdateRpmValues()
        private void UpdateRpmValues()
        {
            ushort cylindarFactor = (ushort)(EdisType / 2);
            RawRpmCount = (ushort)(1 / (.000001 * cylindarFactor * ((float)RpmValue / (float)60)));
            RpmBin = FindRpmBin((byte)(RpmValue / 100));
            IgnitionAdvance = FindAdvance(LoadBin, RpmBin);
            AdvanceCorrectionBin = FindAdvanceCorrectionBin((byte)(RpmValue / 100));
            AdvanceCorrectionValue = AdvanceCorrectionBins[AdvanceCorrectionBin];

            if ((RpmValue / 100) >= ShiftLightThreshold)
                SetControllerStateFlag("Shift Light", 0x20);
            else
                ClearControllerStateFlag("Shift Light", 0xDF);

            if ((RpmValue / 100) >= RevLimitThreshold)
                SetControllerStateFlag("Rev Limit", 0x10);
            else
                ClearControllerStateFlag("Rev Limit", 0xEF);

            LogMessage(" RPM set: {0}", RpmValue);
        }
        #endregion UpdateRpmValues
        #region UpdateLoadValues()
        private void UpdateLoadValues()
        {
            LoadBin = FindLoadBin(LoadValue);
            IgnitionAdvance = FindAdvance(LoadBin, RpmBin);
            LogMessage(" Load set: {0}", LoadValue);
        }
        #endregion UpdateLoadValues

        #region SaveIgnitionConfiguration()
        private void SaveIgnitionConfiguration()
        {
            Settings settings = Settings.Default;
            settings.RpmBins = RpmBins;
            settings.LoadBins = LoadBins;
            settings.IgnitionMap = IgnitionMap;
           
            settings.UserOutputTypes = UserOutputTypes;
            settings.UserOutputModeConfigurations = UserOutputModeConfigurations;
            settings.UserOutput1Threshold = UserOutput1Threshold;
            settings.UserOutput2Threshold = UserOutput2Threshold;
            settings.UserOutput3Threshold = UserOutput3Threshold;
            settings.UserOutput4Threshold = UserOutput4Threshold;

            settings.RevLimitThreshold = RevLimitThreshold;
            settings.ShiftLightThreshold = ShiftLightThreshold;

            settings.AdvanceCorrectionBins = AdvanceCorrectionBins;
            settings.AdvanceCorrectionValues = AdvanceCorrectionValues;
            settings.AuxiliaryInputPeakHoldDecay = AuxiliaryInputPeakHoldDecay;

            settings.Save();
        }
        #endregion SaveIgnitionConfiguration
        
        #region FindRpmBin(byte)
        private byte FindRpmBin(byte rpm)
        {
            for (byte index = 0; index < 10; index++)
            {
                if (RpmBins[index] >= rpm)
                    return index;
            }
            return 0;
        }
        #endregion FindRpmBin
        #region FindLoadBin(byte)
        private byte FindLoadBin(byte load)
        {
            for (byte index = 0; index < 10; index++)
            {
                if (LoadBins[index] >= load)
                    return index;
            }
            return 0;
        }
        #endregion FindLoadBin
        #region FindAdvance(byte, byte)
        private byte FindAdvance(byte loadBin, byte rpmBin)
        {
            return IgnitionMap[(loadBin * 10) + rpmBin];
        }
        #endregion FindAdvance
        #region FindAdvanceCorrectionBin(byte)
        private byte FindAdvanceCorrectionBin(byte rpm)
        {
            for (byte index = 0; index < 10; index++)
            {
                if (AdvanceCorrectionBins[index] >= rpm)
                    return index;
            }
            return 0;
        }
        #endregion FindAdvanceCorrectionBin
        #endregion METHODS

        #region EVENT HANDLERS
        #region MainForm_FormClosing(object, FormClosingEventArgs)
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings setttings = Settings.Default;
            
            //Window Settings
            setttings.Top = Top;
            setttings.Left = Left;
            setttings.Height = Height;
            setttings.Width = Width;
            setttings.WindowState = WindowState;

            //Application Settings
            setttings.LogMessages = LogMessagesCheckBox.Checked;
            setttings.LogPropertyChanges = LogPropertyChangesCheckBox.Checked;
            setttings.LogEvents = LogEventsCheckBox.Checked;

            //Global Settings
            setttings.Version = (string)VersionComboBox.SelectedItem;
            setttings.EdisType = (string)EdisTypeComboBox.SelectedItem;
            setttings.LoadType = (string)LoadTypeComboBox.SelectedItem;
            setttings.TriggerWheelOffset = TriggerWheelOffsetNumericUpDown.Value;
            setttings.CrankingAdvance = CrankingAdvanceNumericUpDown.Value;
            setttings.PipNoiseFilter = PipNoiseFilterNumericUpDown.Value;
            setttings.AspirationType = (string)AspirationTypeComboBox.SelectedItem;

            //Current State Settings
            setttings.IgnitionConfiguration = (string)IgnitionConfigurationComboBox.SelectedItem;
            setttings.AuxiliaryInput = AuxiliaryInputNumericUpDown.Value;
            setttings.Load = LoadNumericUpDown.Value;
            setttings.Rpm = RpmNumericUpDown.Value;
            setttings.DynamicState = DynamicStateCheckBox.Checked;
            setttings.UserOutput1 = UserOutput1CheckBox.Checked;
            setttings.UserOutput2 = UserOutput2CheckBox.Checked;
            setttings.UserOutput3 = UserOutput3CheckBox.Checked;
            setttings.UserOutput4 = UserOutput4CheckBox.Checked;

            setttings.Save();
        }
        #endregion MainForm_FormClosing
        #region SerialPort_DataReceived(object, SerialDataReceivedEventArgs)
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte command = 0;
            LogMessage("Command received: 0x" + command.ToString("X"));

        }
        #endregion SerialPort_DataReceived

        #region StartServerButton_Click(object, EventArgs)
        private void StartServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogEvent("Waiting for client...");
                Listener = new TcpListener(IPAddress.Loopback, 1234);
                Listener.Start();
                Socket = Listener.AcceptSocket();
                LogEvent("Connected");
                
                StartServerButton.Enabled = false;
                StopServerButton.Enabled = true;

                Stream = new NetworkStream(Socket);
                StreamReader reader = new StreamReader(Stream);
                while (Stream != null)
                {
                    int command = reader.Read();
                    switch (command)
                    {
                        case 0x43: //Get Ignition Configuration
                            SendIgnitionConfigurationResponse();
                            break;
                        case 0x47: //Update Global Configuration
                            UpdateGlobalConfiguration(ReadCommandData(reader, 64));
                            break;
                        case 0x53: //Get State
                            SendStateResponse();
                            break;
                        case 0x55: //Update Ignition Configuration
                            UpdateIgnitionConfiguration(ReadCommandData(reader, 150));
                            break;
                        case 0x56: //Get Version
                            SendVersionResponse();
                            break;
                        case 0x57: //Write Ignition Configuration to Flash
                            SaveIgnitionConfiguration();
                            break;
                        case 0x67: //Get Global Configuration
                            SendGlobalConfigurationResponse();
                            break;
                        case 0x75: //Update Ignition Cell
                            UpdateIgnitionCell(ReadCommandData(reader, 2));
                            break;
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion StartServerButton_Click
        #region StopServerButton_Click(object, EventArgs)
        private void StopServerButton_Click(object sender, EventArgs e)
        {
            LogEvent("Stopping...");

            Stream.Close();
            Stream = null;

            Socket.Close();
            Socket = null;

            Listener.Stop();
            Listener = null;

            LogEvent("Stopped");

            StartServerButton.Enabled = true;
            StopServerButton.Enabled = false;
        }
        #endregion StopServerButton_Click
        #region LogMessagesCheckBox_CheckedChanged(object, EventArgs)
        private void LogMessagesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LogMessages = LogMessagesCheckBox.Checked;
        }
        #endregion LogMessagesCheckBox_CheckedChanged
        #region LogPropertyChangesCheckBox_CheckedChanged(object, EventArgs)
        private void LogPropertyChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LogPropertyChanges = LogPropertyChangesCheckBox.Checked;
        }
        #endregion LogPropertyChangesCheckBox_CheckedChanged
        #region LogEventsCheckBox_CheckedChanged(object, EventArgs)
        private void LogEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LogEvents = LogEventsCheckBox.Checked;
        }
        #endregion LogEventsCheckBox_CheckedChanged

        #region VersionComboBox_SelectedIndexChanged(object, EventArgs)
        private void VersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VersionComboBox.SelectedItem == "4.0.0")
                Version = new byte[3] { 0x04, 0x00, 0x00 };
            else if (VersionComboBox.SelectedItem == "4.0.1")
                Version = new byte[3] { 0x04, 0x00, 0x01 };
            else if (VersionComboBox.SelectedItem == "4.0.2")
                Version = new byte[3] { 0x04, 0x00, 0x02 };
            else
                throw new InvalidOperationException("Unsupported Version selected: " + VersionComboBox.SelectedItem);

            LogMessage("Version set: {0}", VersionComboBox.SelectedItem);
        }
        #endregion VersionComboBox_SelectedIndexChanged

        #region EdisTypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void EdisTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EdisTypeComboBox.SelectedItem == "EDIS-4")
                EdisType = 4;
            else if (EdisTypeComboBox.SelectedItem == "EDIS-6")
                EdisType = 6;
            else if (EdisTypeComboBox.SelectedItem == "EDIS-8")
                EdisType = 8;
            else
                throw new InvalidOperationException("Unsupported EDIS Type selected: " + EdisTypeComboBox.SelectedItem);

            LogMessage("EDIS Type set: {0}", EdisTypeComboBox.SelectedItem);
        }
        #endregion EdisTypeComboBox_SelectedIndexChanged
        #region LoadTypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void LoadTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadTypeComboBox.SelectedItem == "MAP")
                LoadType = MapLoadType;
            else if (LoadTypeComboBox.SelectedItem == "TPS")
                LoadType = TpsLoadType;
            else
                throw new InvalidOperationException("Unsupported Load Type selected: " + LoadTypeComboBox.SelectedItem);

            SetMaxForLoad();
            LogMessage("Load Type set: {0}", LoadTypeComboBox.SelectedItem);
        }
        #endregion LoadTypeComboBox_SelectedIndexChanged
        #region CrankingAdvanceNumericUpDown_ValueChanged(object, EventArgs)
        private void CrankingAdvanceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CrankingAdvance = (byte)CrankingAdvanceNumericUpDown.Value;
            LogMessage("Cranking Advance set: {0}", CrankingAdvance);
        }
        #endregion CrankingAdvanceNumericUpDown_ValueChanged
        #region TriggerWheelOffsetNumericUpDown_ValueChanged(object, EventArgs)
        private void TriggerWheelOffsetNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            TriggerWheelOffset = unchecked((byte)((sbyte)TriggerWheelOffsetNumericUpDown.Value));
            LogMessage("Trigger Wheel Offset set: {0}", TriggerWheelOffsetNumericUpDown.Value);
        }
        #endregion TriggerWheelOffsetNumericUpDown_ValueChanged
        #region PipNoiseFilterNumericUpDown_ValueChanged(object, EventArgs)
        private void PipNoiseFilterNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PipNoiseFilter = (byte)PipNoiseFilterNumericUpDown.Value;
            LogMessage("Pip Filter set: {0}", PipNoiseFilter);
        }
        #endregion PipNoiseFilterNumericUpDown_ValueChanged
        #region AspirationTypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void AspirationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boosted = AspirationTypeComboBox.SelectedItem.ToString().Equals("Boosted");
            SetMaxForLoad();
            LogMessage("AspirationType set: {0}", AspirationTypeComboBox.SelectedItem);
        }
        #endregion AspirationTypeComboBox_SelectedIndexChanged

        #region RpmNumericUpDown_ValueChanged(object, EventArgs)
        private void RpmNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RpmValue = (ushort)RpmNumericUpDown.Value;
            UpdateRpmValues();
        }
        #endregion RpmNumericUpDown_ValueChanged
        #region LoadNumericUpDown_ValueChanged(object, EventArgs)
        private void LoadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadValue = (byte)LoadNumericUpDown.Value;
            UpdateLoadValues();
        }
        #endregion LoadNumericUpDown_ValueChanged
        #region UserOutput1CheckBox_CheckedChanged(object, EventArgs)
        private void UserOutput1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserOutput1CheckBox.Checked)
                SetControllerStateFlag("User Output 1", 0x01);
            else
                ClearControllerStateFlag("User Output 1", 0xFE);
        }
        #endregion UserOutput1CheckBox_CheckedChanged
        #region UserOutput2CheckBox_CheckedChanged(object, EventArgs)
        private void UserOutput2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserOutput2CheckBox.Checked)
                SetControllerStateFlag("User Output 2", 0x02);
            else
                ClearControllerStateFlag("User Output 2", 0xFD);
        }
        #endregion UserOutput2CheckBox_CheckedChanged
        #region UserOutput3CheckBox_CheckedChanged(object, EventArgs)
        private void UserOutput3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserOutput3CheckBox.Checked)
                SetControllerStateFlag("User Output 3", 0x04);
            else
                ClearControllerStateFlag("User Output 3", 0xFB);
        }
        #endregion UserOutput3CheckBox_CheckedChanged
        #region UserOutput4CheckBox_CheckedChanged(object, EventArgs)
        private void UserOutput4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserOutput4CheckBox.Checked)
                SetControllerStateFlag("User Output 4", 0x08);
            else
                ClearControllerStateFlag("User Output 4", 0xF7);
        }
        #endregion UserOutput4CheckBox_CheckedChanged
        #region IgnitionConfigurationComboBox_SelectedIndexChanged(object, EventArgs)
        private void IgnitionConfigurationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IgnitionConfigurationComboBox.SelectedItem == "Configuration 1")
                SetControllerStateFlag("Configuration 1", 0x80);
            else
                ClearControllerStateFlag("Configuration 1", 0x7F);
        }
        #endregion IgnitionConfigurationComboBox_SelectedIndexChanged
        #region AuxiliaryInputNumericUpDown_ValueChanged(object, EventArgs)
        private void AuxiliaryInputNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AuxiliaryInput = (byte)AuxiliaryInputNumericUpDown.Value;
            LogMessage("Auxiliary Input set: {0}", AuxiliaryInput);
        }
        #endregion AuxiliaryInputNumericUpDown_ValueChanged
        #endregion EVENT HANDLERS
    }
}

