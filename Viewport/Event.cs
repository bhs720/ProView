using System;

namespace ProView
{
	public class PageChangedEventArgs : EventArgs
	{
		public PageChangedEventArgs(System.Drawing.Size size, int pageNumber, System.Drawing.Imaging.PixelFormat pixelFormat)
		{
			Size = size;
			PageNumber = pageNumber;
			PixelFormat = pixelFormat;
		}
		
		public PageChangedEventArgs()
		{
			NullPage = true;
		}
		
		readonly public bool NullPage;
		readonly public System.Drawing.Size Size;
		readonly public int PageNumber;
		readonly public System.Drawing.Imaging.PixelFormat PixelFormat;
	}
	
	public class FileChangedEventArgs : EventArgs
	{
		public FileChangedEventArgs(string fileName, int pageCount)
		{
			FileName = fileName;
			PageCount = pageCount;
		}
		
		/// <summary>
		/// Parameterless constructor is called when listeners should that there is no page
		/// </summary>
		public FileChangedEventArgs()
		{
			NullFile = true;
		}
		
		readonly public bool NullFile;
		readonly public string FileName;
		readonly public int PageCount;
	}
	
	public class ScaleChangedEventArgs : EventArgs
	{
		public ScaleChangedEventArgs(double scale)
		{
			Scale = scale;
		}
		
		readonly public double Scale;
	}
	
	public class ViewportResizedEventArgs : EventArgs
	{
		public ViewportResizedEventArgs(System.Drawing.Size size)
		{
			Size = size;
		}
		
		readonly public System.Drawing.Size Size;
	}
	
	public class ViewportMovedEventArgs : EventArgs
	{
		public ViewportMovedEventArgs(System.Drawing.Rectangle srcRect)
		{
			SrcRect = srcRect;
		}
		
		readonly public System.Drawing.Rectangle SrcRect;
	}
}
