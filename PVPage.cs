using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace ProView
{
    public class PVPage : IDisposable
    {
        public PVPage(Bitmap bmp)
        {
            src = bmp;
            srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, src.PixelFormat);
            srcPx = (byte*)srcData.Scan0.ToPointer();
        }

        protected static int renderBoxWidth;
        public delegate void RenderBoxHandler(int size);
        public static event RenderBoxHandler RenderBoxWidthChanged;

        public static int RenderBoxWidth
        {
            get
            {
                return renderBoxWidth;
            }
            set
            {
                renderBoxWidth = value;
                RenderBoxWidthChanged.Invoke(renderBoxWidth);
            }
        }

        protected abstract void RenderBox();
        protected abstract void RenderBilerp();
        public delegate void RenderRequestHandler();
        public RenderRequestHandler PerformRender;

        public void OnViewPortChange(Rectangle vpRect)
        {
            dstRect = vpRect;
        }

        public virtual void OnScaleChange(double vpScale)
        {
            scale = vpScale;
            if (scale < 0.5D)
                PerformRender = RenderBox;
            else
                PerformRender = RenderBilerp;
        }

        public Bitmap ScreenBuffer
        {
            set
            {
                dst = value;
            }
        }

        public Size Size
        {
            get
            {
                return src.Size;
            }
        }

        public virtual void Dispose()
        {
            srcPx = null;
            if (src != null)
            {
                src.UnlockBits(srcData);
                src.Dispose();
                src = null;
            }
        }
    }
}
