using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	#pragma warning disable CS0612
	class ScreensHandler : Screen.IScreensHandler
	{
		public void Initialize ()
		{
		}

		public Eto.Platform Platform { get; set; }

		public IEnumerable<Screen> Screens
		{
			get
			{
#if GTKCORE
				var list = new List<Screen>();
				try
				{
					var display = Gdk.Display.Default;
					for (int i = 0; i < display.NMonitors; i++)
					{
						var monitor = display.GetMonitor(i);
						list.Add(new Screen(new ScreenHandler(monitor)));
					}
				}
				catch (Exception) {}
				return list;

#else
				var display = Gdk.Display.Default;
				for (int i = 0; i < display.NScreens; i++) {
					var screen = display.GetScreen (i);
					for (int monitor = 0; monitor < screen.NMonitors; monitor++) {
						yield return new Screen (new ScreenHandler (screen, monitor));
					}
				}
#endif
			}
		}

		public Screen PrimaryScreen
		{
			get
			{
#if GTKCORE
				try
				{
					var monitor = Gdk.Display.Default.PrimaryMonitor;
					if (monitor == null) monitor = Gdk.Display.Default.GetMonitor(0);
					return new Screen(new ScreenHandler(monitor));
				}
				catch (Exception) { return null; }
#else
				return new Screen(new ScreenHandler(Gdk.Display.Default.DefaultScreen, 0));
#endif
			}
		}

		public static bool LegacyMode { get; private set; } // Deprecated
	}
}

