#region USING STATEMENTS
using System;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class CurrentState
    {
        #region CurrentState(Controller)
        internal CurrentState(Controller controller)
        {
            Controller = controller;
        }
        #endregion CurrentState

        #region EVENTS
        public event EventHandler<EventArgs<byte>> IgnitionAdvanceChanged;
        public event EventHandler<EventArgs<ushort>> RpmChanged;
        public event EventHandler<EventArgs<ushort>> LoadChanged;
        public event EventHandler<EventArgs<IgnitionCellIndex>> IgnitionCellChanged;
        public event EventHandler<EventArgs<IgnitionConfiguration>> IgnitionConfigurationChanged;
        public event EventHandler<EventArgs<byte>> AuxiliaryInputChanged;
        public event EventHandler<EventArgs<byte>> AdvanceCorrectionBinChanged;
        public event EventHandler<EventArgs<sbyte>> AdvanceCorrectionChanged;
        public event EventHandler<EventArgs<BitState>> UserOutput1StateChanged;
        public event EventHandler<EventArgs<BitState>> UserOutput2StateChanged;
        public event EventHandler<EventArgs<BitState>> UserOutput3StateChanged;
        public event EventHandler<EventArgs<BitState>> UserOutput4StateChanged;
        public event EventHandler<EventArgs<BitState>> ShiftLightStateChanged;
        public event EventHandler<EventArgs<BitState>> RevLimitStateChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region Controller Property
        public Controller Controller { get; private set; }
        #endregion Controller
        #region IgnitionAdvance Property
        private byte _IgnitionAdvance;
        public byte IgnitionAdvance
        {
            get { return _IgnitionAdvance; }
            private set
            {
                if (_IgnitionAdvance == value) return;
                _IgnitionAdvance = value;
                OnIgnitionAdvanceChanged(value);
            }
        }
        #endregion IgnitionAdvance
        #region Rpm Property
        private ushort _Rpm;
        public ushort Rpm
        {
            get { return _Rpm; }
            private set
            {
                if (_Rpm == value) return;
                _Rpm = value;
                OnRpmChanged(value);
            }
        }
        #endregion Rpm
        #region Load Property
        private ushort _Load;
        public ushort Load
        {
            get { return _Load; }
            private set
            {
                if (_Load == value) return;
                _Load = value;
                OnLoadChanged(value);
            }
        }
        #endregion Load
        #region IgnitionCell Property
        private IgnitionCellIndex _IgnitionCell;
        public IgnitionCellIndex IgnitionCell
        {
            get { return _IgnitionCell; }
            private set
            {
                if (value.Equals(_IgnitionCell)) return;
                _IgnitionCell = value;
                OnIgnitionCellChanged(value);
            }
        }
        #endregion IgnitionCell
        #region IgnitionConfiguration Property
        private IgnitionConfiguration _IgnitionConfiguration;
        public IgnitionConfiguration IgnitionConfiguration
        {
            get { return _IgnitionConfiguration; }
            private set
            {
                if (_IgnitionConfiguration == value) return;
                _IgnitionConfiguration = value;
                OnIgnitionConfigurationChanged(value);
            }
        }
        #endregion IgnitionConfiguration
        #region AuxiliaryInput Property
        private byte _AuxiliaryInput;
        public byte AuxiliaryInput
        {
            get { return _AuxiliaryInput; }
            private set
            {
                if (_AuxiliaryInput == value) return;
                _AuxiliaryInput = value;
                OnAuxiliaryInputChanged(value);
            }
        }
        #endregion AuxiliaryInput
        #region AdvanceCorrectionBin Property
        private byte _AdvanceCorrectionBin;
        public byte AdvanceCorrectionBin
        {
            get { return _AdvanceCorrectionBin; }
            private set
            {
                if (_AdvanceCorrectionBin == value) return;
                _AdvanceCorrectionBin = value;
                OnAdvanceCorrectionBinChanged(value);
            }
        }
        #endregion AdvanceCorrectionBin
        #region AdvanceCorrection Property
        private sbyte _AdvanceCorrection;
        public sbyte AdvanceCorrection
        {
            get { return _AdvanceCorrection; }
            private set
            {
                if (_AdvanceCorrection == value) return;
                _AdvanceCorrection = value;
                OnAdvanceCorrectionChanged(value);
            }
        }
        #endregion AdvanceCorrection
        #region UserOutput1State Property
        private BitState _UserOutput1State;
        public BitState UserOutput1State
        {
            get { return _UserOutput1State; }
            internal set
            {
                if (_UserOutput1State == value) return;
                _UserOutput1State = value;
                OnUserOutput1StateChanged(value);
            }
        }
        #endregion UserOutput1State
        #region UserOutput2State Property
        private BitState _UserOutput2State;
        public BitState UserOutput2State
        {
            get { return _UserOutput2State; }
            internal set
            {
                if (_UserOutput2State == value) return;
                _UserOutput2State = value;
                OnUserOutput2StateChanged(value);
            }
        }
        #endregion UserOutput2State
        #region UserOutput3State Property
        private BitState _UserOutput3State;
        public BitState UserOutput3State
        {
            get { return _UserOutput3State; }
            internal set
            {
                if (_UserOutput3State == value) return;
                _UserOutput3State = value;
                OnUserOutput3StateChanged(value);
            }
        }
        #endregion UserOutput3State
        #region UserOutput4State Property
        private BitState _UserOutput4State;
        public BitState UserOutput4State
        {
            get { return _UserOutput4State; }
            internal set
            {
                if (_UserOutput4State == value) return;
                _UserOutput4State = value;
                OnUserOutput4StateChanged(value);
            }
        }
        #endregion UserOutput4State
        #region ShiftLightState Property
        private BitState _ShiftLightState;
        public BitState ShiftLightState
        {
            get { return _ShiftLightState; }
            internal set
            {
                if (_ShiftLightState == value) return;
                _ShiftLightState = value;
                OnShiftLightStateChanged(value);
            }
        }
        #endregion ShiftLightState
        #region RevLimitState Property
        private BitState _RevLimitState;
        public BitState RevLimitState
        {
            get { return _RevLimitState; }
            internal set
            {
                if (_RevLimitState == value) return;
                _RevLimitState = value;
                OnRevLimitStateChanged(value);
            }
        }
        #endregion RevLimitState
        #endregion PROPERTIES

        #region METHODS
        #region Populate(GetStateResponse, EdisType)
        internal void Populate(GetStateResponse response, EdisType edisType)
        {
            IgnitionAdvance = response.CurrentIgnitionAdvance;
            Rpm = response.GetCurrentRpmValue(edisType);
            Load = response.CurrentLoadValue;
            IgnitionCell = new IgnitionCellIndex(response.CurrentLoadBin, response.CurrentRpmBin);
            UserOutput1State = response.UserOutput1;
            UserOutput2State = response.UserOutput2;
            UserOutput3State = response.UserOutput3;
            UserOutput4State = response.UserOutput4;
            ShiftLightState = response.ShiftLightState;
            RevLimitState = response.RevLimitState;
            IgnitionConfiguration = response.IgnitionConfiguration;
            AuxiliaryInput = response.AuxiliaryInputValue;
            AdvanceCorrectionBin = response.CurrentAdvanceCorrectionBin;
            AdvanceCorrection = response.CurrentAdvanceCorrectionValue;
        }
        #endregion Populate
        #region Reset()
        public void Reset()
        {
            IgnitionAdvance = 0;
            Rpm = 0;
            Load = 0;
            IgnitionCell = new IgnitionCellIndex(byte.MaxValue, byte.MaxValue);
            IgnitionConfiguration = IgnitionConfiguration.Unknown;
            AuxiliaryInput = 0;
            AdvanceCorrectionBin = 0;
            AdvanceCorrection = 0;
            UserOutput1State = BitState.Inactive;
            UserOutput2State = BitState.Inactive;
            UserOutput3State = BitState.Inactive;
            UserOutput4State = BitState.Inactive;
            ShiftLightState = BitState.Inactive;
            RevLimitState = BitState.Inactive;
        }
        #endregion Reset

        #region OnIgnitionAdvanceChanged(byte)
        protected virtual void OnIgnitionAdvanceChanged(byte newIgnitionAdvance)
        {
            if (IgnitionAdvanceChanged == null) return;
            IgnitionAdvanceChanged(this, new EventArgs<byte>(newIgnitionAdvance));
        }
        #endregion OnIgnitionAdvanceChanged
        #region OnRpmChanged(ushort)
        protected virtual void OnRpmChanged(ushort newRpm)
        {
            if (RpmChanged == null) return;
            RpmChanged(this, new EventArgs<ushort>(newRpm));
        }
        #endregion OnRpmChanged
        #region OnLoadChanged(ushort)
        protected virtual void OnLoadChanged(ushort newLoad)
        {
            if (LoadChanged == null) return;
            LoadChanged(this, new EventArgs<ushort>(newLoad));
        }
        #endregion OnLoadChanged
        #region OnIgnitionCellChanged(IgnitionCellIndex)
        protected virtual void OnIgnitionCellChanged(IgnitionCellIndex newIgnitionCell)
        {
            if (IgnitionCellChanged == null) return;
            IgnitionCellChanged(this, new EventArgs<IgnitionCellIndex>(newIgnitionCell));
        }
        #endregion OnIgnitionCellChanged
        #region OnIgnitionConfigurationChanged(IgnitionConfiguration)
        protected virtual void OnIgnitionConfigurationChanged(IgnitionConfiguration newIgnitionConfiguration)
        {
            if (IgnitionConfigurationChanged == null) return;
            IgnitionConfigurationChanged(this, new EventArgs<IgnitionConfiguration>(newIgnitionConfiguration));
        }
        #endregion OnIgnitionConfigurationChanged
        #region OnAuxiliaryInputChanged(byte)
        protected virtual void OnAuxiliaryInputChanged(byte newAuxiliaryInputState)
        {
            if (AuxiliaryInputChanged == null) return;
            AuxiliaryInputChanged(this, new EventArgs<byte>(newAuxiliaryInputState));
        }
        #endregion OnAuxiliaryInputChanged
        #region OnAdvanceCorrectionBinChanged(byte)
        protected virtual void OnAdvanceCorrectionBinChanged(byte newAdvanceCorrectionBin)
        {
            if (AdvanceCorrectionBinChanged == null) return;
            AdvanceCorrectionBinChanged(this, new EventArgs<byte>(newAdvanceCorrectionBin));
        }
        #endregion OnAdvanceCorrectionBinChanged
        #region OnAdvanceCorrectionChanged(sbyte)
        protected virtual void OnAdvanceCorrectionChanged(sbyte newAdvanceCorrection)
        {
            if (AdvanceCorrectionChanged == null) return;
            AdvanceCorrectionChanged(this, new EventArgs<sbyte>(newAdvanceCorrection));
        }
        #endregion OnAdvanceCorrectionChanged
        #region OnUserOutput1StateChanged(BitState)
        protected virtual void OnUserOutput1StateChanged(BitState newState)
        {
            if (UserOutput1StateChanged == null) return;
            UserOutput1StateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnUserOutput1StateChanged
        #region OnUserOutput2StateChanged(BitState)
        protected virtual void OnUserOutput2StateChanged(BitState newState)
        {
            if (UserOutput2StateChanged == null) return;
            UserOutput2StateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnUserOutput2StateChanged
        #region OnUserOutput3StateChanged(BitState)
        protected virtual void OnUserOutput3StateChanged(BitState newState)
        {
            if (UserOutput3StateChanged == null) return;
            UserOutput3StateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnUserOutput3StateChanged
        #region OnUserOutput4StateChanged(BitState)
        protected virtual void OnUserOutput4StateChanged(BitState newState)
        {
            if (UserOutput4StateChanged == null) return;
            UserOutput4StateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnUserOutput4StateChanged
        #region OnShiftLightStateChanged(BitState)
        protected virtual void OnShiftLightStateChanged(BitState newState)
        {
            if (ShiftLightStateChanged == null) return;
            ShiftLightStateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnShiftLightStateChanged
        #region OnRevLimitStateChanged(BitState)
        protected virtual void OnRevLimitStateChanged(BitState newState)
        {
            if (RevLimitStateChanged == null) return;
            RevLimitStateChanged(this, new EventArgs<BitState>(newState));
        }
        #endregion OnRevLimitStateChanged
        #endregion METHODS
    }
}
