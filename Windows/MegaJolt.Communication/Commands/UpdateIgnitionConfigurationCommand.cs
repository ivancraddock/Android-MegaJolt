#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    internal class UpdateIgnitionConfigurationCommand : Command
    {
        #region UpdateIgnitionConfigurationCommand(IgnitionMap)
        internal UpdateIgnitionConfigurationCommand(IgnitionMap ignitionMap) : base(0x55, BuildByteArray(ignitionMap)) { }
        #endregion UpdateIgnitionConfigurationCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 0; } }
        #endregion ResponseLength

        #region BuildByteArray(IgnitionMap)
        private static IEnumerable<byte> BuildByteArray(IgnitionMap ignitionMap)
        {
            List<byte> command = new List<byte>();
            
            //Rpm and Load Bins
            command.AddRange(ignitionMap.RpmBins.Select(item => (byte)(item.HighValue / 100)));
            command.AddRange(ignitionMap.LoadBins.Select(item => item.HighValue));
            
            //Advance Map
            for (byte loadIndex = 0; loadIndex < 10; loadIndex++)
            {
                for (byte rpmIndex = 0; rpmIndex < 10; rpmIndex++)
                {
                    command.Add(ignitionMap[loadIndex, rpmIndex]);
                }
            }
            
            //User Output Types
            command.Add
                ((byte)(
                    ((byte)ignitionMap.UserOutput1.OutputType & 0x03) +
                    (((byte)ignitionMap.UserOutput2.OutputType & 0x03) << 2) +
                    (((byte)ignitionMap.UserOutput3.OutputType & 0x03) << 4) +
                    (((byte)ignitionMap.UserOutput4.OutputType & 0x03) << 6)
                ));

            //User Output Modes
            command.Add
                ((byte)(
                    ((byte)ignitionMap.UserOutput1.OutputMode & 0x01) +
                    (((byte)ignitionMap.UserOutput2.OutputMode & 0x01) << 1) +
                    (((byte)ignitionMap.UserOutput3.OutputMode & 0x01) << 2) +
                    (((byte)ignitionMap.UserOutput4.OutputMode & 0x01) << 3)
                ));

            //User Output Thresholds
            command.Add((byte)(ignitionMap.UserOutput1.Threshold / (ignitionMap.UserOutput1.OutputType == UserOutputType.Rpm ? 100 : 1)));
            command.Add((byte)(ignitionMap.UserOutput2.Threshold / (ignitionMap.UserOutput2.OutputType == UserOutputType.Rpm ? 100 : 1)));
            command.Add((byte)(ignitionMap.UserOutput3.Threshold / (ignitionMap.UserOutput3.OutputType == UserOutputType.Rpm ? 100 : 1)));
            command.Add((byte)(ignitionMap.UserOutput4.Threshold / (ignitionMap.UserOutput4.OutputType == UserOutputType.Rpm ? 100 : 1)));
            command.Add((byte)(ignitionMap.RevLimitThreshold / 100));
            command.Add((byte)(ignitionMap.ShiftLightThreshold / 100));

            //Advance Correction Bins, Values and Peak Hold Count
            command.AddRange(ignitionMap.AdvanceCorrection.Bins.Select(item => (byte)(item.HighValue / 100)));
            command.AddRange(ignitionMap.AdvanceCorrection.Values.Select(item => unchecked((byte)item)));
            command.Add((byte)((ignitionMap.AdvanceCorrection.PeakHoldCount & 0xFF00) >> 8));
            command.Add((byte)(ignitionMap.AdvanceCorrection.PeakHoldCount & 0x00FF));
            
            return command;
        }
        #endregion BuildByteArray
        #region ValidateCommand(byte, IEnumerable<byte>)
        protected override void ValidateCommand(byte commandNumber, IEnumerable<byte> data)
        {
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
