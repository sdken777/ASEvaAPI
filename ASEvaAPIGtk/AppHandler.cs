using System;
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
        public Application CreateApp()
        {
            if (ASEva.APIInfo.GetRunningOS() == "linuxarm")
            {
                NativeMethods.DetectNM3();
                Redirection.RedirectMarshaller();
                Redirection.RedirectMenu();
            }

            var platform = new global::Eto.GtkSharp.Platform();
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            platform.Add<NumericStepper.IHandler>(() => new NumericStepperHandler());
            platform.Add<ColorPicker.IHandler>(() => new ColorPickerHandler());
            platform.Add<WebView.IHandler>(() => new WebViewHandler());
            platform.Add<PixelLayout.IHandler>(() => new PixelLayoutHandler());
            platform.Add<Drawable.IHandler>(() => new DrawableHandler());
            platform.Add<Screen.IScreensHandler>(() => new ScreensHandler());
            platform.Add<Dialog.IHandler>(() => new DialogHandler());
            var app = new Application(platform);

            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerGtk();

            var cssProvider = new Gtk.CssProvider();
            cssProvider.LoadFromData(ResourceLoader.LoadText("default.css"));
            foreach (var screen in Screen.Screens)
            {
                var gdkMonitor = screen.ControlObject as Gdk.Monitor;
                Gtk.StyleContext.AddProviderForScreen(gdkMonitor.Display.DefaultScreen, cssProvider, Gtk.StyleProviderPriority.User);
            }

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
    }
}