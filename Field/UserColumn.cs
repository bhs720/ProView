using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace ProView
{
	public class UserColumn : DataGridViewTextBoxColumn
	{
		public UserColumn()
		{
			SortMode = DataGridViewColumnSortMode.NotSortable;
			field = new Field();
			CellTemplate = new XDataGridViewTextBoxCell();
            
		}
		
		Field field;
		public Field Field
		{
			get
			{
				return field;
			}
			set
			{
				field = value;
				HeaderText = field.Name ?? "Field1";
			}
		}
		
		public override object Clone()
		{
			return base.Clone() as UserColumn;
		}
	}
	
	
	
	
	
}
