 #region USING STATEMENTS
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevAge.Windows.Forms;
using MegaJolt.Communication;
using MegaJolt.Communication.SimulatorConnection;
using MegaJolt.Communication.WindowsPC;
using PcJolt.Properties;
using SourceGrid.Cells;
using Resources = PcJolt.Properties.Resources;
#endregion USING STATEMENTS

namespace PcJolt
{
    public partial class MainForm : Form
    {
        #region CONSTRUCTORS
        #region MainForm()
        private MainForm()
        {
            InitializeComponent();
        }
        #endregion MainForm
        #region MainForm(string[])
        public MainForm(string[] args)
        {
            InitializeComponent();
            
            base.CreateHandle();
            Settings setttings = Settings.Default;

            //Window Settings
            StartPosition = FormStartPosition.Manual;
            if (setttings.Height > base.MinimumSize.Height)
                Height = setttings.Height;
            if (setttings.Width > base.MinimumSize.Width)
                Width = setttings.Width;
            Left = setttings.Left > int.MinValue
                       ? setttings.Left
                       : (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            Top = setttings.Top > int.MinValue
                       ? setttings.Top
                       : (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            if (setttings.WindowState != FormWindowState.Minimized)
                WindowState = setttings.WindowState;
            MainTabControl.SelectedIndex = setttings.SelectedTabIndex;
            if (setttings.MainSplitterDistance > -1)
                MainSplitContainer.SplitterDistance = setttings.MainSplitterDistance;
            if (setttings.OptionsSplitterDistance > -1)
                OptionsSplitContainer.SplitterDistance = setttings.OptionsSplitterDistance;

            //Application Settings
#if(SIMULATOR)
            Controller = new Controller(new SimulatorPort(), new FileStorage());
#else
            Controller = new Controller(new VirtualSerialPort(), new FileStorage());
#endif
            Controller.StateChanged += Controller_StateChanged;
            Controller.GlobalConfiguration.LoadTypeLabelChanged += (sender, e) => { LoadGauge.DialText = e.Value; };
            Controller.GlobalConfiguration.LoadTypeMaximumChanged += GlobalConfiguration_LoadTypeMaximumChanged;
            Controller.GlobalConfiguration.EdisTypeChanged += (sender, e) => { Controller.GlobalConfiguration.EdisType = e.Value; };
            Controller.GlobalConfiguration.PipNoiseFilterChanged += (sender, e) => { Controller.GlobalConfiguration.PipNoiseFilter = e.Value; };
            Controller.GlobalConfiguration.CrankingAdvanceChanged += (sender, e) => { Controller.GlobalConfiguration.CrankingAdvance = e.Value; };
            Controller.GlobalConfiguration.TriggerWheelOffsetChanged += (sender, e) => { Controller.GlobalConfiguration.TriggerWheelOffset = e.Value; };

            Controller.CurrentState.IgnitionAdvanceChanged += (sender, e) => SafeInvoke(() => AdvanceGauge.Value = e.Value);
            Controller.CurrentState.IgnitionCellChanged += Controller_CurrentIgnitionCellChanged;
            Controller.CurrentState.IgnitionConfigurationChanged += (sender, e) => SafeInvoke(() => IgnitionConfigurationToolStripStatusLabel.Text = e.Value != IgnitionConfiguration.Unknown ? string.Format("Configuration {0} active", (int)e.Value) : string.Empty); 
            Controller.CurrentState.LoadChanged += (sender, e) => SafeInvoke(() => LoadGauge.Value = e.Value);
            Controller.CurrentState.RpmChanged += (sender, e) => SafeInvoke(() => RpmGauge.Value = e.Value);
            Controller.CurrentState.UserOutput1StateChanged += (sender, e) => SafeInvoke(() => UserOutput1ToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.GreenBall : Resources.SliverBall);
            Controller.CurrentState.UserOutput2StateChanged += (sender, e) => SafeInvoke(() => UserOutput2ToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.GreenBall : Resources.SliverBall);
            Controller.CurrentState.UserOutput3StateChanged += (sender, e) => SafeInvoke(() => UserOutput3ToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.GreenBall : Resources.SliverBall);
            Controller.CurrentState.UserOutput4StateChanged += (sender, e) => SafeInvoke(() => UserOutput4ToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.GreenBall : Resources.SliverBall);
            Controller.CurrentState.ShiftLightStateChanged += (sender, e) => SafeInvoke(() => ShiftLightToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.YeallowBall : Resources.SliverBall);
            Controller.CurrentState.RevLimitStateChanged += (sender, e) => SafeInvoke(() => RevLimitToolStripStatusLabel.Image = e.Value == BitState.Active ? Resources.RedBall : Resources.SliverBall);

            Controller.IgnitionMap.SavedStateChanged += IgnitionMap_SavedStateChanged;
            Controller.IgnitionMap.RpmBinUpdated += IgnitionMap_RpmBinUpdated;
            Controller.IgnitionMap.LoadBinUpdated += IgnitionMap_LoadBinUpdated;
            Controller.IgnitionMap.IgnitionCellUpdated += IgnitionMap_IgnitionCellUpdated;
            Controller.IgnitionMap.AdvanceCorrection.BinChanged += AdvanceCorrection_BinChanged;
            Controller.IgnitionMap.AdvanceCorrection.ValueChanged += AdvanceCorrection_ValueChanged;
            Controller.IgnitionMap.AdvanceCorrection.PeakHoldCountChanged += (sender, e) => { PeakHoldNumericUpDown.Value = e.Value; };
            Controller.IgnitionMap.ShiftLightThresholdChanged += (sender, e) => SafeInvoke(() => ShiftLightActivationPointNumericUpDown.Value = Controller.IgnitionMap.ShiftLightThreshold);
            Controller.IgnitionMap.RevLimitThresholdChanged += RevLimit_ThresholdChanged;
            Controller.IgnitionMap.UserOutput1.ThresholdChanged += (sender, e) => { Output1ActivationPointNumericUpDown.Value = Controller.IgnitionMap.UserOutput1.Threshold; };
            Controller.IgnitionMap.UserOutput1.OutputTypeChanged += (sender, e) => { Output1TypeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput1.OutputType != UserOutputType.Unknown ? ((int)Controller.IgnitionMap.UserOutput1.OutputType) : -1; };
            Controller.IgnitionMap.UserOutput1.OutputModeChanged += (sender, e) => { Output1ModeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput1.OutputMode != UserOutputMode.Unknown ? (int)Controller.IgnitionMap.UserOutput1.OutputMode : -1; };
            Controller.IgnitionMap.UserOutput2.ThresholdChanged += (sender, e) => { Output2ActivationPointNumericUpDown.Value = Controller.IgnitionMap.UserOutput2.Threshold; };
            Controller.IgnitionMap.UserOutput2.OutputTypeChanged += (sender, e) => { Output2TypeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput2.OutputType != UserOutputType.Unknown ? ((int)Controller.IgnitionMap.UserOutput2.OutputType) : -1; };
            Controller.IgnitionMap.UserOutput2.OutputModeChanged += (sender, e) => { Output2ModeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput2.OutputMode != UserOutputMode.Unknown ? (int)Controller.IgnitionMap.UserOutput2.OutputMode : -1; };
            Controller.IgnitionMap.UserOutput3.ThresholdChanged += (sender, e) => { Output3ActivationPointNumericUpDown.Value = Controller.IgnitionMap.UserOutput3.Threshold; };
            Controller.IgnitionMap.UserOutput3.OutputTypeChanged += (sender, e) => { Output3TypeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput3.OutputType != UserOutputType.Unknown ? ((int)Controller.IgnitionMap.UserOutput3.OutputType) : -1; };
            Controller.IgnitionMap.UserOutput3.OutputModeChanged += (sender, e) => { Output3ModeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput3.OutputMode != UserOutputMode.Unknown ? (int)Controller.IgnitionMap.UserOutput3.OutputMode : -1; };
            Controller.IgnitionMap.UserOutput4.ThresholdChanged += (sender, e) => { Output4ActivationPointNumericUpDown.Value = Controller.IgnitionMap.UserOutput4.Threshold; };
            Controller.IgnitionMap.UserOutput4.OutputTypeChanged += (sender, e) => { Output4TypeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput4.OutputType != UserOutputType.Unknown ? ((int)Controller.IgnitionMap.UserOutput4.OutputType) : -1; };
            Controller.IgnitionMap.UserOutput4.OutputModeChanged += (sender, e) => { Output4ModeComboBox.SelectedIndex = Controller.IgnitionMap.UserOutput4.OutputMode != UserOutputMode.Unknown ? (int)Controller.IgnitionMap.UserOutput4.OutputMode : -1; };

            Controller.ComPort = setttings.ComPort;
            Controller.RefreshInterval = setttings.RefreshInterval;
            Controller.GlobalConfiguration.LoadType = setttings.LoadType;
            Controller.GlobalConfiguration.AspirationType = setttings.AspirationType;
            StartupAction = setttings.StartupAction;

            columnHeaderView.Font = new Font(base.Font, FontStyle.Bold);
            rowHeaderView.Font = new Font(base.Font, FontStyle.Bold);
            currentCellView.Font = new Font(base.Font, FontStyle.Bold);
            InitializeIgnitionMap();
            InitializeAdvanceCorrectionMap();

#if (DEBUG)
            Controller.ComPort = "COM7";
            Controller.RefreshInterval = 250;
#endif
            if (ShowGlobalConfigurationForm() == DialogResult.Cancel)
                Application.Exit();

            //Connect if we are supposed to.
            if (StartupAction == StartupAction.Connect)
                Connect();

            IgnitionMapDataGridView.AutoStretchColumnsToFitWidth = true;

            if (args.Length == 1 && File.Exists(args[0]))
                OpenFile(args[0]);
        }
        #endregion MainForm
        #endregion CONSTRUCTORS

        #region FIELDS
        private readonly SourceGrid.Cells.Editors.NumericUpDown advanceEditor = new SourceGrid.Cells.Editors.NumericUpDown { EditableMode = SourceGrid.EditableMode.DoubleClick | SourceGrid.EditableMode.AnyKey, Increment = 1 };
        private readonly SourceGrid.Cells.Views.Cell emptyHeaderView = new SourceGrid.Cells.Views.Cell { BackColor = SystemColors.AppWorkspace };
        private readonly SourceGrid.Cells.Views.Cell columnHeaderView = new SourceGrid.Cells.Views.Cell { TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter };
        private readonly SourceGrid.Cells.Views.Cell rowHeaderView = new SourceGrid.Cells.Views.Cell { TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft };
        private readonly SourceGrid.Cells.Views.Cell currentCellView = new SourceGrid.Cells.Views.Cell { TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft, BackColor = SystemColors.Highlight, ForeColor = SystemColors.HighlightText };
        #endregion FIELDS

        #region PROPERTIES
        #region Controller Property
        private Controller Controller { get; set; }
        #endregion Controller
        #region StartupAction Property
        private StartupAction StartupAction { get; set; }
        #endregion StartupAction

        #region FileName Property
        private string FileName { get; set; }
        #endregion FileName

        #region CurrentCell Property
        private ICell CurrentCell { get; set; }
        #endregion CurrentCell
        #endregion PROPERTIES

        #region METHODS
        #region SafeInvoke(Action)
        private void SafeInvoke(Action action)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        #endregion SafeInvoke
        #region ShowErrorMessage(Exception)
        private void ShowErrorMessage(Exception exception)
        {
            SafeInvoke(() => MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }
        #endregion ShowErrorMessage

        #region Connect()
        private void Connect()
        {
            Task.Run(() =>
            {
                try
                {
                    Settings settings = Settings.Default;
                    Controller.GlobalConfiguration.LoadType = settings.LoadType;
                    Controller.GlobalConfiguration.AspirationType = settings.AspirationType;

                    Controller.Open();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            });
        }
        #endregion Connect
        #region Disconnect()
        private void Disconnect()
        {
            Task.Run(() => Controller.Close());
        }
        #endregion Disconnect

        #region GetAdvanceColor(byte)
        private static Color GetAdvanceColor(byte advance)
        {
            Color color = Color.FromArgb(GetRedValue(advance), GetGreenValue(advance), GetBlueValue(advance));

            return color;
        }
        #endregion GetAdvanceColor
        #region GetRedValue(byte)
        private static int GetRedValue(byte advance)
        {
            return Math.Min(255, 111 + (advance * 21));
        }
        #endregion GetRedValue
        #region GetGreenValue(byte)
        private static int GetGreenValue(byte advance)
        {
            int temp = Math.Max(0, advance - 18);
            return Math.Min(255, temp * 21);
        }
        #endregion GetGreenValue
        #region GetBlueValue(byte)
        private static int GetBlueValue(byte advance)
        {
            if (advance < 8)
                return 255;
            if (advance > 19 && advance < 33)
                return 0;
            if (advance < 20)
                return Math.Max(0, 255 - ((advance - 8) * 21));
            return Math.Min(255, (advance - 32) * 11);
        }
        #endregion GetBlueValue

        #region OpenFile(string)
        private void OpenFile(string fileName)
        {
            Task.Run(() =>
            {
                Controller.IgnitionMap.Load(fileName);
                FileName = fileName;
                SafeInvoke(() => Text = "PC Jolt [" + Path.GetFileName(fileName) + "]");
            });
        }
        #endregion OpenFile
        #region InitializeIgnitionMap()
        private void InitializeIgnitionMap()
        {
            IgnitionMapDataGridView.Redim(11, 11);
            IgnitionMapDataGridView.FixedColumns = 1;
            IgnitionMapDataGridView.FixedRows = 1;

            IgnitionMapDataGridView[0, 0] = new SourceGrid.Cells.ColumnHeader(string.Empty) { View = emptyHeaderView };
            IgnitionMapDataGridView[0, 0].Column.MinimalWidth = 80;
            IgnitionMapDataGridView[0, 0].Column.MaximalWidth = 200;
            IgnitionMapDataGridView[0, 0].Column.Width = 80;

            for (int rowIndex = 1; rowIndex < 11; rowIndex++)
            {
                IgnitionMapDataGridView[rowIndex, 0] = new LoadCell(Font, Controller.GlobalConfiguration.LoadTypeMaximum, (binIndex, value) => Controller.IgnitionMap.UpdateLoadBin(binIndex, value));
                IgnitionMapDataGridView.Rows[rowIndex].Height = 28;

                for (int columnIndex = 1; columnIndex < IgnitionMapDataGridView.ColumnsCount; columnIndex++)
                {
                    if (rowIndex == 1)
                    {
                        Cell rpmHeaderCellheader = new RpmCell(Font, (binIndex, value) => Controller.IgnitionMap.UpdateRpmBin(binIndex, value));
                        IgnitionMapDataGridView[0, columnIndex] = rpmHeaderCellheader;
                        rpmHeaderCellheader.Column.MinimalWidth = 60;
                        rpmHeaderCellheader.Column.Width = 60;
                        rpmHeaderCellheader.View = columnHeaderView;
                    }

                    IgnitionMapDataGridView[rowIndex, columnIndex] = new AdvanceCell(Font, true, (row, col, value) => Controller.IgnitionMap[row, col] = unchecked((byte)value));
                }
            }
            IgnitionMapDataGridView.Width = 0; //This will force a resize so the columns will stretch to fill the space.  The width will not actually be set to 0 because the Dock property is set to Fill.
        }
        #endregion InitializeIgnitionMap
        #region InitializeAdvanceCorrectionMap()
        private void InitializeAdvanceCorrectionMap()
        {           
            AdvanceCorrectionDataGridView.Redim(3, 11);
            AdvanceCorrectionDataGridView.FixedColumns = 1;
            AdvanceCorrectionDataGridView.FixedRows = 1;
            AdvanceCorrectionDataGridView.Rows[0].Height = 28;
            AdvanceCorrectionDataGridView.Rows[1].Height = 28;
            AdvanceCorrectionDataGridView.Rows[2].Height = 28;

            SourceGrid.Cells.Views.Cell binCellView = new SourceGrid.Cells.Views.Cell {Font = new Font(Font, FontStyle.Bold), TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter};

            AdvanceCorrectionDataGridView[0, 0] = new SourceGrid.Cells.ColumnHeader(string.Empty) { View = emptyHeaderView };
            AdvanceCorrectionDataGridView[0, 0].Column.MinimalWidth = 80;
            AdvanceCorrectionDataGridView[0, 0].Column.MaximalWidth = 200;
            AdvanceCorrectionDataGridView[0, 0].Column.Width = 80;
            AdvanceCorrectionDataGridView[1, 0] = new SourceGrid.Cells.ColumnHeader("Bins") { View = rowHeaderView };
            AdvanceCorrectionDataGridView[2, 0] = new SourceGrid.Cells.ColumnHeader("Correction") { View = rowHeaderView };

            for (int index = 1; index < AdvanceCorrectionDataGridView.ColumnsCount; index++)
            {
                SourceGrid.Cells.ColumnHeader header = new SourceGrid.Cells.ColumnHeader(index);
                AdvanceCorrectionDataGridView[0, index] = header;
                header.Column.MinimalWidth = 60;
                header.Column.Width = 60;
                header.View = columnHeaderView;

                AdvanceCorrectionDataGridView[1, index] = new Cell(0, advanceEditor) { View = binCellView };
                AdvanceCorrectionDataGridView[2, index] = new AdvanceCell(Font, true, (row, col, value) => Controller.IgnitionMap.AdvanceCorrection.UpdateValue(col, value));
            }
            AdvanceCorrectionDataGridView.Width = 0; //This will force a resize so the columns will stretch to fill the space.  The width will not actually be set to 0 because the Dock property is set to Fill.
        }
        #endregion InitializeAdvanceCorrectionMap
        #region ShowGlobalConfigurationForm()
        private DialogResult ShowGlobalConfigurationForm()
        {
            GlobalConfigurationForm dialog = new GlobalConfigurationForm(StartupAction, Controller)
            {
                Width = 550,
                Height = 234
            };
            dialog.Top = ((Height - dialog.Height) / 2) + Top;
            dialog.Left = ((Width - dialog.Width) / 2) + Left;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StartupAction = dialog.StartupAction;
                Controller.ComPort = dialog.ComPort;
                Controller.RefreshInterval = dialog.RefreshInterval;
                if (Controller.IsConnected)
                {
                    Controller.GlobalConfiguration.LoadType = dialog.LoadType;
                    Controller.GlobalConfiguration.AspirationType = dialog.AspirationType;
                    Controller.GlobalConfiguration.EdisType = dialog.EdisType;
                    Controller.GlobalConfiguration.PipNoiseFilter = dialog.PipNoiseFilter;
                    Controller.GlobalConfiguration.CrankingAdvance = dialog.CrankingAdvance;
                    Controller.GlobalConfiguration.TriggerWheelOffset = dialog.TriggerWheelOffset;
                }
            }
            return dialog.DialogResult;
        }
        #endregion ShowGlobalConfigurationForm

        #region SaveIgnitionConfiguration()
        private void SaveIgnitionConfiguration()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "MegaJolt/E Files (*.mjlj)|*.mjlj|All files (*.*)|*.*";
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    FileName = dialog.FileName;
                }
            }
            Controller.IgnitionMap.Save(FileName);
            Text = "PC Jolt [" + Path.GetFileName(FileName) + "]";
        }
        #endregion SaveIgnitionConfiguration

