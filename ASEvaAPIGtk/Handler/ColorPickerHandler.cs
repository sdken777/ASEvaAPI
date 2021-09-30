using System;
using ASEva.UIGtk;
using Eto.Forms;
using Eto.Drawing;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;

namespace ASEva.UIEto
{
	class ColorPickerHandler : GtkControl<Gtk.Button, ColorPicker, ColorPicker.ICallback>, ColorPicker.IHandler
	{
		public ColorPickerHandler()
		{
			Control = new Gtk.Button();
			Control.Child = new Gtk.Label { WidthRequest = 30 };
			Control.Clicked += button_Clicked;
		}

		public event EventHandler ColorSet;

        private void button_Clicked(object sender, EventArgs e)
        {
            var dialog = new Gtk.ColorChooserDialog("", DialogHelper.TopWindow);
			dialog.UseAlpha = AllowAlpha;
			dialog.Rgba = (Control.Child as Gtk.Label).GetBackground().ToRGBA();
			int res = dialog.Run();
			var color = dialog.Rgba.ToEto();
			dialog.Dispose();
			if (res == (int)Gtk.ResponseType.Ok)
			{
				(Control.Child as Gtk.Label).SetBackground(color);
				if (ColorSet != null) ColorSet(this, null);
			}
        }

        protected new ColorPickerConnector Connector { get { return (ColorPickerConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new ColorPickerConnector();
		}

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case ColorPicker.ColorChangedEvent:
					this.ColorSet += Connector.HandleSelectedColorChanged;
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		protected class ColorPickerConnector : GtkControlConnector
		{
			new ColorPickerHandler Handler { get { return (ColorPickerHandler)base.Handler; } }

			public void HandleSelectedColorChanged(object sender, EventArgs e)
			{
				Handler.Callback.OnColorChanged(Handler.Widget, EventArgs.Empty);
			}
		}

		public Eto.Drawing.Color Color
		{
			get { return (Control.Child as Gtk.Label).GetBackground(); }
			set { (Control.Child as Gtk.Label).SetBackground(value); }
		}

		public bool AllowAlpha { get; set; }

		public bool SupportsAllowAlpha => true;
	}
}

