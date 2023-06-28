using System;
using Eto.Forms;
using Eto.Drawing;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	class PixelLayoutHandler : GtkContainer<Gtk.ScrolledWindow, PixelLayout, PixelLayout.ICallback>, PixelLayout.IHandler
	{
		public PixelLayoutHandler()
		{
			Control = new Gtk.ScrolledWindow();
			Control.Add(new Gtk.Viewport());
			(Control.Child as Gtk.Viewport).Add(new EtoFixed { Handler = this });
			timer = GLib.Timeout.Add(100, timer_Timeout);
		}

		public override void OnUnLoad(EventArgs e)
		{
			GLib.Timeout.Remove(timer);
			base.OnUnLoad(e);
		}

#if GTK3
        class EtoVBox : Gtk.VBox
		{
			protected override void OnAdjustSizeRequest(Gtk.Orientation orientation, out int minimum_size, out int natural_size)
			{
				base.OnAdjustSizeRequest(orientation, out minimum_size, out natural_size);
				// Gtk.Fixed only uses minimum size, not natural size. ugh.
				minimum_size = natural_size;
			}
		}
#endif

		public void Add(Control child, int x, int y)
		{
			var ctl = child.GetGtkControlHandler();

#if GTK3
			var widget = ctl.ContainerControl;
			if (widget.Parent != null)
				((Gtk.Container)widget.Parent).Remove(widget);
			widget.ShowAll();
			widget = new EtoVBox { Child = widget };
#else
			var widget = ctl.ContainerControl;
			if (widget.Parent != null)
				((Gtk.Container)widget.Parent).Remove(widget);
			widget.ShowAll();
#endif
			((Control.Child as Gtk.Viewport).Child as Gtk.Fixed).Put(widget, x, y);
			ctl.CurrentLocation = new Point(x, y);
		}

		public void Move(Control child, int x, int y)
		{
			var ctl = child.GetGtkControlHandler();
			if (ctl.CurrentLocation.X != x || ctl.CurrentLocation.Y != y)
			{
#if GTK3
				var widget = ctl.ContainerControl.Parent;
#else
				var widget = ctl.ContainerControl;
#endif
				((Control.Child as Gtk.Viewport).Child as Gtk.Fixed).Move(widget, x, y);

				ctl.CurrentLocation = new Point(x, y);
			}
		}

		public void Remove(Control child)
		{
#if GTK3
			((Control.Child as Gtk.Viewport).Child as Gtk.Fixed).Remove(child.GetContainerWidget().Parent);
#else
			Control.Remove(child.GetContainerWidget());
#endif
		}

		public void Update()
		{
#if GTK3
			((Control.Child as Gtk.Viewport).Child as Gtk.Fixed).QueueResize();
#else
			Control.ResizeChildren();
#endif
		}

		public override void OnLoadComplete(System.EventArgs e)
		{
			base.OnLoadComplete(e);
			SetFocusChain();
		}

        private bool timer_Timeout()
        {
			if (Control.AllocatedWidth != lastWidth || Control.AllocatedHeight != lastHeight)
			{
				if (Control.Child != null) Update();
				lastWidth = Control.AllocatedWidth;
				lastHeight = Control.AllocatedHeight;
			}
            return true;
        }

		private uint timer;

		private int lastWidth = 0, lastHeight = 0;
	}
}
