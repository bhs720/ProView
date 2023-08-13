using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProView
{
	public partial class PVSettingsDialog : Form
	{
		#region Constructor

		public PVSettingsDialog()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region OK Cancel Default
		
		void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
		
		void BtnDefaultsClick(object sender, EventArgs e)
		{
			lblResult.BackColor = Color.LightYellow;
			lblResult.ForeColor = SystemColors.ControlText;
			FontFamily fontFamily = null;
			try
			{
				fontFamily = new FontFamily("Tahoma");
			}
			catch (Exception)
			{
				fontFamily = new FontFamily(SystemFonts.DefaultFont.FontFamily.Name);
			}
			var font = new Font(fontFamily, 8.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblResult.Font = font;
			txtPDFResolution.Text = "96";
		}
		
		#endregion
		
		#region Form Load/Close
		
		void PVSettingsDialogLoad(object sender, EventArgs e)
		{
			//lblResult.Font = Properties.DataGrid.Default.Font;
			//lblResult.BackColor = Properties.DataGrid.Default.HighlightRowBG;
			//lblResult.ForeColor = Properties.DataGrid.Default.HighlightRowFG;
			//txtPDFResolution.Text = Properties.ImageViewer.Default.PDFResolution.ToString();
			//EnumerateSavedJobs();
		}
		
		void PVSettingsDialogFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				Properties.DataGrid.Default.Font = lblResult.Font;
				Properties.DataGrid.Default.HighlightRowBG = lblResult.BackColor;
				Properties.DataGrid.Default.HighlightRowFG = lblResult.ForeColor;
				Properties.DataGrid.Default.Save();
				PVDataViewer.LoadUserSettings();
			}
		}
		
		void PVSettingsDialogFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				int value = 0;
				if (!int.TryParse(txtPDFResolution.Text, out value) || value < 1)
				{
					MessageBox.Show("Invalid PDF resolution setting", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
				}
				else
				{
					//PDF.resolution = value;
					Properties.ImageViewer.Default.PDFResolution = value;
					Properties.ImageViewer.Default.Save();
				}
			}
		}
		#endregion
		
		#region DataGrid
		
		public PVDataViewer PVDataViewer { get; set; }
		
		void BtnFontClick(object sender, EventArgs e)
		{
			try
			{
				fontPicker.Font = lblResult.Font;
				DialogResult result = fontPicker.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					lblResult.Font = fontPicker.Font;
					
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				
			}
		}
		void BtnHighlightFGClick(object sender, EventArgs e)
		{
			colorPicker.Color = lblResult.ForeColor;
			DialogResult result = colorPicker.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				lblResult.ForeColor = colorPicker.Color;
			}
		}
		
		void BtnHighlightBGClick(object sender, EventArgs e)
		{
			colorPicker.Color = lblResult.BackColor;
			DialogResult result = colorPicker.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				lblResult.BackColor = colorPicker.Color;
			}
		}
		
		#endregion
		
		#region Job Settings
		
		void BtnSaveJobClick(object sender, EventArgs e)
		{
			var jobName = txtSaveJobName.Text;
			if (string.IsNullOrWhiteSpace(jobName))
			{
				MessageBox.Show("Job name cannot be empty.", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (listBoxSavedJobs.Items.Contains(jobName))
			{
				var result = MessageBox.Show("There is already a job named " + jobName + ".\n\nDo you want to overwrite it?", "Confirm overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.No)
					return;
			}
			JobManager.SaveJob(jobName, PVDataViewer.UserFields);
			EnumerateSavedJobs();
			txtSaveJobName.Text = "";
		}

		void EnumerateSavedJobs()
		{
			listBoxSavedJobs.DataSource = JobManager.GetStoredJobs();
		}
		void BtnDeleteJobClick(object sender, EventArgs e)
		{
			var jobName = listBoxSavedJobs.SelectedItem as string;
			if (jobName != null)
			{
				var result = MessageBox.Show("Are you sure you want to delete " + jobName + "?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.No)
					return;
				JobManager.DeleteJob(jobName);
				EnumerateSavedJobs();
			}
		}
		void BtnLoadJobClick(object sender, EventArgs e)
		{
			var jobName = listBoxSavedJobs.SelectedItem as string;
			if (jobName != null)
			{
				if (PVDataViewer.GridContainsUserData)
				{
					var result = MessageBox.Show("Information you entered will be lost.\n\nContinue loading job settings?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (result == DialogResult.No)
						return;
				}
				PVDataViewer.LoadJobSettings(jobName);
				Close();
			}
		}
		void BtnImportJobClick(object sender, EventArgs e)
		{
			var result = openFileDialog1.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				JobManager.ImportFile(openFileDialog1.FileName);
				EnumerateSavedJobs();
			}
		}
		void BtnExportJobClick(object sender, EventArgs e)
		{
			var jobName = listBoxSavedJobs.SelectedItem as string;
			if (!string.IsNullOrEmpty(jobName))
			{
				var result = saveFileDialog1.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					JobManager.ExportFile(jobName, saveFileDialog1.FileName);
				}
			}
		}
		void BtnLoadCsvClick(object sender, EventArgs e)
		{
			var result = openFileDialog1.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				PVDataViewer.LoadCsvDataSource(openFileDialog1.FileName);
			}
		}
		
		#endregion
	}
}
