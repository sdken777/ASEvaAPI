using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
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

    #pragma warning disable 612
    class AppHandlerGtk : AppHandler
    {
        public Application CreateApp(out String uiBackend, out String webViewBackend)
        {
            if (ASEva.APIInfo.GetRunningOS() == "linuxarm")
            {
                // CHECK: 修正在Arm下打开文件对话框异常
                Redirection.RedirectMarshaller();

                // 修正打开右键菜单异常，Arm-Ubuntu16.04-X11可重现 (不再支持Ubuntu16.04)
                // Redirection.RedirectMenu();
            }
  
            var platform = new global::Eto.GtkSharp.Platform();
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            // platform.Add<NumericStepper.IHandler>(() => new NumericStepperHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<WebView.IHandler>(() => new WebViewHandler());
            platform.Add<PixelLayout.IHandler>(() => new PixelLayoutHandler());
            // platform.Add<Drawable.IHandler>(() => new DrawableHandler());
            platform.Add<Screen.IScreensHandler>(() => new ScreensHandler());
            platform.Add<Dialog.IHandler>(() => new DialogHandler());
            platform.Add<DateTimePicker.IHandler>(() => new DateTimePickerHandler());
            platform.Add<Form.IHandler>(() => new FormHandler());
            platform.Add<Slider.IHandler>(() => new SliderHandler());
            platform.Add<GridView.IHandler>(() => new GridViewHandler());
            platform.Add<GroupBox.IHandler>(() => new GroupBoxHandler());
            platform.Add<SaveFileDialog.IHandler>(() => new SaveFileDialogHandler());
            platform.Add<Label.IHandler>(() => new LabelHandler());
            platform.Add<Button.IHandler>(() => new ButtonHandler());
            var app = new Application(platform);

            // CHECK: 应用全局样式令显示较为紧凑
            try
            {
                var cssProvider = new Gtk.CssProvider();
                cssProvider.LoadFromData(ResourceLoader.LoadText("default.css"));
                foreach (var screen in Screen.Screens)
                {
                    var gdkMonitor = screen.ControlObject as Gdk.Monitor;
                    Gtk.StyleContext.AddProviderForScreen(gdkMonitor.Display.DefaultScreen, cssProvider, Gtk.StyleProviderPriority.Settings);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            uiBackend = queryUIBackend();
            if (uiBackend == "wayland") LinuxFuncLoader.UseEGL = true;
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerGtk();
            ContextMenuExtensions.ShouldAddMouseDownEvent = true;
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.ColorInverted;
            TextTableView.UpdateColorMode = TextTableView.InvalidateMode.EditCell;
            TextTableView.DefaultTextColor = Colors.Black;
            TextTableView.DefaultBackgroundColor = Colors.WhiteSmoke;
            CheckableListBox.UpdateColorMode = CheckableListBox.InvalidateMode.EditCell;
            GLView.Factory = new GLViewFactoryGtk(uiBackend);
            SkiaView.Factory = new GLViewFactoryGtk(uiBackend);
            SkiaCanvasExtensions.DefaultFontName = "Noto Sans CJK SC";
            SkiaCanvasExtensions.DefaultFontSize = 12.5f;
            TopMostExtensions.QueryInterface = new TopMostHandler();
            DefaultFlowLayout2DBackend.FixedScrollBarSize = 15;
            SimpleTreeView.Factory = new SimpleTreeViewFactoryGtk();
            SnapshotExtensions.Handler = new SnapshotHandler();
            if (uiBackend == "wayland") SnapshotExtensions.ScreenModeHandler = new SnapshotHandler();
            else SnapshotExtensions.ScreenModeHandler = new ScreenSnapshotHandler();
            FlowLayout.Factory = new FlowLayoutFactoryGtk();
            FlowLayout2D.Factory = new FlowLayout2DFactoryGtk();
            OverlayLayout.DelayHandleControl = true;

            webViewBackend = "webkit2";

            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default(9.5f);
        }

        public void RunApp(Application application, Form window)
        {
            if (window.ControlObject is Gtk.Window) DialogHelper.MainWindow = window.ControlObject as Gtk.Window;
            application.Run(window);
        }

        public Control ConvertControlToEto(object platformControl)
        {
            if (platformControl is Gtk.Widget) return (platformControl as Gtk.Widget).ToEto();
            else return null;
        }

        public object ConvertControlToPlatform(Control etoControl)
        {
            return etoControl.ToNative(true);
        }

        public bool RunDialog(DialogPanel panel)
        {
            if (panel.Mode == DialogPanel.DialogMode.Invalid) return false;

            var widget = panel.ToNative(true);
            if (widget == null) return false;

            var uiBackend = App.GetUIBackend();
            if (uiBackend != null && uiBackend == "x11")
            {
                var appDialog = new AppDialogX11(widget, panel);
                var ev = new AutoResetEvent(false);
                appDialog.DeleteEvent += delegate { ev.Set(); };
                appDialog.ShowAll();
                while (!ev.WaitOne(0)) Gtk.Application.RunIteration();
                appDialog.Dispose();
                return true;
            }
            else
            {
                var appDialog = new AppDialogWayland(widget, panel);
                appDialog.TransientFor = DialogHelper.TopWindow;
                appDialog.Run();
                appDialog.Dispose();
                return true;
            }
        }

        public Dictionary<String, String> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["GtkSharp"] = ResourceLoader.LoadText("GtkSharp.LICENSE");
            table["webkitgtk"] = ResourceLoader.LoadText("webkitgtk.LICENSE");
            return table;
        }

        public bool ShouldPassParent()
        {
            return false;
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
            }
            catch (Exception) {}
            return "unknown";
        }

		[DllImport("libgdk-3.so.0", SetLastError = true)]
		private static extern IntPtr gdk_x11_monitor_get_type();

		[DllImport("libgdk-3.so.0", SetLastError = true)]
		private static extern IntPtr gdk_wayland_monitor_get_type();
    }
}