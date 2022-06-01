using System;
using System.Runtime.InteropServices;
using ASEva.Utility;
using ASEva.UIEto;
using Eto.Drawing;
using Eto.Forms;

namespace ASEva.UIGtk
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerGtk();
        }
    }

    class AppHandlerGtk : AppHandler
    {
        public Application CreateApp(out String uiBackend, out String webViewBackend)
        {
            if (ASEva.APIInfo.GetRunningOS() == "linuxarm")
            {
                NativeMethods.DetectNM3();
                webViewBackend = NativeMethods.IsUsingNM3() ? "webkit2legacy" : "webkit2";
                Redirection.RedirectMarshaller();
                Redirection.RedirectMenu();
            }
            else webViewBackend = "webkit2";

            var platform = new global::Eto.GtkSharp.Platform();
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            platform.Add<NumericStepper.IHandler>(() => new NumericStepperHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<WebView.IHandler>(() => new WebViewHandler());
            platform.Add<PixelLayout.IHandler>(() => new PixelLayoutHandler());
            platform.Add<Drawable.IHandler>(() => new DrawableHandler());
            platform.Add<Screen.IScreensHandler>(() => new ScreensHandler());
            platform.Add<Dialog.IHandler>(() => new DialogHandler());
            platform.Add<DateTimePicker.IHandler>(() => new DateTimePickerHandler());
            platform.Add<Form.IHandler>(() => new FormHandler());
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerGtk();

            try
            {
                var cssProvider = new Gtk.CssProvider();
                cssProvider.LoadFromData(ResourceLoader.LoadText("default.css"));
                foreach (var screen in Screen.Screens)
                {
                    var gdkMonitor = screen.ControlObject as Gdk.Monitor;
                    Gtk.StyleContext.AddProviderForScreen(gdkMonitor.Display.DefaultScreen, cssProvider, Gtk.StyleProviderPriority.User);
                }
            }
            catch (Exception) {}

            uiBackend = queryUIBackend();
            if (uiBackend == "wayland")
            {
                OverlayLayout.DelayHandleControl = true;
            }

            ContextMenuExtensions.ShouldAddMouseDownEvent = true;
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.ColorInverted;
            TextTableView.UpdateColorMode = TextTableView.InvalidateMode.EditCell;
            TextTableView.DefaultTextColor = Colors.Black;
            TextTableView.DefaultBackgroundColor = Colors.WhiteSmoke;
            CheckableListBox.UpdateColorMode = CheckableListBox.InvalidateMode.EditCell;

            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default(9.5f);
        }

        public void RunApp(Application application, Form window)
        {
            application.Run(window);
        }

        private String queryUIBackend()
        {
            try
            {
                var monitor = Gdk.Display.Default.PrimaryMonitor;
                if (monitor == null) monitor = Gdk.Display.Default.GetMonitor(0);
                var x11MonitorType = new GLib.GType(gdk_x11_monitor_get_type());
                if (x11MonitorType.IsInstance(monitor.Handle)) return "x11";
                var waylandMonitorType = new GLib.GType(gdk_wayland_monitor_get_type());
                if (waylandMonitorType.IsInstance(monitor.Handle)) return "wayland";
                var mirMonitorType = new GLib.GType(gdk_mir_monitor_get_type());
                if (mirMonitorType.IsInstance(monitor.Handle)) return "mir";
            }
            catch (Exception) {}
            return "unknown";
        }

		[DllImport("libgdk-3.so.0", SetLastError = true)]
		private static extern IntPtr gdk_x11_monitor_get_type();

		[DllImport("libgdk-3.so.0", SetLastError = true)]
		private static extern IntPtr gdk_wayland_monitor_get_type();

		[DllImport("libgdk-3.so.0", SetLastError = true)]
		private static extern IntPtr gdk_mir_monitor_get_type();
    }
}