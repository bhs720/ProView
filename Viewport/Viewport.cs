using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProView
{
	public class ViewPort
	{
		Rectangle scaledRect;
		
		Size scaledSize;
		Size available;
		Size fullSize;

		double scale;
		
		bool zoomToFit = true;

		public delegate void ScaleChangedEventHandler(object sender, ScaleChangedEventArgs e);
		public event ScaleChangedEventHandler ScaleChanged;

		public ViewPort() {}

		public HScrollBar hScroll { get; set; }

		public VScrollBar vScroll { get; set; }

		public void OnPanelResize(object sender, EventArgs e)
		{
			Control control = sender as Control;
			available = new Size(control.ClientSize.Width - vScroll.Width, control.ClientSize.Height - hScroll.Height);
			if (zoomToFit)
			{
				ZoomToFit();
			}
			else
			{
				UpdateBounds();
				MoveDelta(0, 0);
			}
		}

		public void OnPageChanged(object sender, PageChangedEventArgs e)
		{
			if (e.NullPage)
			{
				fullSize = new Size(0, 0);
				zoomToFit = true;
			}
			else
			{
				fullSize = e.Size;
			}
			
			UpdateBounds();
			MoveDelta(0, 0);
		}

		public double Scale
		{
			get
			{
				return scale;
			}
			private set
			{
				scale = value;
				scale = Math.Min(4.0D, scale);
				scale = Math.Max(0.01D, scale);
				ScaleChanged.Invoke(this, new ScaleChangedEventArgs(scale));
			}
		}
		
		public Rectangle ScaledRect
		{
			get
			{
				return scaledRect;
			}
		}

		public void ZoomToFit()
		{
			zoomToFit = true;
			
			UpdateBounds();
			MoveAbs(0, 0);
		}

		public void Zoom(Rectangle zoomRect)
		{
			zoomRect.X += scaledRect.X;
			zoomRect.Y += scaledRect.Y;
			// Convert zoomRect to full size coordinates
			zoomRect.X = (int)Math.Round(zoomRect.X / scale);
			zoomRect.Y = (int)Math.Round(zoomRect.Y / scale);
			zoomRect.Width = Math.Min(fullSize.Width, (int)Math.Round(zoomRect.Width / scale));
			zoomRect.Height = Math.Min(fullSize.Height, (int)Math.Round(zoomRect.Height / scale));

			zoomToFit = false;
			
			Scale = Math.Min(available.Width / (double)zoomRect.Width, available.Height / (double)zoomRect.Height);
			UpdateBounds();

			// Convert zoomRect to new scale coordinates
			zoomRect.X = (int)Math.Round(zoomRect.X * scale);
			zoomRect.Y = (int)Math.Round(zoomRect.Y * scale);
			zoomRect.Width = Math.Min(scaledSize.Width, (int)Math.Round(zoomRect.Width * scale));
			zoomRect.Height = Math.Min(scaledSize.Height, (int)Math.Round(zoomRect.Height * scale));

			Point zoomRectCenter = new Point();
			zoomRectCenter.X = zoomRect.X + zoomRect.Width / 2;
			zoomRectCenter.Y = zoomRect.Y + zoomRect.Height / 2;

			MoveCenter(zoomRectCenter.X, zoomRectCenter.Y);
		}
		
		/// <summary>
		/// Zooms the viewport and holds zoomPt stationary
		/// </summary>
		/// <param name="zoomPt">The point to hold stationary after zooming</param>
		/// <param name="zoomDeltaPercent">A number expressed as a percentage by which to change the zoom level. A positive number zooms in - negative zooms out.</param>
		public void ZoomDelta(Point zoomPt, double zoomDeltaPercent)
		{
			// Convert zoomPt to full size coords
			int x = (int)((scaledRect.X + zoomPt.X) / scale);
			int y = (int)((scaledRect.Y + zoomPt.Y) / scale);
			
			zoomToFit = false;
			Scale = scale + zoomDeltaPercent / 100.0D;
			UpdateBounds();
			
			MoveAbs((int)(x * scale) - zoomPt.X, (int)(y * scale) - zoomPt.Y);
		}

		/// <summary>
		/// Zooms the viewport while holding its center point stationary.
		/// </summary>
		/// <param name="newScalePercent">The new scale expressed as an absolute percentage.</param>
		public void ZoomAbs(double newScalePercent)
		{
			// Convert center point of viewer to full size coords
			double centerX = (scaledRect.X + scaledRect.Width / 2.0D) / scale;
			double centerY = (scaledRect.Y + scaledRect.Height / 2.0D) / scale;

			zoomToFit = false;

			Scale = newScalePercent / 100.0D;
			UpdateBounds();
			MoveCenter((int)Math.Round(centerX * scale), (int)Math.Round(centerY * scale));
		}

		/// <summary>
		/// Move the center of the viewport to this location.
		/// </summary>
		/// <param name="centerX"></param>
		/// <param name="centerY"></param>
		public void MoveCenter(int centerX, int centerY)
		{
			MoveAbs(centerX - scaledRect.Width / 2, centerY - scaledRect.Height / 2);
		}

		/// <summary>
		/// The distance to move the viewport from its current location.
		/// </summary>
		/// <param name="dX">Positive (moves to the right) or negative (moves left) value.</param>
		/// <param name="dY">Positive (moves to the right) or negative (moves left) value.</param>
		public void MoveDelta(int dX, int dY)
		{
			MoveAbs(dX + scaledRect.X, dY + scaledRect.Y);
		}
		
		public void MoveAbs(int x, int y)
		{
			if (x + scaledRect.Width > scaledSize.Width)
				x = scaledSize.Width - scaledRect.Width;
			if (y + scaledRect.Height > scaledSize.Height)
				y = scaledSize.Height - scaledRect.Height;
			scaledRect.X = Math.Max(0, x);
			scaledRect.Y = Math.Max(0, y);
			hScroll.Value = scaledRect.X;
			vScroll.Value = scaledRect.Y;
		}
		
		public void UpdateBounds()
		{
			Rectangle oldRect = scaledRect;
			
			if (zoomToFit)
				Scale = Math.Min(available.Width / (double)fullSize.Width, available.Height / (double)fullSize.Height);
			
			scaledSize.Width = (int)Math.Round(fullSize.Width * scale);
			scaledSize.Height = (int)Math.Round(fullSize.Height * scale);

			if (available.Width >= scaledSize.Width)
			{
				scaledRect.Width = scaledSize.Width;
				hScroll.Enabled = false;
			}
			else
			{
				scaledRect.Width = available.Width;
				hScroll.Enabled = true;
				hScroll.LargeChange = (int)Math.Round(scaledRect.Width / (double)10);
				hScroll.SmallChange = (int)Math.Round(scaledRect.Width / (double)20);
				hScroll.Maximum = scaledSize.Width - scaledRect.Width + hScroll.LargeChange;
			}

			if (available.Height >= scaledSize.Height)
			{
				scaledRect.Height = scaledSize.Height;
				vScroll.Enabled = false;
			}
			else
			{
				scaledRect.Height = available.Height;
				vScroll.Enabled = true;
				vScroll.LargeChange = (int)Math.Round(scaledRect.Height / (double)10);
				vScroll.SmallChange = (int)Math.Round(scaledRect.Height / (double)20);
				vScroll.Maximum = scaledSize.Height - scaledRect.Height + vScroll.LargeChange;
			}

			if (oldRect.Size != scaledRect.Size)
			{
				System.Diagnostics.Debug.WriteLine("available {0}", available);
				System.Diagnostics.Debug.WriteLine("scaledRect {0}", scaledRect);
			}
		}
	}
}
