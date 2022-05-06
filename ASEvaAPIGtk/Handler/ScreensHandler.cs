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
				try
				{
					var monitor = Gdk.Display.Default.PrimaryMonitor;
					if (monitor == null) monitor = Gdk.Display.Default.GetMonitor(0);
					return new Screen(new ScreenHandler(monitor));
				}
				catch (Exception) { return null; }
			}
		}
	}
}

