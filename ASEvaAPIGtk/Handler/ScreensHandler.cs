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
				var display = Gdk.Display.Default;
				for (int i = 0; i < display.NMonitors; i++)
				{
					var monitor = display.GetMonitor(i);
					yield return new Screen(new ScreenHandler(monitor));
				}
			}
		}

		public Screen PrimaryScreen
		{
			get
			{
				return new Screen(new ScreenHandler(Gdk.Display.Default.PrimaryMonitor));
			}
		}
	}
}

