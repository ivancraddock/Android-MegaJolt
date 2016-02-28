#region USING STATEMENTS
using System;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class UserOutput
    {
        #region CONSTRUCTORS
        #region UserOutput(Controller)
        internal UserOutput(Controller controller) : this(UserOutputType.Unknown, UserOutputMode.Unknown, 0)
        {
            Controller = controller;
        }
        #endregion UserOutput
        #region UserOutput(UserOutputType, UserOutputMode, byte)
        internal UserOutput(UserOutputType outputType, UserOutputMode outputMode, ushort threshold)
        {
            OutputType = outputType;
            OutputMode = outputMode;
            Threshold = OutputType == UserOutputType.Rpm ? (ushort)(threshold * 100) : threshold;
        }
        #endregion UserOutput
        #endregion CONSTRUCTORS

        #region EVENTS
        public event EventHandler<EventArgs<UserOutputMode>> OutputModeChanged;
        public event EventHandler<EventArgs<UserOutputType>> OutputTypeChanged;
        public event EventHandler<EventArgs<ushort>> ThresholdChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region Controller Property
        public Controller Controller { get; private set; }
        #endregion Controller
        #region OutputMode Property
        private UserOutputMode _OutputMode;
        public UserOutputMode OutputMode
        {
            get { return _OutputMode; }
            set
            {
                if (_OutputMode == value) return;
                _OutputMode = value;
                OnOutputModeChanged(value);
            }
        }
        #endregion OutputMode
        #region OutputType Property
        private UserOutputType _OutputType;
        public UserOutputType OutputType
        {
            get { return _OutputType; }
            set
            {
                if (_OutputType == value) return;
                _OutputType = value;
                OnOutputTypeChanged(value);
            }
        }
        #endregion OutputType
        #region Threshold Property
        private ushort _Threshold;
        public ushort Threshold
        {
            get { return _Threshold; }
            set
            {
                if (Controller != null)
                {
                    ushort maxValue = GetThresholdMaximum(OutputType, Controller.GlobalConfiguration.LoadType, Controller.GlobalConfiguration.AspirationType);
                    if (value > maxValue)
                        throw new ArgumentOutOfRangeException("Threshold cannot be greater than " + maxValue + ".");
                }
                if (_Threshold == value) return;
                _Threshold = value;
                OnThresholdChanged(value);
            }
        }
        #endregion Threshold
        #endregion PROPERTIES

        #region METHODS
        #region Populate(UserOutput)
        internal void Populate(UserOutput newUserOutput)
        {
            OutputType = newUserOutput.OutputType;
            OutputMode = newUserOutput.OutputMode;
            Threshold = newUserOutput.Threshold;
        }
        #endregion Populate
        #region Reset()
        internal void Reset()
        {
            Threshold = 0;
            OutputMode = UserOutputMode.Unknown;
            OutputType = UserOutputType.Unknown;
        }
        #endregion Reset

        #region OnOutputModeChanged(UserOutputMode)
        protected virtual void OnOutputModeChanged(UserOutputMode newOutputMode)
        {
            if (OutputModeChanged == null) return;
            OutputModeChanged(this, new EventArgs<UserOutputMode>(newOutputMode));
        }
        #endregion OnThresholdChanged
        #region OnOutputTypeChanged(UserOutputType)
        protected virtual void OnOutputTypeChanged(UserOutputType newOutputType)
        {
            if (OutputTypeChanged == null) return;
            OutputTypeChanged(this, new EventArgs<UserOutputType>(newOutputType));
        }
        #endregion OnThresholdChanged
        #region OnThresholdChanged(ushort)
        protected virtual void OnThresholdChanged(ushort newThreshold)
        {
            if (ThresholdChanged == null) return;
            ThresholdChanged(this, new EventArgs<ushort>(newThreshold));
        }
        #endregion OnThresholdChanged

        #region GetThresholdMaximum(UserOutputType, LoadType, AspirationType)
        public static ushort GetThresholdMaximum(UserOutputType outputType, LoadType loadType, AspirationType aspirationType)
        {
            if (outputType == UserOutputType.Unknown) return ushort.MaxValue;
            if (outputType == UserOutputType.Rpm) return 10000;
            if (outputType == UserOutputType.AuxiliaryInput) return 255;
            return loadType.LoadTypeMaximum(aspirationType);
        }
        #endregion GetThresholdMaximum
        #region Equals(object)
        public override bool Equals(object obj)
        {
            UserOutput that = obj as UserOutput;
            if (that == null) return false;
            return
                base.Equals(obj) &&
                this.OutputMode == that.OutputMode &&
                this.OutputType == that.OutputType;
        }
        #endregion Equals
        #endregion METHODS
    }
}
