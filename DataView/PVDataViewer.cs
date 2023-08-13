using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Data.Odbc;
using System.Data;
using System.IO;
using CsvHelper;

namespace ProView
{
	public partial class PVDataViewer : DataGridView
	{
		//public PVImageViewer PVImageViewer { get; set; }
		public MainForm PVMainForm { get; set; }
		
		#region Constructor
		public PVDataViewer()
		{
			InitializeComponent();
			
			CellBeginEdit += delegate { SelectionMode = DataGridViewSelectionMode.CellSelect; };
            
			CellEndEdit += delegate { SelectionMode = DataGridViewSelectionMode.FullRowSelect; };
		}
		#endregion
		
		#region User preferences
		public void LoadUserSettings()
		{
			//DefaultCellStyle.Font = Properties.DataGrid.Default.Font;
		}
		#endregion
		
		#region User Columns, Job Settings
		public string LoadedJobName { get; private set; }
		
		public void MainFormClosing(FormClosingEventArgs e)
		{
			if (GridContainsUserData)
			{
				var result = MessageBox.Show("Information you entered will be lost. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}
			}
			PromptUserToSaveJob();
		}
		
		public void MainFormLoad(EventArgs e)
		{
			LoadUserSettings();
			var uCol = new UserColumn();
			uCol.Name = "colDefault";
			uCol.HeaderText = "Field1";
			Columns.Add(uCol);
		}
		
		public void PromptUserToSaveJob()
		{
			if (!string.IsNullOrEmpty(LoadedJobName))
			{
				var result = MessageBox.Show("Save changes to job " + LoadedJobName + "?", "Save changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.Yes)
					JobManager.SaveJob(LoadedJobName, UserFields);
			}
		}
		
		public void LoadJobSettings(string jobName)
		{
			PromptUserToSaveJob();
			
			var fields = JobManager.LoadJob(jobName);
			if (fields == null || fields.Count < 1)
			{
				MessageBox.Show("Could not load job settings.", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			foreach (var uCol in UserColumns)
				Columns.Remove(uCol);
			foreach (var field in fields)
			{
				var uCol = new UserColumn();
				uCol.Field = field;
				Columns.Add(uCol);
			}
			LoadedJobName = jobName;
		}
		
		void ExportDataGrid()
		{
			//var result = exportDialog.ShowDialog(PVMainForm);
			//if (result != DialogResult.OK)
			//	return;
			//try
			//{
   //             using (var writer = new CsvWriter(File.AppendText(exportDialog.FileName)))
   //             {
   //                 writer.Configuration.Encoding = System.Text.Encoding.UTF8;
   //                 //writer.Configuration.Delimiter = "|";
   //                 //writer.Configuration.QuoteNoFields = true;
   //                 foreach (DataGridViewRow row in Rows)
   //                 {
   //                     foreach (DataGridViewCell cell in row.Cells)
   //                     {
   //                         writer.WriteField(cell.Value as string);
   //                     }
   //                     writer.NextRecord();
   //                 }
   //             }
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message, "Problem writing CSV file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	return;
			//}
			//result = MessageBox.Show("Successfully wrote CSV file.\n\nOpen the file now?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
			//if (result == DialogResult.Yes)
			//	Process.Start(exportDialog.FileName);
		}
		
		public void LoadCsvDataSource(string csvFile)
		{
			//foreach (DataGridViewColumn col in Columns)
			//{
			//	Columns.Remove(col);
			//}
			
			//try
			//{
			//	var folder = Path.GetDirectoryName(csvFile);
			//	var fileName = Path.GetFileName(csvFile);
			//	var connection = new OdbcConnection(@"Driver={Microsoft Text Driver (*.txt; *.csv)};DBQ=" + @folder);
			//	var adapter = new OdbcDataAdapter("SELECT * FROM [" + fileName + "]", connection);
			//	var dataTable = new DataTable();
			//	adapter.Fill(dataTable);
			//	DataSource = dataTable;
				
			//	adapter.Dispose();
			//	connection.Close();
			//	connection.Dispose();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message, "Problem connecting to data source", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			
		}
		
		public bool GridContainsUserData
		{
			get
			{
				foreach (DataGridViewRow row in Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{
						var uCol = cell.OwningColumn as UserColumn;
						if (uCol != null && !string.IsNullOrEmpty(cell.Value as string))
							return true;
					}
				}
				return false;
			}
		}
		
		public List<UserColumn> UserColumnsModified
		{
			get
			{
				var modifiedColumns = new List<UserColumn>();
				foreach (var uCol in UserColumns)
				{
					if (uCol.Field.WasModified)
						modifiedColumns.Add(uCol);
				}
				return modifiedColumns;
			}
		}
		
		public List<UserColumn> UserColumns
		{
			get
			{
				var userColumns = new List<UserColumn>();
				foreach (DataGridViewColumn col in Columns)
				{
					var uCol = col as UserColumn;
					if (uCol != null)
						userColumns.Add(uCol);
				}
				return userColumns;
			}
		}
		
		public List<Field> UserFields
		{
			get
			{
				var userFields = new List<Field>();
				foreach (var uCol in UserColumns)
				{
					userFields.Add(uCol.Field);
				}
				return userFields;
			}
		}
		#endregion

		#region Image viewer operations
		
		DataGridViewRow loadedRow;
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
					//PVImageViewer.SetFile(null);
					loadedRow = null;
				}
				else
				{
					//PVImageViewer.SetFile(value.Tag as PVFile);
					loadedRow = value;
					loadedRow.DefaultCellStyle.BackColor = Properties.DataGrid.Default.HighlightRowBG;
					loadedRow.DefaultCellStyle.ForeColor = Properties.DataGrid.Default.HighlightRowFG;
				}
			}
		}
		
		void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
				LoadedRow = Rows[e.RowIndex];
		}
		
