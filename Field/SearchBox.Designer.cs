/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 11/4/2014
 * Time: 8:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProView
{
	partial class SearchBox
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListBox listBox;
		private System.Windows.Forms.ContextMenuStrip ctxDeleteItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		
		/// <summary>
		/// Disposes resources used by the form.
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.ctxDeleteItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipHotKeysHelper = new System.Windows.Forms.ToolTip(this.components);
            this.ctxDeleteItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(284, 262);
            this.listBox.TabIndex = 0;
            this.listBox.SizeChanged += new System.EventHandler(this.ListBoxSizeChanged);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            this.listBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBoxMouseDown);
            // 
            // ctxDeleteItem
            // 
            this.ctxDeleteItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.ctxDeleteItem.Name = "ctxDeleteItem";
            this.ctxDeleteItem.Size = new System.Drawing.Size(135, 26);
            this.ctxDeleteItem.Click += new System.EventHandler(this.CtxDeleteItemClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.deleteToolStripMenuItem.Text = "Delete Item";
            // 
            // SearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.listBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AutoCompleteHelperForm";
            this.ctxDeleteItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ToolTip toolTipHotKeysHelper;
	}
}
