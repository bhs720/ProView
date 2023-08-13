
namespace ProView
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.ContextMenuStrip ctxSplitter;
        private System.Windows.Forms.ToolStripMenuItem ctxSplitterHoriz;
        private System.Windows.Forms.ToolStripMenuItem ctxSplitterVert;
        //private ProView.PVImageViewer pvImageViewer;
        private ProView.PVDataViewer pvDataViewer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
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
            this.splitter = new System.Windows.Forms.SplitContainer();
            //this.pvImageViewer = new ProView.PVImageViewer();
            this.pvDataViewer = new ProView.PVDataViewer();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxSplitter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxSplitterHoriz = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSplitterVert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuMainEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainEditSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainHelpWebsite = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvDataViewer)).BeginInit();
            this.ctxSplitter.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitter.Location = new System.Drawing.Point(0, 24);
            this.splitter.Margin = new System.Windows.Forms.Padding(0);
            this.splitter.Name = "splitter";
            this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            //this.splitter.Panel1.Controls.Add(this.pvImageViewer);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.pvDataViewer);
            this.splitter.Size = new System.Drawing.Size(564, 337);
            this.splitter.SplitterDistance = 169;
            this.splitter.SplitterWidth = 7;
            this.splitter.TabIndex = 0;
            this.splitter.TabStop = false;
            this.splitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitterMouseDown);
            // 
            // pvImageViewer
            // 
            //this.pvImageViewer.CausesValidation = false;
            //this.pvImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.pvImageViewer.Location = new System.Drawing.Point(0, 0);
            //this.pvImageViewer.Name = "pvImageViewer";
            //this.pvImageViewer.PVMainForm = null;
            //this.pvImageViewer.Size = new System.Drawing.Size(564, 169);
            //this.pvImageViewer.TabIndex = 0;
            // 
            // pvDataViewer
            // 
            this.pvDataViewer.AllowUserToAddRows = false;
            this.pvDataViewer.AllowUserToResizeRows = false;
            this.pvDataViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pvDataViewer.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.pvDataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pvDataViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFile});
            this.pvDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvDataViewer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.pvDataViewer.Location = new System.Drawing.Point(0, 0);
            this.pvDataViewer.Name = "pvDataViewer";
            //this.pvDataViewer.PVImageViewer = null;
            this.pvDataViewer.PVMainForm = null;
            this.pvDataViewer.RowHeadersVisible = false;
            this.pvDataViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pvDataViewer.Size = new System.Drawing.Size(564, 161);
            this.pvDataViewer.TabIndex = 0;
            // 
            // colFile
            // 
            this.colFile.HeaderText = "File";
            this.colFile.Name = "colFile";
            this.colFile.ReadOnly = true;
            this.colFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ctxSplitter
            // 
            this.ctxSplitter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxSplitterHoriz,
            this.ctxSplitterVert});
            this.ctxSplitter.Name = "ctxSplitter";
            this.ctxSplitter.Size = new System.Drawing.Size(130, 48);
            // 
            // ctxSplitterHoriz
            // 
            this.ctxSplitterHoriz.Name = "ctxSplitterHoriz";
            this.ctxSplitterHoriz.Size = new System.Drawing.Size(129, 22);
            this.ctxSplitterHoriz.Text = "Horizontal";
            this.ctxSplitterHoriz.Click += new System.EventHandler(this.ctxSplitterHorizClick);
            // 
            // ctxSplitterVert
            // 
            this.ctxSplitterVert.Name = "ctxSplitterVert";
            this.ctxSplitterVert.Size = new System.Drawing.Size(129, 22);
            this.ctxSplitterVert.Text = "Vertical";
            this.ctxSplitterVert.Click += new System.EventHandler(this.ctxSplitterVertClick);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainEdit,
            this.mnuMainHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(564, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuMainEdit
            // 
            this.mnuMainEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainEditSettings});
            this.mnuMainEdit.Name = "mnuMainEdit";
            this.mnuMainEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuMainEdit.Text = "&Edit";
            // 
            // mnuMainHelp
            // 
            this.mnuMainHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainHelpWebsite});
            this.mnuMainHelp.Name = "mnuMainHelp";
            this.mnuMainHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuMainHelp.Text = "&Help";
            // 
            // mnuMainEditSettings
            // 
            this.mnuMainEditSettings.Name = "mnuMainEditSettings";
            this.mnuMainEditSettings.Size = new System.Drawing.Size(180, 22);
            this.mnuMainEditSettings.Text = "&Settings";
            this.mnuMainEditSettings.Click += new System.EventHandler(this.mnuMainEditSettings_Click);
            // 
            // mnuMainHelpWebsite
            // 
            this.mnuMainHelpWebsite.Name = "mnuMainHelpWebsite";
            this.mnuMainHelpWebsite.Size = new System.Drawing.Size(180, 22);
            this.mnuMainHelpWebsite.Text = "ProView Website";
            this.mnuMainHelpWebsite.Click += new System.EventHandler(this.mnuMainHelpWebsite_Click);
            // 
            // PVMainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 361);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.splitter);
            this.Icon = global::ProView.Properties.Resources.ProViewIcon;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "PVMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ProView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PVMainFormClosing);
            this.Load += new System.EventHandler(this.PVMainFormLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PVMainFormDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PVMainFormDragEnter);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pvDataViewer)).EndInit();
            this.ctxSplitter.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuMainEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuMainEditSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuMainHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuMainHelpWebsite;
    }
}