		void HandleRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			// Did the user remove LoadedRow?
			if (!Rows.Contains(LoadedRow))
				LoadedRow = null;
		}
		#endregion

		#region Context Menu DataGrid Operations
		
		DataGridViewCell cellRightClicked;
		
		void HandleMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
			{
				itmApplyAll.Enabled = Columns[e.ColumnIndex] is UserColumn;
				itmApplyDown.Enabled = Columns[e.ColumnIndex] is UserColumn;
                itmApplyUp.Enabled = Columns[e.ColumnIndex] is UserColumn;


				/*if (CurrentCell == null || CurrentCell != this[e.ColumnIndex, e.RowIndex] || !CurrentCell.IsInEditMode) */
				
                {
					if (!Rows[e.RowIndex].Selected)
					{
						CurrentCell = null;
						ClearSelection();
						Rows[e.RowIndex].Selected = true;
					}
					cellRightClicked = this[e.ColumnIndex, e.RowIndex];
                    
					ctxDataGrid.Show(Cursor.Position);
				}
                 
			}
		}
		
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
			foreach (DataGridViewRow row in this.Rows)
			{
				row.Cells[cellRightClicked.ColumnIndex].Value = cellRightClicked.Value as string;
			}
		}
		
		void ItmApplyDownClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.Rows)
			{
				if (row.Index > cellRightClicked.RowIndex)
					row.Cells[cellRightClicked.ColumnIndex].Value = cellRightClicked.Value as string;
			}
		}
		
		void ItmApplyUpClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.Rows)
			{
				if (row.Index < cellRightClicked.RowIndex)
					row.Cells[cellRightClicked.ColumnIndex].Value = cellRightClicked.Value as string;
			}
		}

		void itmRename_Click(object sender, EventArgs e)
		{
			RenameFiles();
		}
		
		void ItmExportClick(object sender, EventArgs e)
		{
			ExportDataGrid();
		}

		#endregion
		
		#region Context Menu Field Editor
		DataGridViewColumn colClicked;
		void HandleColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				colClicked = Columns[e.ColumnIndex];
				ctxDeleteField.Enabled = colClicked is UserColumn;
				ctxEditField.Enabled = colClicked is UserColumn;
				ctxFieldMgmt.Show(Cursor.Position);
			}
		}
		void CtxNewFieldClick(object sender, System.EventArgs e)
		{
			var uCol = new UserColumn();
			if (colClicked is UserColumn)
				Columns.Insert(colClicked.Index, uCol);
			else
				Columns.Add(uCol);
			
			uCol.Field.Name = "Field" + UserColumns.Count;
			uCol.HeaderText = uCol.Field.Name;
		}
		
		void CtxEditFieldClick(object sender, System.EventArgs e)
		{
			var uCol = colClicked as UserColumn;
			using (var fieldEditor = new FieldEditor())
			{
				fieldEditor.Field = uCol.Field.Clone();
				var result = fieldEditor.ShowDialog(this);
				if (result == DialogResult.OK)
					uCol.Field = fieldEditor.Field;
			}
		}
		void CtxDeleteFieldClick(object sender, System.EventArgs e)
		{
			var uCol = colClicked as UserColumn;
			if (uCol != null)
			{
				var result = MessageBox.Show("Are you sure you want to delete " + uCol.Field.Name + "?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.Yes)
					Columns.Remove(uCol);
			}
			
		}
		#endregion
		
		#region File Operations
		
		public void AddItem(PVFile pvFile)
		{
			// Check if the file has already been added
			foreach (DataGridViewRow row in Rows)
			{
				var existingFile = row.Tag as PVFile;
				if (existingFile.FileName == pvFile.FileName)
					throw new Exception("The file is already in the list.");
			}
			int rowIndex = Rows.Add(pvFile.FileName);
			Rows[rowIndex].Tag = pvFile;
		}
		
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
					if (cell.OwningColumn is UserColumn)
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

				string newFileNameNoExt = dgvr.GetConcatenatedColumnValue("_");
				if (string.IsNullOrEmpty(newFileNameNoExt))
					continue;
				
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
			if (CurrentCell != null)
			{
				Debug.WriteLine(msg + "  " + keyData);
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

                // Give editing control a chance to handle [Tab] key

                if (keyData == Keys.Tab)
                {
                    var editingControl = EditingControl as XEditingControl;
                    if (EditingControl != null)
                    {
                        if (editingControl.HandledTabKey())
                            return true;
                    }
                }
				
				// Move down one row
				if (keyData == Keys.Enter)
				{
                    var editingControl = EditingControl as XEditingControl;
                    if (EditingControl != null)
                    {
                        if (editingControl.HandledEnterKey())
                            return true;
                    }

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
						BeginEdit(true);
						EditMode = DataGridViewEditMode.EditOnEnter;
					}
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		
		#endregion

    }
}
