#region USING STATEMENTSusing System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.SimulatorConnection
{
    public class SimulatorPort : ICommunicationPort
    {
        #region PROPERTIES
        #region Client Property
        private TcpClient Client { get; set; }
        #endregion Client
        #region Stream Property
        private Stream Stream { get; set; }
        #endregion Stream
        #region IsConnected Property
        public bool IsConnected
        {
            get { return Client != null; }
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
                }
                IsDisposed = true;
            }
        }
        #endregion Dispose

        #region Open(string)
        public bool Open(string comPort)
        {
            if (Client != null) return true;
            try
            {

                Client = new TcpClient("LocalHost", 1234);
                Stream = Client.GetStream();
                return true;
            }
            catch
            {
                Close();
                return false;
            }
        }
        #endregion Open
        #region Close()
        public void Close()
        {
            if (Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }

            if (Client != null)
            {
                Client.Close();
                Client = null;
            }
        }
        #endregion Close
        #region Write(IEnumerable<byte>)
        public void Write(IEnumerable<byte> commandData)
        {
            byte[] bytes = commandData.ToArray();
            Stream.Write(bytes, 0, bytes.Length);
        }
        #endregion Write
        #region Read(int)
        public IEnumerable<byte> Read(int byteCount)
        {
            try
            {
                byte[] buffer = new byte[byteCount];
                Stream.Read(buffer, 0, byteCount);
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
