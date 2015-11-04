/*
 * Created by SharpDevelop.
 * User: ben
 * Date: 6/24/2012
 * Time: 2:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BitMiracle.LibTiff.Classic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
//using System.Runtime.InteropServices;

//namespace ProView
//{
//    public class PVTifFile : PVFile
//    {
//        public PVTifFile(string fullName) : base(fullName){}

//        public override bool Open()
//        {
//            using (Tiff tif = Tiff.Open(fullName, "r"))
//            {
//                if (tif == null)
//                    return false;
//                pageCount = tif.NumberOfDirectories();
//            }

//            if (pageCount < 1)
//                return false;
			
//            return true;
//        }

//        public override PVPage LoadPage(int pageNumber)
//        {
//            if (pageNumber < 0 || pageNumber >= pageCount || !Open())
//                return null;

//            PVPage pvPage = null;
//            using (Tiff tif = Tiff.Open(fullName, "r"))
//            {

//            }
//        }

//        public override void UnloadPage(short number)
//        {
//            pages[number].Unload();
//        }
//    }
//}

   

//using (Tiff tif = Tiff.Open(file, "r"))
//{
//    if (tif == null) {

//        throw new Exception("The file format could not be determined.");
//    }


//				fieldValue = tif.GetField(TiffTag.SAMPLESPERPIXEL);
//				short spp = fieldValue[0].ToShort();
//
//				if (spp != 1)
//				{
//					error = TiffError.SppNoSupport;
//					return;
//				}


//}




//public unsafe void LoadImage()
//{
//    using (Tiff tif = Tiff.Open(file, "r"))
//    {
//        if (tif == null)
//        {
//            return;
//        }
//        this.bmp = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
//        this.bmd = bmp.LockBits(new Rectangle(0, 0, this.width, this.height),
//                                ImageLockMode.ReadWrite,
//                                PixelFormat.Format1bppIndexed);
//        byte* pBmd = (byte*)bmd.Scan0.ToPointer();
//        byte* pDst;
//        byte[] buffer = new byte[this.stride];
//        fixed (byte* pBfr = buffer)
//        {
//            byte* pSrc = pBfr;

//            for (int y = 0; y < this.height; y++)
//            {
//                tif.ReadScanline(buffer, y);
//                pDst = pBmd + this.bmd.Stride * y;
//                for (int i = 0; i < this.stride; i++)
//                {
//                    *pDst++ = *pSrc++;
//                }
//                pSrc = pBfr;
//            }
//        }

//        if (this.photo == Photometric.MINISBLACK)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                pDst = pBmd + this.bmd.Stride * y;
//                for (int s = 0; s < this.stride; s++)
//                {
//                    *pDst++ ^= 0xFF;
//                }
//            }
//        }
//    }
//}

//public void UnloadImage()
//{
//    if (bmd != null) {
//        try {
//            bmp.UnlockBits(bmd);
//        } finally {
//            bmd = null;
//        }
//    }
//    if (bmp != null) {
//        bmp.Dispose();
//        bmp = null;
//    }
//}

//void WriteTiffTags(Tiff tif)
//{
//    tif.SetField(TiffTag.COMPRESSION, Compression.CCITT_T6);
//    tif.SetField(TiffTag.IMAGEWIDTH, this.width);
//    tif.SetField(TiffTag.IMAGELENGTH, this.height);
//    tif.SetField(TiffTag.ROWSPERSTRIP, this.height);
//    tif.SetField(TiffTag.BITSPERSAMPLE, 1);
//    tif.SetField(TiffTag.SAMPLESPERPIXEL, 1);
//    tif.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
//    tif.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISWHITE);
//    tif.SetField(TiffTag.XRESOLUTION, this.xRes);
//    tif.SetField(TiffTag.YRESOLUTION, this.yRes);
//    tif.SetField(TiffTag.MAKE, this.make);
//    tif.SetField(TiffTag.MODEL, this.model);
//    tif.SetField(TiffTag.SOFTWARE, "ProView");
//}

//public unsafe void Rotate90()
//{
//    this.LoadImage();
//    using (Tiff tif = Tiff.Open(file, "w"))
//    {
//        if (tif == null) {
//            MessageBox.Show("Could not open the file for writing. It is probably already opened in another application.\n\n" + file.ToString());
//            this.UnloadImage();
//            return ;
//        }
//        this.stride = (int)Math.Ceiling(this.bmd.Height / (double)8);
//        this.width = this.stride * 8;
//        this.height = this.bmd.Width;
//        this.WriteTiffTags(tif);
//        byte[] buffer = new byte[this.stride];
//        byte* pBmd = (byte*)this.bmd.Scan0.ToPointer();
//        byte* pSrc;
//        byte pixel;
//        int dstRow = 0;
//        for (int col = this.bmd.Stride - 1; col >= 0; col--) {
//            for (int bit = 0; bit < 8; bit++) {
//                pSrc = pBmd + col + this.bmd.Stride * (this.bmd.Height - 1);
//                for (int row = this.bmd.Height - 1; row >= 0; row--) {
//                    buffer[row / 8] >>= 0x1;
//                    pixel = (byte)((*pSrc & 0x1) << 0x7);
//                    buffer[row / 8] |= pixel;
//                    *pSrc >>= 0x1;
//                    pSrc -= this.bmd.Stride;
//                }
//                tif.WriteScanline(buffer, dstRow++);
//            }
//        }
//        tif.Close();
//    }
//    this.UnloadImage();
//    return ;
//}

//public void Rotate270()
//{

//}

//public void Rotate180()
//{

//}