#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Responses
{
    internal class GetStateResponse : Response
    {
        #region GetStateResponse(IEnumerable<byte>)
        internal GetStateResponse(IEnumerable<byte> data) : base(data) { }
        #endregion GetStateResponse

        #region PROPERTIES
        #region CurrentIgnitionAdvance Property
        public byte CurrentIgnitionAdvance { get; private set; }
        #endregion CurrentIgnitionAdvance
        #region RawRpmCount Property
        public ushort RawRpmCount { get; private set; }
        #endregion RawRpmCount
        #region CurrentRpmBin Property
        public byte CurrentRpmBin { get; private set; }
        #endregion CurrentRpmBin
        #region CurrentLoadBin Property
        public byte CurrentLoadBin { get; private set; }
        #endregion CurrentLoadBin
        #region CurrentLoadValue Property
        public byte CurrentLoadValue { get; private set; }
        #endregion CurrentLoadValue
        #region UserOutput1 Property
        public BitState UserOutput1 { get; private set; }
        #endregion UserOutput1
        #region UserOutput2 Property
        public BitState UserOutput2 { get; private set; }
        #endregion UserOutput2
        #region UserOutput3 Property
        public BitState UserOutput3 { get; private set; }
        #endregion UserOutput3
        #region UserOutput4 Property
        public BitState UserOutput4 { get; private set; }
        #endregion UserOutput4
        #region RevLimitState Property
        public BitState RevLimitState { get; private set; }
        #endregion RevLimitState
        #region ShiftLightState Property
        public BitState ShiftLightState { get; private set; }
        #endregion ShiftLightState
        #region IgnitionConfiguration Property
        public IgnitionConfiguration IgnitionConfiguration { get; private set; }
        #endregion IgnitionConfiguration
        #region AuxiliaryInputValue Property
        public byte AuxiliaryInputValue { get; private set; }
        #endregion AuxiliaryInputValue
        #region CurrentAdvanceCorrectionBin Property
        public byte CurrentAdvanceCorrectionBin { get; private set; }
        #endregion CurrentAdvanceCorrectionBin
        #region CurrentAdvanceCorrectionValue Property
        public sbyte CurrentAdvanceCorrectionValue { get; private set; }
        #endregion CurrentAdvanceCorrectionValue
        #endregion PROPERTIES

        #region METHODS
        #region Parse(IEnumerable<byte>)
        internal override void Parse(IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();

            if (bytes[0] > 59)
                throw new InvalidResponseException("Unsupported Current Ignition Advance in response: " + bytes[0]);
            CurrentIgnitionAdvance = bytes[0];

            RawRpmCount = (ushort)((((ushort)bytes[1]) << 8) | bytes[2]);
            if (RawRpmCount == ushort.MaxValue) RawRpmCount = 0;

            byte currentRpmBin = (byte)((bytes[3] >> 4) & 0x0F);
            if (currentRpmBin > 9)
                throw new InvalidResponseException("Unsupported Current RPM Bin in response: " + currentRpmBin);
            CurrentRpmBin = currentRpmBin;

            byte currentLoadBin = (byte)(bytes[3] & 0x0F);
            if (currentLoadBin > 9)
                throw new InvalidResponseException("Unsupported Current Load Bin in response: " + currentLoadBin);
            CurrentLoadBin = currentLoadBin;
            
            CurrentLoadValue = bytes[4]; //TODO: Set limits.
            UserOutput1 = (bytes[5] & 0x01) == 0x01 ? BitState.Active : BitState.Inactive;
            UserOutput2 = (bytes[5] & 0x02) == 0x02 ? BitState.Active : BitState.Inactive;
            UserOutput3 = (bytes[5] & 0x04) == 0x04 ? BitState.Active : BitState.Inactive;
            UserOutput4 = (bytes[5] & 0x08) == 0x08 ? BitState.Active : BitState.Inactive;
            RevLimitState = (bytes[5] & 0x10) == 0x10 ? BitState.Active : BitState.Inactive;
            ShiftLightState = (bytes[5] & 0x20) == 0x20 ? BitState.Active : BitState.Inactive;
            IgnitionConfiguration = (bytes[5] & 0x80) == 0x80 ? IgnitionConfiguration.Configuration1 : IgnitionConfiguration.Configuration2;
            AuxiliaryInputValue = bytes[6]; //TODO: Set limits.

            if (bytes[7] > 9)
                throw new InvalidResponseException("Unsupported Current Advance Correction Bin in response: " + bytes[7]);
            CurrentAdvanceCorrectionBin = bytes[7];

            sbyte currentAdvanceCorrectionValue = unchecked((sbyte)bytes[8]);
            if (currentAdvanceCorrectionValue < -5 || currentAdvanceCorrectionValue > 5)
                throw new InvalidResponseException("Unsupported Current Advance Correction Value in response: " + currentAdvanceCorrectionValue);
            CurrentAdvanceCorrectionValue = currentAdvanceCorrectionValue;
        }
        #endregion Parse
        #region GetCurrentRpmValue(EdisType)
        public ushort GetCurrentRpmValue(EdisType edisType)
        {
            float cylinder = (float)edisType / 2;
            return (ushort)(60 * (1 / (float)(((float)RawRpmCount / (float)1000000) * cylinder)));
        }
        #endregion GetCurrentRpmValue
        #endregion METHODS
    }
}
