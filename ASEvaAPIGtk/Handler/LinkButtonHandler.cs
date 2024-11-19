using System;
using Eto.Forms;
using Eto.Drawing;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	// CHECK: 改为跟其他界面框架接近的显示效果
	class LinkButtonHandler : GtkControl<LinkItem, LinkButton, LinkButton.ICallback>, LinkButton.IHandler
	{
		public override Gtk.Widget ContainerControl
		{
			get { return Control; }
		}

		public LinkButtonHandler()
		{
			Control = new LinkItem();
		}

		public Color TextColor
		{
			get
			{
				var c = Control.ForeColor;
				var k = 1.0f / 255;
				return new Color(k * c.R, k * c.G, k * c.B, k * c.A);
			}
			set
			{
				Control.ForeColor = new ColorRGBA((byte)value.Rb, (byte)value.Gb, (byte)value.Bb, (byte)value.Ab);
			}
		}

		public Color DisabledTextColor
		{
			get { return Colors.Gray; }
			set { }
		}

		public override string Text
		{
			get { return Control.Text; }
			set { Control.Text = value; }
		}

		public override Font? Font
		{
			get { return font; }
			set
			{
				font = value;
				var description = value?.ControlObject as Pango.FontDescription;
				if (description != null) Control.SetFont(description);
			}
		}
		private Font? font;

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case LinkButton.ClickEvent:
					Control.Clicked += Connector.HandleClicked;
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		protected new LinkButtonConnector Connector { get { return (LinkButtonConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new LinkButtonConnector();
		}

		protected class LinkButtonConnector : GtkControlConnector
		{
			public new LinkButtonHandler Handler { get { return (LinkButtonHandler)base.Handler; } }

			public void HandleClicked(object? sender, EventArgs e)
			{
				Handler?.Callback.OnClick(Handler.Widget, EventArgs.Empty);
			}
		}

	}
}