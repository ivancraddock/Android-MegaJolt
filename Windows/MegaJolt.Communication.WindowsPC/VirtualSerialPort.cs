#region USING STATEMENTSusing System;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.WindowsPC
{
    public class VirtualSerialPort : ICommunicationPort
    {
        #region VirtualSerialPort()
        public VirtualSerialPort()
        {
            SerialPort = new SerialPort();
        }
        #endregion VirtualSerialPort

        #region PROPERTIES
        #region SerialPort Property
        private SerialPort SerialPort { get; set; }
        #endregion SerialPort
        #region IsConnected Property
        public bool IsConnected
        {
            get { return SerialPort.IsOpen; }
        }
        #endregion IsConnected
        #region IsDisposed Property
        private bool IsDisposed { get; set; }
        #endregion IsDisposed
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
                if (disposing)
                {
                    Close();
                    SerialPort.Dispose();
                    SerialPort = null;
                }
                IsDisposed = true;
            }
        }
        #endregion Dispose

        #region Open(string)
        public bool Open(string comPort)
        {
            if (SerialPort == null) return false;
            if (SerialPort.IsOpen) return true;

            SerialPort.PortName = comPort;
            SerialPort.BaudRate = 38400;
            SerialPort.DataBits = 8;
            SerialPort.Parity = Parity.None;
            SerialPort.StopBits = StopBits.One;
            SerialPort.ReadTimeout = 5000;
            SerialPort.WriteTimeout = 5000;
            SerialPort.Open();
            return true;
        }
        #endregion Open
        #region Close()
        public void Close()
        {
            //Close the serial port.
            SerialPort.DiscardInBuffer();
            SerialPort.Close();
        }
        #endregion Close
        #region Write(IEnumerable<byte>)
        public void Write(IEnumerable<byte> commandData)
        {
            byte[] bytes = commandData.ToArray();
            SerialPort.Write(bytes, 0, bytes.Length);
        }
        #endregion Write
        #region Read(int)
        public IEnumerable<byte> Read(int byteCount)
        {
            try
            {
                int rxCount = 0;
                byte[] buffer = new byte[byteCount];
                while (rxCount < byteCount)
                {
                    int count = SerialPort.Read(buffer, rxCount, byteCount - rxCount);
                    rxCount += count;
                }
                return buffer;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
        #endregion Read
        #endregion METHODS
    }
}
