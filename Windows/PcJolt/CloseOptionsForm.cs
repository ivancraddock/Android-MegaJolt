#region USING STATEMENTS
using System;
using System.Drawing;
using System.Windows.Forms;
using MegaJolt.Communication;
#endregion USING STATEMENTS

namespace PcJolt
{
    public partial class CloseOptionsForm : Form
    {
        #region CONSTRUCTORS
        #region CloseOptionsForm()
        private CloseOptionsForm()
        {
            InitializeComponent();
        }
        #endregion CloseOptionsForm
        #region CloseOptionsForm(Controller)
        public CloseOptionsForm(Controller controller) : this()
        {
            SaveImage = SavePictureBox.Image;
            WriteImage = WritePictureBox.Image;
            FlashImage = FlashPictureBox.Image;

            SavePanel.Enabled = !controller.IgnitionMap.SavedState.HasFlag(SavedStates.SavedToFile);
            WritePanel.Enabled = controller.IsConnected && !controller.IgnitionMap.SavedState.HasFlag(SavedStates.SavedToController);
            FlashPanel.Enabled = controller.IsConnected && !controller.IgnitionMap.SavedState.HasFlag(SavedStates.WrittenToFlash);
        }
        #endregion CloseOptionsForm
        #endregion CONSTRUCTORS

        #region FIELDS
        private readonly Image SaveImage;
        private readonly Image WriteImage;
        private readonly Image FlashImage;
        #endregion FIELDS

        #region PROPERTIES
        #region SaveSelected Property
        private bool saveSelected;
        public bool SaveSelected
        {
            get { return saveSelected; }
            private set
            {
                saveSelected = value;
                SaveSelectedPictureBox.Visible = value;
                UpdateOkButtonText();
            }
        }
        #endregion SaveSelected
        #region WriteSelected Property
        private bool writeSelected;
        public bool WriteSelected
        {
            get { return writeSelected; }
            private set 
            {
                writeSelected = value;
                WriteSelectedPictureBox.Visible = value;
                UpdateOkButtonText(); 
            }
        }
        #endregion WriteSelected
        #region FlashSelected Property
        private bool flashSelected;
        public bool FlashSelected
        {
            get { return flashSelected; }
            private set
            {
                flashSelected = value;
                FlashSelectedPictureBox.Visible = value;
                UpdateOkButtonText();
            }
        }
        #endregion FlashSelected
        #endregion PROPERTIES

        #region UpdateOkButtonText()
        public void UpdateOkButtonText()
        {
            OkButton.Text = !SaveSelected && !WriteSelected && !FlashSelected
                                ? "Exit"
                                : "OK";
        }
        #endregion UpdateOkButtonText

        #region EVENT HANDLERS
        #region SaveButton_Click(object, EventArgs)
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSelected = !SaveSelected;
        }
        #endregion SaveButton_Click
        #region SaveButton_EnabledChanged(object, EventArgs)
        private void SaveButton_EnabledChanged(object sender, EventArgs e)
        {
            SavePictureBox.Image = SavePanel.Enabled
                                       ? SaveImage
                                       : SaveImage.MakeGrayscale();
        }
        #endregion SaveButton_EnabledChanged

        #region WriteButton_Click(object, EventArgs)
        private void WriteButton_Click(object sender, EventArgs e)
        {
            WriteSelected = !WriteSelected;
        }
        #endregion WriteButton_Click
        #region WriteButton_EnabledChanged(object, EventArgs)
        private void WriteButton_EnabledChanged(object sender, EventArgs e)
        {
            WritePictureBox.Image = WritePanel.Enabled
                                       ? WriteImage
                                       : WriteImage.MakeGrayscale();
        }
        #endregion WriteButton_EnabledChanged

        #region FlashButton_Click(object, EventArgs)
        private void FlashButton_Click(object sender, EventArgs e)
        {
            FlashSelected = !FlashSelected;
        }
        #endregion FlashButton_Click
        #region FlashButton_EnabledChanged(object, EventArgs)
        private void FlashButton_EnabledChanged(object sender, EventArgs e)
        {
            FlashPictureBox.Image = FlashPanel.Enabled
                                       ? FlashImage
                                       : FlashImage.MakeGrayscale();
        }
        #endregion FlashButton_EnabledChanged
        #endregion EVENT HANDLERS

        #region HoverPanel Class
        private class HoverPanel : Panel
        {
            #region Hover Property
            private bool Hover { get; set; }
            #endregion Hover

            #region Cursor Property
            public override Cursor Cursor
            {
                get
                {
                    return Cursors.Hand;
                }
                set
                {
                    base.Cursor = Cursors.Hand;
                }
            }
            #endregion Cursor
            #region OnMouseEnter(EventArgs)
            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);

                if (Hover) return;
                Hover = true;
                Invalidate();
            }
            #endregion OnMouseEnter
            #region OnMouseLeave(EventArgs)
            protected override void OnMouseLeave(EventArgs e)
            {
                if (FindForm().RectangleToScreen(Bounds).Contains(Cursor.Position)) return;

                Hover = false;
                Invalidate();
            }
            #endregion OnMouseLeave
            #region OnPaint(PaintEventArgs)
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle rect = new Rectangle(0, 0, Bounds.Width - 1, Bounds.Height - 1);
                e.Graphics.DrawRectangle(Hover ? Pens.Black : SystemPens.Window, rect);
            }
            #endregion OnPaint
        }
        #endregion HoverPanel
    }
}
