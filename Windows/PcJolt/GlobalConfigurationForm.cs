#region USING STATEMENTS
using System;
using System.Linq;
using System.Windows.Forms;
using MegaJolt.Communication;
#endregion USING STATEMENTS

namespace PcJolt
{
    public partial class GlobalConfigurationForm : Form
    {
        #region GlobalConfigurationForm()
        private GlobalConfigurationForm() : this(StartupAction.None, null) { }
        #endregion GlobalConfigurationForm
        #region GlobalConfigurationForm(StartupAction, Controller)
        public GlobalConfigurationForm(StartupAction startupAction, Controller controller)
        {
            InitializeComponent();
            Controller = controller;

            ComPortComboBox.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            ComPortComboBox.SelectedIndex = string.IsNullOrEmpty(controller.ComPort) ? 0 : ComPortComboBox.FindString(controller.ComPort);
            ComPortComboBox.Enabled = !controller.IsConnected;

            RefreshIntervalNumericUpDown.Value = controller.RefreshInterval;
            RefreshIntervalNumericUpDown.Enabled = !controller.IsConnected;

            LoadTypeComboBox.Items.AddRange(Enum.GetNames(typeof(LoadType)).Where(item => item != "Unknown").ToArray());
            LoadTypeComboBox.SelectedIndex = LoadTypeComboBox.FindString(controller.GlobalConfiguration.LoadType.ToString());
            LoadTypeComboBox.Enabled = controller.IsConnected;

            AspirationTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AspirationType)).Where(item => item != "Unknown").ToArray());
            AspirationTypeComboBox.SelectedIndex = AspirationTypeComboBox.FindString(controller.GlobalConfiguration.AspirationType.ToString());
            AspirationTypeComboBox.Enabled = controller.IsConnected;
       
            StartupActionComboBox.Items.AddRange(Enum.GetNames(typeof(StartupAction)));
            StartupActionComboBox.SelectedIndex = StartupActionComboBox.FindString(startupAction.ToString());
            StartupActionComboBox.Enabled = controller.IsConnected;

            EdisTypeComboBox.Items.AddRange(Enum.GetNames(typeof(EdisType)).Where(item => item != "Unknown").ToArray());
            EdisTypeComboBox.SelectedIndex = EdisTypeComboBox.FindString(controller.GlobalConfiguration.EdisType.ToString());
            EdisTypeComboBox.Enabled = controller.IsConnected;

            PipNoiseFilterNumericUpDown.Value = controller.GlobalConfiguration.PipNoiseFilter;
            PipNoiseFilterNumericUpDown.Enabled = controller.IsConnected;
            CrankingAdvanceNumericUpDown.Value = controller.GlobalConfiguration.CrankingAdvance;
            CrankingAdvanceNumericUpDown.Enabled = controller.IsConnected;
            TriggerWheelOffsetNumericUpDown.Value = controller.GlobalConfiguration.TriggerWheelOffset;
            TriggerWheelOffsetNumericUpDown.Enabled = controller.IsConnected;

            if (ComPortComboBox.Enabled)
                ComPortComboBox.Focus();
            else
                LoadTypeComboBox.Focus();
        }
        #endregion GlobalConfigurationForm

        #region Controller Property
        private Controller Controller { get; set; }
        #endregion Controller
        #region ComPort Property
        public string ComPort { get { return (string)ComPortComboBox.SelectedItem; } }
        #endregion ComPort
        #region RefreshInterval Property
        public int RefreshInterval { get { return (int)RefreshIntervalNumericUpDown.Value; } }
        #endregion RefreshInterval
        #region LoadType Property
        public LoadType LoadType { get { return (LoadType)Enum.Parse(typeof(LoadType), (string)LoadTypeComboBox.SelectedItem); } }
        #endregion LoadType
        #region AspirationType Property
        public AspirationType AspirationType { get { return (AspirationType)Enum.Parse(typeof(AspirationType), (string)AspirationTypeComboBox.SelectedItem); } }
        #endregion AspirationType
        #region StartupAction Property
        public StartupAction StartupAction { get { return (StartupAction)Enum.Parse(typeof(StartupAction), (string)StartupActionComboBox.SelectedItem); } }
        #endregion AspirationType
        #region EdisType Property
        public EdisType EdisType { get { return (EdisType)Enum.Parse(typeof(EdisType), (string)EdisTypeComboBox.SelectedItem); } }
        #endregion EdisType
        #region PipNoiseFilter Property
        public byte PipNoiseFilter { get { return (byte)PipNoiseFilterNumericUpDown.Value; } }
        #endregion PipNoiseFilter
        #region CrankingAdvance Property
        public byte CrankingAdvance { get { return (byte)CrankingAdvanceNumericUpDown.Value; } }
        #endregion CrankingAdvance
        #region TriggerWheelOffset Property
        public sbyte TriggerWheelOffset { get { return (sbyte)TriggerWheelOffsetNumericUpDown.Value; } }
        #endregion TriggerWheelOffset
    }
}
