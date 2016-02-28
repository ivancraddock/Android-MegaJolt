namespace DevAge.Windows.Forms
{
    partial class BetterNumericUpDown
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BetterNumericUpDown));
            this.DownButton = new DevAge.Windows.Forms.BetterNumericUpDown.AarowButton();
            this.ValueLabel = new DevAge.Windows.Forms.BetterNumericUpDown.AarowLabel();
            this.UpButton = new DevAge.Windows.Forms.BetterNumericUpDown.AarowButton();
            this.SuspendLayout();
            // 
            // DownButton
            // 
            this.DownButton.BackColor = System.Drawing.SystemColors.Control;
            this.DownButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DownButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DownButton.Image = ((System.Drawing.Image)(resources.GetObject("DownButton.Image")));
            this.DownButton.Location = new System.Drawing.Point(0, 0);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(18, 27);
            this.DownButton.TabIndex = 1;
            this.DownButton.TabStop = false;
            this.DownButton.UseVisualStyleBackColor = false;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            this.DownButton.Enter += new System.EventHandler(this.Button_Enter);
            this.DownButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // ValueLabel
            // 
            this.ValueLabel.BackColor = System.Drawing.SystemColors.Window;
            this.ValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ValueLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ValueLabel.Location = new System.Drawing.Point(18, 0);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(166, 27);
            this.ValueLabel.TabIndex = 0;
            this.ValueLabel.Text = "0";
            this.ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ValueLabel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            this.ValueLabel.Enter += new System.EventHandler(this.ValueLabel_Enter);
            this.ValueLabel.Leave += new System.EventHandler(this.ValueLabel_Leave);
            // 
            // UpButton
            // 
            this.UpButton.BackColor = System.Drawing.SystemColors.Control;
            this.UpButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.UpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UpButton.Image = ((System.Drawing.Image)(resources.GetObject("UpButton.Image")));
            this.UpButton.Location = new System.Drawing.Point(184, 0);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(18, 27);
            this.UpButton.TabIndex = 2;
            this.UpButton.TabStop = false;
            this.UpButton.UseVisualStyleBackColor = false;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            this.UpButton.Enter += new System.EventHandler(this.Button_Enter);
            this.UpButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // BetterNumericUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ValueLabel);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Name = "BetterNumericUpDown";
            this.Size = new System.Drawing.Size(202, 27);
            this.Resize += new System.EventHandler(this.BetterNumericUpDown_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private DevAge.Windows.Forms.BetterNumericUpDown.AarowButton UpButton;
        private DevAge.Windows.Forms.BetterNumericUpDown.AarowButton DownButton;
        private DevAge.Windows.Forms.BetterNumericUpDown.AarowLabel ValueLabel;
    }
}
