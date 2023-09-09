using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProView
{
	public partial class PVProgressForm : Form
	{
		public PVProgressForm()
		{
			InitializeComponent();
		}

		public ProgressBar ProgressBar { get { return progressBar1; } }

		public Button BtnCancel { get { return btnCancel; } }
	}
}
