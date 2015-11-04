using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProView
{
    public partial class PVImageViewerStatus : UserControl
    {
        double zoom;
        int pageNumber;
        int pageCount;

        public delegate void PageRequestHandler(int pageNumber);
        public delegate void ZoomRequestHandler(double scale);
        public event PageRequestHandler PageRequest;
        public event ZoomRequestHandler ZoomRequest;
        

        public PVImageViewerStatus()
        {
            InitializeComponent();
            zoom = 0.0D;
            pageNumber = 0;
            pageCount = 0;
            Disable();
        }

        public void Enable()
        {
            txtPage.Enabled = true;
            txtScale.Enabled = true;
            PageNumber = pageNumber;
            Zoom = zoom;
        }

        public void Disable()
        {
            txtPage.Clear();
            txtScale.Clear();
            txtPage.Enabled = false;
            txtScale.Enabled = false;
        }

        public void OnZoomChanged(object sender, ScaleChangedEventArgs e)
        {
        	Zoom = e.Scale;
        }

        public void OnPageChanged(object sender, PageChangedEventArgs e)
        {
        	if (e.NullPage)
        	{
        		Disable();
        	}
        	else
        	{
        		Enable();
        		PageNumber = e.PageNumber;
        	}
        }

        public void OnFileChanged(object sender, FileChangedEventArgs e)
        {
        	if (e.NullFile)
        	{
        		Disable();
        	}
        	else
        	{
        		//Enable();
        		PageCount = e.PageCount;
        	}
        }
        
        int PageNumber
        {
        	set
        	{
        		pageNumber = value;
        		if (txtPage.Enabled)
                	txtPage.Text = pageNumber + " / " + pageCount;
        	}
        }
        
        int PageCount
        {
        	set
        	{
        		pageCount = value;
        		if (txtPage.Enabled)
        			txtPage.Text = pageNumber + " / " + pageCount;
        	}
        }
        
        double Zoom
        {
        	set
        	{
        		zoom = value;
        		if (txtScale.Enabled)
        			txtScale.Text = Math.Round(zoom * 100, 1) + "%";
        	}
        }
        
        void txtZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value;
                if (!double.TryParse(txtScale.Text, out value) || !(value > 0.0D) || value > 400.0D)
                {
                	Zoom = zoom;
                }
                else
                {
	                zoom = value;
	                ZoomRequest.Invoke(zoom);
                }
                txtScale.SelectAll();
            }
        }

        void txtZoom_Leave(object sender, EventArgs e)
        {
        	Zoom = zoom;
        }

        void txtZoom_Click(object sender, EventArgs e)
        {
            txtScale.SelectAll();
        }

        void txtPage_Leave(object sender, EventArgs e)
        {
        	PageNumber = pageNumber;
        }

        void txtPage_Click(object sender, EventArgs e)
        {
            txtPage.SelectAll();
        }

        void txtPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int value;
                if (!int.TryParse(txtPage.Text, out value) || value > pageCount || value < 1)
                {
                	PageNumber = pageNumber;
                }
                else
                {
	                PageNumber = value;
	                PageRequest.Invoke(pageNumber);
                }
                txtPage.SelectAll();
            }
        }
    }
}
