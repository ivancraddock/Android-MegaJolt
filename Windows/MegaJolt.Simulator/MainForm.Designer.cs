namespace MegaJolt.Simulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LogMessagesTextBox = new System.Windows.Forms.TextBox();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ServerSettingsTabPage = new System.Windows.Forms.TabPage();
            this.LogEventsCheckBox = new System.Windows.Forms.CheckBox();
            this.LogPropertyChangesCheckBox = new System.Windows.Forms.CheckBox();
            this.LogMessagesCheckBox = new System.Windows.Forms.CheckBox();
            this.StartServerButton = new System.Windows.Forms.Button();
            this.StopServerButton = new System.Windows.Forms.Button();
            this.GlobalSettingsTabPage = new System.Windows.Forms.TabPage();
            this.AspirationTypeLabel = new System.Windows.Forms.Label();
            this.AspirationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.LoadTypeLabel = new System.Windows.Forms.Label();
            this.LoadTypeComboBox = new System.Windows.Forms.ComboBox();
            this.TriggerWheelOffsetNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.TriggerWheelOffsetLabel = new System.Windows.Forms.Label();
            this.CrankingAdvanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CrankingAdvanceLabel = new System.Windows.Forms.Label();
            this.PipNoiseFilterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PipNoiseFilterLabel = new System.Windows.Forms.Label();
            this.EdisTypeLabel = new System.Windows.Forms.Label();
            this.EdisTypeComboBox = new System.Windows.Forms.ComboBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.VersionComboBox = new System.Windows.Forms.ComboBox();
            this.CurrentStateSettingsTabPage = new System.Windows.Forms.TabPage();
            this.DynamicStateCheckBox = new System.Windows.Forms.CheckBox();
            this.UserOutput4CheckBox = new System.Windows.Forms.CheckBox();
            this.UserOutput3CheckBox = new System.Windows.Forms.CheckBox();
            this.UserOutput2CheckBox = new System.Windows.Forms.CheckBox();
            this.UserOutput1CheckBox = new System.Windows.Forms.CheckBox();
            this.AuxiliaryInputNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AuxiliaryInputLabel = new System.Windows.Forms.Label();
            this.IgnitionConfigurationLabel = new System.Windows.Forms.Label();
            this.IgnitionConfigurationComboBox = new System.Windows.Forms.ComboBox();
            this.LoadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LoadLabel = new System.Windows.Forms.Label();
            this.RpmNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RpmLabel = new System.Windows.Forms.Label();
            this.MainTabControl.SuspendLayout();
            this.ServerSettingsTabPage.SuspendLayout();
            this.GlobalSettingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TriggerWheelOffsetNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CrankingAdvanceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipNoiseFilterNumericUpDown)).BeginInit();
            this.CurrentStateSettingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AuxiliaryInputNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpmNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // LogMessagesTextBox
            // 
            this.LogMessagesTextBox.BackColor = System.Drawing.Color.Black;
            this.LogMessagesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogMessagesTextBox.ForeColor = System.Drawing.Color.White;
            this.LogMessagesTextBox.Location = new System.Drawing.Point(0, 125);
            this.LogMessagesTextBox.Multiline = true;
            this.LogMessagesTextBox.Name = "LogMessagesTextBox";
            this.LogMessagesTextBox.Size = new System.Drawing.Size(757, 572);
            this.LogMessagesTextBox.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.ServerSettingsTabPage);
            this.MainTabControl.Controls.Add(this.GlobalSettingsTabPage);
            this.MainTabControl.Controls.Add(this.CurrentStateSettingsTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(757, 125);
            this.MainTabControl.TabIndex = 11;
            // 
            // ServerSettingsTabPage
            // 
            this.ServerSettingsTabPage.Controls.Add(this.LogEventsCheckBox);
            this.ServerSettingsTabPage.Controls.Add(this.LogPropertyChangesCheckBox);
            this.ServerSettingsTabPage.Controls.Add(this.LogMessagesCheckBox);
            this.ServerSettingsTabPage.Controls.Add(this.StartServerButton);
            this.ServerSettingsTabPage.Controls.Add(this.StopServerButton);
            this.ServerSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ServerSettingsTabPage.Name = "ServerSettingsTabPage";
            this.ServerSettingsTabPage.Size = new System.Drawing.Size(749, 99);
            this.ServerSettingsTabPage.TabIndex = 2;
            this.ServerSettingsTabPage.Text = "Server Settings";
            this.ServerSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // LogEventsCheckBox
            // 
            this.LogEventsCheckBox.AutoSize = true;
            this.LogEventsCheckBox.Location = new System.Drawing.Point(241, 57);
            this.LogEventsCheckBox.Name = "LogEventsCheckBox";
            this.LogEventsCheckBox.Size = new System.Drawing.Size(80, 17);
            this.LogEventsCheckBox.TabIndex = 6;
            this.LogEventsCheckBox.Text = "Log Events";
            this.LogEventsCheckBox.UseVisualStyleBackColor = true;
            this.LogEventsCheckBox.CheckedChanged += new System.EventHandler(this.LogEventsCheckBox_CheckedChanged);
            // 
            // LogPropertyChangesCheckBox
            // 
            this.LogPropertyChangesCheckBox.AutoSize = true;
            this.LogPropertyChangesCheckBox.Location = new System.Drawing.Point(241, 34);
            this.LogPropertyChangesCheckBox.Name = "LogPropertyChangesCheckBox";
            this.LogPropertyChangesCheckBox.Size = new System.Drawing.Size(131, 17);
            this.LogPropertyChangesCheckBox.TabIndex = 5;
            this.LogPropertyChangesCheckBox.Text = "Log Property Changes";
            this.LogPropertyChangesCheckBox.UseVisualStyleBackColor = true;
            this.LogPropertyChangesCheckBox.CheckedChanged += new System.EventHandler(this.LogPropertyChangesCheckBox_CheckedChanged);
            // 
            // LogMessagesCheckBox
            // 
            this.LogMessagesCheckBox.AutoSize = true;
            this.LogMessagesCheckBox.Location = new System.Drawing.Point(241, 9);
            this.LogMessagesCheckBox.Name = "LogMessagesCheckBox";
            this.LogMessagesCheckBox.Size = new System.Drawing.Size(95, 17);
            this.LogMessagesCheckBox.TabIndex = 4;
            this.LogMessagesCheckBox.Text = "Log Messagse";
            this.LogMessagesCheckBox.UseVisualStyleBackColor = true;
            this.LogMessagesCheckBox.CheckedChanged += new System.EventHandler(this.LogMessagesCheckBox_CheckedChanged);
            // 
            // StartServerButton
            // 
            this.StartServerButton.Location = new System.Drawing.Point(11, 9);
            this.StartServerButton.Name = "StartServerButton";
            this.StartServerButton.Size = new System.Drawing.Size(88, 65);
            this.StartServerButton.TabIndex = 3;
            this.StartServerButton.Text = "Start";
            this.StartServerButton.UseVisualStyleBackColor = true;
            this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // StopServerButton
            // 
            this.StopServerButton.Enabled = false;
            this.StopServerButton.Location = new System.Drawing.Point(108, 9);
            this.StopServerButton.Name = "StopServerButton";
            this.StopServerButton.Size = new System.Drawing.Size(88, 65);
            this.StopServerButton.TabIndex = 2;
            this.StopServerButton.Text = "Stop";
            this.StopServerButton.UseVisualStyleBackColor = true;
            this.StopServerButton.Click += new System.EventHandler(this.StopServerButton_Click);
            // 
            // GlobalSettingsTabPage
            // 
            this.GlobalSettingsTabPage.Controls.Add(this.AspirationTypeLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.AspirationTypeComboBox);
            this.GlobalSettingsTabPage.Controls.Add(this.LoadTypeLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.LoadTypeComboBox);
            this.GlobalSettingsTabPage.Controls.Add(this.TriggerWheelOffsetNumericUpDown);
            this.GlobalSettingsTabPage.Controls.Add(this.TriggerWheelOffsetLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.CrankingAdvanceNumericUpDown);
            this.GlobalSettingsTabPage.Controls.Add(this.CrankingAdvanceLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.PipNoiseFilterNumericUpDown);
            this.GlobalSettingsTabPage.Controls.Add(this.PipNoiseFilterLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.EdisTypeLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.EdisTypeComboBox);
            this.GlobalSettingsTabPage.Controls.Add(this.VersionLabel);
            this.GlobalSettingsTabPage.Controls.Add(this.VersionComboBox);
            this.GlobalSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.GlobalSettingsTabPage.Name = "GlobalSettingsTabPage";
            this.GlobalSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GlobalSettingsTabPage.Size = new System.Drawing.Size(749, 99);
            this.GlobalSettingsTabPage.TabIndex = 0;
            this.GlobalSettingsTabPage.Text = "Global Settings";
            this.GlobalSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // AspirationTypeLabel
            // 
            this.AspirationTypeLabel.AutoSize = true;
            this.AspirationTypeLabel.Location = new System.Drawing.Point(489, 11);
            this.AspirationTypeLabel.Name = "AspirationTypeLabel";
            this.AspirationTypeLabel.Size = new System.Drawing.Size(80, 13);
            this.AspirationTypeLabel.TabIndex = 24;
            this.AspirationTypeLabel.Text = "Aspiration Type";
            // 
            // AspirationTypeComboBox
            // 
            this.AspirationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AspirationTypeComboBox.FormattingEnabled = true;
            this.AspirationTypeComboBox.Items.AddRange(new object[] {
            "Normal",
            "Boosted"});
            this.AspirationTypeComboBox.Location = new System.Drawing.Point(579, 8);
            this.AspirationTypeComboBox.Name = "AspirationTypeComboBox";
            this.AspirationTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.AspirationTypeComboBox.TabIndex = 23;
            this.AspirationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.AspirationTypeComboBox_SelectedIndexChanged);
            // 
            // LoadTypeLabel
            // 
            this.LoadTypeLabel.AutoSize = true;
            this.LoadTypeLabel.Location = new System.Drawing.Point(8, 64);
            this.LoadTypeLabel.Name = "LoadTypeLabel";
            this.LoadTypeLabel.Size = new System.Drawing.Size(58, 13);
            this.LoadTypeLabel.TabIndex = 22;
            this.LoadTypeLabel.Text = "Load Type";
            // 
            // LoadTypeComboBox
            // 
            this.LoadTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoadTypeComboBox.FormattingEnabled = true;
            this.LoadTypeComboBox.Items.AddRange(new object[] {
            "MAP",
            "TPS"});
            this.LoadTypeComboBox.Location = new System.Drawing.Point(76, 61);
            this.LoadTypeComboBox.Name = "LoadTypeComboBox";
            this.LoadTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.LoadTypeComboBox.TabIndex = 21;
            this.LoadTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.LoadTypeComboBox_SelectedIndexChanged);
            // 
            // TriggerWheelOffsetNumericUpDown
            // 
            this.TriggerWheelOffsetNumericUpDown.Location = new System.Drawing.Point(329, 36);
            this.TriggerWheelOffsetNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.TriggerWheelOffsetNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.TriggerWheelOffsetNumericUpDown.Name = "TriggerWheelOffsetNumericUpDown";
            this.TriggerWheelOffsetNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.TriggerWheelOffsetNumericUpDown.TabIndex = 20;
            this.TriggerWheelOffsetNumericUpDown.ValueChanged += new System.EventHandler(this.TriggerWheelOffsetNumericUpDown_ValueChanged);
            // 
            // TriggerWheelOffsetLabel
            // 
            this.TriggerWheelOffsetLabel.AutoSize = true;
            this.TriggerWheelOffsetLabel.Location = new System.Drawing.Point(218, 38);
            this.TriggerWheelOffsetLabel.Name = "TriggerWheelOffsetLabel";
            this.TriggerWheelOffsetLabel.Size = new System.Drawing.Size(105, 13);
            this.TriggerWheelOffsetLabel.TabIndex = 19;
            this.TriggerWheelOffsetLabel.Text = "Trigger Wheel Offset";
            // 
            // CrankingAdvanceNumericUpDown
            // 
            this.CrankingAdvanceNumericUpDown.Location = new System.Drawing.Point(329, 9);
            this.CrankingAdvanceNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.CrankingAdvanceNumericUpDown.Name = "CrankingAdvanceNumericUpDown";
            this.CrankingAdvanceNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.CrankingAdvanceNumericUpDown.TabIndex = 18;
            this.CrankingAdvanceNumericUpDown.ValueChanged += new System.EventHandler(this.CrankingAdvanceNumericUpDown_ValueChanged);
            // 
            // CrankingAdvanceLabel
            // 
            this.CrankingAdvanceLabel.AutoSize = true;
            this.CrankingAdvanceLabel.Location = new System.Drawing.Point(218, 11);
            this.CrankingAdvanceLabel.Name = "CrankingAdvanceLabel";
            this.CrankingAdvanceLabel.Size = new System.Drawing.Size(95, 13);
            this.CrankingAdvanceLabel.TabIndex = 17;
            this.CrankingAdvanceLabel.Text = "Cranking Advance";
            // 
            // PipNoiseFilterNumericUpDown
            // 
            this.PipNoiseFilterNumericUpDown.Location = new System.Drawing.Point(329, 62);
            this.PipNoiseFilterNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.PipNoiseFilterNumericUpDown.Name = "PipNoiseFilterNumericUpDown";
            this.PipNoiseFilterNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.PipNoiseFilterNumericUpDown.TabIndex = 16;
            this.PipNoiseFilterNumericUpDown.ValueChanged += new System.EventHandler(this.PipNoiseFilterNumericUpDown_ValueChanged);
            // 
            // PipNoiseFilterLabel
            // 
            this.PipNoiseFilterLabel.AutoSize = true;
            this.PipNoiseFilterLabel.Location = new System.Drawing.Point(218, 64);
            this.PipNoiseFilterLabel.Name = "PipNoiseFilterLabel";
            this.PipNoiseFilterLabel.Size = new System.Drawing.Size(49, 13);
            this.PipNoiseFilterLabel.TabIndex = 15;
            this.PipNoiseFilterLabel.Text = "PIP Filter";
            // 
            // EdisTypeLabel
            // 
            this.EdisTypeLabel.AutoSize = true;
            this.EdisTypeLabel.Location = new System.Drawing.Point(8, 38);
            this.EdisTypeLabel.Name = "EdisTypeLabel";
            this.EdisTypeLabel.Size = new System.Drawing.Size(59, 13);
            this.EdisTypeLabel.TabIndex = 14;
            this.EdisTypeLabel.Text = "EDIS Type";
            // 
            // EdisTypeComboBox
            // 
            this.EdisTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EdisTypeComboBox.FormattingEnabled = true;
            this.EdisTypeComboBox.Items.AddRange(new object[] {
            "EDIS-4",
            "EDIS-6",
            "EDIS-8"});
            this.EdisTypeComboBox.Location = new System.Drawing.Point(76, 35);
            this.EdisTypeComboBox.Name = "EdisTypeComboBox";
            this.EdisTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.EdisTypeComboBox.TabIndex = 13;
            this.EdisTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.EdisTypeComboBox_SelectedIndexChanged);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(8, 11);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(42, 13);
            this.VersionLabel.TabIndex = 12;
            this.VersionLabel.Text = "Version";
            // 
            // VersionComboBox
            // 
            this.VersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionComboBox.FormattingEnabled = true;
            this.VersionComboBox.Items.AddRange(new object[] {
            "4.0.0",
            "4.0.1",
            "4.0.2"});
            this.VersionComboBox.Location = new System.Drawing.Point(76, 8);
            this.VersionComboBox.Name = "VersionComboBox";
            this.VersionComboBox.Size = new System.Drawing.Size(121, 21);
            this.VersionComboBox.TabIndex = 11;
            this.VersionComboBox.SelectedIndexChanged += new System.EventHandler(this.VersionComboBox_SelectedIndexChanged);
            // 
            // CurrentStateSettingsTabPage
            // 
            this.CurrentStateSettingsTabPage.Controls.Add(this.DynamicStateCheckBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.UserOutput4CheckBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.UserOutput3CheckBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.UserOutput2CheckBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.UserOutput1CheckBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.AuxiliaryInputNumericUpDown);
            this.CurrentStateSettingsTabPage.Controls.Add(this.AuxiliaryInputLabel);
            this.CurrentStateSettingsTabPage.Controls.Add(this.IgnitionConfigurationLabel);
            this.CurrentStateSettingsTabPage.Controls.Add(this.IgnitionConfigurationComboBox);
            this.CurrentStateSettingsTabPage.Controls.Add(this.LoadNumericUpDown);
            this.CurrentStateSettingsTabPage.Controls.Add(this.LoadLabel);
            this.CurrentStateSettingsTabPage.Controls.Add(this.RpmNumericUpDown);
            this.CurrentStateSettingsTabPage.Controls.Add(this.RpmLabel);
            this.CurrentStateSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.CurrentStateSettingsTabPage.Name = "CurrentStateSettingsTabPage";
            this.CurrentStateSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.CurrentStateSettingsTabPage.Size = new System.Drawing.Size(749, 99);
            this.CurrentStateSettingsTabPage.TabIndex = 1;
            this.CurrentStateSettingsTabPage.Text = "Current State Settings";
            this.CurrentStateSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // DynamicStateCheckBox
            // 
            this.DynamicStateCheckBox.AutoSize = true;
            this.DynamicStateCheckBox.Checked = true;
            this.DynamicStateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DynamicStateCheckBox.Location = new System.Drawing.Point(335, 65);
            this.DynamicStateCheckBox.Name = "DynamicStateCheckBox";
            this.DynamicStateCheckBox.Size = new System.Drawing.Size(93, 17);
            this.DynamicStateCheckBox.TabIndex = 41;
            this.DynamicStateCheckBox.Text = "Dynamic state";
            this.DynamicStateCheckBox.UseVisualStyleBackColor = true;
            // 
            // UserOutput4CheckBox
            // 
            this.UserOutput4CheckBox.AutoSize = true;
            this.UserOutput4CheckBox.Location = new System.Drawing.Point(591, 37);
            this.UserOutput4CheckBox.Name = "UserOutput4CheckBox";
            this.UserOutput4CheckBox.Size = new System.Drawing.Size(92, 17);
            this.UserOutput4CheckBox.TabIndex = 40;
            this.UserOutput4CheckBox.Text = "User Output 4";
            this.UserOutput4CheckBox.UseVisualStyleBackColor = true;
            this.UserOutput4CheckBox.CheckedChanged += new System.EventHandler(this.UserOutput4CheckBox_CheckedChanged);
            // 
            // UserOutput3CheckBox
            // 
            this.UserOutput3CheckBox.AutoSize = true;
            this.UserOutput3CheckBox.Location = new System.Drawing.Point(494, 37);
            this.UserOutput3CheckBox.Name = "UserOutput3CheckBox";
            this.UserOutput3CheckBox.Size = new System.Drawing.Size(92, 17);
            this.UserOutput3CheckBox.TabIndex = 39;
            this.UserOutput3CheckBox.Text = "User Output 3";
            this.UserOutput3CheckBox.UseVisualStyleBackColor = true;
            this.UserOutput3CheckBox.CheckedChanged += new System.EventHandler(this.UserOutput3CheckBox_CheckedChanged);
            // 
            // UserOutput2CheckBox
            // 
            this.UserOutput2CheckBox.AutoSize = true;
            this.UserOutput2CheckBox.Location = new System.Drawing.Point(591, 11);
            this.UserOutput2CheckBox.Name = "UserOutput2CheckBox";
            this.UserOutput2CheckBox.Size = new System.Drawing.Size(92, 17);
            this.UserOutput2CheckBox.TabIndex = 38;
            this.UserOutput2CheckBox.Text = "User Output 2";
            this.UserOutput2CheckBox.UseVisualStyleBackColor = true;
            this.UserOutput2CheckBox.CheckedChanged += new System.EventHandler(this.UserOutput2CheckBox_CheckedChanged);
            // 
            // UserOutput1CheckBox
            // 
            this.UserOutput1CheckBox.AutoSize = true;
            this.UserOutput1CheckBox.Location = new System.Drawing.Point(494, 12);
            this.UserOutput1CheckBox.Name = "UserOutput1CheckBox";
            this.UserOutput1CheckBox.Size = new System.Drawing.Size(92, 17);
            this.UserOutput1CheckBox.TabIndex = 37;
            this.UserOutput1CheckBox.Text = "User Output 1";
            this.UserOutput1CheckBox.UseVisualStyleBackColor = true;
            this.UserOutput1CheckBox.CheckedChanged += new System.EventHandler(this.UserOutput1CheckBox_CheckedChanged);
            // 
            // AuxiliaryInputNumericUpDown
            // 
            this.AuxiliaryInputNumericUpDown.Location = new System.Drawing.Point(122, 39);
            this.AuxiliaryInputNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AuxiliaryInputNumericUpDown.Name = "AuxiliaryInputNumericUpDown";
            this.AuxiliaryInputNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.AuxiliaryInputNumericUpDown.TabIndex = 36;
            this.AuxiliaryInputNumericUpDown.ValueChanged += new System.EventHandler(this.AuxiliaryInputNumericUpDown_ValueChanged);
            // 
            // AuxiliaryInputLabel
            // 
            this.AuxiliaryInputLabel.AutoSize = true;
            this.AuxiliaryInputLabel.Location = new System.Drawing.Point(38, 41);
            this.AuxiliaryInputLabel.Name = "AuxiliaryInputLabel";
            this.AuxiliaryInputLabel.Size = new System.Drawing.Size(75, 13);
            this.AuxiliaryInputLabel.TabIndex = 35;
            this.AuxiliaryInputLabel.Text = " Auxiliary Input";
            // 
            // IgnitionConfigurationLabel
            // 
            this.IgnitionConfigurationLabel.AutoSize = true;
            this.IgnitionConfigurationLabel.Location = new System.Drawing.Point(8, 12);
            this.IgnitionConfigurationLabel.Name = "IgnitionConfigurationLabel";
            this.IgnitionConfigurationLabel.Size = new System.Drawing.Size(109, 13);
            this.IgnitionConfigurationLabel.TabIndex = 30;
            this.IgnitionConfigurationLabel.Text = " Ignition Configuration";
            // 
            // IgnitionConfigurationComboBox
            // 
            this.IgnitionConfigurationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IgnitionConfigurationComboBox.FormattingEnabled = true;
            this.IgnitionConfigurationComboBox.Items.AddRange(new object[] {
            "Configuration 1",
            "Configuration 2"});
            this.IgnitionConfigurationComboBox.Location = new System.Drawing.Point(121, 9);
            this.IgnitionConfigurationComboBox.Name = "IgnitionConfigurationComboBox";
            this.IgnitionConfigurationComboBox.Size = new System.Drawing.Size(121, 21);
            this.IgnitionConfigurationComboBox.TabIndex = 29;
            this.IgnitionConfigurationComboBox.SelectedIndexChanged += new System.EventHandler(this.IgnitionConfigurationComboBox_SelectedIndexChanged);
            // 
            // LoadNumericUpDown
            // 
            this.LoadNumericUpDown.Location = new System.Drawing.Point(335, 13);
            this.LoadNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.LoadNumericUpDown.Name = "LoadNumericUpDown";
            this.LoadNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.LoadNumericUpDown.TabIndex = 24;
            this.LoadNumericUpDown.ValueChanged += new System.EventHandler(this.LoadNumericUpDown_ValueChanged);
            // 
            // LoadLabel
            // 
            this.LoadLabel.AutoSize = true;
            this.LoadLabel.Location = new System.Drawing.Point(278, 15);
            this.LoadLabel.Name = "LoadLabel";
            this.LoadLabel.Size = new System.Drawing.Size(34, 13);
            this.LoadLabel.TabIndex = 23;
            this.LoadLabel.Text = " Load";
            // 
            // RpmNumericUpDown
            // 
            this.RpmNumericUpDown.Location = new System.Drawing.Point(335, 39);
            this.RpmNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.RpmNumericUpDown.Name = "RpmNumericUpDown";
            this.RpmNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.RpmNumericUpDown.TabIndex = 22;
            this.RpmNumericUpDown.ValueChanged += new System.EventHandler(this.RpmNumericUpDown_ValueChanged);
            // 
            // RpmLabel
            // 
            this.RpmLabel.AutoSize = true;
            this.RpmLabel.Location = new System.Drawing.Point(278, 41);
            this.RpmLabel.Name = "RpmLabel";
            this.RpmLabel.Size = new System.Drawing.Size(34, 13);
            this.RpmLabel.TabIndex = 21;
            this.RpmLabel.Text = " RPM";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 697);
            this.Controls.Add(this.LogMessagesTextBox);
            this.Controls.Add(this.MainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MegaJolt Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainTabControl.ResumeLayout(false);
            this.ServerSettingsTabPage.ResumeLayout(false);
            this.ServerSettingsTabPage.PerformLayout();
            this.GlobalSettingsTabPage.ResumeLayout(false);
            this.GlobalSettingsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TriggerWheelOffsetNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CrankingAdvanceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipNoiseFilterNumericUpDown)).EndInit();
            this.CurrentStateSettingsTabPage.ResumeLayout(false);
            this.CurrentStateSettingsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AuxiliaryInputNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RpmNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogMessagesTextBox;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ServerSettingsTabPage;
        private System.Windows.Forms.TabPage GlobalSettingsTabPage;
        private System.Windows.Forms.NumericUpDown TriggerWheelOffsetNumericUpDown;
        private System.Windows.Forms.Label TriggerWheelOffsetLabel;
        private System.Windows.Forms.NumericUpDown CrankingAdvanceNumericUpDown;
        private System.Windows.Forms.Label CrankingAdvanceLabel;
        private System.Windows.Forms.NumericUpDown PipNoiseFilterNumericUpDown;
        private System.Windows.Forms.Label PipNoiseFilterLabel;
        private System.Windows.Forms.Label EdisTypeLabel;
        private System.Windows.Forms.ComboBox EdisTypeComboBox;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.ComboBox VersionComboBox;
        private System.Windows.Forms.TabPage CurrentStateSettingsTabPage;
        private System.Windows.Forms.Button StartServerButton;
        private System.Windows.Forms.Button StopServerButton;
        private System.Windows.Forms.NumericUpDown RpmNumericUpDown;
        private System.Windows.Forms.Label RpmLabel;
        private System.Windows.Forms.NumericUpDown LoadNumericUpDown;
        private System.Windows.Forms.Label LoadLabel;
        private System.Windows.Forms.Label IgnitionConfigurationLabel;
        private System.Windows.Forms.ComboBox IgnitionConfigurationComboBox;
        private System.Windows.Forms.Label LoadTypeLabel;
        private System.Windows.Forms.ComboBox LoadTypeComboBox;
        private System.Windows.Forms.NumericUpDown AuxiliaryInputNumericUpDown;
        private System.Windows.Forms.Label AuxiliaryInputLabel;
        private System.Windows.Forms.CheckBox UserOutput4CheckBox;
        private System.Windows.Forms.CheckBox UserOutput3CheckBox;
        private System.Windows.Forms.CheckBox UserOutput2CheckBox;
        private System.Windows.Forms.CheckBox UserOutput1CheckBox;
        private System.Windows.Forms.CheckBox LogPropertyChangesCheckBox;
        private System.Windows.Forms.CheckBox LogMessagesCheckBox;
        private System.Windows.Forms.CheckBox LogEventsCheckBox;
        private System.Windows.Forms.Label AspirationTypeLabel;
        private System.Windows.Forms.ComboBox AspirationTypeComboBox;
        private System.Windows.Forms.CheckBox DynamicStateCheckBox;
    }
}

