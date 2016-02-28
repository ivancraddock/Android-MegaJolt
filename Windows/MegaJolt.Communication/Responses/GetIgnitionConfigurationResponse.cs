#region USING STATEMENTS
using System.Collections.Generic;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Responses
{
    internal class GetIgnitionConfigurationResponse : Response
    {
        #region GetIgnitionConfigurationResponse(IEnumerable<byte>)
        internal GetIgnitionConfigurationResponse(IEnumerable<byte> data) : base(data) { }
        #endregion GetIgnitionConfigurationResponse

        #region PROPERTIES
        #region RpmBins Property
        public ushort[] RpmBins { get; private set; }
        #endregion RpmBins
        #region LoadBins Property
        public byte[] LoadBins { get; private set; }
        #endregion LoadBins
        #region IgnitionMap Property
        public byte[] IgnitionMap { get; private set; }
        #endregion IgnitionMap
        #region UserOutput1 Property
        public UserOutput UserOutput1 { get; private set; }
        #endregion UserOutput1
        #region UserOutput2 Property
        public UserOutput UserOutput2 { get; private set; }
        #endregion UserOutput2
        #region UserOutput3 Property
        public UserOutput UserOutput3 { get; private set; }
        #endregion UserOutput3
        #region UserOutput4 Property
        public UserOutput UserOutput4 { get; private set; }
        #endregion UserOutput4
        #region RevLimitThreshold Property
        public ushort RevLimitThreshold { get; private set; }
        #endregion RevLimitThreshold
        #region ShiftLightThreshold Property
        public ushort ShiftLightThreshold { get; private set; }
        #endregion ShiftLightThreshold
        #region AdvanceCorrectionBins Property
        public byte[] AdvanceCorrectionBins { get; private set; }
        #endregion AdvanceCorrectionBins
        #region AdvanceCorrectionValues Property
        public sbyte[] AdvanceCorrectionValues { get; private set; }
        #endregion AdvanceCorrectionValues
        #region PeakHoldCount Property
        public ushort PeakHoldCount { get; private set; }
        #endregion PeakHoldCount
        #endregion PROPERTIES

        #region METHODS
        #region Parse(IEnumerable<byte>)
        internal override void Parse(IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();

            List<ushort> rpmBins = new List<ushort>(10);
            for (int index = 0; index < 10; index++)
            {
                rpmBins.Add((ushort)(bytes[index] * 100));
            }
            RpmBins = rpmBins.ToArray();

            List<byte> loadBins = new List<byte>(10);
            for (int index = 10; index < 20; index++)
            {
                loadBins.Add(bytes[index]);
            }
            LoadBins = loadBins.ToArray();

            List<byte> ignitionMap = new List<byte>(100);
            for (int index = 20; index < 120; index++)
            {
                ignitionMap.Add(bytes[index]);
            }
            IgnitionMap = ignitionMap.ToArray();

            UserOutput1 = GetUserOutput(1, bytes[120], bytes[121], bytes[122]);
            UserOutput2 = GetUserOutput(2, bytes[120], bytes[121], bytes[123]);
            UserOutput3 = GetUserOutput(3, bytes[120], bytes[121], bytes[124]);
            UserOutput4 = GetUserOutput(4, bytes[120], bytes[121], bytes[125]);
            RevLimitThreshold = (ushort)(bytes[126] * 100);
            ShiftLightThreshold = (ushort)(bytes[127] * 100);

            List<byte> advanceCorrectionBins = new List<byte>(10);
            List<sbyte> advanceCorrectionValues = new List<sbyte>(10);
            for (int index = 128; index < 138; index++)
            {
                advanceCorrectionBins.Add(bytes[index]);
                advanceCorrectionValues.Add(unchecked((sbyte)(bytes[index + 10])));
            }
            AdvanceCorrectionBins = advanceCorrectionBins.ToArray();
            AdvanceCorrectionValues = advanceCorrectionValues.ToArray();

            PeakHoldCount = (ushort)((bytes[148] << 8) + bytes[149]);
        }
        #endregion Parse
        #region GetUserOutput(int, byte, byte, byte)
        private UserOutput GetUserOutput(int index, byte userOutputTypes, byte userOutputModes, byte threshold)
        {
            return  new UserOutput(
                (UserOutputType)((userOutputTypes >> ((index - 1) * 2)) & 0x03),
                (UserOutputMode)((userOutputModes >> (index - 1)) & 0x01),
                threshold);
        }
        #endregion GetUserOutput
        #endregion METHODS
    }
}
