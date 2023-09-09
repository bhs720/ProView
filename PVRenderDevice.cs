using mupdf;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ProView
{
    internal sealed class PVRenderDevice : IDisposable
    {
        
        private const int PIXMAP_FILL = unchecked((int)0xFFFFFFFF);
        private const int POINTS_PER_INCH = 72;
        private const int ALPHA = 1;
        public const PixelFormat DEFAULT_PIXEL_FORMAT = PixelFormat.Format32bppPArgb;
        //flags: flag bits.
        //Bit 0: If set, draw the image with linear interpolation.
        //Bit 1: If set, free the samples buffer when the pixmap is destroyed.
        //private const int PIXMAP_FLAGS = 0;
        private static readonly FzMatrix IDENTITY = new FzMatrix(1, 0, 0, 1, 0, 0);
        private static readonly FzColorspace DEFAULT_COLORSPACE = new FzColorspace(FzColorspace.Fixed.Fixed_BGR);
        private static readonly FzSeparations DEFAULT_SEPARATIONS = new FzSeparations();
        private static readonly FzRect INFINITE_RECT = new FzRect(FzRect.Fixed.Fixed_INFINITE);
        //private FzPage _page;
        private FzDisplayList _fzDisplayList;
        private FzRect _fzPageRect;
        //private int _refCount;
        //private object _threadLocker;
        //private PVPage _pvPage;
        //private bool _isDisposed;
        private HashSet<object> _users = new HashSet<object>();

        public bool IsDisposed { get; private set; }

        public PVPage Page { get; private set; }

        //public event EventHandler<DisposingEventArgs> Disposing;

        //public class DisposingEventArgs : EventArgs
        //{
        //    public bool Cancel { get; set; }
        //}

        public PVRenderDevice(PVPage pvPage, FzPage fzPage)
        {
            Page = pvPage;
            _fzDisplayList = fzPage.fz_new_display_list_from_page();
            _fzPageRect = fzPage.fz_bound_page();
        }

        public void Render(Bitmap dest, float scale, float xoff, float yoff)
        {
            var bmd = dest.LockBits(new Rectangle(0, 0, dest.Width, dest.Height), ImageLockMode.WriteOnly, dest.PixelFormat);
            using var pix = new FzPixmap(DEFAULT_COLORSPACE, bmd.Width, bmd.Height, DEFAULT_SEPARATIONS, ALPHA, bmd.Stride, new SWIGTYPE_p_unsigned_char(bmd.Scan0, false));
            pix.fz_clear_pixmap_with_value(PIXMAP_FILL);
            using var dev = new FzDevice(IDENTITY, pix);
            using var ctm = _fzPageRect.fz_transform_page(scale * POINTS_PER_INCH, Page.Rotation);
            ctm.e -= xoff;
            ctm.f -= yoff;
            using var cookie = new FzCookie();
            _fzDisplayList.fz_run_display_list(dev, ctm, INFINITE_RECT, cookie);
            Debug.Print($"{nameof(Render)} scale={scale} ctm.a={ctm.a} ctm.b={ctm.b} ctm.c={ctm.c} ctm.d={ctm.d} ctm.e={ctm.e} ctm.f={ctm.f}");
            dest.UnlockBits(bmd);
        }

        public void Acquire(object owner)
        {
            _users.Add(owner);
        }

        public void Release(object owner)
        {
            if (_users.Remove(owner) && _users.Count == 0)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _fzPageRect?.Dispose();
            _fzPageRect = null;
            _fzDisplayList?.Dispose();
            _fzDisplayList = null;
            IsDisposed = true;
        }

        //private void OnDisposing(object sender, DisposingEventArgs e)
        //{
        //    var handler = Disposing;
        //    handler?.Invoke(sender, e);
        //}


        //public void Render(FzPixmap dest, float scale, int rotation, float xoff, float yoff)
        //{
        //    dest.fz_clear_pixmap_with_value(PIXMAP_FILL);
        //    using var dev = new FzDevice(IDENTITY, dest);
        //    using var ctm = _pageRect.fz_transform_page(scale * POINTS_PER_INCH, rotation);
        //    ctm.e -= xoff;
        //    ctm.f -= yoff;
        //    _displayList.fz_run_display_list(dev, ctm, INFINITE_RECT, _cookie);
        //    Debug.Print($"{nameof(Render)} scale={scale} ctm.a={ctm.a} ctm.b={ctm.b} ctm.c={ctm.c} ctm.d={ctm.d} ctm.e={ctm.e} ctm.f={ctm.f}");
        //}
        //}
    }
}
