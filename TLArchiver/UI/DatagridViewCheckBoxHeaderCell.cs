using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TLArchiver.UI
{
    // CheckBox Header Column For DataGridView
    // http://www.codeproject.com/Articles/20165/CheckBox-Header-Column-For-DataGridView
    // ---

    public delegate void CheckBoxClickedHandler(bool state);

    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }

    class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        Point _cellLocation = new Point();
        public event CheckBoxClickedHandler OnCheckBoxClicked;

        public bool Checked { get; private set; }
        public CheckBoxState CheckState { get; private set; }
        public bool IsDirty { get; private set; }

        public DatagridViewCheckBoxHeaderCell()
        {
            Checked = false;
            CheckState = CheckBoxState.UncheckedNormal;
            IsDirty = false;
        }

        public void SetDirty()
        {
            IsDirty = true;
            CheckState = CheckBoxState.MixedNormal;
            DataGridView.InvalidateCell(this); // Trigger the Paint() event
        }

        protected override void Paint(System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X +
                (cellBounds.Width / 2) - (s.Width / 2);
            p.Y = cellBounds.Location.Y +
                (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (IsDirty)
                CheckState = CheckBoxState.MixedNormal;
            else if (Checked)
                CheckState = CheckBoxState.CheckedNormal;
            else
                CheckState = CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, CheckState);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <=
                checkBoxLocation.X + checkBoxSize.Width
            && p.Y >= checkBoxLocation.Y && p.Y <=
                checkBoxLocation.Y + checkBoxSize.Height)
            {
                IsDirty = false;
                Checked = !Checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(Checked);
                    DataGridView.InvalidateCell(this);
                }

            }
            base.OnMouseClick(e);
        }
    }
}
