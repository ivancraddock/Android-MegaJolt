#region USING STATEMENTS
using System;
using System.Windows.Forms;
#endregion USING STATEMENTS

namespace PcJolt
{
    #region DataGridViewNumericUpDownColumn Class
    public class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        #region DataGridViewNumericUpDownColumn()
        public DataGridViewNumericUpDownColumn() : base(new DataGridViewNumericUpDownCell()) { }
        #endregion DataGridViewNumericUpDownColumn

        #region PROPERTIES
        #region MinimumValue Property
        public int MinimumValue { get; set; }
        #endregion MinimumValue
        #region MaximumValue Property
        public int MaximumValue { get; set; }
        #endregion MaximumValue
        #region CellTemplate Property
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                // Ensure that the cell used for the template is a DataGridViewNumericUpDownCell.
                DataGridViewNumericUpDownCell dataGridViewNumericUpDownCell = value as DataGridViewNumericUpDownCell;
                if (dataGridViewNumericUpDownCell == null)
                    throw new InvalidCastException("Must be a DataGridViewNumericUpDownCell");

                dataGridViewNumericUpDownCell.MinimumValue = MinimumValue;
                dataGridViewNumericUpDownCell.MaximumValue = MaximumValue;
                base.CellTemplate = value;
            }
        }
        #endregion CellTemplate
        #endregion PROPERTIES
    }
    #endregion DataGridViewNumericUpDownColumn
    #region DataGridViewNumericUpDownColumnExtension Class
    public static class DataGridViewNumericUpDownColumnExtension
    {
        #region SetMinAndMax(this, int, int)
        public static void SetMinAndMax(this DataGridViewNumericUpDownColumn column, int min, int max)
        {
            DataGridViewNumericUpDownCell cellTemplate = ((DataGridViewNumericUpDownCell)column.CellTemplate);
            cellTemplate.MinimumValue = min;
            cellTemplate.MaximumValue = max;
        }
        #endregion SetMinAndMax
    }
    #endregion DataGridViewNumericUpDownColumnExtension
    #region DataGridViewNumericUpDownCell Class
    public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        #region DataGridViewNumericUpDownCell()
        public DataGridViewNumericUpDownCell()
        {
            Style.Format = "0";
        }
        #endregion DataGridViewNumericUpDownCell

        #region PROPERTIES
        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }
        public override Type EditType { get { return typeof(NumericUpDownEditingControl); } }
        public override Type ValueType { get { return typeof(int); } }
        public override object DefaultNewRowValue { get { return DefaultValue; } }
        #endregion PROPERTIES

        #region FIELDS
        private const int DefaultValue = 0;
        #endregion FIELDS

        #region InitializeEditingControl(int, object, DataGridViewCellStyle)
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            NumericUpDownEditingControl ctl = (NumericUpDownEditingControl)DataGridView.EditingControl;
            DataGridViewNumericUpDownCell cellTemplate = (DataGridViewNumericUpDownCell)DataGridView.Columns[ColumnIndex].CellTemplate;
            ctl.Maximum = cellTemplate.MaximumValue;
            ctl.Minimum = cellTemplate.MinimumValue;

            // Use the default row value when Value property is null.
            ctl.Value = Value == null ? DefaultValue : Convert.ToInt32(Value);
            ctl.Select(0, ctl.Value.ToString().Length);

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }
        #endregion InitializeEditingControl
    }
    #endregion DataGridViewNumericUpDownCell
    #region NumericUpDownEditingControl Class
    internal class NumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        #region NumericUpDownEditingControl()
        public NumericUpDownEditingControl()
        {
            DecimalPlaces = 0;
        }
        #endregion NumericUpDownEditingControl

        #region PROPERTIES
        #region EditingControlFormattedValue Property
        public object EditingControlFormattedValue
        {
            get
            {
                return Value.ToString("0"); 
            }
            set
            {
                int v;
                Value = int.TryParse(value.ToString(), out v) ? v : 0;
            }
        }
        #endregion EditingControlFormattedValue
        #region GetEditingControlFormattedValue(DataGridViewDataErrorContexts)
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) { return EditingControlFormattedValue; }
        #endregion GetEditingControlFormattedValue
        #region EditingControlRowIndex Property
        public int EditingControlRowIndex { get; set; }
        #endregion EditingControlRowIndex
        #region RepositionEditingControlOnValueChange Property
        public bool RepositionEditingControlOnValueChange { get { return false; } }
        #endregion RepositionEditingControlOnValueChange
        #region EditingControlDataGridView Property
        public DataGridView EditingControlDataGridView { get; set; }
        #endregion EditingControlDataGridView
        #region EditingControlValueChanged Property
        public bool EditingControlValueChanged { get; set; }
        #endregion EditingControlValueChanged
        #region EditingPanelCursor Property
        public Cursor EditingPanelCursor { get { return base.Cursor; } }
        #endregion EditingPanelCursor
        #endregion PROPERTIES

        #region METHODS
        #region ApplyCellStyleToEditingControl(DataGridViewCellStyle)
        // Implements the IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
            TextAlign = HorizontalAlignment.Center;
        }
        #endregion ApplyCellStyleToEditingControl
        #region EditingControlWantsInputKey(Keys, bool)
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }
        #endregion EditingControlWantsInputKey
        #region PrepareEditingControlForEdit(bool)
        public void PrepareEditingControlForEdit(bool selectAll) { }
        #endregion PrepareEditingControlForEdit
        #endregion METHODS

        #region OnValueChanged(EventArgs)
        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell have changed.
            EditingControlValueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
        #endregion OnValueChanged
    }
    #endregion NumericUpDownEditingControl
}