#region USING STATEMENTS
using System;
using System.Collections.Generic;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public interface ICommunicationPort : IDisposable
    {
        bool IsConnected { get; }

        bool Open(string comPort);
        void Close();
        void Write(IEnumerable<byte> commandData);
        IEnumerable<byte> Read(int byteCount);
    }
}
