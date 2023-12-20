using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace ASEva.UIGtk
{
	// CHECK: 继承WindowHandlerGtkWindow，修正GtkWindowConnector异常问题，Eto-2.7.0已修复
	class FormHandler : WindowHandlerGtkWindow<Gtk.Window, Form, Form.ICallback>, Form.IHandler
	{
		public FormHandler(Gtk.Window window)
		{
			Control = window;
		}

		public FormHandler()
		{
			Control = new Gtk.Window(Gtk.WindowType.Toplevel);
<<<<<<< HEAD
=======
#if GTK2
			Control.AllowGrow = true;
#endif
			Resizable = true;
>>>>>>> official-handler
			Control.SetPosition(Gtk.WindowPosition.Center);

			var vbox = new EtoVBox { Handler = this };
			vbox.PackStart(WindowActionControl, false, true, 0);
			vbox.PackStart(WindowContentControl, true, true, 0);
			Control.Child = vbox;
		}

		public async void Show()
		{
			DisableAutoSizeUpdate++;
			Control.Child.ShowAll();
			if (ShowActivated || !Control.AcceptFocus)
				Control.Show();
			else
			{
				Control.AcceptFocus = false;
				Control.Show();
				await System.Threading.Tasks.Task.Delay(1); // why???  Only way I can get it to work properly on ubuntu 16.04
				Control.AcceptFocus = CanFocus; // in case user changes it right after this call, but should be true
			}
			DisableAutoSizeUpdate--;
		}

		static object ShowActivated_Key = new object();

		public bool ShowActivated
		{
			get { return Widget.Properties.Get<bool>(ShowActivated_Key, true); }
			set { Widget.Properties.Set(ShowActivated_Key, value, true); }
		}

		static object CanFocus_Key = new object();

		public bool CanFocus
		{
			get { return Widget.Properties.Get<bool>(CanFocus_Key, true); }
			set { Widget.Properties.Set(CanFocus_Key, value, () => Control.AcceptFocus = value, true); }
		}

		// CHECK: 修正设置Resizable属性无效问题
		public new bool Resizable
		{
			get
			{
				return Control.Resizable;
			}
			set
			{
				Control.Resizable = value;
			}
		}
	}
}
