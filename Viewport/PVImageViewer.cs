using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ProView
{
    public partial class PVImageViewer : Panel
    {
        readonly ViewPort viewPort = new ViewPort();
        readonly Resample resample = new Resample();
        readonly Bitmap bmpScreen = InitScreenBuffer();

        Bitmap bmpSource;
        PVFile pvFile;
        int currentPage;

        public delegate void RenderEventHandler(Rectangle srcRect, double scale);
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);
        public delegate void FileChangedEventHandler(object sender, FileChangedEventArgs e);
        public event RenderEventHandler Render;
        public event PageChangedEventHandler PageChanged;
        public event FileChangedEventHandler FileChanged;

        public PVMainForm PVMainForm { get; set; }

        public PVImageViewer()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);

            resample.Destination = bmpScreen;

            viewPort.ScaleChanged += OnScaleChanged;
            viewPort.ScaleChanged += pvStatus.OnZoomChanged;

            pvStatus.ZoomRequest += Zoom;
            pvStatus.PageRequest += OnPageRequest;

            Resize += viewPort.OnPanelResize;
            Resize += OnResize;

            PageChanged += viewPort.OnPageChanged;
            PageChanged += OnPageChanged;
            PageChanged += pvStatus.OnPageChanged;

            FileChanged += OnFileChanged;
            FileChanged += pvStatus.OnFileChanged;

            MouseDown += HandleMouseDown;
            MouseMove += HandleMouseMove;
            MouseUp += HandleMouseUp;

            vScrollBar1.Scroll += vScroll_Scroll;
            hScrollBar1.Scroll += hScroll_Scroll;
            viewPort.vScroll = vScrollBar1;
            viewPort.hScroll = hScrollBar1;

            cMenuZoomFit.Click += delegate { ZoomToFit(); };


        }

        void OnPageRequest(int pageNumber)
        {
            SetPage(pageNumber);
        }

        void OnResize(object sender, EventArgs e)
        {
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
        }

        void vScroll_Scroll(object sender, ScrollEventArgs e)
        {
            viewPort.MoveDelta(0, e.NewValue - e.OldValue);
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
        }

        void hScroll_Scroll(object sender, ScrollEventArgs e)
        {
            viewPort.MoveDelta(e.NewValue - e.OldValue, 0);
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
        }

        void OnFileChanged(object sender, FileChangedEventArgs e)
        {

        }

        void OnPageChanged(object sender, PageChangedEventArgs e)
        {
            SetResampler();
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
            if (e.NullPage)
                InvokePaint(this, null);
        }

        void OnScaleChanged(object sender, ScaleChangedEventArgs e)
        {
            SetResampler();
        }

        void SetResampler()
        {
            Render = delegate { };
            if (bmpSource != null)
            {
                if (bmpSource.PixelFormat == PixelFormat.Format1bppIndexed)
                {
                    if (viewPort.Scale < 1.0D && viewPort.Scale > 0.50D)
                        Render = resample.MonoBilerp;
                    else
                        Render = resample.MonoBox;
                }
                if (bmpSource.PixelFormat == PixelFormat.Format8bppIndexed)
                    Render = resample.GrayBox;
                if (bmpSource.PixelFormat == PixelFormat.Format24bppRgb)
                    Render = resample.ColorBox;
                foreach (var del in Render.GetInvocationList())
                {
                    Debug.WriteLine(del.Method.Name);
                }
                Render += delegate { InvokePaint(this, null); };
                Render += delegate { Debug.WriteLine("Render"); };
            }
        }

        static Bitmap InitScreenBuffer()
        {
            var totalScreenSize = new Size();
            foreach (var screen in Screen.AllScreens)
            {
                totalScreenSize.Width += screen.Bounds.Width;
                totalScreenSize.Height += screen.Bounds.Height;
            }
            Debug.WriteLine("Screen {0}", totalScreenSize);

            return new Bitmap(totalScreenSize.Width, totalScreenSize.Height, PixelFormat.Format32bppPArgb);
        }

        public void SetFile(PVFile newFile)
        {
            if (pvFile != null)
                pvFile.Dispose();

            pvFile = newFile;
            if (pvFile == null)
            {
                FileChanged.Invoke(this, new FileChangedEventArgs());
                PageChanged.Invoke(this, new PageChangedEventArgs());

            }
            else
            {
                FileChanged.Invoke(this, new FileChangedEventArgs(pvFile.FileName, pvFile.PageCount));
                SetPage(1);
                //SetPage(currentPage < 1 ? 1 : currentPage > pvFile.PageCount ? pvFile.PageCount : currentPage);
            }
        }

        public void SetPageNext()
        {
            SetPage(currentPage + 1);
        }

        public void SetPagePrev()
        {
            SetPage(currentPage - 1);
        }

        public void SetPage(int page)
        {
            if (pvFile != null && page <= pvFile.PageCount && page > 0)
            {
                try
                {
                    PVMainForm.Cursor = Cursors.WaitCursor;

                    bmpSource = pvFile.LoadPage(page);
                    currentPage = page;
                    resample.Source = bmpSource;
                    PageChanged.Invoke(this, new PageChangedEventArgs(bmpSource.Size, currentPage, bmpSource.PixelFormat));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while attempting to load page {page} of file:{Environment.NewLine}{Environment.NewLine}{pvFile.FileName}{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (pvFile != null)
                    {
                        pvFile.Dispose();
                    }

                    PageChanged.Invoke(this, new PageChangedEventArgs());
                }
                finally
                {
                    PVMainForm.Cursor = Cursors.Default;
                }
            }
        }

        #region Public zoom methods
        public void ZoomToFit()
        {
            viewPort.ZoomToFit();
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
        }

        public void Zoom(double newScale)
        {
            viewPort.ZoomAbs(newScale);
            Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
        }
        #endregion

        #region Mouse activity ( zooming, panning, etc )

        bool panning, selecting;
        MouseEventArgs mouseDown;
        Point ptLast;

        void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.X <= viewPort.ScaledRect.Width && e.Location.Y <= viewPort.ScaledRect.Height)
            {
                mouseDown = e;
                ptLast.X = -1;
                ptLast.Y = -1;
            }
        }

        void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown != null && Math.Abs(mouseDown.X - e.X) > 2 && Math.Abs(mouseDown.Y - e.Y) > 2)
            {
                if (mouseDown.Button == MouseButtons.Left)
                {
                    selecting = true;
                    panning = false;
                    Cursor = Cursors.Cross;
                    if (ptLast.X != -1)
                    {
                        // There is already a rectangle drawn, erase it
                        DrawRect(mouseDown.Location, ptLast);
                    }
                    // Don't let the selection go outside the bounds of the panel or viewport
                    ptLast.X = Math.Max(e.X, 0);
                    ptLast.Y = Math.Max(e.Y, 0);
                    ptLast.X = Math.Min(ptLast.X, viewPort.ScaledRect.Width);
                    ptLast.Y = Math.Min(ptLast.Y, viewPort.ScaledRect.Height);

                    DrawRect(mouseDown.Location, ptLast);

                }
                else if (mouseDown.Button == MouseButtons.Right)
                {
                    panning = true;
                    selecting = false;
                    Cursor = Cursors.SizeAll;

                    if (ptLast.X != -1)
                    {
                        viewPort.MoveDelta(ptLast.X - e.Location.X, ptLast.Y - e.Location.Y);
                        Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
                    }
                    ptLast = e.Location;
                }
            }
        }

        void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (selecting)
            {
                var zoomRect = new Rectangle();
                zoomRect.X = Math.Min(mouseDown.X, ptLast.X);
                zoomRect.Y = Math.Min(mouseDown.Y, ptLast.Y);
                zoomRect.Width = Math.Abs(ptLast.X - mouseDown.X);
                zoomRect.Height = Math.Abs(ptLast.Y - mouseDown.Y);
                viewPort.Zoom(zoomRect);
                Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
            }
            else if (!panning && e.Button == MouseButtons.Right)
            {
                cMenu.Show(this, e.Location.X, e.Location.Y);
            }
            mouseDown = null;
            panning = false;
            selecting = false;
            ptLast.X = -1;
            ptLast.Y = -1;
            Cursor = Cursors.Default;
        }

        void DrawRect(Point p1, Point p2)
        {
            p1 = PointToScreen(p1);
            p2 = PointToScreen(p2);
            var rect = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            ControlPaint.DrawReversibleFrame(rect, Color.Black, FrameStyle.Thick);
        }

        public void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                var screenPt = PVMainForm.PointToScreen(e.Location);
                var mouseLocation = PointToClient(screenPt);
                if (!ClientRectangle.Contains(mouseLocation.X, mouseLocation.Y))
                    return;
                const int sensitivity = 25;
                int ticks = e.Delta / 120;
                double deltaScale = sensitivity * viewPort.Scale * ticks;
                viewPort.ZoomDelta(mouseLocation, deltaScale);
                Debug.WriteLine("Ticks {0} DeltaScale {1}", ticks, deltaScale);
                Render.Invoke(viewPort.ScaledRect, viewPort.Scale);
            }
        }

        #endregion

        #region Control paint
        protected override void OnPaint(PaintEventArgs e)
        {
            var region = new Region(new RectangleF(0, 0, ClientSize.Width - vScrollBar1.Width, ClientSize.Height - hScrollBar1.Height));
            region.Exclude(new Rectangle(0, 0, viewPort.ScaledRect.Width, viewPort.ScaledRect.Height));

            using (Graphics g = CreateGraphics())
            using (Brush ctrlBg = new SolidBrush(BackColor))
            using (Brush dkGray = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33)))
            {
                g.FillRegion(dkGray, region);
                g.FillRegion(ctrlBg, new Region(new RectangleF(vScrollBar1.Location.X, hScrollBar1.Location.Y, vScrollBar1.Width, hScrollBar1.Height)));
                g.Clip = new Region(new RectangleF(0, 0, viewPort.ScaledRect.Width, viewPort.ScaledRect.Height));
                g.DrawImage(bmpScreen, 0, 0);
            }

            //base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        #endregion
    }
}
