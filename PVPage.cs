using mupdf;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ProView
{
    internal sealed class PVPage : IDisposable
    {
        private int _rotation;
        private float _pageWidth;
        private float _pageHeight;
        private float _rotPageWidth;
        private float _rotPageHeight;
        //private PVRenderDevice _renderDevice;
        //private int _renderDeviceRefCount;
        //private FzDocument _fzDocument;
        //private int _renderRefCount;
        //private object _threadLocker;
        //private List<RenderLease> _leases;
        private FzPage _fzPage;
        private PVRenderDevice _renderDevice;

        public event EventHandler RotationChanged;

        //public PVDocument Document { get; private set; }
        public float Width => _pageWidth;
        public float Height => _pageHeight;
        public float RotWidth => _rotPageWidth;
        public float RotHeight => _rotPageHeight;
        public int Number { get; private set; }
        public int Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                int temp = value;

                while (temp < 0)
                {
                    temp += 360;
                }

                temp = temp / 90 % 4 * 90;

                if (temp == 90 || temp == 270)
                {
                    _rotPageWidth = _pageHeight;
                    _rotPageHeight = _pageWidth;
                }
                else
                {
                    _rotPageWidth = _pageWidth;
                    _rotPageHeight = _pageHeight;
                }

                if (_rotation != temp)
                {
                    _rotation = temp;
                    OnRotationChanged(this, EventArgs.Empty);
                }
            }
        }

        public PVPage(PVDocument document, FzDocument fzDocument, int number)
        {
            //Document = document;
            Number = number;
            //_fzDocument = fzDocument;
            _fzPage = fzDocument.fz_load_page(Number - 1);
            using var fzPageRect = _fzPage.fz_bound_page();
            _pageWidth = _rotPageWidth = fzPageRect.x1 - fzPageRect.x0;
            _pageHeight = _rotPageHeight = fzPageRect.y1 - fzPageRect.y0;
        }

        public PVRenderDevice GetRenderDevice(object owner)
        {
            if (_renderDevice is null || _renderDevice.IsDisposed)
            {
                _renderDevice = new PVRenderDevice(this, _fzPage);
                _renderDevice.Acquire(owner);
            }

            return _renderDevice;
        }

        private void OnRotationChanged(object sender, EventArgs e)
        {
            var handler = this.RotationChanged;
            handler?.Invoke(this, e);
        }

        public void Dispose()
        {
            _fzPage?.Dispose();
            _fzPage = null;
        }

        //public class RenderLease : IDisposable
        //{
        //    public bool IsDisposed { get; private set; }
        //    public object Owner { get; private set; }

        //    public RenderLease(object owner)
        //    {
        //        Owner = owner;
        //    }

        //    public void Dispose()
        //    {
        //        if (!IsDisposed)
        //        {
        //            OnDisposing(Owner, EventArgs.Empty);
        //            IsDisposed = true;
        //        }
        //    }

        //    public event EventHandler Disposed;

        //    private void OnDisposing(object sender, EventArgs e)
        //    {
        //        var handler = Disposed;
        //        handler?.Invoke(sender, e);
        //    }
        //}

        //public PVRenderDevice GetRenderDevice()
        //{
        //    if (_renderDevice is null || _renderDevice.IsDisposed)
        //    {
        //        using var fzPage = _fzDocument.fz_load_page(Number - 1);
        //        _renderDevice = new PVRenderDevice(this, fzPage);
        //        _renderDeviceRefCount = 1;
        //        _renderDevice.Disposing += RenderDevice_Disposing;
        //    }
        //    else
        //    {
        //        _renderDeviceRefCount++;
        //    }

        //    return _renderDevice;
        //}

        //private void RenderDevice_Disposing(object sender, PVRenderDevice.DisposingEventArgs e)
        //{
        //    _renderDeviceRefCount--;
        //    e.Cancel = _renderDeviceRefCount > 0;
        //}

    }
}
