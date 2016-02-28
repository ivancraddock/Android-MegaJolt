#region USING STATEMENTS
using System;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class GlobalConfiguration
    {
        #region GlobalConfiguration(Controller)
        public GlobalConfiguration(Controller controller)
        {
            Controller = controller;
            _loadType = LoadType.Unknown;
            _aspirationType = AspirationType.Unknown;
            _EdisType = EdisType.Unknown;
        }
        #endregion GlobalConfiguration

        #region EVENTS
        public event EventHandler<EventArgs<LoadType>> LoadTypeChanged;
        public event EventHandler<EventArgs<string>> LoadTypeLabelChanged;
        public event EventHandler<EventArgs<byte>> LoadTypeMaximumChanged;
        public event EventHandler<EventArgs<AspirationType>> AspirationTypeChanged;
        public event EventHandler<EventArgs<EdisType>> EdisTypeChanged;
        public event EventHandler<EventArgs<byte>> PipNoiseFilterChanged;
        public event EventHandler<EventArgs<byte>> CrankingAdvanceChanged;
        public event EventHandler<EventArgs<sbyte>> TriggerWheelOffsetChanged;
        #endregion EVENTS

        #region PROPERTIES
        #region Controller Property
        public Controller Controller { get; private set; }
        #endregion Controller
        #region LoadType Property
        private LoadType _loadType;
        public LoadType LoadType
        {
            get { return _loadType; }
            set
            {
                if (_loadType == value) return;
                _loadType = value;
                OnLoadTypeChanged(LoadType);
                OnLoadTypeLabelChanged(LoadTypeLabel);
                OnLoadTypeMaximumChanged(LoadTypeMaximum);
            }
        }
        #endregion LoadType
        #region LoadTypeLabel Property
        public string LoadTypeLabel { get { return LoadType.LoadTypeLabel(); } }
        #endregion LoadTypeLabel
        #region LoadTypeMaximum Property
        public byte LoadTypeMaximum { get { return LoadType.LoadTypeMaximum(AspirationType); } }
        #endregion LoadTypeMaximum
        #region AspirationType Property
        private AspirationType _aspirationType;
        public AspirationType AspirationType
        {
            get { return _aspirationType; }
            set
            {
                if (_aspirationType == value) return;
                _aspirationType = value;
                OnAspirationTypeChanged(AspirationType);
                if (LoadType == LoadType.MAP)
                    OnLoadTypeMaximumChanged(LoadTypeMaximum);
            }
        }
        #endregion AspirationType
        #region EdisType Property
        private EdisType _EdisType;
        public EdisType EdisType
        {
            get { return _EdisType; }
            set
            {
                if (_EdisType == value) return;
                _EdisType = value;
                OnEdisTypeChanged(value);
            }
        }
        #endregion EdisType
        #region PipNoiseFilter Property
        private byte _PipNoiseFilter;
        public byte PipNoiseFilter
        {
            get { return _PipNoiseFilter; }
            set
            {
                if (_PipNoiseFilter == value) return;
                _PipNoiseFilter = value;
                OnPipNoiseFilterChanged(value);
            }
        }
        #endregion PipNoiseFilter
        #region CrankingAdvance Property
        private byte _CrankingAdvance;
        public byte CrankingAdvance
        {
            get { return _CrankingAdvance; }
            set
            {
                if (_CrankingAdvance == value) return;
                _CrankingAdvance = value;
                OnCrankingAdvanceChanged(value);
            }
        }
        #endregion CrankingAdvance
        #region TriggerWheelOffset Property
        private sbyte _TriggerWheelOffset;
        public sbyte TriggerWheelOffset
        {
            get { return _TriggerWheelOffset; }
            set
            {
                if (_TriggerWheelOffset == value) return;
                _TriggerWheelOffset = value;
                OnTriggerWheelOffsetChanged(value);
            }
        }
        #endregion TriggerWheelOffset
        #endregion PROPERTIES

        #region METHODS
        #region Populate(GetGlobalConfigurationResponse)
        public void Populate(GetGlobalConfigurationResponse response)
        {
            EdisType = response.EdisType;
            PipNoiseFilter = response.PipNoiseFilter;
            CrankingAdvance = response.CrankingAdvance;
            TriggerWheelOffset = response.TriggerWheelOffset;
        }
        #endregion Populate

        #region OnLoadTypeChanged(LoadType)
        protected virtual void OnLoadTypeChanged(LoadType newLoadType)
        {
            if (LoadTypeChanged == null) return;
            LoadTypeChanged(this, new EventArgs<LoadType>(newLoadType));
        }
        #endregion OnLoadTypeChanged
        #region OnLoadTypeLabelChanged(string)
        protected virtual void OnLoadTypeLabelChanged(string newLoadTypeLabel)
        {
            if (LoadTypeLabelChanged == null) return;
            LoadTypeLabelChanged(this, new EventArgs<string>(newLoadTypeLabel));
        }
        #endregion OnLoadTypeLabelChanged
        #region OnLoadTypeMaximumChanged(byte)
        protected virtual void OnLoadTypeMaximumChanged(byte newLoadTypeMaximum)
        {
            if (LoadTypeMaximumChanged == null) return;
            LoadTypeMaximumChanged(this, new EventArgs<byte>(newLoadTypeMaximum));
        }
        #endregion OnLoadTypeMaximumChanged
        #region OnAspirationTypeChanged(AspirationType)
        protected virtual void OnAspirationTypeChanged(AspirationType newAspirationType)
        {
            if (AspirationTypeChanged == null) return;
            AspirationTypeChanged(this, new EventArgs<AspirationType>(newAspirationType));
        }
        #endregion OnAspirationTypeChanged
        #region OnEdisTypeChanged(EdisType)
        protected virtual void OnEdisTypeChanged(EdisType newEdisType)
        {
            if (EdisTypeChanged == null) return;
            EdisTypeChanged(this, new EventArgs<EdisType>(newEdisType));
        }
        #endregion OnEdisTypeChanged
        #region OnPipNoiseFilterChanged(byte)
        protected virtual void OnPipNoiseFilterChanged(byte newCurrentIgnitionAdvance)
        {
            if (PipNoiseFilterChanged == null) return;
            PipNoiseFilterChanged(this, new EventArgs<byte>(newCurrentIgnitionAdvance));
        }
        #endregion OnPipNoiseFilterChanged
        #region OnCrankingAdvanceChanged(byte)
        protected virtual void OnCrankingAdvanceChanged(byte newCurrentIgnitionAdvance)
        {
            if (CrankingAdvanceChanged == null) return;
            CrankingAdvanceChanged(this, new EventArgs<byte>(newCurrentIgnitionAdvance));
        }
        #endregion OnCrankingAdvanceChanged
        #region OnTriggerWheelOffsetChanged(sbyte)
        protected virtual void OnTriggerWheelOffsetChanged(sbyte newCurrentIgnitionAdvance)
        {
            if (TriggerWheelOffsetChanged == null) return;
            TriggerWheelOffsetChanged(this, new EventArgs<sbyte>(newCurrentIgnitionAdvance));
        }
        #endregion OnTriggerWheelOffsetChanged
        #endregion METHODS
    }
}
