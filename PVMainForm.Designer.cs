
namespace ProView
{
    partial class PVMainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            mnuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            addFilesToolStripMenuItem = new ToolStripMenuItem();
            addFoldersToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            clearAllFilesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            saveCurrentFileToolStripMenuItem = new ToolStripMenuItem();
            saveAllFilesToolStripMenuItem = new ToolStripMenuItem();
            renameFilesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem1 = new ToolStripMenuItem();
            layout1ToolStripMenuItem = new ToolStripMenuItem();
            layout2ToolStripMenuItem = new ToolStripMenuItem();
            gridToolStripMenuItem = new ToolStripMenuItem();
            nextFileToolStripMenuItem = new ToolStripMenuItem();
            previousFileToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            fitToPageToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            rotateRightToolStripMenuItem = new ToolStripMenuItem();
            rotateLeftToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            nextPageToolStripMenuItem = new ToolStripMenuItem();
            previousPageToolStripMenuItem = new ToolStripMenuItem();
            mnuMainEdit = new ToolStripMenuItem();
            mnuMainToolsSettings = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mnuMainHelpKeyboardShortcuts = new ToolStripMenuItem();
            mnuMainHelpAboutProView = new ToolStripMenuItem();
            mnuMain.SuspendLayout();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem1, gridToolStripMenuItem, viewToolStripMenuItem, mnuMainEdit, helpToolStripMenuItem });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Padding = new Padding(7, 2, 0, 2);
            mnuMain.Size = new Size(658, 24);
            mnuMain.TabIndex = 1;
            mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addFilesToolStripMenuItem, addFoldersToolStripMenuItem, toolStripSeparator4, clearAllFilesToolStripMenuItem, toolStripSeparator5, saveCurrentFileToolStripMenuItem, saveAllFilesToolStripMenuItem, renameFilesToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // addFilesToolStripMenuItem
            // 
            addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            addFilesToolStripMenuItem.Size = new Size(213, 22);
            addFilesToolStripMenuItem.Text = "A&dd Files...";
            // 
            // addFoldersToolStripMenuItem
            // 
            addFoldersToolStripMenuItem.Name = "addFoldersToolStripMenuItem";
            addFoldersToolStripMenuItem.Size = new Size(213, 22);
            addFoldersToolStripMenuItem.Text = "Add F&olders...";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(210, 6);
            // 
            // clearAllFilesToolStripMenuItem
            // 
            clearAllFilesToolStripMenuItem.Name = "clearAllFilesToolStripMenuItem";
            clearAllFilesToolStripMenuItem.Size = new Size(213, 22);
            clearAllFilesToolStripMenuItem.Text = "&Clear All Files";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(210, 6);
            // 
            // saveCurrentFileToolStripMenuItem
            // 
            saveCurrentFileToolStripMenuItem.Name = "saveCurrentFileToolStripMenuItem";
            saveCurrentFileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveCurrentFileToolStripMenuItem.Size = new Size(213, 22);
            saveCurrentFileToolStripMenuItem.Text = "&Save Curernt File";
            // 
            // saveAllFilesToolStripMenuItem
            // 
            saveAllFilesToolStripMenuItem.Name = "saveAllFilesToolStripMenuItem";
            saveAllFilesToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAllFilesToolStripMenuItem.Size = new Size(213, 22);
            saveAllFilesToolStripMenuItem.Text = "Save &All Files";
            // 
            // renameFilesToolStripMenuItem
            // 
            renameFilesToolStripMenuItem.Name = "renameFilesToolStripMenuItem";
            renameFilesToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            renameFilesToolStripMenuItem.Size = new Size(213, 22);
            renameFilesToolStripMenuItem.Text = "&Rename Files...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(210, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(213, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // viewToolStripMenuItem1
            // 
            viewToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { layout1ToolStripMenuItem, layout2ToolStripMenuItem });
            viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            viewToolStripMenuItem1.Size = new Size(44, 20);
            viewToolStripMenuItem1.Text = "&View";
            // 
            // layout1ToolStripMenuItem
            // 
            layout1ToolStripMenuItem.Name = "layout1ToolStripMenuItem";
            layout1ToolStripMenuItem.Size = new Size(119, 22);
            layout1ToolStripMenuItem.Text = "Layout &1";
            // 
            // layout2ToolStripMenuItem
            // 
            layout2ToolStripMenuItem.Name = "layout2ToolStripMenuItem";
            layout2ToolStripMenuItem.Size = new Size(119, 22);
            layout2ToolStripMenuItem.Text = "Layout &2";
            // 
            // gridToolStripMenuItem
            // 
            gridToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nextFileToolStripMenuItem, previousFileToolStripMenuItem });
            gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            gridToolStripMenuItem.Size = new Size(41, 20);
            gridToolStripMenuItem.Text = "&Grid";
            // 
            // nextFileToolStripMenuItem
            // 
            nextFileToolStripMenuItem.Name = "nextFileToolStripMenuItem";
            nextFileToolStripMenuItem.ShortcutKeyDisplayString = "Enter";
            nextFileToolStripMenuItem.Size = new Size(206, 22);
            nextFileToolStripMenuItem.Text = "&Next File";
            // 
            // previousFileToolStripMenuItem
            // 
            previousFileToolStripMenuItem.Name = "previousFileToolStripMenuItem";
            previousFileToolStripMenuItem.ShortcutKeyDisplayString = "Shift+Enter";
            previousFileToolStripMenuItem.Size = new Size(206, 22);
            previousFileToolStripMenuItem.Text = "&Previous File";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomInToolStripMenuItem, zoomOutToolStripMenuItem, fitToPageToolStripMenuItem, toolStripSeparator2, rotateRightToolStripMenuItem, rotateLeftToolStripMenuItem, toolStripSeparator3, nextPageToolStripMenuItem, previousPageToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(45, 20);
            viewToolStripMenuItem.Text = "&Page";
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl [+]";
            zoomInToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Oemplus;
            zoomInToolStripMenuItem.Size = new Size(216, 22);
            zoomInToolStripMenuItem.Text = "Zoom &In";
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl [-]";
            zoomOutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.OemMinus;
            zoomOutToolStripMenuItem.Size = new Size(216, 22);
            zoomOutToolStripMenuItem.Text = "Zoom &Out";
            // 
            // fitToPageToolStripMenuItem
            // 
            fitToPageToolStripMenuItem.Name = "fitToPageToolStripMenuItem";
            fitToPageToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
            fitToPageToolStripMenuItem.Size = new Size(216, 22);
            fitToPageToolStripMenuItem.Text = "&Fit to Page";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(213, 6);
            // 
            // rotateRightToolStripMenuItem
            // 
            rotateRightToolStripMenuItem.Name = "rotateRightToolStripMenuItem";
            rotateRightToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift [+]";
            rotateRightToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Oemplus;
            rotateRightToolStripMenuItem.Size = new Size(216, 22);
            rotateRightToolStripMenuItem.Text = "Rotate &Right";
            // 
            // rotateLeftToolStripMenuItem
            // 
            rotateLeftToolStripMenuItem.Name = "rotateLeftToolStripMenuItem";
            rotateLeftToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift [-]";
            rotateLeftToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.OemMinus;
            rotateLeftToolStripMenuItem.Size = new Size(216, 22);
            rotateLeftToolStripMenuItem.Text = "Rotate &Left";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(213, 6);
            // 
            // nextPageToolStripMenuItem
            // 
            nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
            nextPageToolStripMenuItem.ShortcutKeyDisplayString = "Page Down";
            nextPageToolStripMenuItem.Size = new Size(216, 22);
            nextPageToolStripMenuItem.Text = "&Next Page";
            // 
            // previousPageToolStripMenuItem
            // 
            previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
            previousPageToolStripMenuItem.ShortcutKeyDisplayString = "Page Up";
            previousPageToolStripMenuItem.Size = new Size(216, 22);
            previousPageToolStripMenuItem.Text = "&Previous Page";
            // 
            // mnuMainEdit
            // 
            mnuMainEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuMainToolsSettings });
            mnuMainEdit.Name = "mnuMainEdit";
            mnuMainEdit.Size = new Size(46, 20);
            mnuMainEdit.Text = "&Tools";
            // 
            // mnuMainToolsSettings
            // 
            mnuMainToolsSettings.Name = "mnuMainToolsSettings";
            mnuMainToolsSettings.Size = new Size(125, 22);
            mnuMainToolsSettings.Text = "&Settings...";
            mnuMainToolsSettings.Click += mnuMainToolsSettings_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuMainHelpKeyboardShortcuts, mnuMainHelpAboutProView });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // mnuMainHelpKeyboardShortcuts
            // 
            mnuMainHelpKeyboardShortcuts.Name = "mnuMainHelpKeyboardShortcuts";
            mnuMainHelpKeyboardShortcuts.Size = new Size(186, 22);
            mnuMainHelpKeyboardShortcuts.Text = "&Keyboard Shortcuts...";
            // 
            // mnuMainHelpAboutProView
            // 
            mnuMainHelpAboutProView.Name = "mnuMainHelpAboutProView";
            mnuMainHelpAboutProView.Size = new Size(186, 22);
            mnuMainHelpAboutProView.Text = "&About ProView...";
            // 
            // PVMainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(658, 417);
            Controls.Add(mnuMain);
            Icon = Properties.Resources.ProViewIcon;
            MainMenuStrip = mnuMain;
            Margin = new Padding(4, 3, 4, 3);
            Name = "PVMainForm";
            StartPosition = FormStartPosition.Manual;
            Text = "ProView";
            FormClosing += PVMainFormClosing;
            Load += PVMainFormLoad;
            DragDrop += HandleDragDrop;
            DragEnter += HandleDragEnter;
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip mnuMain;
        private ToolStripMenuItem mnuMainEdit;
        private ToolStripMenuItem mnuMainToolsSettings;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem renameFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem saveCurrentFileToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem zoomInToolStripMenuItem;
        private ToolStripMenuItem zoomOutToolStripMenuItem;
        private ToolStripMenuItem fitToPageToolStripMenuItem;
        private ToolStripMenuItem saveAllFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem rotateRightToolStripMenuItem;
        private ToolStripMenuItem rotateLeftToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem nextPageToolStripMenuItem;
        private ToolStripMenuItem previousPageToolStripMenuItem;
        private ToolStripMenuItem gridToolStripMenuItem;
        private ToolStripMenuItem nextFileToolStripMenuItem;
        private ToolStripMenuItem previousFileToolStripMenuItem;
        private ToolStripMenuItem addFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem addFoldersToolStripMenuItem;
        private ToolStripMenuItem clearAllFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem mnuMainHelpKeyboardShortcuts;
        private ToolStripMenuItem mnuMainHelpAboutProView;
        private ToolStripMenuItem viewToolStripMenuItem1;
        private ToolStripMenuItem layout1ToolStripMenuItem;
        private ToolStripMenuItem layout2ToolStripMenuItem;
    }
}
