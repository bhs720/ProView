using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ProView
{
	public partial class PVDataViewer : DataGridView
	{
		DataGridViewRow loadedRow;
		
		public PVDataViewer()
		{
			InitializeComponent();
			
			CellBeginEdit += delegate { SelectionMode = DataGridViewSelectionMode.CellSelect; };
			CellEndEdit += delegate { SelectionMode = DataGridViewSelectionMode.FullRowSelect; };
			
		}
		
		public void InitUserSettings()
		{
			DefaultCellStyle.Font = Properties.DataGrid.Default.Font;
		}

		public PVImageViewer pvImageViewer { get; set; }
		
		public bool GridContainsUserData
		{
			get
			{
				foreach (DataGridViewRow row in Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{
						if (cell.OwningColumn is XDataGridViewColumn && cell.Value != null)
							return true;
					}
				}
				return false;
			}
		}
		
		/// <summary>
		/// A list of columns that are editable by the user (Field Columns)
		/// </summary>
		public List<XDataGridViewColumn> UserColumns
		{
			get
			{
				var userColumns = new List<XDataGridViewColumn>();
				foreach (DataGridViewColumn col in Columns)
				{
					var xCol = col as XDataGridViewColumn;
					if (xCol != null)
						userColumns.Add(xCol);
				}
				return userColumns;
			}
		}

		public void AddItem(PVFile pvFile)
		{
			// Check if the file has already been added
			foreach (DataGridViewRow dgvr in Rows)
			{
				var existingFile = dgvr.Tag as PVFile;
				if (existingFile.FileName == pvFile.FileName)
					throw new Exception("The file has already been added.");
			}
			int rowNum = Rows.Add(pvFile.FileName);
			Rows[rowNum].Tag = pvFile;
		}
		
		DataGridViewRow LoadedRow
		{
			get
			{
				return loadedRow;
			}
			set
			{
				if (loadedRow != null)
				{
					loadedRow.DefaultCellStyle.BackColor = DefaultCellStyle.BackColor;
					loadedRow.DefaultCellStyle.ForeColor = DefaultCellStyle.ForeColor;
				}
				if (value == null)
				{
					pvImageViewer.SetFile(null);
					loadedRow = null;
				}
				else
				{
					pvImageViewer.SetFile(value.Tag as PVFile);
					loadedRow = value;
					loadedRow.DefaultCellStyle.BackColor = Properties.DataGrid.Default.HighlightRowBG;
					loadedRow.DefaultCellStyle.ForeColor = Properties.DataGrid.Default.HighlightRowFG;
				}
			}
		}
		
		void HandleEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var xCol = CurrentCell.OwningColumn as XDataGridViewColumn;
			if (xCol != null && xCol.Field.SourceMode == SourceMode.Append)
			{
				var tBox = e.Control as TextBox;
				tBox.Multiline = false;
				tBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				tBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
				tBox.AutoCompleteCustomSource = xCol.Field.Source;
			}
			else
			{
				var tBox = e.Control as TextBox;
				tBox.AutoCompleteCustomSource = null;
			}
		}
		
		void HandleCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var xCol = CurrentCell.OwningColumn as XDataGridViewColumn;
			if (xCol != null && xCol.Field.SourceMode == SourceMode.Append)
			{
				string value = this[e.ColumnIndex, e.RowIndex].Value as string;
				if (!string.IsNullOrWhiteSpace(value))
				{
					bool exists = false;
					foreach (string entered in xCol.Field.Source)
					{
						exists = string.Equals(entered, value, StringComparison.CurrentCultureIgnoreCase);
						if (exists)
						{
							this[e.ColumnIndex, e.RowIndex].Value = entered;
							break;
						}
					}
					if (!exists)
						xCol.Field.Source.Add(value);
				}
			}	
		}
		
		
		#region Context Menu DataGrid Operations
		
		void itmSelectAll_Click(object sender, EventArgs e)
		{
			//EndEdit();
			SelectAll();
		}

		void itmRemoveSelected_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow dgvr in SelectedRows)
			{
				Rows.Remove(dgvr);
			}
		}

		void ItmApplyAllClick(object sender, EventArgs e)
		{
			string value = cellClicked.Value as string;
			foreach (DataGridViewRow row in Rows)
			{
				row.Cells[cellClicked.ColumnIndex].Value = value;
			}
		}
		void ItmApplyDownClick(object sender, EventArgs e)
		{
			string value = cellClicked.Value as string;
			for (int i = cellClicked.RowIndex; i < Rows.Count; i++)
			{
				this[cellClicked.ColumnIndex, i].Value = value;
			}
		}
		void itmExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.AddExtension = true;
			sfd.FileName = "export.xml";
			sfd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			sfd.DefaultExt = "xml";
			sfd.OverwritePrompt = true;
			sfd.ShowDialog(this);
		}

		void itmRename_Click(object sender, EventArgs e)
		{
			RenameFiles();
		}

		#endregion
		
		#region Context Menu Field Editor
		DataGridViewColumn colClicked;
		void HandleColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				colClicked = Columns[e.ColumnIndex];
				if (colClicked is XDataGridViewColumn)
				{
					ctxDeleteField.Enabled = true;
					ctxEditField.Enabled = true;
				}
				else
				{
					ctxDeleteField.Enabled = false;
					ctxEditField.Enabled = false;
				}
				ctxFieldMgmt.Show(Cursor.Position);
			}
		}
		void CtxNewFieldClick(object sender, System.EventArgs e)
		{
			var xCol = new XDataGridViewColumn();
			if (colClicked is XDataGridViewColumn)
				Columns.Insert(colClicked.Index, xCol);
			else
				Columns.Add(xCol);
			
			xCol.Field.Name = "Field" + UserColumns.Count;
			xCol.HeaderText = xCol.Field.Name;
		}
		
		void CtxEditFieldClick(object sender, System.EventArgs e)
		{
			var col = colClicked as XDataGridViewColumn;
			if (col != null)
			{
				using (var fieldEditor = new FieldEditor())
				{
					fieldEditor.Field = col.Field.Clone();
					var dr = fieldEditor.ShowDialog(this);
					if (dr == DialogResult.OK)
						col.Field = fieldEditor.Field;
				}
			}
		}
		void CtxDeleteFieldClick(object sender, System.EventArgs e)
		{
			if (colClicked is XDataGridViewColumn)
			{
				Columns.Remove(colClicked);
			}
		}
		#endregion

		#region Mouse Events
		void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
				LoadedRow = Rows[e.RowIndex];
		}
		DataGridViewCell cellClicked;
		void HandleMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
			{
				itmApplyAll.Enabled = !Columns[e.ColumnIndex].ReadOnly;
				itmApplyDown.Enabled = !Columns[e.ColumnIndex].ReadOnly;
				
				if (CurrentCell == null || CurrentCell != this[e.ColumnIndex, e.RowIndex] || !CurrentCell.IsInEditMode)
				{
					if (!Rows[e.RowIndex].Selected)
					{
						CurrentCell = null;
						ClearSelection();
						Rows[e.RowIndex].Selected = true;
					}
					cellClicked = this[e.ColumnIndex, e.RowIndex];
					ctxDataGrid.Show(Cursor.Position);
				}
			}
		}
		#endregion
		
		#region Row Events
		void HandleRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Did the user remove LoadedRow?
			if (!Rows.Contains(LoadedRow))
				LoadedRow = null;
		}
		#endregion
		
		#region Rename Operation
		ProgressWindow progressWindow;
		List<DataGridViewRow> succeeded = new List<DataGridViewRow>();
		public void RenameFiles()
		{
			var renameRows = new List<DataGridViewRow>();
			foreach (DataGridViewRow row in SelectedRows)
				renameRows.Add(row);
			
			if (renameRows.Count == 0)
				return;
			
			var result = MessageBox.Show("Click OK to rename " + renameRows.Count + " file(s).", "ProView", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
			if (result == DialogResult.Cancel)
				return;

			if (renameRows.Contains(LoadedRow))
				LoadedRow = null;
			
			renameRows = renameRows.OrderBy(dgvr => dgvr.Index).ToList();
			
			progressWindow = new ProgressWindow();
			progressWindow.Text = "Renaming " + renameRows.Count + " file(s)";

			var renameFilesBgWorker = new BackgroundWorker();
			renameFilesBgWorker.WorkerReportsProgress = true;
			renameFilesBgWorker.WorkerSupportsCancellation = true;
			renameFilesBgWorker.DoWork += renameFilesBgWorker_DoWork;
			renameFilesBgWorker.ProgressChanged += renameFilesBgWorker_ProgressChanged;
			renameFilesBgWorker.RunWorkerCompleted += renameFilesBgWorker_RunWorkerCompleted;
			renameFilesBgWorker.RunWorkerAsync(renameRows);

			progressWindow.BtnCancel.Click += delegate { renameFilesBgWorker.CancelAsync(); };
			progressWindow.ShowDialog(this);
		}
		void renameFilesBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
			else if (e.Cancelled)
			{
				//
			}
			else
			{
				
				// Finally, handle the case where the operation
				// succeeded.
				
			}

			foreach (DataGridViewRow dgvr in succeeded)
			{
				var pvFile = dgvr.Tag as PVFile;
				dgvr.Cells["colFile"].Value = pvFile.FileName;
				foreach (DataGridViewCell cell in dgvr.Cells)
				{
					if (cell.OwningColumn is XDataGridViewColumn)
						cell.Value = "";
				}
			}
			succeeded.Clear();
			progressWindow.Close();
			progressWindow.Dispose();
		}

		void renameFilesBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressWindow.ProgressBar.Value = e.ProgressPercentage;
		}

		void renameFilesBgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker;
			var renameRows = e.Argument as List<DataGridViewRow>;
			
			int progressCounter = 1;
			foreach (DataGridViewRow dgvr in renameRows)
			{
				worker.ReportProgress(progressCounter++ * 100 / renameRows.Count);
				
				if (worker.CancellationPending)
				{
					//e.Cancel = true;
					break;
				}

				string newFileNameNoExt = "";
				foreach (DataGridViewCell cell in dgvr.Cells)
				{
					if (cell.OwningColumn is XDataGridViewColumn)
						newFileNameNoExt += cell.Value;
				}
				var pvFile = dgvr.Tag as PVFile;

				bool success = false;
				while (!success)
				{
					try
					{
						pvFile.Rename(newFileNameNoExt);
					}
					catch (Exception ex)
					{
						DialogResult dr = MessageBox.Show(pvFile.FileName + "\n\n" + ex.Message + "\n\n" +
						                                  "Abort to stop renaming\n" +
						                                  "Retry to try renaming the file again\n" +
						                                  "Ignore to continue renaming other files",
						                                  "Problem", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
						if (dr == DialogResult.Abort)
						{
							e.Cancel = true;
							return;
						}
						if (dr == DialogResult.Retry)
							continue;
						if (dr == DialogResult.Ignore)
							break;
					}
					success = true;
				}
				if (success)
					succeeded.Add(dgvr);
			}
		}
		#endregion

		#region Keyboard Shortcuts
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			System.Diagnostics.Debug.WriteLine(msg + "  " + keyData);
			if (CurrentCell != null)
			{
				int rowIndex = CurrentCell.RowIndex;

				// Move up one row
				if (keyData == (Keys.Shift | Keys.Enter))
				{
					if (rowIndex > 0)
					{
						LoadedRow = Rows[rowIndex - 1];
						CurrentCell = this[CurrentCell.ColumnIndex, rowIndex - 1];
					}
					return true;
				}
				
				// Move down one row
				if (keyData == Keys.Enter)
				{
					if (rowIndex + 1 < Rows.Count)
					{
						LoadedRow = Rows[rowIndex + 1];
						CurrentCell = this[CurrentCell.ColumnIndex, rowIndex + 1];
					}
					else
					{
						CurrentCell = null;
					}
					return true;
				}
				
				// Copy value from above cell
				if (keyData == (Keys.Control | Keys.D))
				{
					if (rowIndex > 0 && CurrentCell.IsInEditMode)
					{
						EditMode = DataGridViewEditMode.EditProgrammatically;
						EndEdit(DataGridViewDataErrorContexts.Commit);
						CurrentCell.Value = this[CurrentCell.ColumnIndex, rowIndex - 1].Value;
						BeginEdit(false);
						EditMode = DataGridViewEditMode.EditOnEnter;
					}
					return true;
				}
				
				// Insert underscore character
				if (keyData == (Keys.Subtract | Keys.Shift))
				{
					SendKeys.Send("_");
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		
		
		#endregion

	}
}
