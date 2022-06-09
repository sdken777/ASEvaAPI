using System;
using Eto.Forms;
using Eto;
using Eto.GtkSharp.Forms;
using System.Collections.Generic;

namespace ASEva.UIGtk
{
	#pragma warning disable 612
	public class ScreensHandler : Screen.IScreensHandler
	{
		public void Initialize ()
		{
		}

		public Widget Widget { get; set; }

		public Eto.Platform Platform { get; set; }

		public IEnumerable<Screen> Screens
		{
			get
			{
				if (LegacyMode)
				{
					var display = Gdk.Display.Default;
					for (int i = 0; i < display.NScreens; i++) {
						var screen = display.GetScreen (i);
						for (int monitor = 0; monitor < screen.NMonitors; monitor++) {
							yield return new Screen (new LegacyScreenHandler(screen, monitor));
						}
					}
				}
				else
				{
					var display = Gdk.Display.Default;
					for (int i = 0; i < display.NMonitors; i++)
					{
						var monitor = display.GetMonitor(i);
						yield return new Screen(new ScreenHandler(monitor));
					}
				}
			}
		}

		public Screen PrimaryScreen
		{
			get
			{
				if (LegacyMode)
				{
					return new Screen(new LegacyScreenHandler(Gdk.Display.Default.DefaultScreen, 0));
				}
				else
				{
					return new Screen(new ScreenHandler(Gdk.Display.Default.PrimaryMonitor));
				}
			}
		}

		public static void TestLegacy()
		{
			try
			{
				var monitor = Gdk.Display.Default.PrimaryMonitor;
			}
			catch (Exception)
			{
				LegacyMode = true;
			}
		}

		public static bool LegacyMode { get; private set; }
	}
}

