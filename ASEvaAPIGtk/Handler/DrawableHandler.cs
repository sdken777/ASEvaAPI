using System;
using Eto.Drawing;
using Eto.Forms;
using Eto.GtkSharp;
using Eto.GtkSharp.Drawing;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	class DrawableHandler : GtkPanel<Gtk.EventBox, Drawable, Drawable.ICallback>, Drawable.IHandler
	{
		Gtk.VBox content;

		public bool SupportsCreateGraphics { get { return true; } }

		public void Create()
		{
			Control = new Gtk.EventBox();
			Control.Events |= Gdk.EventMask.ExposureMask;
			//Control.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(0, 0, 0));
			//Control.DoubleBuffered = false;
			Control.CanFocus = false;
			Control.CanDefault = true;
			Control.Events |= Gdk.EventMask.ButtonPressMask;

			content = new Gtk.VBox();

			Control.Add(content);
		}

		protected override void Initialize()
		{
			base.Initialize();
			Control.Drawn += Connector.HandleDrawn;
			Control.ButtonPressEvent += Connector.HandleDrawableButtonPressEvent;
		}

		public void Create(bool largeCanvas)
		{
			Create();
		}

		public bool CanFocus
		{
			get { return Control.CanFocus; }
			set { Control.CanFocus = value; }
		}

		protected new DrawableConnector Connector { get { return (DrawableConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new DrawableConnector();
		}

		protected class DrawableConnector : GtkPanelEventConnector
		{
			public new DrawableHandler Handler { get { return (DrawableHandler)base.Handler; } }

			public void HandleDrawableButtonPressEvent(object o, Gtk.ButtonPressEventArgs args)
			{
				if (Handler.CanFocus)
					Handler.Control.GrabFocus();
			}

			// [GLib.ConnectBefore]
			public void HandleDrawn(object o, Gtk.DrawnArgs args)
			{
				var h = Handler;

				var allocation = h.Control.Allocation.Size;
				args.Cr.Rectangle(new Cairo.Rectangle(0, 0, allocation.Width, allocation.Height));
				args.Cr.Clip();
				Gdk.Rectangle rect = new Gdk.Rectangle();
				if (!GraphicsHandler.GetClipRectangle(args.Cr, ref rect))
					rect = new Gdk.Rectangle(Gdk.Point.Zero, allocation);

				using (var graphics = new Graphics(new GraphicsHandler(args.Cr, h.Control.PangoContext, false)))
				{
					if (h.SelectedBackgroundColor != null)
						graphics.Clear(h.SelectedBackgroundColor.Value);
					
					h.Callback.OnPaint(h.Widget, new PaintEventArgs(graphics, rect.ToEto()));
				}
			}
		}

		public void Update(Rectangle rect)
		{
			using (var graphics = new Graphics(new GraphicsHandler(Control, Control.GetWindow())))
			{
				Callback.OnPaint(Widget, new PaintEventArgs(graphics, rect));
			}
		}

		public Graphics CreateGraphics()
		{
			return new Graphics(new GraphicsHandler(Control, Control.GetWindow()));
		}

		protected override void SetContainerContent(Gtk.Widget content)
		{
			this.content.Add(content);
		}

		protected override void SetBackgroundColor(Color? color)
		{
			// we handle this ourselves
			//base.SetBackgroundColor(color);
			Invalidate(false);
		}
	}
}
