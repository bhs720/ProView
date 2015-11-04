/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 10/28/2014
 * Time: 8:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProView
{
	partial class RenameDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPattern;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.DataGridView dgRename;
		private System.Windows.Forms.DataGridViewTextBoxColumn colOldFileName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNewFileName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colError;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label3;
		
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPattern = new System.Windows.Forms.TextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.dgRename = new System.Windows.Forms.DataGridView();
			this.colOldFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colNewFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colError = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgRename)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(738, 346);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(657, 346);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Pattern";
			// 
			// txtPattern
			// 
			this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPattern.Location = new System.Drawing.Point(72, 17);
			this.txtPattern.Name = "txtPattern";
			this.txtPattern.Size = new System.Drawing.Size(421, 20);
			this.txtPattern.TabIndex = 3;
			// 
			// btnInsert
			// 
			this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsert.Location = new System.Drawing.Point(499, 15);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(31, 23);
			this.btnInsert.TabIndex = 4;
			this.btnInsert.Text = "<<";
			this.btnInsert.UseVisualStyleBackColor = true;
			// 
			// dgRename
			// 
			this.dgRename.AllowUserToAddRows = false;
			this.dgRename.AllowUserToDeleteRows = false;
			this.dgRename.AllowUserToResizeRows = false;
			this.dgRename.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dgRename.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgRename.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgRename.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.colOldFileName,
			this.colNewFileName,
			this.colError});
			this.dgRename.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgRename.Location = new System.Drawing.Point(13, 60);
			this.dgRename.Name = "dgRename";
			this.dgRename.ReadOnly = true;
			this.dgRename.RowHeadersVisible = false;
			this.dgRename.Size = new System.Drawing.Size(800, 280);
			this.dgRename.TabIndex = 5;
			// 
			// colOldFileName
			// 
			this.colOldFileName.HeaderText = "Old File Name";
			this.colOldFileName.Name = "colOldFileName";
			this.colOldFileName.ReadOnly = true;
			// 
			// colNewFileName
			// 
			this.colNewFileName.HeaderText = "New File Name";
			this.colNewFileName.Name = "colNewFileName";
			this.colNewFileName.ReadOnly = true;
			// 
			// colError
			// 
			this.colError.FillWeight = 25F;
			this.colError.HeaderText = "Error";
			this.colError.Name = "colError";
			this.colError.ReadOnly = true;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(556, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Duplicates";
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
			"Auto-Number",
			"Auto-Letter"});
			this.comboBox1.Location = new System.Drawing.Point(624, 17);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(79, 21);
			this.comboBox1.TabIndex = 7;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(770, 17);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(43, 20);
			this.textBox1.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(724, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Suffix";
			// 
			// RenameDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(825, 381);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dgRename);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.txtPattern);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.MinimizeBox = false;
			this.Name = "RenameDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rename Files";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.dgRename)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
