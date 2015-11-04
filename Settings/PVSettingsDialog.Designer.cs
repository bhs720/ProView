/*
 * Created by SharpDevelop.
 * User: ben
 * Date: 6/1/2013
 * Time: 8:45 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProView
{
	partial class PVSettingsDialog
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblResult = new System.Windows.Forms.Label();
			this.btnHighlightFG = new System.Windows.Forms.Button();
			this.btnHighlightBG = new System.Windows.Forms.Button();
			this.fontPicker = new System.Windows.Forms.FontDialog();
			this.colorPicker = new System.Windows.Forms.ColorDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabJobSetup = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnLoadJob = new System.Windows.Forms.Button();
			this.listBoxSavedJobs = new System.Windows.Forms.ListBox();
			this.btnExportJob = new System.Windows.Forms.Button();
			this.btnDeleteJob = new System.Windows.Forms.Button();
			this.btnImportJob = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblJobName = new System.Windows.Forms.Label();
			this.txtSaveJobName = new System.Windows.Forms.TextBox();
			this.btnSaveJob = new System.Windows.Forms.Button();
			this.tabSpreadsheet = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnFont = new System.Windows.Forms.Button();
			this.tabViewer = new System.Windows.Forms.TabPage();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPDFResolution = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDefaults = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnLoadCsv = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabJobSetup.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabSpreadsheet.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabViewer.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(329, 383);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(248, 383);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblResult
			// 
			this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblResult.Location = new System.Drawing.Point(19, 74);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(264, 19);
			this.lblResult.TabIndex = 3;
			this.lblResult.Text = "Sample Text";
			this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnHighlightFG
			// 
			this.btnHighlightFG.Location = new System.Drawing.Point(19, 33);
			this.btnHighlightFG.Name = "btnHighlightFG";
			this.btnHighlightFG.Size = new System.Drawing.Size(84, 23);
			this.btnHighlightFG.TabIndex = 0;
			this.btnHighlightFG.Text = "Text Color...";
			this.btnHighlightFG.UseVisualStyleBackColor = true;
			this.btnHighlightFG.Click += new System.EventHandler(this.BtnHighlightFGClick);
			// 
			// btnHighlightBG
			// 
			this.btnHighlightBG.Location = new System.Drawing.Point(109, 33);
			this.btnHighlightBG.Name = "btnHighlightBG";
			this.btnHighlightBG.Size = new System.Drawing.Size(84, 23);
			this.btnHighlightBG.TabIndex = 1;
			this.btnHighlightBG.Text = "Back Color...";
			this.btnHighlightBG.UseVisualStyleBackColor = true;
			this.btnHighlightBG.Click += new System.EventHandler(this.BtnHighlightBGClick);
			// 
			// colorPicker
			// 
			this.colorPicker.AnyColor = true;
			this.colorPicker.FullOpen = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabJobSetup);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabSpreadsheet);
			this.tabControl1.Controls.Add(this.tabViewer);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(392, 365);
			this.tabControl1.TabIndex = 0;
			// 
			// tabJobSetup
			// 
			this.tabJobSetup.Controls.Add(this.groupBox3);
			this.tabJobSetup.Controls.Add(this.groupBox2);
			this.tabJobSetup.Location = new System.Drawing.Point(4, 22);
			this.tabJobSetup.Name = "tabJobSetup";
			this.tabJobSetup.Padding = new System.Windows.Forms.Padding(12);
			this.tabJobSetup.Size = new System.Drawing.Size(384, 339);
			this.tabJobSetup.TabIndex = 2;
			this.tabJobSetup.Text = "Job Setup";
			this.tabJobSetup.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnLoadJob);
			this.groupBox3.Controls.Add(this.listBoxSavedJobs);
			this.groupBox3.Controls.Add(this.btnExportJob);
			this.groupBox3.Controls.Add(this.btnDeleteJob);
			this.groupBox3.Controls.Add(this.btnImportJob);
			this.groupBox3.Location = new System.Drawing.Point(16, 85);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(12);
			this.groupBox3.Size = new System.Drawing.Size(349, 230);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Saved Jobs";
			// 
			// btnLoadJob
			// 
			this.btnLoadJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoadJob.Location = new System.Drawing.Point(15, 192);
			this.btnLoadJob.Name = "btnLoadJob";
			this.btnLoadJob.Size = new System.Drawing.Size(75, 23);
			this.btnLoadJob.TabIndex = 1;
			this.btnLoadJob.Text = "Load";
			this.btnLoadJob.UseVisualStyleBackColor = true;
			this.btnLoadJob.Click += new System.EventHandler(this.BtnLoadJobClick);
			// 
			// listBoxSavedJobs
			// 
			this.listBoxSavedJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxSavedJobs.FormattingEnabled = true;
			this.listBoxSavedJobs.Location = new System.Drawing.Point(15, 28);
			this.listBoxSavedJobs.Name = "listBoxSavedJobs";
			this.listBoxSavedJobs.Size = new System.Drawing.Size(319, 147);
			this.listBoxSavedJobs.TabIndex = 0;
			// 
			// btnExportJob
			// 
			this.btnExportJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExportJob.Location = new System.Drawing.Point(258, 192);
			this.btnExportJob.Name = "btnExportJob";
			this.btnExportJob.Size = new System.Drawing.Size(75, 23);
			this.btnExportJob.TabIndex = 4;
			this.btnExportJob.Text = "Export";
			this.btnExportJob.UseVisualStyleBackColor = true;
			this.btnExportJob.Click += new System.EventHandler(this.BtnExportJobClick);
			// 
			// btnDeleteJob
			// 
			this.btnDeleteJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDeleteJob.Location = new System.Drawing.Point(96, 192);
			this.btnDeleteJob.Name = "btnDeleteJob";
			this.btnDeleteJob.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteJob.TabIndex = 2;
			this.btnDeleteJob.Text = "Delete";
			this.btnDeleteJob.UseVisualStyleBackColor = true;
			this.btnDeleteJob.Click += new System.EventHandler(this.BtnDeleteJobClick);
			// 
			// btnImportJob
			// 
			this.btnImportJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnImportJob.Location = new System.Drawing.Point(177, 192);
			this.btnImportJob.Name = "btnImportJob";
			this.btnImportJob.Size = new System.Drawing.Size(75, 23);
			this.btnImportJob.TabIndex = 3;
			this.btnImportJob.Text = "Import";
			this.btnImportJob.UseVisualStyleBackColor = true;
			this.btnImportJob.Click += new System.EventHandler(this.BtnImportJobClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblJobName);
			this.groupBox2.Controls.Add(this.txtSaveJobName);
			this.groupBox2.Controls.Add(this.btnSaveJob);
			this.groupBox2.Location = new System.Drawing.Point(16, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(12);
			this.groupBox2.Size = new System.Drawing.Size(349, 62);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Save Current Setup";
			// 
			// lblJobName
			// 
			this.lblJobName.Location = new System.Drawing.Point(15, 25);
			this.lblJobName.Name = "lblJobName";
			this.lblJobName.Size = new System.Drawing.Size(63, 23);
			this.lblJobName.TabIndex = 1;
			this.lblJobName.Text = "Job Name";
			// 
			// txtSaveJobName
			// 
			this.txtSaveJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSaveJobName.Location = new System.Drawing.Point(84, 22);
			this.txtSaveJobName.Name = "txtSaveJobName";
			this.txtSaveJobName.Size = new System.Drawing.Size(169, 20);
			this.txtSaveJobName.TabIndex = 0;
			// 
			// btnSaveJob
			// 
			this.btnSaveJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveJob.Location = new System.Drawing.Point(259, 20);
			this.btnSaveJob.Name = "btnSaveJob";
			this.btnSaveJob.Size = new System.Drawing.Size(75, 23);
			this.btnSaveJob.TabIndex = 1;
			this.btnSaveJob.Text = "Save";
			this.btnSaveJob.UseVisualStyleBackColor = true;
			this.btnSaveJob.Click += new System.EventHandler(this.BtnSaveJobClick);
			// 
			// tabSpreadsheet
			// 
			this.tabSpreadsheet.Controls.Add(this.groupBox1);
			this.tabSpreadsheet.Location = new System.Drawing.Point(4, 22);
			this.tabSpreadsheet.Name = "tabSpreadsheet";
			this.tabSpreadsheet.Padding = new System.Windows.Forms.Padding(12);
			this.tabSpreadsheet.Size = new System.Drawing.Size(384, 339);
			this.tabSpreadsheet.TabIndex = 0;
			this.tabSpreadsheet.Text = "Spreadsheet";
			this.tabSpreadsheet.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblResult);
			this.groupBox1.Controls.Add(this.btnHighlightFG);
			this.groupBox1.Controls.Add(this.btnFont);
			this.groupBox1.Controls.Add(this.btnHighlightBG);
			this.groupBox1.Location = new System.Drawing.Point(15, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 120);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Row Highlight";
			// 
			// btnFont
			// 
			this.btnFont.Location = new System.Drawing.Point(199, 33);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(84, 23);
			this.btnFont.TabIndex = 2;
			this.btnFont.Text = "Font...";
			this.btnFont.UseVisualStyleBackColor = true;
			this.btnFont.Click += new System.EventHandler(this.BtnFontClick);
			// 
			// tabViewer
			// 
			this.tabViewer.Controls.Add(this.comboBox3);
			this.tabViewer.Controls.Add(this.comboBox2);
			this.tabViewer.Controls.Add(this.comboBox1);
			this.tabViewer.Controls.Add(this.label4);
			this.tabViewer.Controls.Add(this.label3);
			this.tabViewer.Controls.Add(this.label2);
			this.tabViewer.Controls.Add(this.txtPDFResolution);
			this.tabViewer.Controls.Add(this.label1);
			this.tabViewer.Location = new System.Drawing.Point(4, 22);
			this.tabViewer.Name = "tabViewer";
			this.tabViewer.Padding = new System.Windows.Forms.Padding(12);
			this.tabViewer.Size = new System.Drawing.Size(384, 339);
			this.tabViewer.TabIndex = 1;
			this.tabViewer.Text = "Image Viewer";
			this.tabViewer.UseVisualStyleBackColor = true;
			// 
			// comboBox3
			// 
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(130, 114);
			this.comboBox3.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(75, 21);
			this.comboBox3.TabIndex = 8;
			this.comboBox3.Visible = false;
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(130, 83);
			this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(75, 21);
			this.comboBox2.TabIndex = 7;
			this.comboBox2.Visible = false;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(130, 52);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(75, 21);
			this.comboBox1.TabIndex = 6;
			this.comboBox1.Visible = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 86);
			this.label4.Margin = new System.Windows.Forms.Padding(4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(106, 23);
			this.label4.TabIndex = 5;
			this.label4.Text = "Gray Supersamples";
			this.label4.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 117);
			this.label3.Margin = new System.Windows.Forms.Padding(4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Color Supersamples";
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 55);
			this.label2.Margin = new System.Windows.Forms.Padding(4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "B&&W Supersamples";
			this.label2.Visible = false;
			// 
			// txtPDFResolution
			// 
			this.txtPDFResolution.Location = new System.Drawing.Point(130, 21);
			this.txtPDFResolution.Margin = new System.Windows.Forms.Padding(4);
			this.txtPDFResolution.Name = "txtPDFResolution";
			this.txtPDFResolution.Size = new System.Drawing.Size(75, 20);
			this.txtPDFResolution.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "PDF Resolution (dpi)";
			// 
			// btnDefaults
			// 
			this.btnDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDefaults.Location = new System.Drawing.Point(12, 383);
			this.btnDefaults.Name = "btnDefaults";
			this.btnDefaults.Size = new System.Drawing.Size(106, 23);
			this.btnDefaults.TabIndex = 2;
			this.btnDefaults.Text = "Defaults";
			this.btnDefaults.UseVisualStyleBackColor = true;
			this.btnDefaults.Click += new System.EventHandler(this.BtnDefaultsClick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "ProView Settings XML|*.xml|All files|*.*";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "ProView Settings XML|*.xml|All files|*.*";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnLoadCsv);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(12);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(12);
			this.tabPage1.Size = new System.Drawing.Size(384, 339);
			this.tabPage1.TabIndex = 3;
			this.tabPage1.Text = "Data Source";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnLoadCsv
			// 
			this.btnLoadCsv.Location = new System.Drawing.Point(15, 15);
			this.btnLoadCsv.Name = "btnLoadCsv";
			this.btnLoadCsv.Size = new System.Drawing.Size(75, 23);
			this.btnLoadCsv.TabIndex = 0;
			this.btnLoadCsv.Text = "Load CSV";
			this.btnLoadCsv.UseVisualStyleBackColor = true;
			this.btnLoadCsv.Click += new System.EventHandler(this.BtnLoadCsvClick);
			// 
			// PVSettingsDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(416, 418);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnDefaults);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(255, 250);
			this.Name = "PVSettingsDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PVSettingsDialogFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PVSettingsDialogFormClosed);
			this.Load += new System.EventHandler(this.PVSettingsDialogLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabJobSetup.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabSpreadsheet.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabViewer.ResumeLayout(false);
			this.tabViewer.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button btnHighlightBG;
		private System.Windows.Forms.Button btnHighlightFG;
		private System.Windows.Forms.Label lblResult;
		
		
		private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FontDialog fontPicker;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSpreadsheet;
        private System.Windows.Forms.TabPage tabViewer;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.TextBox txtPDFResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabJobSetup;
        private System.Windows.Forms.ListBox listBoxSavedJobs;
        private System.Windows.Forms.Button btnSaveJob;
        private System.Windows.Forms.Button btnDeleteJob;
        private System.Windows.Forms.Button btnExportJob;
        private System.Windows.Forms.Button btnImportJob;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoadJob;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblJobName;
        private System.Windows.Forms.TextBox txtSaveJobName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnLoadCsv;
	}
}
