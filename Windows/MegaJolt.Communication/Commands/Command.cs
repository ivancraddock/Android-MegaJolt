#region USINGS
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MegaJolt.Communication.Responses;
#endregion USINGS

namespace MegaJolt.Communication.Commands
{
    public abstract class Command
    {
        #region CONSTRUCTORS
        #region Command(byte)
        protected Command(byte commandNumber) : this(commandNumber, new List<byte>()) { }
        #endregion Command
        #region Command(byte, IEnumerable<byte>)
        protected Command(byte commandNumber, IEnumerable<byte> data) 
        {
            if (!SupportedCommands.Contains(commandNumber))
                throw new InvalidOperationException("Unsupported command: " + commandNumber);

            ValidateCommand(commandNumber, data);

            CommandNumber = commandNumber;
            Data = data;
        }
        #endregion Command
        #endregion CONSTRUCTORS

        #region PROPERTIES
        private static readonly ReadOnlyCollection<byte> SupportedCommands = new ReadOnlyCollection<byte>(new byte[] {
            0x43, //Get Ignition Configuration
            0x47, //Update Global Configuration
            0x53, //Get State
            0x55, //Update Ignition Configuration
            0x56, //Get Version
            0x57, //Write Ignition Configuration to Flash
            0x67, //Get Global Configuration
            0x75, //Update Ignition Cell
        }); 

        public byte CommandNumber { get; private set; }
        public IEnumerable<byte> Data { get; private set; }
        internal abstract int ResponseLength { get; }
        #endregion PROPERTIES

        #region METHODS
        #region ValidateCommand(byte, IEnumerable<byte>)
        protected virtual void ValidateCommand(byte commandNumber, IEnumerable<byte> data)
        {
            if (data.Count() != 0)
                throw new InvalidCommandException(GetType().Name + " has no data.");
        }
        #endregion ValidateCommand
        #region BuildResponse(IEnumerable<byte>)
        internal abstract Response BuildResponse(IEnumerable<byte> data);
        #endregion BuildResponse
        #endregion METHODS
    }
}
