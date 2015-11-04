using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ProView
{
    public unsafe class Renderer
    {
        Bitmap _dst;
        BitmapData _dstData;
        byte* _dstPx;
        Rectangle _dstRect;

        Bitmap _src;
        BitmapData _srcData;
        byte* _srcPx;
        
        double _scale;
        delegate void RenderOp();
        RenderOp renderOp;
        int renderBoxWidth = 4;

        public Renderer()
        {
            Size totalScreenSize = new Size(0, 0);
            foreach (Screen s in Screen.AllScreens)
            {
                totalScreenSize.Width += s.Bounds.Width;
                totalScreenSize.Height += s.Bounds.Height;
            }

            try
            {
                _dst = new Bitmap(totalScreenSize.Width, totalScreenSize.Height, PixelFormat.Format32bppPArgb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ~Renderer()
        {
            if (_dst != null)
            {
                _dst.Dispose();
            }
            renderOp = null;
        }

        public void OnPageChange(Bitmap source)
        {
            if (_src != null)
            {
                _src.UnlockBits(_srcData);
                _src.Dispose();
            }
            if ((_src = source) == null)
                return;
            _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, _src.PixelFormat);
            _srcPx = (byte*)_srcData.Scan0.ToPointer();
            (renderOp = GetRenderOp()).Invoke();
        }

        public void OnViewportChange(double scale, Rectangle dstRect)
        {
            _dstRect = dstRect;
            _scale = scale;
            if (_src == null)
                return;
            (renderOp = GetRenderOp()).Invoke();
        }

        RenderOp GetRenderOp()
        {
            if (_scale < 0.5D)
            {
                switch (_src.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        return MonoBox;
                    case PixelFormat.Format24bppRgb:
                        return ColorBox;
                }       
            }
            else
            {
                switch (_src.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        return MonoBilerp;
                    case PixelFormat.Format24bppRgb:
                        return ColorBilerp;
                }
            }
            return null;
        }

        /// <summary>
        /// For 1bpp images
        /// </summary>
        

    }
}
