#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    internal class UpdateIgnitionCellCommand : Command
    {
        #region UpdateIgnitionCellCommand(byte, byte, byte)
        internal UpdateIgnitionCellCommand(byte rpmBin, byte loadBin, byte advance) : base(0x75, new[] { (byte)((rpmBin << 4) + loadBin), advance } ) {  }
        #endregion UpdateIgnitionCellCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 0; } }
        #endregion ResponseLength

        #region ValidateCommand(byte, IEnumerable<byte>)
        protected override void ValidateCommand(byte commandNumber, IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();

            if (((bytes[0] >> 4) & 0x0F) > 9)
                throw new ArgumentOutOfRangeException("RPM Bin must be between 0 and 9.");

            if ((bytes[0] & 0x0F) > 9)
                throw new ArgumentOutOfRangeException("Load Bin must be between 0 and 9.");

            if (bytes[1] > 59)
                throw new ArgumentOutOfRangeException("Advance must be between 0 and 59.");
        }
        #endregion ValidateCommand
        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            throw new InvalidOperationException("No response is expected for the Update Ignition Cell Command");
        }
        #endregion BuildResponse
    }
}
