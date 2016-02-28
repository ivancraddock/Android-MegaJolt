#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class IgnitionMap
    {
        #region IgnitionMap(Controller)
        internal IgnitionMap(Controller controller, IPersistedStorage persistedStorage)
        {
            Controller = controller;
            PersistedStorage = persistedStorage;

            AdvanceMap = new byte[100];
            RpmBins = new Bins<ushort>();
            LoadBins = new Bins<byte>();
            UserOutput1 = new UserOutput(Controller);
            UserOutput2 = new UserOutput(Controller);
            UserOutput3 = new UserOutput(Controller);
            UserOutput4 = new UserOutput(Controller);
            AdvanceCorrection = new AdvanceCorrection();

            Initialize(UserOutput1);
            Initialize(UserOutput2);
            Initialize(UserOutput3);
            Initialize(UserOutput4);
            Initialize(AdvanceCorrection);
        }
        #endregion IgnitionMap

        #region EVENTS
        public event EventHandler<EventArgs<SavedStates>> SavedStateChanged;
        public event EventHandler<EventArgs<int>> RpmBinUpdated;
        public event EventHandler<EventArgs<int>> LoadBinUpdated;
        public event EventHandler<EventArgs<IgnitionCellIndex>> IgnitionCellUpdated;
        public event EventHandler<EventArgs<ushort>> ShiftLightThresholdChanged;
        public event EventHandler<EventArgs<ushort>> RevLimitThresholdChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region Indexer Property
        public byte this[byte loadBin, byte rpmBin]
        {
            get { return this[new IgnitionCellIndex(loadBin, rpmBin)]; }
            set { UpdateAdvance(new IgnitionCellIndex(loadBin, rpmBin), value); }
        }
        #endregion this
        #region Indexer Property
        public byte this[IgnitionCellIndex cellIndex]
        {
            get { return AdvanceMap[cellIndex.MapIndex]; }
            set { UpdateAdvance(cellIndex, value); }
        }
        #endregion this

        #region Controller Property
        public Controller Controller { get; private set; }
        #endregion Controller
        #region PersistedStorage Property
        public IPersistedStorage PersistedStorage { get; private set; }
        #endregion PersistedStorage
        #region LoadInProgress Property
        private bool LoadInProgress { get; set; }
        #endregion LoadInProgress
        #region SavedState Property
        private SavedStates _SavedState;
        public SavedStates SavedState
        {
            get { return _SavedState; }
            internal set
            {
                if (_SavedState == value) return;
                _SavedState = value;
                OnSavedStateChanged(value);
            }
        }
        #endregion SavedState
        #region RpmBins Property
        public Bins<ushort> RpmBins { get; private set; }
        #endregion RpmBins
        #region LoadBins Property
        public Bins<byte> LoadBins { get; private set; }
        #endregion LoadBins
        #region AdvanceMap Property
        private byte[] AdvanceMap { get; set; }
        #endregion AdvanceMap
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
        #region AdvanceCorrection Property
        public AdvanceCorrection AdvanceCorrection { get; private set; }
        #endregion AdvanceCorrection
        #region ShiftLightThreshold Property
        private ushort _ShiftLightThreshold;
        public ushort ShiftLightThreshold
        {
            get { return _ShiftLightThreshold; }
            set
            {
                if (_ShiftLightThreshold == value) return;
                _ShiftLightThreshold = value;
                OnShiftLightThresholdChanged(value);
            }
        }
        #endregion ShiftLightThreshold
        #region RevLimitThreshold Property
        private ushort _RevLimitThreshold;
        public ushort RevLimitThreshold
        {
            get { return _RevLimitThreshold; }
            set
            {
                if (_RevLimitThreshold == value) return;
                _RevLimitThreshold = value;
                OnRevLimitThresholdChanged(value);
            }
        }
        #endregion RevLimitThreshold
        #endregion PROPERTIES

        #region METHODS
        #region Load(string)
        public void Load(string fileName)
        {
            LoadInProgress = true;

            try
            {
                byte[] advance0 = new byte[10];
                byte[] advance1 = new byte[10];
                byte[] advance2 = new byte[10];
                byte[] advance3 = new byte[10];
                byte[] advance4 = new byte[10];
                byte[] advance5 = new byte[10];
                byte[] advance6 = new byte[10];
                byte[] advance7 = new byte[10];
                byte[] advance8 = new byte[10];
                byte[] advance9 = new byte[10];
                byte[] advanceCorrectionBins = null;
                sbyte[] advanceCorrectionValues = null;
                ushort peakHoldCount = 0;
                UserOutput userOutput1 = new UserOutput(Controller);
                UserOutput userOutput2 = new UserOutput(Controller);
                UserOutput userOutput3 = new UserOutput(Controller);
                UserOutput userOutput4 = new UserOutput(Controller);

                IEnumerable<string> fileContents = PersistedStorage.ReadAll(fileName);
                foreach(string line in fileContents)
                {
                    //Load Bins
                    if (line.StartsWith("mapBins", StringComparison.OrdinalIgnoreCase))
                        PopulateLoadBins(GetByteArrayFromLine(line));

                        //RPM Bins
                    else if (line.StartsWith("rpmBins", StringComparison.OrdinalIgnoreCase))
                        PopulateRpmBins(GetByteArrayFromLine(line).Select(item => (ushort)(item * 100)).ToArray());

                        //Advance Correction
                    else if (line.StartsWith("correctionBins", StringComparison.OrdinalIgnoreCase))
                        advanceCorrectionBins = GetByteArrayFromLine(line);
                    else if (line.StartsWith("correctionValues", StringComparison.OrdinalIgnoreCase))
                        advanceCorrectionValues = GetSByteArrayFromLine(line);
                    else if (line.StartsWith("correctionPeakHold", StringComparison.OrdinalIgnoreCase))
                        peakHoldCount = GetUShortFromLine(line);

                        //Ignition Advance
                    else if (line.StartsWith("advance0", StringComparison.OrdinalIgnoreCase))
                        advance0 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance1", StringComparison.OrdinalIgnoreCase))
                        advance1 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance2", StringComparison.OrdinalIgnoreCase))
                        advance2 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance3", StringComparison.OrdinalIgnoreCase))
                        advance3 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance4", StringComparison.OrdinalIgnoreCase))
                        advance4 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance5", StringComparison.OrdinalIgnoreCase))
                        advance5 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance6", StringComparison.OrdinalIgnoreCase))
                        advance6 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance7", StringComparison.OrdinalIgnoreCase))
                        advance7 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance8", StringComparison.OrdinalIgnoreCase))
                        advance8 = GetByteArrayFromLine(line);
                    else if (line.StartsWith("advance9", StringComparison.OrdinalIgnoreCase))
                        advance9 = GetByteArrayFromLine(line);

                        //User Output1
                    else if (line.StartsWith("userOutType0", StringComparison.OrdinalIgnoreCase))
                        userOutput1.OutputType = (UserOutputType)(2 - GetByteFromLine(line));
                    else if (line.StartsWith("userOutMode0", StringComparison.OrdinalIgnoreCase))
                        userOutput1.OutputMode = GetUserOutputModeFromLine(line);
                    else if (line.StartsWith("userOutValue0", StringComparison.OrdinalIgnoreCase))
                        userOutput1.Threshold = (ushort)(GetUShortFromLine(line) * (userOutput1.OutputType == UserOutputType.Rpm ? 100 : 1));

                        //User Output2
                    else if (line.StartsWith("userOutType1", StringComparison.OrdinalIgnoreCase))
                        userOutput2.OutputType = (UserOutputType)(2 - GetByteFromLine(line));
                    else if (line.StartsWith("userOutMode1", StringComparison.OrdinalIgnoreCase))
                        userOutput2.OutputMode = GetUserOutputModeFromLine(line);
                    else if (line.StartsWith("userOutValue1", StringComparison.OrdinalIgnoreCase))
                        userOutput2.Threshold = (ushort)(GetUShortFromLine(line) * (userOutput2.OutputType == UserOutputType.Rpm ? 100 : 1));

                        //User Output3
                    else if (line.StartsWith("userOutType2", StringComparison.OrdinalIgnoreCase))
                        userOutput3.OutputType = (UserOutputType)(2 - GetByteFromLine(line));
                    else if (line.StartsWith("userOutMode2", StringComparison.OrdinalIgnoreCase))
                        userOutput3.OutputMode = GetUserOutputModeFromLine(line);
                    else if (line.StartsWith("userOutValue2", StringComparison.OrdinalIgnoreCase))
                        userOutput3.Threshold = (ushort)(GetUShortFromLine(line) * (userOutput3.OutputType == UserOutputType.Rpm ? 100 : 1));

                        //User Output4
                    else if (line.StartsWith("userOutType3", StringComparison.OrdinalIgnoreCase))
                        userOutput4.OutputType = (UserOutputType)(2 - GetByteFromLine(line));
                    else if (line.StartsWith("userOutMode3", StringComparison.OrdinalIgnoreCase))
                        userOutput4.OutputMode = GetUserOutputModeFromLine(line);
                    else if (line.StartsWith("userOutValue3", StringComparison.OrdinalIgnoreCase))
                        userOutput4.Threshold = (ushort)(GetUShortFromLine(line) * (userOutput4.OutputType == UserOutputType.Rpm ? 100 : 1));

                    else if (line.StartsWith("shiftLight", StringComparison.OrdinalIgnoreCase))
                        ShiftLightThreshold = (ushort)(GetUShortFromLine(line) * 100);
                    else if (line.StartsWith("revLimit", StringComparison.OrdinalIgnoreCase))
                        RevLimitThreshold = (ushort)(GetUShortFromLine(line) * 100);
                }
                

                List<byte> ignitionMap = new List<byte>(100);
                ignitionMap.AddRange(advance0);
                ignitionMap.AddRange(advance1);
                ignitionMap.AddRange(advance2);
                ignitionMap.AddRange(advance3);
                ignitionMap.AddRange(advance4);
                ignitionMap.AddRange(advance5);
                ignitionMap.AddRange(advance6);
                ignitionMap.AddRange(advance7);
                ignitionMap.AddRange(advance8);
                ignitionMap.AddRange(advance9);

                PopulateIgnitionMap(ignitionMap.ToArray());
                UserOutput1.Populate(userOutput1);
                UserOutput2.Populate(userOutput2);
                UserOutput3.Populate(userOutput3);
                UserOutput4.Populate(userOutput4);
                if (advanceCorrectionBins != null && advanceCorrectionValues != null)
                    AdvanceCorrection.Populate(advanceCorrectionBins, advanceCorrectionValues, peakHoldCount);

                LoadInProgress = false;
                SavedState = SavedStates.SavedToFile;
            }
            finally
            {
                LoadInProgress = false;
            }
        }
        #endregion Load
        #region Save(string)
        public void Save(string fileName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("#Created by PcJolt");
            builder.AppendLine("#" + DateTime.Now.ToString("d") + " " + DateTime.Now.ToString("T"));
            builder.AppendLine("mapBins=" + BuildOutputString(LoadBins));
            builder.AppendLine("rpmBins=" + BuildOutputString(RpmBins));
            for (byte loadIndex = 0; loadIndex < 10; loadIndex++)
            {
                byte[] bytes = new byte[10];
                Array.Copy(AdvanceMap, loadIndex * 10, bytes, 0, 10);
                builder.AppendLine("advance" + loadIndex + "=" + string.Join(",", bytes.Select(b => b.ToString()).ToArray()));
            }
            builder.AppendLine("correctionBins=" + BuildOutputString(AdvanceCorrection.Bins));
            builder.AppendLine("correctionValues=" + BuildOutputString(AdvanceCorrection.Values));
            builder.AppendLine("correctionPeakHold=" + AdvanceCorrection.PeakHoldCount);
            builder.AppendLine(BuildOutputString(UserOutput1, 0));
            builder.AppendLine(BuildOutputString(UserOutput2, 1));
            builder.AppendLine(BuildOutputString(UserOutput3, 2));
            builder.AppendLine(BuildOutputString(UserOutput4, 3));
            builder.AppendLine("shiftLight=" + ShiftLightThreshold / 100);
            builder.AppendLine("revLimit=" + RevLimitThreshold / 100);

            PersistedStorage.Write(fileName, builder.ToString());
            SavedState = SavedState ^ SavedStates.SavedToFile;
        }
        #endregion Save
        #region Reset()
        public void Reset()
        {
            LoadInProgress = true;

            for (int index = 0; index < 10; index++)
            {
                if (LoadBins[index].HighValue == 0 && LoadBins[index].LowValue == 0) continue;
                LoadBins[index].HighValue = 0;
                LoadBins[index].LowValue = 0;
                OnLoadBinUpdated(index);
            }

            for (int index = 0; index < 10; index++)
            {
                if (RpmBins[index].HighValue == 0 && RpmBins[index].LowValue == 0) continue;
                RpmBins[index].HighValue = 0;
                RpmBins[index].LowValue = 0;
                OnRpmBinUpdated(index);
            }

            for (byte loadIndex = 0; loadIndex < 10; loadIndex++)
            {
                for (byte rpmIndex = 0; rpmIndex < 10; rpmIndex++)
                {
                    UpdateAdvance(loadIndex, rpmIndex, 0);
                }
            }

            UserOutput1.Reset();
            UserOutput2.Reset();
            UserOutput3.Reset();
            UserOutput4.Reset();
            ShiftLightThreshold = 0;
            RevLimitThreshold = 0;

            AdvanceCorrection.Reset();

            LoadInProgress = false;
            SavedState = SavedStates.Dirty;
        }
        #endregion Reset

        #region Populate(GetIgnitionConfigurationResponse)
        internal void Populate(GetIgnitionConfigurationResponse response)
        {
            LoadInProgress = true;

            PopulateLoadBins(response.LoadBins.ToArray());
            PopulateRpmBins(response.RpmBins.ToArray());
            PopulateIgnitionMap(response.IgnitionMap);
            UserOutput1.Populate(response.UserOutput1);
            UserOutput2.Populate(response.UserOutput2);
            UserOutput3.Populate(response.UserOutput3);
            UserOutput4.Populate(response.UserOutput4);
            ShiftLightThreshold = response.ShiftLightThreshold;
            RevLimitThreshold = response.RevLimitThreshold;
            AdvanceCorrection.Populate(response.AdvanceCorrectionBins, response.AdvanceCorrectionValues, response.PeakHoldCount);

            LoadInProgress = false;
            SavedState = SavedStates.SavedToController | SavedStates.WrittenToFlash;
        }
        #endregion Populate
        #region ReadNextLine(StreamReader)
        private string ReadNextLine(StreamReader reader)
        {
            string line = "#";
            while (!reader.EndOfStream && (string.IsNullOrEmpty(line) || line.StartsWith("#")))
            {
                line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                line = line.Trim();
            }
            if (line.StartsWith("#")) line = string.Empty;
            return line;
        }
        #endregion ReadNextLine
        #region GetByteFromLine(string)
        private byte GetByteFromLine(string line)
        {
            return byte.Parse(line.Substring(line.IndexOf('=') + 1).Trim());
        }
        #endregion GetByteFromLine
        #region GetUShortFromLine(string)
        private ushort GetUShortFromLine(string line)
        {
            return ushort.Parse(line.Substring(line.IndexOf('=') + 1).Trim());
        }
        #endregion GetUShortFromLine
        #region GetUserOutputModeFromLine(string)
        private UserOutputMode GetUserOutputModeFromLine(string line)
        {
            byte value = byte.Parse(line.Substring(line.IndexOf('=') + 1).Trim());
            return value == 0 ? UserOutputMode.Normal : UserOutputMode.Inverted;
        }
        #endregion GetUserOutputModeFromLine
        #region GetByteArrayFromLine(string)
        private byte[] GetByteArrayFromLine(string line)
        {
            line = line.Substring(line.IndexOf('=') + 1).Trim();
            string[] values = line.Split(',');
            return values.Select(byte.Parse).ToArray();
        }
        #endregion GetByteArrayFromLine
        #region GetSByteArrayFromLine(string)
        private sbyte[] GetSByteArrayFromLine(string line)
        {
            line = line.Substring(line.IndexOf('=') + 1).Trim();
            string[] values = line.Split(',');
            return values.Select(sbyte.Parse).ToArray();
        }
        #endregion GetSByteArrayFromLine

        #region BuildOutputString(IEnumerable<Bin<byte>>)
        private string BuildOutputString(IEnumerable<Bin<byte>> values)
        {
            return string.Join(",", values.Select(value => value.HighValue.ToString()).ToArray());
        }
        #endregion BuildOutputString
        #region BuildOutputString(IEnumerable<Bin<ushort>>)
        private string BuildOutputString(IEnumerable<Bin<ushort>> values)
        {
            return string.Join(",", values.Select(value => (value.HighValue/100).ToString()).ToArray());
        }
        #endregion BuildOutputString
        #region BuildOutputString(IEnumerable<sbyte>)
        private string BuildOutputString(IEnumerable<sbyte> values)
        {
            return string.Join(",", values.Select(value => value.ToString()).ToArray());
        }
        #endregion BuildOutputString
        #region BuildOutputString(IEnumerable<sbyte>)
        private string BuildOutputString(UserOutput value, int index)
        {
            return string.Format("{1}{0}={2}\n{3}{0}={4}\n{5}{0}={6}", index,
                                 "userOutType", Math.Abs((int)value.OutputType - 2),
                                 "userOutMode", value.OutputMode == UserOutputMode.Normal ? "0" : "1",
                                 "userOutValue", value.Threshold / (value.OutputType == UserOutputType.Rpm ? 100 : 1));
        }
        #endregion BuildOutputString

        #region PopulateLoadBins(byte[])
        private void PopulateLoadBins(byte[] loadBins)
        {
            for (int index = 0; index < loadBins.Length; index++)
            {
                UpdateLoadBin(index, loadBins[index]);
            }
        }
        #endregion PopulateLoadBins
        #region UpdateLoadBin(int, byte)
        public void UpdateLoadBin(int index, byte newHighValue)
        {
            if (index < 0 || index > 9) throw new ArgumentOutOfRangeException("Index must be between 0 and 9.");
            if (LoadBins[index].HighValue == newHighValue) return;

            //Make sure the next bin lower does not have a higher value.
            if (index > 0 && LoadBins[index - 1].HighValue >= newHighValue)
                UpdateLoadBin(index - 1, newHighValue > 1 ? (byte)(newHighValue - 1) : (byte)1);

            //Update the current bin.
            LoadBins[index].HighValue = newHighValue;
            LoadBins[index].LowValue = index > 0
                ? (byte)(LoadBins[index - 1].HighValue + 1)
                : (byte)0;

            //Make sure the next higher bin does not have a lower value.
            if (index < 9 && LoadBins[index + 1].HighValue <= newHighValue)
                UpdateLoadBin(index + 1, newHighValue < Controller.GlobalConfiguration.LoadTypeMaximum ? (byte)(newHighValue + 1) : Controller.GlobalConfiguration.LoadTypeMaximum);

            OnLoadBinUpdated(index);
        }
        #endregion UpdateLoadBin
        #region PopulateRpmBins(ushort[])
        private void PopulateRpmBins(ushort[] rpmBins)
        {
            for (int index = 0; index < rpmBins.Length; index++)
            {
                UpdateRpmBin(index, rpmBins[index]);
            }
        }
        #endregion PopulateRpmBins
        #region UpdateRpmBin(int, ushort)
        public void UpdateRpmBin(int index, ushort newHighValue)
        {
            if (index < 0 || index > 9) throw new ArgumentOutOfRangeException("Index must be between 0 and 9.");
            if (newHighValue % 100 != 0) throw new ArgumentException("newHighValue must be a multiple of 100.");
            if (RpmBins[index].HighValue == newHighValue) return;

            //Make sure the next bin lower does not have a higher value.
            if (index > 0 && RpmBins[index - 1].HighValue >= newHighValue)
                UpdateRpmBin(index - 1, newHighValue > 100 ? (ushort)(newHighValue - 100) : (ushort)100);

            //Update the current bin.
            RpmBins[index].HighValue = newHighValue;
            RpmBins[index].LowValue = index > 0 
                ? (ushort)(RpmBins[index-1].HighValue + 1)
                : (ushort)0;
            
            //Make sure the next higher bin does not have a lower value.
            if (index < 9 && RpmBins[index + 1].HighValue <= newHighValue)
                UpdateRpmBin(index + 1, newHighValue < 10000 ? (ushort)(newHighValue + 100) : (ushort)10000);

            OnRpmBinUpdated(index);
        }
        #endregion UpdateRpmBin
        #region PopulateIgnitionMap(byte[])
        private void PopulateIgnitionMap(byte[] ignitionMap)
        {
            for (byte loadIndex = 0; loadIndex < 10; loadIndex++)
            {
                for (byte rpmIndex = 0; rpmIndex < 10; rpmIndex++)
                {
                    IgnitionCellIndex cellIndex = new IgnitionCellIndex(loadIndex, rpmIndex);
                    UpdateAdvance(cellIndex, ignitionMap[cellIndex.MapIndex]);
                }
            }
        }
        #endregion PopulateIgnitionMap
        #region UpdateAdvance(byte, byte, byte)
        private void UpdateAdvance(byte loadBin, byte rpmBin, byte advance)
        {
            UpdateAdvance(new IgnitionCellIndex(loadBin, rpmBin), advance);
        }
        #endregion UpdateAdvance
        #region UpdateAdvance(IgnitionCellIndex, byte)
        private void UpdateAdvance(IgnitionCellIndex cellIndex, byte advance)
        {
            if (AdvanceMap[cellIndex.MapIndex] == advance) return;
            AdvanceMap[cellIndex.MapIndex] = advance;
            OnIgnitionCellUpdated(cellIndex);
        }
        #endregion UpdateAdvance

        #region Initialize(UserOutput)
        private void Initialize(UserOutput userOutput)
        {
            userOutput.OutputModeChanged += (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
            userOutput.OutputTypeChanged += (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
            userOutput.ThresholdChanged+= (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
        }
        #endregion Initialize
        #region Initialize(AdvanceCorrection)
        private void Initialize(AdvanceCorrection advanceCorrection)
        {
            advanceCorrection.BinChanged += (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
            advanceCorrection.PeakHoldCountChanged += (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
            advanceCorrection.ValueChanged += (sender, e) => { if (!LoadInProgress) SavedState = SavedStates.Dirty; };
        }
        #endregion Initialize

        #region OnSavedStateChanged(SavedStates)
        protected virtual void OnSavedStateChanged(SavedStates newSavedState)
        {
            if (SavedStateChanged == null) return;
            SavedStateChanged(this, new EventArgs<SavedStates>(newSavedState));
        }
        #endregion OnSavedStateChanged
        #region OnRpmBinUpdated(int)
        protected virtual void OnRpmBinUpdated(int index)
        {
            if (RpmBinUpdated == null) return;
            if (!LoadInProgress) SavedState = SavedStates.Dirty;
            RpmBinUpdated(this, new EventArgs<int>(index));
        }
        #endregion OnRpmBinUpdated
        #region OnLoadBinUpdated(int)
        protected virtual void OnLoadBinUpdated(int index)
        {
            if (LoadBinUpdated == null) return;
            if (!LoadInProgress) SavedState = SavedStates.Dirty;
            LoadBinUpdated(this, new EventArgs<int>(index));
        }
        #endregion OnLoadBinUpdated
        #region OnIgnitionCellUpdated(IgnitionMap)
        protected virtual void OnIgnitionCellUpdated(IgnitionCellIndex cellIndex)
        {
            if (IgnitionCellUpdated == null) return;
            if (!LoadInProgress) SavedState = SavedStates.Dirty;
            IgnitionCellUpdated(this, new EventArgs<IgnitionCellIndex>(cellIndex));
        }
        #endregion OnIgnitionCellUpdated
        #region OnShiftLightThresholdChanged(ushort)
        protected virtual void OnShiftLightThresholdChanged(ushort index)
        {
            if (ShiftLightThresholdChanged == null) return;
            if (!LoadInProgress) SavedState = SavedStates.Dirty;
            ShiftLightThresholdChanged(this, new EventArgs<ushort>(index));
        }
        #endregion OnShiftLightThresholdChanged
        #region OnRevLimitThresholdChanged(ushort)
        protected virtual void OnRevLimitThresholdChanged(ushort index)
        {
            if (RevLimitThresholdChanged == null) return;
            if (!LoadInProgress) SavedState = SavedStates.Dirty;
            RevLimitThresholdChanged(this, new EventArgs<ushort>(index));
        }
        #endregion OnRevLimitThresholdChanged
        #endregion METHODS
    }
}
