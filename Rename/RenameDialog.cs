using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ProView
{
	/// <summary>
	/// Customize file renaming pattern
	/// </summary>
	public partial class RenameDialog : Form
	{	
		public RenameDialog()
		{
			InitializeComponent();
		}
		
		public RenameDialog(PVDataViewer pvDataViewer)
		{
			InitializeComponent();
			
		}
		
		public class RenameRow : DataGridViewRow
		{
			#region Properties
			public string OldFileName
			{
				get
				{
					return Cells["colOldFileName"].Value as string;
				}
				set
				{
					Cells["colOldFileName"].Value = value;
				}
			}
			public string NewFileName
			{
				get
				{
					return Cells["colNewFileName"].Value as string;
				}
				set
				{
					
					Cells["NewFileName"].Value = value;
				}
			}
			public string Error
			{
				get
				{
					return Cells["colError"].Value as string;
				}
				set
				{
					Cells["colError"].Value = value;
				}
			}
			#endregion
			
			public override object Clone()
			{
				return base.Clone() as RenameRow;
			}
		}
	}
}
