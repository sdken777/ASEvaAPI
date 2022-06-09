
using Eto.Forms;
using Eto.Drawing;
using Eto;
using Eto.GtkSharp;
using Eto.GtkSharp.Drawing;
using Eto.GtkSharp.Forms;
using System;
using System.Runtime.InteropServices;

namespace ASEva.UIGtk
{
	#pragma warning disable 612
	public class LegacyScreenHandler : WidgetHandler<Gdk.Screen, Screen>, Screen.IHandler
	{
		readonly int monitor;

		public LegacyScreenHandler (Gdk.Screen screen, int monitor)
		{
			this.Control = screen;
			this.monitor = monitor;
		}

		public float RealScale
		{
			get { return (float)Control.Resolution / 72f; }
		}

		public float Scale
		{
			get { return (float)Control.Resolution / 72f; }
		}

		public RectangleF Bounds
		{
			get {
				return Control.GetMonitorGeometry (monitor).ToEto ();
			}
		}

		public RectangleF WorkingArea
		{
			get
			{
				return Control.GetMonitorGeometry (monitor).ToEto ();
			}
		}

		public int BitsPerPixel
		{
			get { return 24; }
		}

		public bool IsPrimary
		{
			get { return monitor == 0; }
		}

		public Image GetImage(RectangleF rect)
		{
			var rootWindowPtr = gdk_get_default_root_window();
			if (rootWindowPtr == IntPtr.Zero)
				return null;

			var bounds = Bounds;
			rect.Location += bounds.Location;
			var rectInt = Rectangle.Ceiling(rect);

			var pbptr = gdk_pixbuf_get_from_window(rootWindowPtr, rectInt.X, rectInt.Y, rectInt.Width, rectInt.Height);

			if (pbptr == IntPtr.Zero)
				return null;

			var pb = new Gdk.Pixbuf(pbptr);
			return new Bitmap(new BitmapHandler(pb));
		}

		[DllImport("libgdk-3.so.0", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr gdk_get_default_root_window();

		[DllImport("libgdk-3.so.0", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr gdk_pixbuf_get_from_window(IntPtr window, int x, int y, int width, int height);
	}
}