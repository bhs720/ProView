using mupdf;
using System.Diagnostics;
using System.Text;

namespace ProView
{
    internal sealed class PVDocument : IDisposable
    {
        private FzDocument _fzDocument;
        private Dictionary<int, PVPage> _pvPages;

        public int PageCount { get; private set; }
        public string Filename { get; private set; }
        public bool IsOpen => _fzDocument is not null;
        public bool IsModified => _pvPages?.Any(p => p.Value?.Rotation != 0) ?? false;

        public PVDocument(string fileName)
        {
            Filename = fileName;
        }

        public void Open()
        {
            Dispose();

            try
            {
                _fzDocument = new FzDocument(Encoding.UTF8.GetBytes(Filename));
                int pageCount = _fzDocument.fz_count_pages();

                if (pageCount < 1)
                {
                    throw new Exception("Could not get document page count");
                }

                PageCount = pageCount;
                InitializePages();
            }
            catch (Exception ex)
            {
                Debug.Print($"Failed to open document '{Filename}': {ex.Message}");
                throw;
            }
        }

        private void InitializePages()
        {
            _pvPages = new Dictionary<int, PVPage>();

            for (int i = 1; i <= PageCount; i++)
            {
                _pvPages.Add(i, new PVPage(this, _fzDocument, i));
            }
        }

        private void DisposePages()
        {
            if (_pvPages is not null)
            {
                foreach (var kv in _pvPages)
                {
                    kv.Value?.Dispose();
                    _pvPages[kv.Key] = null;
                }

                _pvPages = null;
            }
        }

        public PVPage GetPage(int pageNumber)
        {
            if (!IsOpen)
            {
                Open();
            }

            if (pageNumber < 1 || pageNumber > PageCount)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"Page number must be between 1 and {PageCount}");
            }

            return _pvPages[pageNumber];
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            DisposePages();
            _fzDocument?.Dispose();
            _fzDocument = null;
        }
    }

    //public void Rename(string newFileNameNoExt)
    //{
    //    if (!File.Exists(_fileName))
    //        throw new Exception("File does not exist.");

    //    string oldFileNameNoExt = Path.GetFileNameWithoutExtension(_fileName);
    //    string ext = Path.GetExtension(_fileName);
    //    string path = Path.GetDirectoryName(_fileName);

    //    foreach (char c in Path.GetInvalidFileNameChars())
    //        newFileNameNoExt = newFileNameNoExt.Replace(c, '_');

    //    string newFullName = Path.Combine(path, newFileNameNoExt + ext);

    //    const UInt32 fileCountMax = UInt32.MaxValue;
    //    UInt32 fileCount = 1;
    //    int retries = 0;
    //    while (true)
    //    {
    //        try
    //        {
    //            File.Move(_fileName, newFullName);
    //        }
    //        catch (IOException)
    //        {
    //            if (File.Exists(newFullName))
    //            {
    //                // The destination file exists. Auto-number the file
    //                if (fileCount != fileCountMax)
    //                    fileCount++;
    //                else
    //                    throw new Exception("Can't rename more than " + fileCountMax + " files with the same name.");
    //                newFullName = Path.Combine(path, newFileNameNoExt + " (" + fileCount + ")" + ext);
    //                continue;
    //            }

    //            // Files on a network share can cause this exception even when the file does not exist. Don't know why. Simply trying to rename the file again works.
    //            System.Threading.Thread.Sleep(250);
    //            if (retries++ < 3)
    //                continue;
    //            throw;
    //        }
    //        // Other exceptions bubble up to caller
    //        // Success
    //        _fileName = newFullName;
    //        break;
    //    }
    //}

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