        #region SetUserOutputActivationType(UserOutputType, Label, NumericUpDown)
        private void SetUserOutputActivationType(UserOutputType userOutputType, Label label, BetterNumericUpDown numericUpDown)
        {
            switch (userOutputType)
            {
                case UserOutputType.Rpm:
                    label.Text = "RPM";
                    numericUpDown.MaximumValue = 10000;
                    numericUpDown.Increment = 100;
                    break;
                case UserOutputType.Load:
                    label.Text = Controller.GlobalConfiguration.LoadType.LoadTypeLabel();
                    numericUpDown.MaximumValue = Controller.GlobalConfiguration.LoadType.LoadTypeMaximum(Controller.GlobalConfiguration.AspirationType);
                    numericUpDown.Increment = 1;
                    break;
                case UserOutputType.AuxiliaryInput:
                    label.Text = "Aux Input";
                    numericUpDown.MaximumValue = 255;
                    numericUpDown.Increment = 1;
                    break;
            }
        }
        #endregion SetUserOutputActivationType
        #endregion METHODS

        #region EVENT HANDLERS
        #region MainForm_FormClosing(object, FormClosingEventArgs)
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Controller.IsConnected &&  Controller.IgnitionMap.SavedState != SavedStates.Clean) ||
                (!Controller.IsConnected && Controller.IgnitionMap.SavedState != SavedStates.SavedToFile))
            {
                CloseOptionsForm dialog = new CloseOptionsForm(Controller);
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (dialog.SaveSelected) SaveIgnitionConfiguration();
                if (dialog.WriteSelected) Controller.UpdateIgnitionConfiguration();
                if (dialog.FlashSelected) Controller.FlashIgnitionConfiguration();
                Thread.Sleep(500); //TODO: Do this with Wait Signals instead.
            }

            Disconnect();

            Settings setttings = Settings.Default;

            //Window Settings
            setttings.Top = Top;
            setttings.Left = Left;
            setttings.Height = Height;
            setttings.Width = Width;
            setttings.WindowState = WindowState;
            setttings.SelectedTabIndex = MainTabControl.SelectedIndex;
            setttings.MainSplitterDistance = MainSplitContainer.SplitterDistance;
            setttings.OptionsSplitterDistance = OptionsSplitContainer.SplitterDistance;

            //Application Settings
            setttings.ComPort = Controller.ComPort;
            setttings.RefreshInterval = Controller.RefreshInterval;
            setttings.LoadType = Controller.GlobalConfiguration.LoadType;
            setttings.AspirationType = Controller.GlobalConfiguration.AspirationType;
            setttings.StartupAction = StartupAction;

            setttings.Save();
        }
        #endregion MainForm_FormClosing

        #region NewToolStripButton_Click(object, EventArgs)
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            FileName = string.Empty;
            Text = "PC Jolt";
            Controller.IgnitionMap.Reset();
        }
        #endregion NewToolStripButton_Click
        #region OpenToolStripButton_Click(object, EventArgs)
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "MegaJolt/E Files (*.mjlj)|*.mjlj|All files (*.*)|*.*";
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    OpenFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion OpenToolStripButton_Click
        #region SaveToolStripButton_Click(object, EventArgs)
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveIgnitionConfiguration();
        }
        #endregion SaveToolStripButton_Click

        #region SettingsToolStripButton_Click(object, EventArgs)
        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowGlobalConfigurationForm();
        }
        #endregion SettingsToolStripButton_Click
        #region ConnectToolStripButton_Click(object, EventArgs)
        private void ConnectToolStripButton_Click(object sender, EventArgs e)
        {
            Connect();
        }
        #endregion ConnectToolStripButton_Click
        #region DisconnectToolStripButton_Click(object, EventArgs)
        private void DisconnectToolStripButton_Click(object sender, EventArgs e)
        {
            Disconnect();
        }
        #endregion DisconnectToolStripButton_Click

        #region ReadConfigurationToolStripButton_Click(object, EventArgs)
        private void ReadConfigurationToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.GetIgnitionConfiguration();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion ReadConfigurationToolStripButton_Click
        #region WriteConfigurationToolStripButton_Click(object, EventArgs)
        private void WriteConfigurationToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.UpdateIgnitionConfiguration();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion WriteConfigurationToolStripButton_Click
        #region FlashConfigurationToolStripButton_Click(object, EventArgs)
        private void FlashConfigurationToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.FlashIgnitionConfiguration();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion FlashConfigurationToolStripButton_Click

        #region HelpToolStripButton_Click(object, EventArgs)
        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
        #endregion HelpToolStripButton_Click

        #region Output1TypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output1TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOutputType outputType = (UserOutputType)Output1TypeComboBox.SelectedIndex;
            Controller.IgnitionMap.UserOutput1.OutputType = outputType;
            SetUserOutputActivationType(outputType, Output1ActivationTypeLabel, Output1ActivationPointNumericUpDown);
        }
        #endregion Output1TypeComboBox_SelectedIndexChanged
        #region Output2TypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output2TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOutputType outputType = (UserOutputType)Output2TypeComboBox.SelectedIndex;
            Controller.IgnitionMap.UserOutput2.OutputType = outputType;
            SetUserOutputActivationType(outputType, Output2ActivationTypeLabel, Output2ActivationPointNumericUpDown);
        }
        #endregion Output2TypeComboBox_SelectedIndexChanged
        #region Output3TypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output3TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOutputType outputType = (UserOutputType)Output3TypeComboBox.SelectedIndex;
            Controller.IgnitionMap.UserOutput3.OutputType = outputType;
            SetUserOutputActivationType(outputType, Output3ActivationTypeLabel, Output3ActivationPointNumericUpDown);
        }
        #endregion Output3TypeComboBox_SelectedIndexChanged
        #region Output4TypeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output4TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOutputType outputType = (UserOutputType)Output4TypeComboBox.SelectedIndex;
            Controller.IgnitionMap.UserOutput4.OutputType = outputType;
            SetUserOutputActivationType(outputType, Output4ActivationTypeLabel, Output4ActivationPointNumericUpDown);
        }
        #endregion Output4TypeComboBox_SelectedIndexChanged

        #region Output1ModeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output1ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput1.OutputMode = (UserOutputMode)Output1ModeComboBox.SelectedIndex;
        }
        #endregion Output1ModeComboBox_SelectedIndexChanged
        #region Output2ModeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output2ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput2.OutputMode = (UserOutputMode)Output2ModeComboBox.SelectedIndex;
        }
        #endregion Output2ModeComboBox_SelectedIndexChanged
        #region Output3ModeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output3ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput3.OutputMode = (UserOutputMode)Output3ModeComboBox.SelectedIndex;
        }
        #endregion Output3ModeComboBox_SelectedIndexChanged
        #region Output4ModeComboBox_SelectedIndexChanged(object, EventArgs)
        private void Output4ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput4.OutputMode = (UserOutputMode)Output4ModeComboBox.SelectedIndex;
        }
        #endregion Output4ModeComboBox_SelectedIndexChanged

        #region Output1ActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void Output1ActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput1.Threshold = (ushort)Output1ActivationPointNumericUpDown.Value;
        }
        #endregion Output1ActivationPointNumericUpDown_ValueChanged
        #region Output2ActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void Output2ActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput2.Threshold = (ushort)Output2ActivationPointNumericUpDown.Value;
        }
        #endregion Output2ActivationPointNumericUpDown_ValueChanged
        #region Output3ActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void Output3ActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput3.Threshold = (ushort)Output3ActivationPointNumericUpDown.Value;
        }
        #endregion Output3ActivationPointNumericUpDown_ValueChanged
        #region Output4ActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void Output4ActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.UserOutput4.Threshold = (ushort)Output4ActivationPointNumericUpDown.Value;
        }
        #endregion Output4ActivationPointNumericUpDown_ValueChanged
        #region ShiftLightActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void ShiftLightActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.ShiftLightThreshold = (ushort)ShiftLightActivationPointNumericUpDown.Value;
        }
        #endregion ShiftLightActivationPointNumericUpDown_ValueChanged
        #region RevLimitActivationPointNumericUpDown_ValueChanged(object, EventArgs)
        private void RevLimitActivationPointNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.RevLimitThreshold = (ushort)RevLimitActivationPointNumericUpDown.Value;
        }
        #endregion RevLimitActivationPointNumericUpDown_ValueChanged

        #region Controller_StateChanged(object, EventArgs)
        void Controller_StateChanged(object sender, EventArgs e)
        {
            SafeInvoke(() =>
            {
                if (Controller.IsConnected)
                {
                    ConnectToolStripButton.Visible = false;
                    DisconnectToolStripButton.Visible = true;
                    ReadConfigurationToolStripButton.Enabled = true;
                }
                else
                {
                    DisconnectToolStripButton.Visible = false;
                    ConnectToolStripButton.Visible = true;
                    ReadConfigurationToolStripButton.Enabled = false;
                    WriteConfigurationToolStripButton.Enabled = false;
                    FlashConfigurationToolStripButton.Enabled = false;
                }
            });
        }
        #endregion Controller_StateChanged
        #region Controller_CurrentIgnitionCellChanged(object, EventArgs<IgnitionCellIndex>)
        void Controller_CurrentIgnitionCellChanged(object sender, EventArgs<IgnitionCellIndex> e)
        {
            SafeInvoke(() =>
            {
                if (CurrentCell != null)
                {
                    CurrentCell.View.BackColor = GetAdvanceColor(Convert.ToByte(CurrentCell.Value));
                    CurrentCell.View.ForeColor = Color.Black;
                }

                if ((e.Value.LoadBin == byte.MaxValue) || (e.Value.RpmBin == byte.MaxValue)) return;
                ICell cell = IgnitionMapDataGridView[e.Value.LoadBin + 1, e.Value.RpmBin + 1];
                cell.View.BackColor = SystemColors.Highlight;
                cell.View.ForeColor = SystemColors.HighlightText;
                CurrentCell = cell;

                IgnitionMapDataGridView.Refresh();
            });
        }
        #endregion Controller_CurrentIgnitionCellChanged

        #region IgnitionMap_SavedStateChanged(object, EventArgs<SavedStates>)
        void IgnitionMap_SavedStateChanged(object sender, EventArgs<SavedStates> e)
        {
            SafeInvoke(() =>
            {
                SaveToolStripButton.Enabled = !e.Value.HasFlag(SavedStates.SavedToFile);
                WriteConfigurationToolStripButton.Enabled = Controller.IsConnected && !e.Value.HasFlag(SavedStates.SavedToController);
                FlashConfigurationToolStripButton.Enabled = Controller.IsConnected && e.Value.HasFlag(SavedStates.SavedToController) && !e.Value.HasFlag(SavedStates.WrittenToFlash);
            });
        }
        #endregion IgnitionMap_SavedStateChanged
        #region IgnitionMap_RpmBinUpdated(object, EventArgs<int>)
        void IgnitionMap_RpmBinUpdated(object sender, EventArgs<int> e)
        {
            SafeInvoke(() =>
            {
                Bin<ushort> bin = Controller.IgnitionMap.RpmBins[e.Value];
                IgnitionMapDataGridView[0, e.Value + 1].Value = bin.HighValue;
            });
        }
        #endregion IgnitionMap_RpmBinUpdated
        #region IgnitionMap_LoadBinUpdated(object, EventArgs<int>)
        void IgnitionMap_LoadBinUpdated(object sender, EventArgs<int> e)
        {
            SafeInvoke(() =>
            {
                Bin<byte> bin = Controller.IgnitionMap.LoadBins[e.Value];
                IgnitionMapDataGridView[e.Value + 1, 0].Value = bin.HighValue;
            });

        }
        #endregion IgnitionMap_LoadBinUpdated
        #region IgnitionMap_IgnitionCellUpdated(object, EventArgs<IgnitionCellIndex>)
        void IgnitionMap_IgnitionCellUpdated(object sender, EventArgs<IgnitionCellIndex> e)
        {
            SafeInvoke(() =>
            {
                ICell cell = IgnitionMapDataGridView[e.Value.LoadBin + 1, e.Value.RpmBin + 1];
                cell.Value = Controller.IgnitionMap[e.Value];
            });
        }
        #endregion IgnitionMap_IgnitionCellUpdated
        #region RevLimit_ThresholdChanged(object, EventArgs<ushort>)
        void RevLimit_ThresholdChanged(object sender, EventArgs<ushort> e)
        {
            SafeInvoke(() =>
            {
                RpmGauge.ThresholdPercent = Controller.IgnitionMap.RevLimitThreshold/100;
                RevLimitActivationPointNumericUpDown.Value = Controller.IgnitionMap.RevLimitThreshold;
            });
        }
        #endregion RevLimit_ThresholdChanged

        #region GlobalConfiguration_LoadTypeMaximumChanged(object, EventArgs<byte>)
        void GlobalConfiguration_LoadTypeMaximumChanged(object sender, EventArgs<byte> e)
        {
            SafeInvoke(() =>
            {
                if (e.Value == 102)
                {
                    LoadGauge.MaxValue = 110;
                    LoadGauge.NoOfDivisions = 11;
                }
                else if (e.Value == 255)
                {
                    LoadGauge.MaxValue = 260;
                    LoadGauge.NoOfDivisions = 13;
                }
                else
                {
                    LoadGauge.MaxValue = e.Value;
                    LoadGauge.NoOfDivisions = 10;
                }
            });
        }
        #endregion GlobalConfiguration_LoadTypeMaximumChanged

        #region AdvanceCorrection_ValueChanged(object, EventArgs<int>)
        private void AdvanceCorrection_ValueChanged(object sender, EventArgs<int> e)
        {
            SafeInvoke(() =>
            {
                ICell cell = AdvanceCorrectionDataGridView[1, e.Value + 1];
                cell.Value = Controller.IgnitionMap.AdvanceCorrection.Values[e.Value];
            });
        }
        #endregion AdvanceCorrection_ValueChanged
        #region AdvanceCorrection_BinChanged(object, EventArgs<int>)
        private void AdvanceCorrection_BinChanged(object sender, EventArgs<int> e)
        {
            SafeInvoke(() =>
            {
                ICell cell = AdvanceCorrectionDataGridView[2, e.Value + 1];
                cell.Value = Controller.IgnitionMap.AdvanceCorrection.Values[e.Value];
            });
        }
        #endregion AdvanceCorrection_BinChanged

        #region PeakHoldNumericUpDown_ValueChanged(object, EventArgs)
        private void PeakHoldNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Controller.IgnitionMap.AdvanceCorrection.PeakHoldCount = (ushort)PeakHoldNumericUpDown.Value;
        }
        #endregion PeakHoldNumericUpDown_ValueChanged
        #endregion EVENT HANDLERS

        #region AdvanceCell Class
        private class AdvanceCell : Cell
        {
            public AdvanceCell(Font baseFont, bool allowNegative, Action<byte, byte, sbyte> endEditCallback) : base(0, new SourceGrid.Cells.Editors.NumericUpDown(typeof(int), 59, allowNegative ? -59 : 0, 1))
            {
                base.View = new SourceGrid.Cells.Views.Cell
                {
                    BackColor = GetAdvanceColor(0),
                    ForeColor = Color.Black,
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter,
                    Font = new Font(baseFont, FontStyle.Bold),
                };
                AddController(new AdvanceController(endEditCallback));
            }
        }
        #endregion AdvanceCell
        #region AdvanceController Class
        class AdvanceController : SourceGrid.Cells.Controllers.ControllerBase
        {
            public AdvanceController(Action<byte, byte, sbyte> endEditCallback)
            {
                EndEditCallback = endEditCallback;
            }

            private Action<byte, byte, sbyte> EndEditCallback { get; set; }

            public override void OnValueChanged(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnValueChanged(sender, e);

                sender.Cell.View.BackColor = GetAdvanceColor((byte)Math.Abs(Convert.ToSByte(sender.Value)));
                sender.Cell.View.ForeColor = Color.Black;
                EndEditCallback.Invoke((byte)(sender.Position.Row - 1), (byte)(sender.Position.Column - 1), Convert.ToSByte(sender.Value));
            }
        }
        #endregion AdvanceController

        #region RpmCell Class
        private class RpmCell : Cell
        {
            public RpmCell(Font baseFont, Action<byte, ushort> endEditCallback) : base(0, new SourceGrid.Cells.Editors.NumericUpDown(typeof(int), 10000, 100, 100))
            {
                base.View = new SourceGrid.Cells.Views.Cell
                {
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter,
                    Font = new Font(baseFont, FontStyle.Bold),
                };
                AddController(new RpmController(endEditCallback));
            }
        }
        #endregion RpmCell
        #region RpmController Class
        class RpmController : SourceGrid.Cells.Controllers.ControllerBase
        {
            public RpmController(Action<byte, ushort> endEditCallback)
            {
                EndEditCallback = endEditCallback;
            }

            private Action<byte, ushort> EndEditCallback { get; set; }

            public override void OnValueChanged(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnValueChanged(sender, e);

                EndEditCallback.Invoke((byte)(sender.Position.Column - 1), Convert.ToUInt16(sender.Value));
            }
        }
        #endregion RpmController

        #region LoadCell Class
        private class LoadCell : Cell
        {
            public LoadCell(Font baseFont, byte maximum, Action<byte, byte> endEditCallback) : base(0, new SourceGrid.Cells.Editors.NumericUpDown(typeof(int), maximum, 1, 1))
            {
                base.View = new SourceGrid.Cells.Views.Cell
                {
                    TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter,
                    Font = new Font(baseFont, FontStyle.Bold),
                };
                AddController(new LoadController(endEditCallback));
            }
        }
        #endregion LoadCell
        #region LoadController Class
        class LoadController : SourceGrid.Cells.Controllers.ControllerBase
        {
            public LoadController(Action<byte, byte> endEditCallback)
            {
                EndEditCallback = endEditCallback;
            }

            private Action<byte, byte> EndEditCallback { get; set; }

            public override void OnValueChanged(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnValueChanged(sender, e);

                EndEditCallback.Invoke((byte)(sender.Position.Row - 1), Convert.ToByte(sender.Value));
            }
        }
        #endregion LoadController
    }
}
