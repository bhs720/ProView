namespace ProView
{
    partial class PVImageViewer
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
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.cMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenuZoomFit = new System.Windows.Forms.ToolStripMenuItem();
            this.pvStatus = new ProView.PVImageViewerStatus();
            this.cMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.Location = new System.Drawing.Point(92, 162);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(371, 20);
            this.hScrollBar1.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.Location = new System.Drawing.Point(463, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 162);
            this.vScrollBar1.TabIndex = 2;
            // 
            // cMenu
            // 
            this.cMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenuZoomFit});
            this.cMenu.Name = "contextMenuStrip1";
            this.cMenu.ShowImageMargin = false;
            this.cMenu.Size = new System.Drawing.Size(148, 26);
            // 
            // cMenuZoomFit
            // 
            this.cMenuZoomFit.Name = "cMenuZoomFit";
            this.cMenuZoomFit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.cMenuZoomFit.Size = new System.Drawing.Size(147, 22);
            this.cMenuZoomFit.Text = "Zoom to Fit";
            // 
            // pvStatus
            // 
            this.pvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pvStatus.Location = new System.Drawing.Point(0, 162);
            this.pvStatus.Margin = new System.Windows.Forms.Padding(0);
            this.pvStatus.Name = "pvStatus";
            this.pvStatus.Size = new System.Drawing.Size(92, 20);
            this.pvStatus.TabIndex = 3;
            // 
            // PVImageViewer
            // 
            this.Controls.Add(this.pvStatus);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Name = "PVImageViewer";
            this.Size = new System.Drawing.Size(483, 182);
            this.cMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ToolStripMenuItem cMenuZoomFit;
        private System.Windows.Forms.ContextMenuStrip cMenu;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;

        private ProView.PVImageViewerStatus pvStatus;
    }
}
