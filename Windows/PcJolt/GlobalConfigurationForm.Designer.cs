namespace PcJolt
{
    partial class GlobalConfigurationForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.ApplicationSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshIntervalLabel = new System.Windows.Forms.Label();
            this.RefreshIntervalNumericUpDown = new DevAge.Windows.Forms.BetterNumericUpDown();
            this.StartupActionLabel = new System.Windows.Forms.Label();
            this.StartupActionComboBox = new System.Windows.Forms.ComboBox();
            this.AspirationTypeLabel = new System.Windows.Forms.Label();
            this.AspirationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.LoadTypeLabel = new System.Windows.Forms.Label();
            this.LoadTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ComPortLabel = new System.Windows.Forms.Label();
            this.ComPortComboBox = new System.Windows.Forms.ComboBox();
            this.ControllerSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.TriggerWheelOffsetLabel = new System.Windows.Forms.Label();
            this.TriggerWheelOffsetNumericUpDown = new DevAge.Windows.Forms.BetterNumericUpDown();
            this.CrankingAdvanceLabel = new System.Windows.Forms.Label();
            this.CrankingAdvanceNumericUpDown = new DevAge.Windows.Forms.BetterNumericUpDown();
            this.PipNoiseFilterLabel = new System.Windows.Forms.Label();
            this.PipNoiseFilterNumericUpDown = new DevAge.Windows.Forms.BetterNumericUpDown();
            this.EdisTypeLabel = new System.Windows.Forms.Label();
            this.EdisTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ApplicationSettingsGroupBox.SuspendLayout();
            this.ControllerSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(446, 159);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(365, 159);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // ApplicationSettingsGroupBox
            // 
            this.ApplicationSettingsGroupBox.Controls.Add(this.RefreshIntervalLabel);
            this.ApplicationSettingsGroupBox.Controls.Add(this.RefreshIntervalNumericUpDown);
            this.ApplicationSettingsGroupBox.Controls.Add(this.StartupActionLabel);
            this.ApplicationSettingsGroupBox.Controls.Add(this.StartupActionComboBox);
            this.ApplicationSettingsGroupBox.Controls.Add(this.AspirationTypeLabel);
            this.ApplicationSettingsGroupBox.Controls.Add(this.AspirationTypeComboBox);
            this.ApplicationSettingsGroupBox.Controls.Add(this.LoadTypeLabel);
            this.ApplicationSettingsGroupBox.Controls.Add(this.LoadTypeComboBox);
            this.ApplicationSettingsGroupBox.Controls.Add(this.ComPortLabel);
            this.ApplicationSettingsGroupBox.Controls.Add(this.ComPortComboBox);
            this.ApplicationSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ApplicationSettingsGroupBox.Name = "ApplicationSettingsGroupBox";
            this.ApplicationSettingsGroupBox.Size = new System.Drawing.Size(251, 160);
            this.ApplicationSettingsGroupBox.TabIndex = 0;
            this.ApplicationSettingsGroupBox.TabStop = false;
            this.ApplicationSettingsGroupBox.Text = "Application Settings";
            // 
            // RefreshIntervalLabel
            // 
            this.RefreshIntervalLabel.AutoSize = true;
            this.RefreshIntervalLabel.Location = new System.Drawing.Point(17, 49);
            this.RefreshIntervalLabel.Name = "RefreshIntervalLabel";
            this.RefreshIntervalLabel.Size = new System.Drawing.Size(104, 13);
            this.RefreshIntervalLabel.TabIndex = 39;
            this.RefreshIntervalLabel.Text = "Refresh Interval (ms)";
            // 
            // RefreshIntervalNumericUpDown
            // 
            this.RefreshIntervalNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshIntervalNumericUpDown.Increment = ((long)(10));
            this.RefreshIntervalNumericUpDown.Location = new System.Drawing.Point(124, 47);
            this.RefreshIntervalNumericUpDown.MaximumValue = ((long)(10000));
            this.RefreshIntervalNumericUpDown.MinimumValue = ((long)(50));
            this.RefreshIntervalNumericUpDown.Name = "RefreshIntervalNumericUpDown";
            this.RefreshIntervalNumericUpDown.Size = new System.Drawing.Size(118, 20);
            this.RefreshIntervalNumericUpDown.TabIndex = 1;
            this.RefreshIntervalNumericUpDown.Value = ((long)(250));
            // 
            // StartupActionLabel
            // 
            this.StartupActionLabel.AutoSize = true;
            this.StartupActionLabel.Location = new System.Drawing.Point(41, 129);
            this.StartupActionLabel.Name = "StartupActionLabel";
            this.StartupActionLabel.Size = new System.Drawing.Size(74, 13);
            this.StartupActionLabel.TabIndex = 25;
            this.StartupActionLabel.Text = "Startup Action";
            // 
            // StartupActionComboBox
            // 
            this.StartupActionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartupActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartupActionComboBox.FormattingEnabled = true;
            this.StartupActionComboBox.Location = new System.Drawing.Point(124, 126);
            this.StartupActionComboBox.Name = "StartupActionComboBox";
            this.StartupActionComboBox.Size = new System.Drawing.Size(115, 21);
            this.StartupActionComboBox.TabIndex = 4;
            // 
            // AspirationTypeLabel
            // 
            this.AspirationTypeLabel.AutoSize = true;
            this.AspirationTypeLabel.Location = new System.Drawing.Point(38, 102);
            this.AspirationTypeLabel.Name = "AspirationTypeLabel";
            this.AspirationTypeLabel.Size = new System.Drawing.Size(80, 13);
            this.AspirationTypeLabel.TabIndex = 23;
            this.AspirationTypeLabel.Text = "Aspiration Type";
            // 
            // AspirationTypeComboBox
            // 
            this.AspirationTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AspirationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AspirationTypeComboBox.FormattingEnabled = true;
            this.AspirationTypeComboBox.Location = new System.Drawing.Point(124, 99);
            this.AspirationTypeComboBox.Name = "AspirationTypeComboBox";
            this.AspirationTypeComboBox.Size = new System.Drawing.Size(115, 21);
            this.AspirationTypeComboBox.TabIndex = 3;
            // 
            // LoadTypeLabel
            // 
            this.LoadTypeLabel.AutoSize = true;
            this.LoadTypeLabel.Location = new System.Drawing.Point(60, 75);
            this.LoadTypeLabel.Name = "LoadTypeLabel";
            this.LoadTypeLabel.Size = new System.Drawing.Size(58, 13);
            this.LoadTypeLabel.TabIndex = 21;
            this.LoadTypeLabel.Text = "Load Type";
            // 
            // LoadTypeComboBox
            // 
            this.LoadTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoadTypeComboBox.FormattingEnabled = true;
            this.LoadTypeComboBox.Location = new System.Drawing.Point(124, 72);
            this.LoadTypeComboBox.Name = "LoadTypeComboBox";
            this.LoadTypeComboBox.Size = new System.Drawing.Size(115, 21);
            this.LoadTypeComboBox.TabIndex = 2;
            // 
            // ComPortLabel
            // 
            this.ComPortLabel.AutoSize = true;
            this.ComPortLabel.Location = new System.Drawing.Point(68, 22);
            this.ComPortLabel.Name = "ComPortLabel";
            this.ComPortLabel.Size = new System.Drawing.Size(50, 13);
            this.ComPortLabel.TabIndex = 19;
            this.ComPortLabel.Text = "Com Port";
            // 
            // ComPortComboBox
            // 
            this.ComPortComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComPortComboBox.FormattingEnabled = true;
            this.ComPortComboBox.Location = new System.Drawing.Point(124, 19);
            this.ComPortComboBox.Name = "ComPortComboBox";
            this.ComPortComboBox.Size = new System.Drawing.Size(115, 21);
            this.ComPortComboBox.TabIndex = 0;
            // 
            // ControllerSettingsGroupBox
            // 
            this.ControllerSettingsGroupBox.Controls.Add(this.TriggerWheelOffsetLabel);
            this.ControllerSettingsGroupBox.Controls.Add(this.TriggerWheelOffsetNumericUpDown);
            this.ControllerSettingsGroupBox.Controls.Add(this.CrankingAdvanceLabel);
            this.ControllerSettingsGroupBox.Controls.Add(this.CrankingAdvanceNumericUpDown);
            this.ControllerSettingsGroupBox.Controls.Add(this.PipNoiseFilterLabel);
            this.ControllerSettingsGroupBox.Controls.Add(this.PipNoiseFilterNumericUpDown);
            this.ControllerSettingsGroupBox.Controls.Add(this.EdisTypeLabel);
            this.ControllerSettingsGroupBox.Controls.Add(this.EdisTypeComboBox);
            this.ControllerSettingsGroupBox.Location = new System.Drawing.Point(269, 12);
            this.ControllerSettingsGroupBox.Name = "ControllerSettingsGroupBox";
            this.ControllerSettingsGroupBox.Size = new System.Drawing.Size(251, 138);
            this.ControllerSettingsGroupBox.TabIndex = 1;
            this.ControllerSettingsGroupBox.TabStop = false;
            this.ControllerSettingsGroupBox.Text = "Controller Settings";
            // 
            // TriggerWheelOffsetLabel
            // 
            this.TriggerWheelOffsetLabel.AutoSize = true;
            this.TriggerWheelOffsetLabel.Location = new System.Drawing.Point(16, 103);
            this.TriggerWheelOffsetLabel.Name = "TriggerWheelOffsetLabel";
            this.TriggerWheelOffsetLabel.Size = new System.Drawing.Size(105, 13);
            this.TriggerWheelOffsetLabel.TabIndex = 33;
            this.TriggerWheelOffsetLabel.Text = "Trigger Wheel Offset";
            // 
            // TriggerWheelOffsetNumericUpDown
            // 
            this.TriggerWheelOffsetNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TriggerWheelOffsetNumericUpDown.Increment = ((long)(1));
            this.TriggerWheelOffsetNumericUpDown.Location = new System.Drawing.Point(127, 100);
            this.TriggerWheelOffsetNumericUpDown.MaximumValue = ((long)(5));
            this.TriggerWheelOffsetNumericUpDown.MinimumValue = ((long)(-5));
            this.TriggerWheelOffsetNumericUpDown.Name = "TriggerWheelOffsetNumericUpDown";
            this.TriggerWheelOffsetNumericUpDown.Size = new System.Drawing.Size(118, 20);
            this.TriggerWheelOffsetNumericUpDown.TabIndex = 3;
            this.TriggerWheelOffsetNumericUpDown.Value = ((long)(0));
            // 
            // CrankingAdvanceLabel
            // 
            this.CrankingAdvanceLabel.AutoSize = true;
            this.CrankingAdvanceLabel.Location = new System.Drawing.Point(26, 76);
            this.CrankingAdvanceLabel.Name = "CrankingAdvanceLabel";
            this.CrankingAdvanceLabel.Size = new System.Drawing.Size(95, 13);
            this.CrankingAdvanceLabel.TabIndex = 31;
            this.CrankingAdvanceLabel.Text = "Cranking Advance";
            // 
            // CrankingAdvanceNumericUpDown
            // 
            this.CrankingAdvanceNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrankingAdvanceNumericUpDown.Increment = ((long)(1));
            this.CrankingAdvanceNumericUpDown.Location = new System.Drawing.Point(127, 73);
            this.CrankingAdvanceNumericUpDown.MaximumValue = ((long)(59));
            this.CrankingAdvanceNumericUpDown.MinimumValue = ((long)(0));
            this.CrankingAdvanceNumericUpDown.Name = "CrankingAdvanceNumericUpDown";
            this.CrankingAdvanceNumericUpDown.Size = new System.Drawing.Size(118, 20);
            this.CrankingAdvanceNumericUpDown.TabIndex = 2;
            this.CrankingAdvanceNumericUpDown.Value = ((long)(0));
            // 
            // PipNoiseFilterLabel
            // 
            this.PipNoiseFilterLabel.AutoSize = true;
            this.PipNoiseFilterLabel.Location = new System.Drawing.Point(42, 49);
            this.PipNoiseFilterLabel.Name = "PipNoiseFilterLabel";
            this.PipNoiseFilterLabel.Size = new System.Drawing.Size(79, 13);
            this.PipNoiseFilterLabel.TabIndex = 29;
            this.PipNoiseFilterLabel.Text = "PIP Noise Filter";
            // 
            // PipNoiseFilterNumericUpDown
            // 
            this.PipNoiseFilterNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PipNoiseFilterNumericUpDown.Increment = ((long)(1));
            this.PipNoiseFilterNumericUpDown.Location = new System.Drawing.Point(127, 46);
            this.PipNoiseFilterNumericUpDown.MaximumValue = ((long)(255));
            this.PipNoiseFilterNumericUpDown.MinimumValue = ((long)(0));
            this.PipNoiseFilterNumericUpDown.Name = "PipNoiseFilterNumericUpDown";
            this.PipNoiseFilterNumericUpDown.Size = new System.Drawing.Size(118, 20);
            this.PipNoiseFilterNumericUpDown.TabIndex = 1;
            this.PipNoiseFilterNumericUpDown.Value = ((long)(0));
            // 
            // EdisTypeLabel
            // 
            this.EdisTypeLabel.AutoSize = true;
            this.EdisTypeLabel.Location = new System.Drawing.Point(62, 22);
            this.EdisTypeLabel.Name = "EdisTypeLabel";
            this.EdisTypeLabel.Size = new System.Drawing.Size(59, 13);
            this.EdisTypeLabel.TabIndex = 27;
            this.EdisTypeLabel.Text = "EDIS Type";
            // 
            // EdisTypeComboBox
            // 
            this.EdisTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EdisTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EdisTypeComboBox.FormattingEnabled = true;
            this.EdisTypeComboBox.Location = new System.Drawing.Point(127, 19);
            this.EdisTypeComboBox.Name = "EdisTypeComboBox";
            this.EdisTypeComboBox.Size = new System.Drawing.Size(118, 21);
            this.EdisTypeComboBox.TabIndex = 0;
            // 
            // GlobalConfigurationForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 191);
            this.ControlBox = false;
            this.Controls.Add(this.ControllerSettingsGroupBox);
            this.Controls.Add(this.ApplicationSettingsGroupBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Global Configuration";
            this.ApplicationSettingsGroupBox.ResumeLayout(false);
            this.ApplicationSettingsGroupBox.PerformLayout();
            this.ControllerSettingsGroupBox.ResumeLayout(false);
            this.ControllerSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.GroupBox ApplicationSettingsGroupBox;
        private System.Windows.Forms.Label StartupActionLabel;
        private System.Windows.Forms.ComboBox StartupActionComboBox;
        private System.Windows.Forms.Label AspirationTypeLabel;
        private System.Windows.Forms.ComboBox AspirationTypeComboBox;
        private System.Windows.Forms.Label LoadTypeLabel;
        private System.Windows.Forms.ComboBox LoadTypeComboBox;
        private System.Windows.Forms.Label ComPortLabel;
        private System.Windows.Forms.ComboBox ComPortComboBox;
        private System.Windows.Forms.GroupBox ControllerSettingsGroupBox;
        private System.Windows.Forms.Label TriggerWheelOffsetLabel;
        private DevAge.Windows.Forms.BetterNumericUpDown TriggerWheelOffsetNumericUpDown;
        private System.Windows.Forms.Label CrankingAdvanceLabel;
        private DevAge.Windows.Forms.BetterNumericUpDown CrankingAdvanceNumericUpDown;
        private System.Windows.Forms.Label PipNoiseFilterLabel;
        private DevAge.Windows.Forms.BetterNumericUpDown PipNoiseFilterNumericUpDown;
        private System.Windows.Forms.Label EdisTypeLabel;
        private System.Windows.Forms.ComboBox EdisTypeComboBox;
        private System.Windows.Forms.Label RefreshIntervalLabel;
        private DevAge.Windows.Forms.BetterNumericUpDown RefreshIntervalNumericUpDown;
    }
}