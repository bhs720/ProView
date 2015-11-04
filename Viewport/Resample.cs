using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProView
{
	public class Resample
	{
		public Resample() {}
		
		public Bitmap Source { get; set; }
		
		public Bitmap Destination { get; set; }
		
		public unsafe void MonoBox(Rectangle srcRect, double scale)
		{
			BitmapData bdSrc = Source.LockBits(new Rectangle(0, 0, Source.Width, Source.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
			BitmapData bdDst = Destination.LockBits(new Rectangle(0, 0, Destination.Width, Destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			
			byte* pSrc = (byte*)bdSrc.Scan0.ToPointer();
			byte* pDst = (byte*)bdDst.Scan0.ToPointer();
			
			int boxWidth = (int)Math.Floor(1 / scale);
			boxWidth = Math.Min(boxWidth, 8);
			boxWidth = Math.Max(boxWidth, 1);
			double boxArea = boxWidth * boxWidth;
			
			for (int y = srcRect.Top; y < srcRect.Bottom; y++)
			{
				int srcY = (int)(y / scale);
				int boxBottom = srcY + boxWidth;
				byte* ppDst = pDst + (y - srcRect.Top) * bdDst.Stride;
				for (int x = srcRect.Left; x < srcRect.Right; x++)
				{
					int srcX = (int)(x / scale);
					int boxRight = srcX + boxWidth;
					int accumulator = 0;
					
					for (int boxY = srcY; boxY < boxBottom; boxY++)
					{
						int boundY = Math.Min(boxY, bdSrc.Height - 1);
						for (int boxX = srcX; boxX < boxRight; boxX++)
						{
							int boundX = Math.Min(boxX, bdSrc.Width - 1);
							accumulator += (pSrc[boundY * bdSrc.Stride + (boundX >> 3)] & (byte)(0x80 >> (boundX & 0x7))) == 0 ? 255 : 0;
						}
					}
					byte gray = (byte)Math.Round(accumulator / boxArea);
					*ppDst++ = gray;
					*ppDst++ = gray;
					*ppDst++ = gray;
					*ppDst++ = 0xFF;
				}
			}
			
			Source.UnlockBits(bdSrc);
			Destination.UnlockBits(bdDst);
		}
		
		public unsafe void MonoBilerp(Rectangle srcRect, double scale)
		{
			BitmapData bdSrc = Source.LockBits(new Rectangle(0, 0, Source.Width, Source.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
			BitmapData bdDst = Destination.LockBits(new Rectangle(0, 0, Destination.Width, Destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			
			byte* pSrc = (byte*)bdSrc.Scan0.ToPointer();
			byte* pDst = (byte*)bdDst.Scan0.ToPointer();
			
            for (int y = srcRect.Top; y < srcRect.Bottom; y++)
            {
                double srcY = y / scale;
                int topY = Math.Min(bdSrc.Height - 1, (int)Math.Floor(srcY));
                int botY = Math.Min(bdSrc.Height - 1, (int)Math.Ceiling(srcY));
                double botWeight = srcY - topY;
                double topWeight = 1.0D - botWeight;
                byte* ppDst = pDst + (y - srcRect.Top) * bdDst.Stride;
                for (int x = srcRect.Left; x < srcRect.Right; x++)
                {
                    double srcX = x / scale;
                    int leftX = Math.Min(bdSrc.Width - 1, (int)Math.Floor(srcX));
					int rightX = Math.Min(bdSrc.Width - 1, (int)Math.Ceiling(srcX));
					double rightWeight = srcX - leftX;
					double leftWeight = 1.0D - rightWeight;
                    
                    byte gray = (byte)Math.Round(
						topWeight * (((pSrc[topY * bdSrc.Stride + (leftX >> 3)] & (byte)(0x80 >> (leftX & 0x7))) == 0 ? 255 : 0) * leftWeight + ((pSrc[topY * bdSrc.Stride + (rightX >> 3)] & (byte)(0x80 >> (rightX & 0x7))) == 0 ? 255 : 0) * rightWeight) +
						botWeight * (((pSrc[botY * bdSrc.Stride + (leftX >> 3)] & (byte)(0x80 >> (leftX & 0x7))) == 0 ? 255 : 0) * leftWeight + ((pSrc[botY * bdSrc.Stride + (rightX >> 3)] & (byte)(0x80 >> (rightX & 0x7))) == 0 ? 255 : 0) * rightWeight)
                    );
                    
                    *ppDst++ = gray;
                    *ppDst++ = gray;
                    *ppDst++ = gray;
                    *ppDst++ = 0xFF;
                }
            }
            
            Source.UnlockBits(bdSrc);
			Destination.UnlockBits(bdDst);
		}
		
		public unsafe void GrayBox(Rectangle srcRect, double scale)
		{
			BitmapData bdSrc = Source.LockBits(new Rectangle(0, 0, Source.Width, Source.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
			BitmapData bdDst = Destination.LockBits(new Rectangle(0, 0, Destination.Width, Destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			
			byte* pSrc = (byte*)bdSrc.Scan0.ToPointer();
			byte* pDst = (byte*)bdDst.Scan0.ToPointer();
			
			int boxWidth = (int)Math.Floor(1 / scale);
			boxWidth = Math.Min(boxWidth, 4);
			boxWidth = Math.Max(boxWidth, 1);
			double boxArea = boxWidth * boxWidth;
			
			for (int y = srcRect.Top; y < srcRect.Bottom; y++)
			{
				int srcY = (int)(y / scale);
				int boxBottom = srcY + boxWidth;
				byte* ppDst = pDst + (y - srcRect.Top) * bdDst.Stride;
				for (int x = srcRect.Left; x < srcRect.Right; x++)
				{
					int srcX = (int)(x / scale);
					int boxRight = srcX + boxWidth;
					int accumulator = 0;
					for (int boxY = srcY; boxY < boxBottom; boxY++)
					{
						for (int boxX = srcX; boxX < boxRight; boxX++)
						{
							accumulator += pSrc[Math.Min(boxY, bdSrc.Height - 1) * bdSrc.Stride + Math.Min(boxX, bdSrc.Width - 1)];
						}
					}
					byte px = (byte)Math.Round(accumulator / boxArea);
					*ppDst++ = px;
					*ppDst++ = px;
					*ppDst++ = px;
					*ppDst++ = 0xFF;
				}
			}
			
			Source.UnlockBits(bdSrc);
			Destination.UnlockBits(bdDst);
		}
		
		public unsafe void ColorBox(Rectangle srcRect, double scale)
		{
			BitmapData bdSrc = Source.LockBits(new Rectangle(0, 0, Source.Width, Source.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			BitmapData bdDst = Destination.LockBits(new Rectangle(0, 0, Destination.Width, Destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			
			byte* pSrc = (byte*)bdSrc.Scan0.ToPointer();
			byte* pDst = (byte*)bdDst.Scan0.ToPointer();
			
			int boxWidth = (int)Math.Floor(1 / scale);
			boxWidth = Math.Min(boxWidth, 4);
			boxWidth = Math.Max(boxWidth, 1);
			double boxArea = boxWidth * boxWidth;
			
			for (int y = srcRect.Top; y < srcRect.Bottom; y++)
			{
				int srcY = (int)(y / scale);
				int boxBottom = srcY + boxWidth;
				byte* ppDst = pDst + (y - srcRect.Top) * bdDst.Stride;
				for (int x = srcRect.Left; x < srcRect.Right; x++)
				{
					int srcX = (int)(x / scale);
					int boxRight = srcX + boxWidth;
					int r = 0, g = 0, b = 0;
					for (int boxY = srcY; boxY < boxBottom; boxY++)
					{
						for (int boxX = srcX; boxX < boxRight; boxX++)
						{
							byte* srcPx = pSrc + Math.Min(boxY, bdSrc.Height - 1) * bdSrc.Stride + Math.Min(boxX, bdSrc.Width - 1) * 3;
							b += *srcPx++;
							g += *srcPx++;
							r += *srcPx;
						}
					}
					*ppDst++ = (byte)Math.Round(b / boxArea);
					*ppDst++ = (byte)Math.Round(g / boxArea);
					*ppDst++ = (byte)Math.Round(r / boxArea);
					*ppDst++ = 0xFF;
				}
			}
			
			Source.UnlockBits(bdSrc);
			Destination.UnlockBits(bdDst);
		}
	}
}
