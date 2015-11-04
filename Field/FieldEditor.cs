/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 9/30/2014
 * Time: 4:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace ProView
{
	public partial class FieldEditor : Form
	{
		public FieldEditor()
		{
			InitializeComponent();
		}
		
		public Field Field { get; set; }
		
		void FieldEditorLoad(object sender, System.EventArgs e)
		{
			if (Field == null)
				Field = new Field();
			
			txtName.Text = Field.Name;
			if (Field.SourceMode != SourceMode.None)
			{
				SourceModeOptionsEnabled = true;
				cbAutoComplete.Checked = true;
				radAdhere.Checked = Field.SourceMode == SourceMode.Adhere;
				radAppend.Checked = Field.SourceMode == SourceMode.Append;
			}
			else
			{
				SourceModeOptionsEnabled = false;
			}
			lblNumTerms.Text = Field.Source.Count + " items";
		}
		
		bool SourceModeOptionsEnabled
		{
			set
			{
				radAdhere.Enabled = value;
				radAppend.Enabled = value;
				btnLoad.Enabled = value;
				btnSave.Enabled = value;
				lblNumTerms.Enabled = value;
			}
		}
		
		void BtnCancelClick(object sender, System.EventArgs e)
		{
			Close();
		}
		void BtnOKClick(object sender, System.EventArgs e)
		{
			string name = txtName.Text;
			if (string.IsNullOrWhiteSpace(name))
			{
				MessageBox.Show("Need a name", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Field.Name = name;
			if (cbAutoComplete.Checked)
			{
				if (radAdhere.Checked)
					Field.SourceMode = SourceMode.Adhere;
				if (radAppend.Checked)
					Field.SourceMode = SourceMode.Append;
			}
			else
			{
				Field.SourceMode = SourceMode.None;
			}
			
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void BtnLoadClick(object sender, EventArgs e)
		{
			var result = openFileDialog1.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				int countAddedItems = 0;
				StreamReader reader = null;
				try
				{
					reader = new StreamReader(openFileDialog1.OpenFile());
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						if (string.IsNullOrWhiteSpace(line) || Field.Source.Contains(line, true))
							continue;
						Field.Source.Add(line);
						countAddedItems++;
					}
					
					MessageBox.Show("Added " + countAddedItems + " new items.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					lblNumTerms.Text = Field.Source.Count + " items";
					Field.Source.Sort();
				}
				catch (Exception)
				{
					MessageBox.Show("Something went wrong. Load a text file with a term on each new line.", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					reader.Dispose();
					openFileDialog1.Dispose();
				}
			}
		}
		void BtnSaveClick(object sender, EventArgs e)
		{
			var result = saveFileDialog1.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				var newTerms = new List<string>();
				StreamWriter writer = null;
				try
				{
					writer = new StreamWriter(saveFileDialog1.FileName, false);
					foreach (string term in Field.Source)
					{
						writer.WriteLine(term);
					}
					writer.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("Something went wrong. Could not save the file.", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					writer.Dispose();
					openFileDialog1.Dispose();
				}
			}
		}
		void CbAutoCompleteCheckedChanged(object sender, EventArgs e)
		{
			SourceModeOptionsEnabled = cbAutoComplete.Checked;
			if (cbAutoComplete.Checked && !radAdhere.Checked && !radAppend.Checked)
				radAppend.Checked = true;
		}
		void BtnClearClick(object sender, EventArgs e)
		{
			Field.Source.Clear();
			lblNumTerms.Text = Field.Source.Count + " items";
		}
	}
}
