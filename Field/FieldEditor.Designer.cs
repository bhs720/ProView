/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 9/30/2014
 * Time: 4:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProView
{
	partial class FieldEditor
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.cbAutoComplete = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblNumTerms = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.radAdhere = new System.Windows.Forms.RadioButton();
			this.radAppend = new System.Windows.Forms.RadioButton();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnClear = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(60, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(163, 20);
			this.txtName.TabIndex = 0;
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(11, 15);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(43, 23);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Name";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(148, 210);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(66, 210);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// cbAutoComplete
			// 
			this.cbAutoComplete.Location = new System.Drawing.Point(13, 0);
			this.cbAutoComplete.Name = "cbAutoComplete";
			this.cbAutoComplete.Size = new System.Drawing.Size(92, 24);
			this.cbAutoComplete.TabIndex = 0;
			this.cbAutoComplete.Text = "AutoComplete";
			this.cbAutoComplete.UseVisualStyleBackColor = true;
			this.cbAutoComplete.CheckedChanged += new System.EventHandler(this.CbAutoCompleteCheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnClear);
			this.groupBox1.Controls.Add(this.lblNumTerms);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Controls.Add(this.btnLoad);
			this.groupBox1.Controls.Add(this.radAdhere);
			this.groupBox1.Controls.Add(this.radAppend);
			this.groupBox1.Controls.Add(this.cbAutoComplete);
			this.groupBox1.Location = new System.Drawing.Point(12, 41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 144);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// lblNumTerms
			// 
			this.lblNumTerms.Location = new System.Drawing.Point(13, 119);
			this.lblNumTerms.Name = "lblNumTerms";
			this.lblNumTerms.Size = new System.Drawing.Size(192, 23);
			this.lblNumTerms.TabIndex = 7;
			this.lblNumTerms.Text = "# of items";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(77, 89);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(58, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(13, 89);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(58, 23);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.BtnLoadClick);
			// 
			// radAdhere
			// 
			this.radAdhere.Location = new System.Drawing.Point(13, 59);
			this.radAdhere.Name = "radAdhere";
			this.radAdhere.Size = new System.Drawing.Size(168, 24);
			this.radAdhere.TabIndex = 2;
			this.radAdhere.Text = "Adhere to specified list";
			this.radAdhere.UseVisualStyleBackColor = true;
			// 
			// radAppend
			// 
			this.radAppend.Location = new System.Drawing.Point(13, 30);
			this.radAppend.Name = "radAppend";
			this.radAppend.Size = new System.Drawing.Size(192, 24);
			this.radAppend.TabIndex = 1;
			this.radAppend.Text = "Build list during data entry";
			this.radAppend.UseVisualStyleBackColor = true;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "txt";
			this.openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "txt";
			this.saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(141, 89);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(58, 23);
			this.btnClear.TabIndex = 8;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
			// 
			// FieldEditor
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(235, 245);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FieldEditor";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Field Editor";
			this.Load += new System.EventHandler(this.FieldEditorLoad);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox cbAutoComplete;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.RadioButton radAdhere;
		private System.Windows.Forms.RadioButton radAppend;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label lblNumTerms;
		private System.Windows.Forms.Button btnClear;
	}
}
