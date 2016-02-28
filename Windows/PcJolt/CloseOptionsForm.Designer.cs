namespace PcJolt
{
    partial class CloseOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseOptionsForm));
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.DirectionsLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.SavePanel = new PcJolt.CloseOptionsForm.HoverPanel();
            this.SaveSelectedPictureBox = new System.Windows.Forms.PictureBox();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.SavePictureBox = new System.Windows.Forms.PictureBox();
            this.WritePanel = new PcJolt.CloseOptionsForm.HoverPanel();
            this.WriteSelectedPictureBox = new System.Windows.Forms.PictureBox();
            this.WriteLabel = new System.Windows.Forms.Label();
            this.WritePictureBox = new System.Windows.Forms.PictureBox();
            this.FlashPanel = new PcJolt.CloseOptionsForm.HoverPanel();
            this.FlashSelectedPictureBox = new System.Windows.Forms.PictureBox();
            this.FlashLabel = new System.Windows.Forms.Label();
            this.FlashPictureBox = new System.Windows.Forms.PictureBox();
            this.SavePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveSelectedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SavePictureBox)).BeginInit();
            this.WritePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WriteSelectedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WritePictureBox)).BeginInit();
            this.FlashPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlashSelectedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlashPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(213, 323);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(132, 323);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Exit";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // DirectionsLabel
            // 
            this.DirectionsLabel.Location = new System.Drawing.Point(13, 30);
            this.DirectionsLabel.Name = "DirectionsLabel";
            this.DirectionsLabel.Size = new System.Drawing.Size(249, 31);
            this.DirectionsLabel.TabIndex = 5;
            this.DirectionsLabel.Text = "Select the operations  you would like to perform before exiting the applicaiton.";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 12);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(250, 16);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Unsaved Changes";
            // 
            // SavePanel
            // 
            this.SavePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePanel.BackColor = System.Drawing.SystemColors.Window;
            this.SavePanel.Controls.Add(this.SaveSelectedPictureBox);
            this.SavePanel.Controls.Add(this.SaveLabel);
            this.SavePanel.Controls.Add(this.SavePictureBox);
            this.SavePanel.Location = new System.Drawing.Point(17, 70);
            this.SavePanel.Name = "SavePanel";
            this.SavePanel.Size = new System.Drawing.Size(266, 60);
            this.SavePanel.TabIndex = 7;
            this.SavePanel.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveSelectedPictureBox
            // 
            this.SaveSelectedPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.SaveSelectedPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveSelectedPictureBox.Image")));
            this.SaveSelectedPictureBox.Location = new System.Drawing.Point(239, 1);
            this.SaveSelectedPictureBox.Name = "SaveSelectedPictureBox";
            this.SaveSelectedPictureBox.Size = new System.Drawing.Size(26, 58);
            this.SaveSelectedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.SaveSelectedPictureBox.TabIndex = 2;
            this.SaveSelectedPictureBox.TabStop = false;
            this.SaveSelectedPictureBox.Visible = false;
            this.SaveSelectedPictureBox.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveLabel
            // 
            this.SaveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLabel.BackColor = System.Drawing.SystemColors.Window;
            this.SaveLabel.Location = new System.Drawing.Point(67, 6);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(165, 48);
            this.SaveLabel.TabIndex = 0;
            this.SaveLabel.Text = "Save the configuration to a file.";
            this.SaveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveLabel.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SavePictureBox
            // 
            this.SavePictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.SavePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SavePictureBox.Image")));
            this.SavePictureBox.Location = new System.Drawing.Point(6, 6);
            this.SavePictureBox.Name = "SavePictureBox";
            this.SavePictureBox.Size = new System.Drawing.Size(48, 48);
            this.SavePictureBox.TabIndex = 0;
            this.SavePictureBox.TabStop = false;
            this.SavePictureBox.EnabledChanged += new System.EventHandler(this.SaveButton_EnabledChanged);
            this.SavePictureBox.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // WritePanel
            // 
            this.WritePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WritePanel.BackColor = System.Drawing.SystemColors.Window;
            this.WritePanel.Controls.Add(this.WriteSelectedPictureBox);
            this.WritePanel.Controls.Add(this.WriteLabel);
            this.WritePanel.Controls.Add(this.WritePictureBox);
            this.WritePanel.Location = new System.Drawing.Point(17, 144);
            this.WritePanel.Name = "WritePanel";
            this.WritePanel.Size = new System.Drawing.Size(266, 60);
            this.WritePanel.TabIndex = 8;
            this.WritePanel.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // WriteSelectedPictureBox
            // 
            this.WriteSelectedPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.WriteSelectedPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("WriteSelectedPictureBox.Image")));
            this.WriteSelectedPictureBox.Location = new System.Drawing.Point(239, 1);
            this.WriteSelectedPictureBox.Name = "WriteSelectedPictureBox";
            this.WriteSelectedPictureBox.Size = new System.Drawing.Size(26, 58);
            this.WriteSelectedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.WriteSelectedPictureBox.TabIndex = 3;
            this.WriteSelectedPictureBox.TabStop = false;
            this.WriteSelectedPictureBox.Visible = false;
            this.WriteSelectedPictureBox.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // WriteLabel
            // 
            this.WriteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WriteLabel.BackColor = System.Drawing.SystemColors.Window;
            this.WriteLabel.Location = new System.Drawing.Point(67, 6);
            this.WriteLabel.Name = "WriteLabel";
            this.WriteLabel.Size = new System.Drawing.Size(166, 48);
            this.WriteLabel.TabIndex = 0;
            this.WriteLabel.Text = "Write the configuration to the MegaJolt/E Controller.";
            this.WriteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WriteLabel.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // WritePictureBox
            // 
            this.WritePictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.WritePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("WritePictureBox.Image")));
            this.WritePictureBox.Location = new System.Drawing.Point(6, 6);
            this.WritePictureBox.Name = "WritePictureBox";
            this.WritePictureBox.Size = new System.Drawing.Size(48, 48);
            this.WritePictureBox.TabIndex = 0;
            this.WritePictureBox.TabStop = false;
            this.WritePictureBox.EnabledChanged += new System.EventHandler(this.WriteButton_EnabledChanged);
            this.WritePictureBox.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // FlashPanel
            // 
            this.FlashPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlashPanel.BackColor = System.Drawing.SystemColors.Window;
            this.FlashPanel.Controls.Add(this.FlashSelectedPictureBox);
            this.FlashPanel.Controls.Add(this.FlashLabel);
            this.FlashPanel.Controls.Add(this.FlashPictureBox);
            this.FlashPanel.Location = new System.Drawing.Point(17, 219);
            this.FlashPanel.Name = "FlashPanel";
            this.FlashPanel.Size = new System.Drawing.Size(266, 60);
            this.FlashPanel.TabIndex = 9;
            this.FlashPanel.Click += new System.EventHandler(this.FlashButton_Click);
            // 
            // FlashSelectedPictureBox
            // 
            this.FlashSelectedPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.FlashSelectedPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("FlashSelectedPictureBox.Image")));
            this.FlashSelectedPictureBox.Location = new System.Drawing.Point(239, 1);
            this.FlashSelectedPictureBox.Name = "FlashSelectedPictureBox";
            this.FlashSelectedPictureBox.Size = new System.Drawing.Size(26, 58);
            this.FlashSelectedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.FlashSelectedPictureBox.TabIndex = 3;
            this.FlashSelectedPictureBox.TabStop = false;
            this.FlashSelectedPictureBox.Visible = false;
            this.FlashSelectedPictureBox.Click += new System.EventHandler(this.FlashButton_Click);
            // 
            // FlashLabel
            // 
            this.FlashLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlashLabel.BackColor = System.Drawing.SystemColors.Window;
            this.FlashLabel.Location = new System.Drawing.Point(67, 6);
            this.FlashLabel.Name = "FlashLabel";
            this.FlashLabel.Size = new System.Drawing.Size(166, 48);
            this.FlashLabel.TabIndex = 0;
            this.FlashLabel.Text = "Flash the configuration to the MegaJolt/E Controller.";
            this.FlashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FlashLabel.Click += new System.EventHandler(this.FlashButton_Click);
            // 
            // FlashPictureBox
            // 
            this.FlashPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.FlashPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("FlashPictureBox.Image")));
            this.FlashPictureBox.Location = new System.Drawing.Point(6, 6);
            this.FlashPictureBox.Name = "FlashPictureBox";
            this.FlashPictureBox.Size = new System.Drawing.Size(48, 48);
            this.FlashPictureBox.TabIndex = 0;
            this.FlashPictureBox.TabStop = false;
            this.FlashPictureBox.EnabledChanged += new System.EventHandler(this.FlashButton_EnabledChanged);
            this.FlashPictureBox.Click += new System.EventHandler(this.FlashButton_Click);
            // 
            // CloseOptionsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(300, 358);
            this.ControlBox = false;
            this.Controls.Add(this.FlashPanel);
            this.Controls.Add(this.WritePanel);
            this.Controls.Add(this.SavePanel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DirectionsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CloseOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exit PC Jolt";
            this.SavePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaveSelectedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SavePictureBox)).EndInit();
            this.WritePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WriteSelectedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WritePictureBox)).EndInit();
            this.FlashPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlashSelectedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlashPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label DirectionsLabel;
        private System.Windows.Forms.Label TitleLabel;
        private HoverPanel SavePanel;
        private System.Windows.Forms.PictureBox SaveSelectedPictureBox;
        private System.Windows.Forms.Label SaveLabel;
        private System.Windows.Forms.PictureBox SavePictureBox;
        private HoverPanel WritePanel;
        private System.Windows.Forms.PictureBox WriteSelectedPictureBox;
        private System.Windows.Forms.Label WriteLabel;
        private System.Windows.Forms.PictureBox WritePictureBox;
        private HoverPanel FlashPanel;
        private System.Windows.Forms.PictureBox FlashSelectedPictureBox;
        private System.Windows.Forms.Label FlashLabel;
        private System.Windows.Forms.PictureBox FlashPictureBox;
    }
}