using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ghostscript.NET.Rasterizer;

namespace ProView
{
    class PVPdfFile : PVFile
    {
		#if DEBUG
		static byte[] gsDll = File.ReadAllBytes("gsdll32.dll");
		#else
		static byte[] gsDll = File.ReadAllBytes("gsdll64.dll");
		#endif

        GhostscriptRasterizer pdf;
				
		public PVPdfFile(string fullName) : base(fullName) {}

        public override bool Open()
        {
            if (isOpen)
                return true;

            pdf = new GhostscriptRasterizer();
            pdf.Open(fullName, gsDll);
            pageCount = (short)(pdf.PageCount);

            if (pageCount <= 0)
                return false;

            return true;
        }

        public override void Close()
        {
            pdf.Dispose();
            isOpen = false;
        }
		
		public override PVPage LoadPage(int pageNumber)
		{
            pdf.GetPage(96, 96, pageNumber);
		}
		
		public override void UnloadPage(short number)
		{
			throw new NotImplementedException();
		}
    }
}