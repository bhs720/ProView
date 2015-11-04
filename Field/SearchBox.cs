/*
 * Created by SharpDevelop.
 * User: bsmith
 * Date: 11/4/2014
 * Time: 8:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProView
{
	public partial class SearchBox : Form
	{
		Rectangle editingCellScreenRect;
		public delegate void DeleteItemRequestHandler(string item);
		public event DeleteItemRequestHandler DeleteItemRequest;

        public delegate void SelectItemRequestHandler(string item);
        public event SelectItemRequestHandler SelectItemRequest;
		
		public SearchBox()
		{
			InitializeComponent();
		}
		
		protected override bool ShowWithoutActivation { get { return true; } }

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000008; //WS_EX_TOPMOST
                //cp.ExStyle |= 0x00020000; //CS_DROPSHADOW
                //cp.ExStyle |= 0x08000000; //WS_EX_NOACTIVATE
                //cp.ExStyle |= 0x00000080; //WS_EX_TOOLWINDOW
                //cp.ExStyle |= 0x00000100; //WS_EX_WINDOWEDGE
				return cp;
			}
		}
		
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!Visible)
				DataSource = null;
			base.OnVisibleChanged(e);
		}
		
		public List<string> DataSource
		{
			set
			{
				listBox.DataSource = value;
				AdjustSize();
				Debug.WriteLine("DataSource.Count={0}", listBox.Items.Count);
			}
		}
		
		public string SelectedItem
		{
			get
			{
				Debug.WriteLine("SelectedItem:{0}", listBox.SelectedItem);
				return listBox.SelectedItem as string;
			}
		}
		
		public int SelectedIndex
		{
			get
			{
				return listBox.SelectedIndex;
			}
			set
			{
				if (value > listBox.Items.Count - 1 || value < 0)
					return;
				listBox.SelectedIndex = value;
			}
		}
		
		void AdjustSize()
		{
			int items = Math.Min(listBox.Items.Count, 10);
			listBox.Height = listBox.ItemHeight * (items + 1);
			float width = 0;
			using (var g = listBox.CreateGraphics())
			{
				foreach (string item in listBox.Items)
				{
					var measured = g.MeasureString(item, listBox.Font, 1024);
					width = Math.Max(width, measured.Width);
				}
			}
			listBox.Width = Math.Max((int)width, editingCellScreenRect.Width);
            
		}
		void ListBoxSizeChanged(object sender, EventArgs e)
		{
			Size = listBox.Size;
			Debug.WriteLine("Size{0}", Size);
		}

        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectedItemChanged.Invoke(listBox.SelectedItem as string);
        }

		public void Show(Rectangle editingCellScreenRect)
		{
			this.editingCellScreenRect = editingCellScreenRect;
			// Default screen location for this "search results box" is below the editing cell.
            // If the "search results box" is going off screen, put it above the editing cell.
			var belowEditingCell = new Point(editingCellScreenRect.Left, editingCellScreenRect.Bottom);
			var aboveEditingCell = new Point(editingCellScreenRect.Left, editingCellScreenRect.Top - Height);
			
			Location = belowEditingCell;
            
			if (!PointIsOnScreen(new Point(Left, Bottom)))
				Location = aboveEditingCell;
			Visible = true;
		}
		
		bool PointIsOnScreen(Point point)
		{
			foreach (var screen in Screen.AllScreens)
			{
				if (screen.WorkingArea.Contains(point))
					return true;
			}
			return false;
		}
		
		string itemClicked;
		void ListBoxMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				listBox.SelectedIndex = listBox.IndexFromPoint(e.Location);
				itemClicked = listBox.SelectedItem as string;
				ctxDeleteItem.Show(Cursor.Position);
			}
            
		}
		void CtxDeleteItemClick(object sender, EventArgs e)
		{
			DeleteItemRequest.Invoke(itemClicked);
		}

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            SelectItemRequest.Invoke(listBox.SelectedItem as string);
        }

        // Need these in case this search box takes focus
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Visible = false;
                    return true;
                case Keys.Tab:
                    SelectItemRequest.Invoke(listBox.SelectedItem as string);
                    return true;
                case Keys.Enter:
                    SelectItemRequest.Invoke(listBox.SelectedItem as string);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
	}
}
