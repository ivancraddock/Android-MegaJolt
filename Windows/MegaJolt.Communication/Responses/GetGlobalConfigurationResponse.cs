#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Responses
{
    public class GetGlobalConfigurationResponse : Response
    {
        #region GetGlobalConfigurationResponse(IEnumerable<byte>)
        internal GetGlobalConfigurationResponse(IEnumerable<byte> data) : base(data) { }
        #endregion GetGlobalConfigurationResponse

        #region PROPERTIES
        #region EdisType Property
        public EdisType EdisType { get; private set; }
        #endregion EdisType
        #region PipNoiseFilter Property
        public byte PipNoiseFilter { get; private set; }
        #endregion PipNoiseFilter
        #region CrankingAdvance Property
        public byte CrankingAdvance { get; private set; }
        #endregion CrankingAdvance
        #region TriggerWheelOffset Property
        public sbyte TriggerWheelOffset { get; private set; }
        #endregion TriggerWheelOffset
        #endregion PROPERTIES

        #region Parse(IEnumerable<byte>)
        internal override void Parse(IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();

            if (bytes[0] != 4 && bytes[0] != 6 && bytes[0] != 8)
                throw new InvalidResponseException("Unknown EDIS Type in response: " + bytes[0]);
            EdisType = (EdisType)bytes[0];

            PipNoiseFilter = bytes[1];
            
            if (bytes[2] > 59)
                throw new InvalidResponseException("Unsupported Cranking Advance in response: " + bytes[2]);
            CrankingAdvance = bytes[2];

            sbyte triggerWheelOffset = unchecked((sbyte)bytes[3]);
            if (triggerWheelOffset < -5 || triggerWheelOffset > 5)
                throw new InvalidResponseException("Unsupported Trigger Wheel Offset in response: " + bytes[3]);
            TriggerWheelOffset = triggerWheelOffset;
        }
        #endregion Parse
    }
}
