using mupdf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Input;
using Cursors = System.Windows.Forms.Cursors;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace ProView
{
    internal sealed class ViewPanel : UserControl
    {
        private const PixelFormat PIXEL_FORMAT = PixelFormat.Format32bppPArgb;
        private const float ZOOM_STEP_IN = 1.25F;
        private const float ZOOM_STEP_OUT = 0.8F;
        private const float SCALE_MIN = 0.01F;
        private const float SCALE_MAX = 32.0F;
        private float _pageWidth;
        private float _pageHeight;
        private float _rotPageWidth;
        private float _rotPageHeight;
        private int _rotation = 0;
        private float _scale = 1;
        private bool _zoomFit = true;
        private Bitmap _bitmap;
        private RectangleF _viewport = new RectangleF();
        private MuRender _render;
        private FzDocument _document;
        private int _pageCount;
        private int _currentPageNumber;
        private int _canvasWidth;
        private int _canvasHeight;

        public void ZoomStepIn()
        {
            ZoomStep(_canvasWidth / 2, _canvasHeight / 2, ZOOM_STEP_IN);
        }

        public void ZoomStepOut()
        {
            ZoomStep(_canvasWidth / 2, _canvasHeight / 2, ZOOM_STEP_OUT);
        }

        public void ZoomFit()
        {
            _zoomFit = true;
            ClampScale(0);
            ClampViewport(0, 0);
        }

        // pinX and pinY need to be in client coordinates
        private void ZoomStep(float pinX, float pinY, float step)
        {
            _zoomFit = false;
            float newX = (pinX + _viewport.X) / _scale;
            float newY = (pinY + _viewport.Y) / _scale;
            ClampScale(_scale * step);
            newX = newX * _scale - pinX;
            newY = newY * _scale - pinY;
            Debug.Print($"{nameof(ZoomStep)} pinX={pinX} pinY={pinY} newX={newX} newY={newY}");
            ClampViewport(newX, newY);
        }

        private void ClampScale(float scale)
        {
            _scale = Math.Clamp(scale, Math.Max(SCALE_MIN, Math.Min(_canvasWidth / _rotPageWidth, _canvasHeight / _rotPageHeight)), SCALE_MAX);
        }

        private void ClampViewport(float x, float y, bool forceRender = false)
        {
            float scaleW = _scale * _rotPageWidth;
            float scaleH = _scale * _rotPageHeight;
            float newWidth = Math.Min(_canvasWidth, scaleW);
            float newX = Math.Clamp(x, 0, scaleW - newWidth);
            float newHeight = Math.Min(_canvasHeight, scaleH);
            float newY = Math.Clamp(y, 0, scaleH - newHeight);
            bool posChanged = newX != _viewport.X || newY != _viewport.Y;
            bool sizeChanged = newWidth != _viewport.Width || newHeight != _viewport.Height;
            _viewport.X = newX;
            _viewport.Y = newY;
            _viewport.Width = newWidth;
            _viewport.Height = newHeight;
            Debug.Print($"{nameof(ClampViewport)} {_viewport}");

            if (sizeChanged)
            {
                InitializeBitmap();
            }

            if (sizeChanged || posChanged || forceRender)
            {
                Render();
            }
        }

        private void Render()
        {
            BitmapData bmd = null;
            try
            {
                bmd = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, PIXEL_FORMAT);
                _render.Render(bmd, _scale, _rotation, _viewport.X, _viewport.Y);
            }
            finally
            {
                if (bmd is not null)
                {
                    _bitmap.UnlockBits(bmd);
                }
            }

            this.Refresh();
        }

        public ViewPanel()
        {
            InitializeComponents();
        }

        public enum Rotation
        {
            Right = 90,
            Left = -90
        }

        public void Rotate(Rotation rotation)
        {
            Debug.Print($"{nameof(Rotate)} {rotation}");
            _rotation += (int)rotation;

            if (_rotation < 0)
                _rotation = 270;

            if (_rotation > 270)
                _rotation = 0;

            if (_rotation == 90 || _rotation == 270)
            {
                _rotPageWidth = _pageHeight;
                _rotPageHeight = _pageWidth;
            }
            else
            {
                _rotPageWidth = _pageWidth;
                _rotPageHeight = _pageHeight;
            }

            if (_zoomFit)
            {
                ZoomFit();
            }
            else
            {
                ClampScale(_scale);
                ClampViewport(_viewport.X, _viewport.Y, true);
            }
        }

        public void LoadFile(string filename)
        {
            _render?.Dispose();
            _render = null;
            _document?.Dispose();
            _document = null;
            _document = new FzDocument(filename);
            _pageCount = _document.fz_count_pages();

            if (_pageCount < 1)
            {
                throw new Exception($"Failed to open file (invalid page count):\r\n{filename}");
            }

            _currentPageNumber = 1;
            LoadPage(_currentPageNumber);
        }

        public void UnloadFile()
        {
            _render?.Dispose();
            _render = null;
            _document?.Dispose();
            _document = null;
            _bitmap?.Dispose();
            _bitmap = null;
            this.Refresh();
            _pageCount = 0;
            _currentPageNumber = 0;
        }

        public void LoadPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > _pageCount)
            {
                return;
            }

            _currentPageNumber = pageNumber;
            var page = _document.fz_load_page(_currentPageNumber - 1);
            using var pageRect = page.fz_bound_page();
            using var pageIRect = pageRect.fz_irect_from_rect();
            _pageWidth = _rotPageWidth = (int)pageIRect.fz_irect_width();
            _pageHeight = _rotPageHeight = (int)pageIRect.fz_irect_height();

            if (_bitmap is null)
            {
                InitializeBitmap();
            }

            _render?.Dispose();
            _render = null;
            _render = new MuRender(page);

            Debug.Print($"{nameof(LoadPage)} {pageNumber} {{ {_pageWidth} x {_pageHeight} }}");

            if (_zoomFit)
            {
                ZoomFit();
            }
            else
            {
                ClampScale(_scale);
                ClampViewport(0, 0);
            }
        }

        //private void UnloadPage()
        //{
        //    _page?.Dispose();
        //    _displayList?.Dispose();
        //    _pageRect?.Dispose();
        //    _pageIrect?.Dispose();
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.Print($"{nameof(OnPaint)}");
            base.OnPaint(e);

            if (_bitmap is not null)
            {
                e.Graphics.DrawImageUnscaled(_bitmap, 0, 0, _bitmap.Width, _bitmap.Height);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!_mousePanning && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                float step = e.Delta > 0 ? ZOOM_STEP_IN : ZOOM_STEP_OUT;
                ZoomStep(e.Location.X, e.Location.Y, step);
            }
        }

        Point _mouseDownPoint = new Point(0, 0);
        bool _mousePanning = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mousePanning = true;
                _mouseDownPoint = e.Location;
                Cursor = Cursors.SizeAll;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mousePanning = false;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mousePanning)
            {
                int dx = _mouseDownPoint.X - e.X;
                int dy = _mouseDownPoint.Y - e.Y;
                _mouseDownPoint = e.Location;
                ClampViewport(_viewport.X + dx, _viewport.Y + dy);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetCavasSize();

            if (_currentPageNumber > 0)
            {
                Debug.Print(nameof(OnSizeChanged));
                InitializeBitmap();
                if (_zoomFit)
                {
                    ZoomFit();
                }
                else
                {
                    ClampScale(_scale);
                    ClampViewport(_viewport.X, _viewport.Y);
                }
            }

            base.OnSizeChanged(e);
        }

        private void SetCavasSize()
        {
            _canvasWidth = this.Width - 16;
            _canvasHeight = this.Height - 16;
        }

        private HScrollBar _hscrollbar = new HScrollBar();
        private VScrollBar _vscrollbar = new VScrollBar();
        private TextBox _txtScale = new TextBox();
        private TextBox _txtPageNumber = new TextBox();

        private void InitializeComponents()
        {
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(100, 100);

            _txtScale.Location = new Point(0, this.Height - 16);
            _txtScale.Size = new Size(48, 16);
            _txtScale.MaximumSize = new Size(48, 16);
            _txtScale.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            _txtScale.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);

            _txtPageNumber.Location = new Point(48, this.Height - 16);
            _txtPageNumber.Size = new Size(48, 16);
            _txtPageNumber.MaximumSize = new Size(48, 16);
            _txtPageNumber.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            _txtPageNumber.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);

            _hscrollbar.Location = new Point(96, this.Height - 16);
            _hscrollbar.Size = new Size(this.Width - 96 - 16, 16);
            _hscrollbar.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            _vscrollbar.Location = new Point(this.Width - 16, 0);
            _vscrollbar.Size = new Size(16, this.Height - 16);
            _vscrollbar.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            var pnl = new Panel();
            pnl.Size = new Size(16, 16);
            pnl.Location = new Point(this.Width - 16, this.Height - 16);
            pnl.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            pnl.BackColor = SystemColors.Control;

            this.Controls.Add(_hscrollbar);
            this.Controls.Add(_vscrollbar);
            this.Controls.Add(_txtScale);
            this.Controls.Add(_txtPageNumber);
            this.Controls.Add(pnl);

            this.BackColor = Color.FromArgb(0xCE, 0xCE, 0xCE);
            SetCavasSize();
        }

        private void InitializeBitmap()
        {
            _bitmap?.Dispose();
            int width = (int)(_viewport.Width + 0.5F);
            int height = (int)(_viewport.Height + 0.5F);

            Debug.Print($"{nameof(InitializeBitmap)} Width={width} Height={height}");

            if (width > 0 && height > 0)
            {
                _bitmap = new Bitmap(width, height, PIXEL_FORMAT);
            }
        }

        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _bitmap?.Dispose();
                    _document?.Dispose();
                }

                _disposedValue = true;
            }

            base.Dispose(disposing);
        }
    }

    internal sealed class MuRender : IDisposable
    {
        private const int PIX_MAP_FILL = unchecked((int)0xFFFFFFFF);
        private const int POINTS_PER_INCH = 72;
        private const int ALPHA = 1;
        //flags: flag bits.
        //Bit 0: If set, draw the image with linear interpolation.
        //Bit 1: If set, free the samples buffer when the pixmap is destroyed.
        //private const int PIXMAP_FLAGS = 0;
        private static readonly FzMatrix IDENTITY = new FzMatrix(1, 0, 0, 1, 0, 0);
        private static readonly FzColorspace DEFAULT_COLORSPACE = new FzColorspace(FzColorspace.Fixed.Fixed_BGR);
        private static readonly FzSeparations DEFAULT_SEPARATIONS = new FzSeparations();
        private static readonly FzRect INFINITE_RECT = new FzRect(FzRect.Fixed.Fixed_INFINITE);
        private FzPage _page;
        private FzDisplayList _displayList;
        private FzRect _pageRect;
        private FzCookie _cookie = new FzCookie();
        private bool _disposedValue;

        public MuRender(FzPage page)
        {
            _page = page;
            _displayList = _page.fz_new_display_list_from_page();
            _pageRect = _page.fz_bound_page();
        }

        public void Dispose()
        {
            _pageRect?.Dispose();
            _displayList?.Dispose();
            _page?.Dispose();
        }

        public void Render(BitmapData dest, float scale, int rotation, float xoff, float yoff)
        {
            using var pixmap = new FzPixmap(DEFAULT_COLORSPACE, dest.Width, dest.Height, DEFAULT_SEPARATIONS, ALPHA, dest.Stride, new SWIGTYPE_p_unsigned_char(dest.Scan0, true));
            //pixmap.m_internal.flags = 0;
            pixmap.fz_clear_pixmap_with_value(PIX_MAP_FILL);
            using var dev = new FzDevice(IDENTITY, pixmap);
            using var ctm = _pageRect.fz_transform_page(scale * POINTS_PER_INCH, rotation);
            ctm.e -= xoff;
            ctm.f -= yoff;
            _displayList.fz_run_display_list(dev, ctm, INFINITE_RECT, _cookie);
            Debug.Print($"{nameof(Render)} scale={scale} ctm.a={ctm.a} ctm.b={ctm.b} ctm.c={ctm.c} ctm.d={ctm.d} ctm.e={ctm.e} ctm.f={ctm.f}");
        }
    }
}
