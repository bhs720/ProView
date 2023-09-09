using mupdf;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Input;
using Cursors = System.Windows.Forms.Cursors;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace ProView
{
    internal sealed class PVPanel : UserControl
    {
        //private PVPage _page;
        private const PixelFormat PIXEL_FORMAT = PixelFormat.Format32bppPArgb;
        private const float ZOOM_STEP_IN = 1.25F;
        private const float ZOOM_STEP_OUT = 0.8F;
        private const float SCALE_MIN = 0.01F;
        private const float SCALE_MAX = 32.0F;
        private float _scale = 1;
        private bool _zoomFit = true;
        private Bitmap _canvas;
        private FzPixmap _pixmap;
        private RectangleF _viewport = new RectangleF();
        private PVRenderDevice _renderDevice;
        //private float _pageWidth;
        //private float _pageHeight;

        private PVPage Page => _renderDevice.Page;

        public void ZoomStepIn()
        {
            ZoomStep(_canvas.Width / 2, _canvas.Height / 2, ZOOM_STEP_IN);
        }

        public void ZoomStepOut()
        {
            ZoomStep(_canvas.Width / 2, _canvas.Height / 2, ZOOM_STEP_OUT);
        }

        public void ZoomFit()
        {
            _zoomFit = true;
            ClampScale(0);
            ClampViewport();
        }

        /// <summary>
        /// pinX and pinY need to be in client coordinates
        /// </summary>
        /// <param name="pinX"></param>
        /// <param name="pinY"></param>
        /// <param name="step"></param>
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
            _scale = Math.Clamp(scale, Math.Max(SCALE_MIN, Math.Min(_canvas.Width / Page.RotWidth, _canvas.Height / Page.RotHeight)), SCALE_MAX);
        }

        private void ClampViewport()
        {
            ClampViewport(_viewport.X, _viewport.Y, true);
        }

        private void ClampViewport(float x, float y, bool forceRender = false)
        {
            float scaleW = _scale * Page.RotWidth;
            float scaleH = _scale * Page.RotHeight;
            float newWidth = Math.Min(_canvas.Width, scaleW);
            float newX = Math.Clamp(x, 0, scaleW - newWidth);
            float newHeight = Math.Min(_canvas.Height, scaleH);
            float newY = Math.Clamp(y, 0, scaleH - newHeight);
            bool posChanged = newX != _viewport.X || newY != _viewport.Y;
            bool sizeChanged = newWidth != _viewport.Width || newHeight != _viewport.Height;
            _viewport.X = newX;
            _viewport.Y = newY;
            _viewport.Width = newWidth;
            _viewport.Height = newHeight;
            Debug.Print($"{nameof(ClampViewport)} {_viewport}");

            if (sizeChanged || posChanged || forceRender)
            {
                _renderDevice.Render(_canvas, _scale, _viewport.X, _viewport.Y);
                this.Refresh();
            }
        }

        public PVPanel()
        {
            InitializeComponents();
        }

        public void SetPage(PVPage page)
        {
            //if (_page is not null)
            //{
            //    _page.RotationChanged -= PVPage_RotationChanged;
            //}

            _renderDevice?.Release(this);
            _renderDevice = null;
            //_page = page;

            if (page is not null)
            {
                _renderDevice = page.GetRenderDevice(this);

                if (_zoomFit)
                {
                    ZoomFit();
                }
                else
                {
                    ClampScale(_scale);
                    ClampViewport();
                }
            }
            else
            {
                Refresh();
            }
        }

        //private void PVPage_RotationChanged(object sender, EventArgs e)
        //{
        //    if (_zoomFit)
        //    {
        //        ZoomFit();
        //    }
        //    else
        //    {
        //        ClampScale(_scale);
        //        ClampViewport();
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.Print($"{nameof(OnPaint)}");
            base.OnPaint(e);

            if (_renderDevice is not null)
            {
                e.Graphics.DrawImageUnscaledAndClipped(_canvas, new Rectangle(0, 0, (int)_viewport.Width, (int)_viewport.Height));
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_renderDevice is not null && !_mousePanning && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                float step = e.Delta > 0 ? ZOOM_STEP_IN : ZOOM_STEP_OUT;
                ZoomStep(e.Location.X, e.Location.Y, step);
            }
        }

        Point _mouseDownPoint = new Point(0, 0);
        bool _mousePanning = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_renderDevice is not null && e.Button == MouseButtons.Middle)
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
            InitializeBuffer();

            if (_renderDevice is not null)
            {
                Debug.Print(nameof(OnSizeChanged));

                if (_zoomFit)
                {
                    ZoomFit();
                }
                else
                {
                    ClampScale(_scale);
                    ClampViewport();
                }
            }

            base.OnSizeChanged(e);
        }

        private HScrollBar _hscrollbar = new HScrollBar();
        private VScrollBar _vscrollbar = new VScrollBar();
        private TextBox _txtScale = new TextBox();
        private TextBox _txtPageNumber = new TextBox();

        private void InitializeComponents()
        {
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(100, 100);
            this.Dock = DockStyle.Fill;

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
            InitializeBuffer();
        }

        private void InitializeBuffer()
        {
            _canvas?.Dispose();
            int width = this.Width - 16;
            int height = this.Height - 16;
            Debug.Print($"{nameof(InitializeBuffer)} Width={width} Height={height}");
            _canvas = new Bitmap(width, height, PIXEL_FORMAT);
        }

        bool _disposedValue = false;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _renderDevice?.Dispose();
                    _renderDevice = null;
                    _canvas?.Dispose();
                    _canvas = null;
                }

                _disposedValue = true;
            }

            base.Dispose(disposing);
        }
    }
}
