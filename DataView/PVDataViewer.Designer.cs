/*
 * Created by SharpDevelop.
 * User: ben
 * Date: 6/1/2013
 * Time: 3:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProView
{
	partial class PVDataViewer
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.ctxDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.itmApplyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itmApplyDown = new System.Windows.Forms.ToolStripMenuItem();
            this.itmApplyUp = new System.Windows.Forms.ToolStripMenuItem();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.itmRename = new System.Windows.Forms.ToolStripMenuItem();
            this.sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.itmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxFieldMgmt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxNewField = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxEditField = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDeleteField = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.ctxDataGrid.SuspendLayout();
            this.ctxFieldMgmt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ctxDataGrid
            // 
            this.ctxDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSelectAll,
            this.itmRemoveSelected,
            this.sep1,
            this.itmApplyAll,
            this.itmApplyDown,
            this.itmApplyUp,
            this.sep2,
            this.itmRename,
            this.sep3,
            this.itmExport});
            this.ctxDataGrid.Name = "contextMenuStrip1";
            this.ctxDataGrid.ShowImageMargin = false;
            this.ctxDataGrid.ShowItemToolTips = false;
            this.ctxDataGrid.Size = new System.Drawing.Size(157, 176);
            // 
            // itmSelectAll
            // 
            this.itmSelectAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itmSelectAll.Name = "itmSelectAll";
            this.itmSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.itmSelectAll.Size = new System.Drawing.Size(156, 22);
            this.itmSelectAll.Text = "Select All";
            this.itmSelectAll.ToolTipText = "Select all files in the list";
            this.itmSelectAll.Click += new System.EventHandler(this.itmSelectAll_Click);
            // 
            // itmRemoveSelected
            // 
            this.itmRemoveSelected.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itmRemoveSelected.Name = "itmRemoveSelected";
            this.itmRemoveSelected.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.itmRemoveSelected.Size = new System.Drawing.Size(156, 22);
            this.itmRemoveSelected.Text = "Remove";
            this.itmRemoveSelected.ToolTipText = "Remove selected files from the list";
            this.itmRemoveSelected.Click += new System.EventHandler(this.itmRemoveSelected_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(153, 6);
            // 
            // itmApplyAll
            // 
            this.itmApplyAll.Name = "itmApplyAll";
            this.itmApplyAll.Size = new System.Drawing.Size(156, 22);
            this.itmApplyAll.Text = "Apply All";
            this.itmApplyAll.ToolTipText = "Copy the contents of this cell to all cells in this column.";
            this.itmApplyAll.Click += new System.EventHandler(this.ItmApplyAllClick);
            // 
            // itmApplyDown
            // 
            this.itmApplyDown.Name = "itmApplyDown";
            this.itmApplyDown.Size = new System.Drawing.Size(156, 22);
            this.itmApplyDown.Text = "Apply Down";
            this.itmApplyDown.ToolTipText = "Copy the contents of this cell to all of the cells below.";
            this.itmApplyDown.Click += new System.EventHandler(this.ItmApplyDownClick);
            // 
            // itmApplyUp
            // 
            this.itmApplyUp.Name = "itmApplyUp";
            this.itmApplyUp.Size = new System.Drawing.Size(156, 22);
            this.itmApplyUp.Text = "Apply Up";
            this.itmApplyUp.Click += new System.EventHandler(this.ItmApplyUpClick);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(153, 6);
            // 
            // itmRename
            // 
            this.itmRename.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itmRename.Name = "itmRename";
            this.itmRename.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.itmRename.Size = new System.Drawing.Size(156, 22);
            this.itmRename.Text = "Rename Files";
            this.itmRename.ToolTipText = "Rename selected files";
            this.itmRename.Click += new System.EventHandler(this.itmRename_Click);
            // 
            // sep3
            // 
            this.sep3.Name = "sep3";
            this.sep3.Size = new System.Drawing.Size(153, 6);
            // 
            // itmExport
            // 
            this.itmExport.Name = "itmExport";
            this.itmExport.Size = new System.Drawing.Size(156, 22);
            this.itmExport.Text = "Export Data...";
            this.itmExport.Click += new System.EventHandler(this.ItmExportClick);
            // 
            // ctxFieldMgmt
            // 
            this.ctxFieldMgmt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNewField,
            this.ctxEditField,
            this.ctxDeleteField});
            this.ctxFieldMgmt.Name = "ctxFieldMgmt";
            this.ctxFieldMgmt.Size = new System.Drawing.Size(136, 70);
            // 
            // ctxNewField
            // 
            this.ctxNewField.Name = "ctxNewField";
            this.ctxNewField.Size = new System.Drawing.Size(135, 22);
            this.ctxNewField.Text = "New Field";
            this.ctxNewField.Click += new System.EventHandler(this.CtxNewFieldClick);
            // 
            // ctxEditField
            // 
            this.ctxEditField.Name = "ctxEditField";
            this.ctxEditField.Size = new System.Drawing.Size(135, 22);
            this.ctxEditField.Text = "Edit Field";
            this.ctxEditField.Click += new System.EventHandler(this.CtxEditFieldClick);
            // 
            // ctxDeleteField
            // 
            this.ctxDeleteField.Name = "ctxDeleteField";
            this.ctxDeleteField.Size = new System.Drawing.Size(135, 22);
            this.ctxDeleteField.Text = "Delete Field";
            this.ctxDeleteField.Click += new System.EventHandler(this.CtxDeleteFieldClick);
            // 
            // exportDialog
            // 
            this.exportDialog.FileName = "export.csv";
            this.exportDialog.Filter = "CSV files|*.csv|All files|*.*";
            this.exportDialog.OverwritePrompt = false;
            // 
            // PVDataViewer
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToResizeRows = false;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.RowHeadersVisible = false;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HandleCellDoubleClick);
            this.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HandleMouseDown);
            this.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HandleColumnHeaderMouseClick);
            this.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.HandleRowsRemoved);
            this.ctxDataGrid.ResumeLayout(false);
            this.ctxFieldMgmt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.ContextMenuStrip ctxDataGrid;
        private System.Windows.Forms.ToolStripMenuItem itmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem itmRemoveSelected;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripMenuItem itmRename;
        private System.Windows.Forms.ContextMenuStrip ctxFieldMgmt;
        private System.Windows.Forms.ToolStripMenuItem ctxNewField;
        private System.Windows.Forms.ToolStripMenuItem ctxEditField;
        private System.Windows.Forms.ToolStripMenuItem ctxDeleteField;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripMenuItem itmApplyDown;
        private System.Windows.Forms.ToolStripMenuItem itmApplyAll;
        private System.Windows.Forms.SaveFileDialog exportDialog;
        private System.Windows.Forms.ToolStripSeparator sep3;
        private System.Windows.Forms.ToolStripMenuItem itmExport;
        private System.Windows.Forms.ToolStripMenuItem itmApplyUp;
	}
}
