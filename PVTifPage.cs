using System;
using System.Drawing;
using System.Drawing.Imaging;
using BitMiracle.LibTiff.Classic;

namespace ProView
{
    public class PVTifPage : ProView.PVPage
    {
        int stride;
        short bpp;
        Photometric photo;
                
        public string Make { get; set; }
        public string Model { get; set; }
        public string Software { get; set; }

        public PVTifPage(short pageNum) : base(pageNum) { }

        public bool ReadHeaders(Tiff tif)
        {
            stride = tif.ScanlineSize();
            if (stride < 1)
                return false;
            
            pxWidth = tif.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            pxHeight = tif.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
            
            bpp = tif.GetField(TiffTag.BITSPERSAMPLE)[0].ToShort();
            if (bpp != 1)
                return false;
            
            photo = (Photometric)(tif.GetField(TiffTag.PHOTOMETRIC)[0].ToInt());
            if (photo != Photometric.MINISBLACK && photo != Photometric.MINISWHITE)
                return false;

            FieldValue[] fieldValue;
            
            fieldValue = tif.GetField(TiffTag.XRESOLUTION);
            if (fieldValue != null)
                xRes = fieldValue[0].ToFloat();
            else
                xRes = 72.0F;
            printWidth = (float)Math.Round(pxWidth / xRes, 2);            
            
            fieldValue = tif.GetField(TiffTag.YRESOLUTION);
            if (fieldValue != null)
                yRes = fieldValue[0].ToFloat();
            else
                yRes = 72.0F;
            printHeight = (float)Math.Round(pxHeight / yRes, 2);
            
            fieldValue = tif.GetField(TiffTag.MAKE);
            if (fieldValue != null)
                Make = fieldValue[0].ToString();

            fieldValue = tif.GetField(TiffTag.MODEL);
            if (fieldValue != null)
                Model = fieldValue[0].ToString();
            
            fieldValue = tif.GetField(TiffTag.SOFTWARE);
            if (fieldValue != null)
                Software = fieldValue[0].ToString();

            return true;
        }

        public unsafe bool Load(Tiff tif)
        {
            byte[] buffer;
            int length = pxHeight * stride;
            try
            {
                pxData = new byte[length];
                buffer = new byte[stride];
            }
            catch (Exception)
            {
				throw new ApplicationException("Could not load page. Not enough memory.");
            }
            fixed (byte* src = buffer, dst = pxData)
            {
                byte* pSrc;
                byte* pDst;
                for (int y = 0; y < pxHeight; y++)
                {
                    pSrc = src;
                    pDst = dst + y * stride;
                    tif.ReadScanline(buffer, y);

                    for (int i = 0; i < stride; i++)
                    {
                        *pDst++ = *pSrc++;
                    }
                }
                // It seems most monochrome TIF files found in the wild use MINISWHITE
                if (photo == Photometric.MINISBLACK)
                {
                    pDst = dst;
                    for (int i = 0; i < length; i++)
                    {
                        *pDst++ ^= 0xFF;
                    }
                    //photo = Photometric.MINISWHITE;
                }
            }
            return true;
        }

        

        public override unsafe void RenderBox(Bitmap buffer, Rectangle dst, double scale)
        {
            BitmapData dstPx = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, buffer.PixelFormat);
            UInt32* pDstPx = (UInt32*)dstPx.Scan0.ToPointer();
            UInt32* pDst;

            double accumulator;
            int left, right, top, bottom;
            int boxWidth = Math.Min(renderBoxWidth, (int)(1 / scale));

            for (int y = dst.Top; y < dst.Bottom; y++)
            {
                pDst = pDstPx + (y - dst.Top) * dstPx.Width;
                top = (int)(y / scale);
                bottom = top + boxWidth;
                for (int x = dst.Left; x < dst.Right; x++)
                {
                    left = (int)(x / scale);
                    right = left + boxWidth;
                    accumulator = 0.0D;
                    for (int boxY = top; boxY < bottom; boxY++)
                    {
                        for (int boxX = left; boxX < right; boxX++)
                        {
                            accumulator += GetPixel(boxX, boxY);
                        }
                    }
                    UInt32 gray = (UInt32)(accumulator / (boxWidth * boxWidth));
                    *pDst = 0xFF000000 | ((UInt32)gray << 16) | ((UInt32)gray << 8) | (UInt32)gray;
                    pDst++;
                }
            }
            buffer.UnlockBits(dstPx);
        }
        
        public override unsafe void RenderBiLerp(Bitmap buffer, Rectangle dst, double scale)
        {
            
            
            BitmapData dstPx = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, buffer.PixelFormat);
            UInt32* pDstPx = (UInt32*)dstPx.Scan0.ToPointer();
            UInt32* pDst;

            //System.Diagnostics.Debug.WriteLine("{0} {1} {2} {3} {4}", scale, dst.Top, dst.Top / scale, dst.Left, dst.Left / scale);
            double realX, realY, fractionX, fractionY, oneMinusX, oneMinusY;
            int floorX, floorY, ceilX, ceilY;
            for (int y = dst.Top; y < dst.Bottom; y++)
            {
                realY = y / scale;
                floorY = (int)Math.Floor(realY);
                ceilY = (int)Math.Ceiling(realY);
                fractionY = realY - floorY;
                oneMinusY = 1.0F - fractionY;
                pDst = pDstPx + (y - dst.Top) * dstPx.Width;
                for (int x = dst.Left; x < dst.Right; x++)
                {
                    realX = x / scale;
                    
                    floorX = (int)Math.Floor(realX);
                    ceilX = (int)Math.Ceiling(realX);
                    fractionX = realX - floorX;
                    oneMinusX = 1.0F - fractionX;


                    UInt32 gray = (UInt32)((GetPixel(floorX, floorY) * oneMinusX + GetPixel(ceilX, floorY) * fractionX) * oneMinusY + (GetPixel(floorX, ceilY) * oneMinusX + GetPixel(ceilX, ceilY) * fractionX) * fractionY);

                    *pDst = 0xFF000000 | (gray << 16) | (gray << 8) | gray;
                    pDst++;
                }
            }
            buffer.UnlockBits(dstPx);
        }
    }
}

