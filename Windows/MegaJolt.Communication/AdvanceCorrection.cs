#region USING STATEMENTS
using System;
using System.Collections.ObjectModel;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class AdvanceCorrection
    {
        #region AdvanceCorrection()
        internal AdvanceCorrection()
        {
            Bins = new Bins<byte>();
            Values = new ReadOnlyCollection<sbyte>(new sbyte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        }
        #endregion AdvanceCorrection

        #region EVENTS
        public event EventHandler<EventArgs<int>> BinChanged;
        public event EventHandler<EventArgs<int>> ValueChanged;
        public event EventHandler<EventArgs<ushort>> PeakHoldCountChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region Bins Property
        public Bins<byte> Bins { get; private set; }
        #endregion Bins
        #region Values Property
        public ReadOnlyCollection<sbyte> Values { get; private set; }
        #endregion Values
        #region PeakHoldCount Property
        private ushort _PeakHoldCount;
        public ushort PeakHoldCount
        {
            get { return _PeakHoldCount; }
            set
            {
                if (_PeakHoldCount == value) return;
                _PeakHoldCount = value;
                OnPeakHoldCountChanged(value);
            }
        }
        #endregion PeakHoldCount
        #endregion PROPERTIES

        #region METHODS
        #region Populate(ReadOnlyCollection<byte>, ReadOnlyCollection<sbyte>, ushort)
        public void Populate(byte[] bins, sbyte[] values, ushort peakHoldCount)
        {
            for (byte index = 0; index < 10; index++)
            {
                UpdateBin(index, bins[index]);
                UpdateValue(index, values[index]);
            }
            PeakHoldCount = peakHoldCount;
        }
        #endregion Populate
        #region Reset()
        public void Reset()
        {
            for (byte index = 0; index < 10; index++)
            {
                UpdateBin(index, 0);
                UpdateValue(index, 0);
            }
            PeakHoldCount = 0;
        }
        #endregion Reset
        #region UpdateBin(int, byte)
        public void UpdateBin(int index, byte highValue)
        {
            if (Bins[index].HighValue == highValue) return;
            Bins[index].HighValue = highValue;
            if (index < 9)
                Bins[index + 1].LowValue = highValue < byte.MaxValue 
                    ? (byte)(highValue + 1)
                    : highValue;
            OnBinChanged(index);
        }
        #endregion UpdateValue
        #region UpdateValue(byte, sbyte)
        public void UpdateValue(byte index, sbyte value)
        {
            if (Values[index] == value) return;
            sbyte[] values = Values.ToArray();
            values[index] = value;
            Values = new ReadOnlyCollection<sbyte>(values);
            OnValueChanged(index);
        }
        #endregion UpdateValue

        #region OnBinChanged(DataType)
        protected virtual void OnBinChanged(int index)
        {
            if (BinChanged == null) return;
            BinChanged(this, new EventArgs<int>(index));
        }
        #endregion OnBinChanged
        #region OnValueChanged(DataType)
        protected virtual void OnValueChanged(int index)
        {
            if (ValueChanged == null) return;
            ValueChanged(this, new EventArgs<int>(index));
        }
        #endregion OnValueChanged
        #region OnPeakHoldCountChanged(ushort)
        protected virtual void OnPeakHoldCountChanged(ushort newPeakHoldCount)
        {
            if (PeakHoldCountChanged == null) return;
            PeakHoldCountChanged(this, new EventArgs<ushort>(newPeakHoldCount));
        }
        #endregion OnPeakHoldCountChanged
        #endregion METHODS
    }
}
