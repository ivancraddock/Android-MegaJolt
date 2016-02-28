#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    internal class UpdateGlobalConfigurationCommand : Command
    {
        #region UpdateGlobalConfigurationCommand(GlobalConfiguration)
        internal UpdateGlobalConfigurationCommand(GlobalConfiguration globalConfiguration) : base(0x47, BuildByteArray(globalConfiguration)) { }
        #endregion UpdateGlobalConfigurationCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 0; } }
        #endregion ResponseLength

        #region BuildByteArray(GlobalConfiguration)
        private static IEnumerable<byte> BuildByteArray(GlobalConfiguration globalConfiguration)
        {
            return new[]
            {
                (byte)globalConfiguration.EdisType,
                globalConfiguration.PipNoiseFilter,
                globalConfiguration.CrankingAdvance,
                unchecked((byte)globalConfiguration.TriggerWheelOffset),
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0,
            };
        }
        #endregion BuildByteArray
        #region ValidateCommand(byte, IEnumerable<byte>)
        protected override void ValidateCommand(byte commandNumber, IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();

            if (bytes[0] != 4 && bytes[0] != 6 && bytes[0] != 8)
                throw new ArgumentOutOfRangeException("EdisType must be between 4, 6 or 8.");

            if (bytes[2] > 59)
                throw new ArgumentOutOfRangeException("Cranking Advance must be between 0 and 59.");

            sbyte offset = unchecked((sbyte)bytes[3]);
            if (offset > 5 || offset < -5)
                throw new ArgumentOutOfRangeException("Trigger Wheel Offset must be between -5 and 5.");
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