/*
 * public static unsafe void Render(Bitmap buffer, Rectangle src, Rectangle dst)
        {

            double xScale = dst.Width / (double)src.Width;
            double yScale = dst.Height / (double)src.Height;


            double realX, realY, fractionX, fractionY, oneMinusX, oneMinusY;
            int floorX, floorY, ceilX, ceilY;
            double p1, p2, p3, p4;

            BitmapData dstPx = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, buffer.PixelFormat);
            UInt32* pDstPx = (UInt32*)dstPx.Scan0.ToPointer();
            UInt32* pDst;

            for (int y = dst.Top; y < dst.Bottom; y++)
            {
                realY = Math.Min(y / yScale, src.Bottom - 1);
                floorY = (int)Math.Floor(realY);
                ceilY = (int)Math.Ceiling(realY);
                fractionY = realY - floorY;
                oneMinusY = 1.0D - fractionY;
                pDst = pDstPx + (y - dst.Top) * dstPx.Width;
                for (int x = dst.Left; x < dst.Right; x++)
                {
                    realX = x / xScale;
                    floorX = (int)Math.Floor(realX);
                    ceilX = (int)Math.Ceiling(realX);
                    fractionX = realX - floorX;
                    oneMinusX = 1.0D - fractionX;

                    if ((page.PxData[floorY * page.Stride + (floorX >> 3)] & (byte)(0x80 >> (floorX & 0x7))) == 0)
                        p1 = 0;
                    else
                        p1 = 1;

                    if ((page.PxData[floorY * page.Stride + (ceilX >> 3)] & (byte)(0x80 >> (ceilX & 0x7))) == 0)
                        p2 = 0;
                    else
                        p2 = 1;

                    if ((page.PxData[ceilY * page.Stride + (floorX >> 3)] & (byte)(0x80 >> (floorX & 0x7))) == 0)
                        p3 = 0;
                    else
                        p3 = 1;

                    if ((page.PxData[ceilY * page.Stride + (ceilX >> 3)] & (byte)(0x80 >> (ceilX & 0x7))) == 0)
                        p4 = 0;
                    else
                        p4 = 1;

                    double avgTop = p1 * oneMinusX + p2 * fractionX;
                    double avgBot = p3 * oneMinusX + p4 * fractionX;
                    double avg = avgTop * oneMinusY + avgBot * fractionY;
                    byte gray = (byte)Math.Round(255.0D * avg, 0);

                    *pDst = 0xFF000000 | ((UInt32)gray << 16) | ((UInt32)gray << 8) | (UInt32)gray;
                    pDst++;
                }
            }
            buffer.UnlockBits(dstPx);
        }
 * */

/*
static double tfilter(double t)
{
    if (t < 0.0)
        t = -t;
    if (t < 1.0)
        return 1.0D - t;
    return 0.0D;
}
 * */
/*
        public override unsafe void Render(Bitmap buffer, Rectangle src, Rectangle dst)
        {
            BitmapData dstPx = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, buffer.PixelFormat);
            UInt32* pDstPx = (UInt32*)dstPx.Scan0.ToPointer();
            UInt32* pDst;

            double scale = Math.Min(dst.Width / (double)src.Width, dst.Height / (double)src.Height);
            double fScale = scale > 1.0D ? 1.0D : 1.0D / scale;
            double width = scale > 1.0D ? 2.0D : 2.0D / scale;
            double centerX, centerY;
            int dstX, dstY;
            int srcX, srcY;
            double accumulator;
            int left, right, top, bot;

            for (dstY = dst.Top; dstY < dst.Bottom - 1; dstY++)
            {
                centerY = (dstY + 0.5D) / scale;
                top = (int)Math.Ceiling(centerY - width);
                bot = (int)Math.Floor(centerY + width);
                pDst = pDstPx + (dstY - dst.Top) * dstPx.Width;
                for (dstX = dst.Left; dstX < dst.Right - 1; dstX++)
                {
                    centerX = (dstX + 0.5D) / scale;
                    left = (int)Math.Ceiling(centerX - width);
                    right = (int)Math.Floor(centerX + width);
                    accumulator = 0;
                    for (srcY = top; srcY <= bot; srcY++)
                    {
                        double vWeight = tfilter((srcY - centerY) / fScale) / fScale;
                        for (srcX = left; srcX <= right; srcX++)
                        {
                            double hWeight = tfilter((srcX - centerX) / fScale) / fScale;
                            double pixel = GetPixel(srcX, srcY);
                            accumulator += pixel * vWeight * hWeight;
                        }
                    }
                    byte gray = (byte)accumulator;
                    *pDst = 0xFF000000 | ((UInt32)gray << 16) | ((UInt32)gray << 8) | (UInt32)gray;
                    pDst++;
                }
            }
            buffer.UnlockBits(dstPx);
        }
         * */