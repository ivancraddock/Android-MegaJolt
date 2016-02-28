#region USING STATEMENTS
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
#endregion USING STATEMENTS

namespace DevAge.Windows.Forms
{
    public partial class BetterNumericUpDown : UserControl
    {
        #region BetterNumericUpDown()
        public BetterNumericUpDown()
        {
            InitializeComponent();

            MinimumValue = 0;
            MaximumValue = 100;
            Increment = 1;
        }
        #endregion BetterNumericUpDown

        #region FIELDS
        public event EventHandler IncrementChanged;
        public event EventHandler ValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler MaximumValueChanged;

        private long currentValue;
        private long increment;
        private long minimumValue;
        private long maximumValue;
        #endregion FIELDS

        #region PROPERTIES
        #region IsInEditMode Variable
        public bool IsInEditMode { get; private set; }
        #endregion IsInEditMode
        #region Increment Property
        public long Increment
        {
            get { return increment; }
            set
            {
                if (value == increment) return;
                increment = value;
                OnIncrementChanged();
            }
        }
        #endregion Increment
        #region Value Property
        public long Value
        {
            get { return currentValue; }
            set
            {
                long newValue = Math.Max(Math.Min(MaximumValue, value), MinimumValue);
                if (newValue == Value) return;

                currentValue = newValue;
                ValueLabel.Text = newValue.ToString(CultureInfo.CurrentCulture);
                SetupEditControls();
                OnValueChanged();
            }
        }
        #endregion Value
        #region MinimumValue Property
        public long MinimumValue
        {
            get { return minimumValue; }
            set
            {
                if (value == MinimumValue) return;
                minimumValue = value;
                OnMinimumValueChanged();
            }
        }
        #endregion MinimumValue
        #region MaximumValue Property
        public long MaximumValue
        {
            get { return maximumValue; }
            set
            {
                if (value == MaximumValue) return;
                maximumValue = value;
                OnMaximumValueChanged();
            }
        }
        #endregion MaximumValue
        #region Font Property
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                ValueLabel.Font = value;
            }
        }
        #endregion Font
        #endregion PROPERTIES

        #region METHODS
        #region StartEditMode()
        public void StartEditMode()
        {
            StartEditMode(0);
        }
        #endregion StartEditMode
        #region StartEditMode()
        public void StartEditMode(long initialValue)
        {
            if (!IsInEditMode) ValueLabel.Text = initialValue.ToString(CultureInfo.CurrentCulture);
            ValueLabel.BackColor = SystemColors.Highlight;
            ValueLabel.ForeColor = SystemColors.HighlightText;
            IsInEditMode = true;
        }
        #endregion StartEditMode
        #region EndEditMode(bool)
        public void EndEditMode(bool save)
        {
            if (!IsInEditMode) return;

            if (save)
                Value = long.Parse(ValueLabel.Text);

            ValueLabel.Text = Value.ToString(CultureInfo.CurrentCulture);
            ValueLabel.BackColor = SystemColors.Window;
            ValueLabel.ForeColor = SystemColors.WindowText;
            IsInEditMode = false;
        }
        #endregion EndEditMode
        #region SetupEditControls()
        private void SetupEditControls()
        {
            using (Bitmap bitmap = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int valueWidth = Convert.ToInt32(g.MeasureString(ValueLabel.Text, ValueLabel.Font).Width);
                int minimumWidth = DownButton.Width + ValueLabel.Margin.Left + valueWidth + ValueLabel.Margin.Right + UpButton.Width;
                if (Width >= minimumWidth)
                {
                    UpButton.Visible = true;
                    DownButton.Visible = true;
                }
                else
                {
                    UpButton.Visible = false;
                    DownButton.Visible = false;
                }
            }
        }
        #endregion SetupEditControls

        #region OnEnter(EventArgs)
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            ValueLabel.Focus();
        }
        #endregion OnEnter
        #region OnIncrementChanged()
        protected virtual void OnIncrementChanged()
        {
            if (IncrementChanged == null) return;
            IncrementChanged(this, EventArgs.Empty);
        }
        #endregion OnIncrementChanged
        #region OnValueChanged()
        protected virtual void OnValueChanged()
        {
            if (ValueChanged == null) return;
            ValueChanged(this, EventArgs.Empty);
        }
        #endregion OnValueChanged
        #region OnMinimumValueChanged()
        protected virtual void OnMinimumValueChanged()
        {
            if (MinimumValueChanged == null) return;
            MinimumValueChanged(this, EventArgs.Empty);
        }
        #endregion OnMinimumValueChanged
        #region OnMaximumValueChanged()
        protected virtual void OnMaximumValueChanged()
        {
            if (MaximumValueChanged == null) return;
            MaximumValueChanged(this, EventArgs.Empty);
        }
        #endregion OnMaximumValueChanged
        #endregion METHODS

        #region EVENT HANDERS
        #region BetterNumericUpDown_Resize(object, EventArgs)
        private void BetterNumericUpDown_Resize(object sender, EventArgs e)
        {
            SetupEditControls();
        }
        #endregion BetterNumericUpDown_Resize

        #region Button_Enter(object, EventArgs)
        private void Button_Enter(object sender, EventArgs e)
        {
            ValueLabel.Focus();
        }
        #endregion Button_Enter
        #region DownButton_Click(object, System.EventArgs)
        private void DownButton_Click(object sender, System.EventArgs e)
        {
            Value =  Math.Max(Value - Increment, MinimumValue);
        }
        #endregion DownButton_Click
        #region UpButton_Click(object, EventArgs)
        private void UpButton_Click(object sender, EventArgs e)
        {
            Value = Math.Min(Value + Increment, MaximumValue);
        }
        #endregion UpButton_Click

        #region Control_KeyDown(object, KeyEventArgs)
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
            {
                EndEditMode(false);
                Value = Math.Min(Value + Increment, MaximumValue);
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Down)
            {
                EndEditMode(false);
                Value = Math.Max(Value - Increment, MinimumValue);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                EndEditMode(false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                EndEditMode(true);
            }
            else if (e.KeyCode <= Keys.D9 && e.KeyCode >= Keys.D0)
            {
                StartEditMode();
                ValueLabel.Text = ((long.Parse(ValueLabel.Text) * 10) + (((long)e.KeyCode) - (long)Keys.D0)).ToString(CultureInfo.CurrentCulture);
            }
            else if ((int)e.KeyCode <= 105 && (int)e.KeyCode >= 96)
            {
                StartEditMode();
                ValueLabel.Text = ((long.Parse(ValueLabel.Text) * 10) + (((long)e.KeyCode) - 96)).ToString(CultureInfo.CurrentCulture);
            }
        }
        #endregion Control_KeyDown

        #region ValueLabel_Enter(object, EventArgs)
        private void ValueLabel_Enter(object sender, EventArgs e)
        {
        }
        #endregion ValueLabel_Enter
        #region ValueLabel_Leave(object, EventArgs)
        private void ValueLabel_Leave(object sender, EventArgs e)
        {
            ValueLabel.Invalidate();
            EndEditMode(true);
        }
        #endregion ValueLabel_Leave
        #endregion EVENT HANDERS

        #region AarowButton Class
        private class AarowButton : Button
        {
            #region IsInputKey(Keys)
            protected override bool IsInputKey(Keys keyData)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down) return true;
                return base.IsInputKey(keyData);
            }
            #endregion IsInputKey
        }
        #endregion AarowButton
        #region AarowLabel Class
        private class AarowLabel : Label
        {
            #region IsInputKey(Keys)
            protected override bool IsInputKey(Keys keyData)
            {
                if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down) return true;
                return base.IsInputKey(keyData);
            }
            #endregion IsInputKey
            #region ShowFocusRectangle Property
            private bool ShowFocusRectangle { get; set; }
            #endregion ShowFocusRectangle

            #region OnEnter(EventArgs)
            protected override void OnEnter(EventArgs e)
            {
                base.OnEnter(e);
                ShowFocusRectangle = true;
                Invalidate();
            }
            #endregion OnEnter
            #region OnLeave(EventArgs)
            protected override void OnLeave(EventArgs e)
            {
                base.OnLeave(e);
                ShowFocusRectangle = false;
                Invalidate();
            }
            #endregion OnLeave
            #region OnMouseDown(MouseEventArgs)
            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                Focus();
            }
            #endregion OnMouseDown
            #region OnPaint(PaintEventArgs)
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                //Draw a border in Silver
                Rectangle border = new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                e.Graphics.DrawRectangle(ShowFocusRectangle ? Pens.Black : Pens.Silver, border);
            }
            #endregion OnPaint
        }
        #endregion AarowButton
    }
}
