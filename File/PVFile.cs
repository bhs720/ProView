using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ProView
{
    public abstract class PVFile : IDisposable
    {
        protected string fileName;
        protected int pageCount;
        protected Bitmap bmp;

        protected PVFile(string fileName)
        {
            this.fileName = fileName;
        }

        //public static PVFile Load(string fileName)
        //{
        //    switch (Path.GetExtension(fileName).ToLower())
        //    {
        //        case ".tif":
        //            return new TIF(fileName);
        //        case ".pdf":
        //            return new PDF(fileName);
        //        default:
        //            throw new Exception("Can't open this kind of file.");
        //    }
        //}

        public virtual void Dispose()
        {
            try
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
            }
            catch
            {
                /* Do nothing */
            }
            finally
            {
                bmp = null;
            }
        }

        public abstract Bitmap LoadPage(int pageNumber);

        public int PageCount { get { return pageCount; } }

        public string FileName { get { return fileName; } }

        public void Rename(string newFileNameNoExt)
        {
            if (!File.Exists(fileName))
                throw new Exception("File does not exist.");

            string oldFileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            string path = Path.GetDirectoryName(fileName);

            foreach (char c in Path.GetInvalidFileNameChars())
                newFileNameNoExt = newFileNameNoExt.Replace(c, '_');

            string newFullName = Path.Combine(path, newFileNameNoExt + ext);

            const UInt32 fileCountMax = UInt32.MaxValue;
            UInt32 fileCount = 1;
            int retries = 0;
            while (true)
            {
                try
                {
                    File.Move(fileName, newFullName);
                }
                catch (IOException)
                {
                    if (File.Exists(newFullName))
                    {
                        // The destination file exists. Auto-number the file
                        if (fileCount != fileCountMax)
                            fileCount++;
                        else
                            throw new Exception("Can't rename more than " + fileCountMax + " files with the same name.");
                        newFullName = Path.Combine(path, newFileNameNoExt + " (" + fileCount + ")" + ext);
                        continue;
                    }

                    // Files on a network share can cause this exception even when the file does not exist. Don't know why. Simply trying to rename the file again works.
                    System.Threading.Thread.Sleep(250);
                    if (retries++ < 3)
                        continue;
                    throw;
                }
                // Other exceptions bubble up to caller
                // Success
                fileName = newFullName;
                break;
            }
        }
    }

    //public class TIF : PVFile
    //{
    //    public TIF(string fileName) : base(fileName)
    //    {
    //        using (Tiff tiff = Tiff.Open(fileName, "r"))
    //        {
    //            if (tiff != null)
    //                pageCount = tiff.NumberOfDirectories();
    //            else
    //                throw new Exception("Could not open TIF file.");
    //        }
    //    }

    //    public unsafe override Bitmap LoadPage(int pageNumber)
    //    {
    //        if (!File.Exists(fileName))
    //            throw new Exception("File does not exist.");

    //        if (bmp != null)
    //            bmp.Dispose();

    //        using (var tiff = Tiff.Open(fileName, "r"))
    //        {
    //            tiff.SetDirectory(Convert.ToInt16(--pageNumber));
    //            FieldValue[] field;
    //            field = tiff.GetField(TiffTag.IMAGEWIDTH);
    //            int pxWidth = field[0].ToInt();
    //            field = tiff.GetField(TiffTag.IMAGELENGTH);
    //            int pxHeight = field[0].ToInt();
    //            field = tiff.GetField(TiffTag.BITSPERSAMPLE);
    //            int bpp = field[0].ToInt();
    //            field = tiff.GetField(TiffTag.SAMPLESPERPIXEL);
    //            int spp = field[0].ToInt() == 0 ? 1 : field[0].ToInt();
    //            int stride = tiff.ScanlineSize();

    //            if (bpp == 1 && spp == 1)
    //            {
    //                try
    //                {
    //                    bmp = new Bitmap(pxWidth, pxHeight, PixelFormat.Format1bppIndexed);
    //                }
    //                catch (Exception)
    //                {
    //                    throw new Exception("Not enough memory.");
    //                }
    //                field = tiff.GetField(TiffTag.PHOTOMETRIC);
    //                Photometric photo = (Photometric)field[0].ToInt();

    //                BitmapData bdDst = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
    //                byte* pDst = (byte*)bdDst.Scan0.ToPointer();

    //                for (int y = 0; y < pxHeight; y++)
    //                {
    //                    byte[] buffer = new byte[stride];
    //                    tiff.ReadScanline(buffer, y);
    //                    for (int x = 0; x < stride; x++)
    //                    {
    //                        pDst[y * bdDst.Stride + x] = buffer[x];
    //                    }
    //                }

    //                if (photo == Photometric.MINISBLACK)
    //                {
    //                    for (int y = 0; y < pxHeight; y++)
    //                    {
    //                        for (int x = 0; x < bdDst.Stride; x++)
    //                        {
    //                            pDst[y * bdDst.Stride + x] ^= 0xFF;
    //                        }
    //                    }
    //                }
    //                pDst = null;
    //                bmp.UnlockBits(bdDst);
    //            }
    //            else if (bpp == 8 && spp == 1)
    //            {
    //                try
    //                {
    //                    bmp = new Bitmap(pxWidth, pxHeight, PixelFormat.Format8bppIndexed);
    //                }
    //                catch (Exception)
    //                {
    //                    throw new Exception("Not enough memory.");
    //                }
    //                BitmapData bdDst = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
    //                byte* pDst = (byte*)bdDst.Scan0.ToPointer();

    //                for (int y = 0; y < pxHeight; y++)
    //                {
    //                    byte[] buffer = new byte[stride];
    //                    tiff.ReadScanline(buffer, y);
    //                    for (int x = 0; x < stride; x++)
    //                    {
    //                        pDst[y * bdDst.Stride + x] = buffer[x];
    //                    }
    //                }
    //                pDst = null;
    //                bmp.UnlockBits(bdDst);
    //            }
    //            else if (bpp == 8 && spp == 3)
    //            {
    //                try
    //                {
    //                    bmp = new Bitmap(pxWidth, pxHeight, PixelFormat.Format24bppRgb);
    //                }
    //                catch (Exception)
    //                {
    //                    throw new Exception("Not enough memory.");
    //                }
    //                BitmapData bdDst = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
    //                byte* pDst = (byte*)bdDst.Scan0.ToPointer();

    //                for (int y = 0; y < pxHeight; y++)
    //                {
    //                    byte[] buffer = new byte[stride];
    //                    tiff.ReadScanline(buffer, y);

    //                    for (int x = 0; x < stride; x += 3)
    //                    {
    //                        pDst[y * bdDst.Stride + x] = buffer[x + 2];
    //                        pDst[y * bdDst.Stride + x + 1] = buffer[x + 1];
    //                        pDst[y * bdDst.Stride + x + 2] = buffer[x];
    //                    }
    //                }
    //                pDst = null;
    //                bmp.UnlockBits(bdDst);
    //            }
    //            else
    //            {
    //                throw new Exception("File format not supported.");
    //            }
    //        }
    //        return bmp;
    //    }
    //}

    //public class PDF : PVFile
    //{
    //    public static int resolution = 96;

    //    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    //    public PDF(string fileName) : base(fileName)
    //    {
    //        int retry = 0;

    //        while (true)
    //        {
    //            try
    //            {
    //                using (var pdf = new MuPDF(fileName, ""))
    //                {
    //                    if (pdf == null)
    //                    {
    //                        throw new Exception("Failed to open PDF (internal error).");
    //                    }

    //                    pageCount = pdf.PageCount;
                        
    //                    if (pageCount < 1)
    //                    {
    //                        throw new Exception("Failed to open PDF.");
    //                    }

    //                    break;
    //                }
    //            }
    //            catch
    //            {
    //                if (retry < 3)
    //                {
    //                    retry++;
    //                    System.Threading.Thread.Sleep(250);
    //                    continue;
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //        }
    //    }

    //    public override Bitmap LoadPage(int pageNumber)
    //    {
    //        if (!File.Exists(fileName))
    //            throw new Exception("File does not exist.");

    //        if (bmp != null)
    //            bmp.Dispose();

    //        int retry = 0;

    //        while (true)
    //        {
    //            int zoomWidth = 0;
    //            int zoomHeight = 0;
    //            double pdfWidth = 0;
    //            double pdfHeight = 0;

    //            try
    //            {
    //                using (var pdf = new MuPDF(fileName, ""))
    //                {
    //                    if (pdf == null)
    //                    {
    //                        throw new Exception("Failed to open PDF (internal error).");
    //                    }

    //                    pdf.Page = pageNumber;
    //                    pdf.AntiAlias = true;

    //                    pdfWidth = pdf.Width;
    //                    pdfHeight = pdf.Height;

    //                    zoomWidth = checked((int)(pdfWidth * resolution / 72.0F));
    //                    zoomHeight = checked((int)(pdfHeight * resolution / 72.0F));

    //                    try
    //                    {
    //                        bmp = pdf.GetBitmap(zoomWidth, zoomHeight, 72, 72, 0, RenderType.RGB, false, false, 0);
    //                        return bmp;
    //                    }
    //                    catch (AccessViolationException ex)
    //                    {
    //                        throw new Exception("Not enough memory.", ex);
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                if (retry < 3)
    //                {
    //                    retry++;
    //                    System.Threading.Thread.Sleep(250);
    //                    continue;
    //                }
    //                else
    //                {
    //                    Logger.Error(ex, "pdfWidth={@pdfWidth} pdfHeight={@pdfHeight} resolution={@resolution} zoomWidth={@width} zoomHeight={@height}", pdfWidth, pdfHeight, resolution, zoomWidth, zoomHeight);
    //                    throw;
    //                }
    //            }
    //        }
    //    }
    //}
}