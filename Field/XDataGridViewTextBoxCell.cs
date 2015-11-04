using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProView
{
    public class XDataGridViewTextBoxCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            //var ctl = DataGridView.EditingControl as XEditingControl;
            //var owner = OwningColumn as UserColumn;
            //ctl.Field = owner.Field;

        }
        UserColumn Owner { get { return OwningColumn as UserColumn; } }

        public override Type EditType { get { return typeof(XEditingControl); } }
        public override Type ValueType { get { return typeof(string); } }
        public override object DefaultNewRowValue { get { return ""; } }

    }
}
