using System;
using System.Windows.Forms;
using DevAge.Windows.Forms;

namespace SourceGrid.Cells.Editors
{
    /// <summary>
    /// EditorNumericUpDown editor class.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public class NumericUpDown : EditorControlBase
    {
        /// <summary>
        /// Create a model of type Decimal
        /// </summary>
        public NumericUpDown():base(typeof(int))
        {
        }

        public NumericUpDown(Type p_CellType, int p_Maximum, int p_Minimum, int p_Increment) : base(p_CellType)
        {
            if (p_CellType == null || p_CellType == typeof(byte) || p_CellType == typeof(sbyte) || p_CellType == typeof(short) || p_CellType == typeof(ushort) || p_CellType == typeof(int) || p_CellType == typeof(uint) || p_CellType == typeof(long))
            {
                Control.MaximumValue = p_Maximum;
                Control.MinimumValue = p_Minimum;
                Control.Increment = p_Increment;
            }
            else
                throw new SourceGridException("Invalid CellType: " + p_CellType.Name);
        }

        protected override Control CreateControl()
        {
            BetterNumericUpDown l_Control = new BetterNumericUpDown();
            return l_Control;
        }

        public new BetterNumericUpDown Control
        {
            get
            {
                return (BetterNumericUpDown)base.Control;
            }
        }

        public long MaximumValue
        {
            get { return Control.MaximumValue; }
            set { Control.MaximumValue = value; }
        }

        public long MinimumValue
        {
            get { return Control.MinimumValue; }
            set { Control.MinimumValue = value; }
        }

        public long Increment
        {
            get { return Control.Increment; }
            set { Control.Increment = value; }
        }

        /// <summary>
        /// Set the specified value in the current editor control.
        /// </summary>
        /// <param name="editValue"></param>
        public override void SetEditValue(object editValue)
        {
            long value;
            if (editValue is byte)
                value = Convert.ToInt64((byte)editValue);
            else if (editValue is sbyte)
                value = Convert.ToInt64((sbyte)editValue);
            else if (editValue is short)
                value = Convert.ToInt64((short)editValue);
            else if (editValue is ushort)
                value = Convert.ToInt64((ushort)editValue);
            else if (editValue is int)
                value = Convert.ToInt64((int)editValue);
            else if (editValue is uint)
                value = Convert.ToInt64((uint)editValue);
            else if (editValue is long)
                value = (long)editValue;
            else if (editValue == null)
                value = Control.MinimumValue;
            else
                throw new SourceGridException("Invalid value: " + editValue.GetType().Name);

            Control.Value = value;
        }

        /// <summary>
        /// Returns the value inserted with the current editor control
        /// </summary>
        /// <returns></returns>
        public override object GetEditedValue()
        {
            Control.EndEditMode(true);

            if (ValueType == null)
                return Control.Value;
            if (ValueType == typeof(byte))
                return Convert.ToByte(Control.Value);
            if (ValueType == typeof(sbyte))
                return Convert.ToSByte(Control.Value);
            if (ValueType == typeof(short))
                return Convert.ToInt16(Control.Value);
            if (ValueType == typeof(ushort))
                return Convert.ToUInt16(Control.Value);
            if (ValueType == typeof(int))
                return Convert.ToInt32(Control.Value);
            if (ValueType == typeof(uint))
                return Convert.ToUInt32(Control.Value);
            if (ValueType == typeof(long))
                return Control.Value;

            throw new SourceGridException("Invalid value type: " + ValueType.Name);
        }

        protected override void OnSendCharToEditor(char key)
        {
            if (Control.IsInEditMode) return;
            if (key >= 48 && key <= 57)
                Control.StartEditMode(key - 48);
        }
    }
}
