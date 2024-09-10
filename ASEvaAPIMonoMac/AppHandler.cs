using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Linq;
using ASEva.Utility;
using ASEva.UIEto;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using Eto.Mac;
using Eto.Mac.Drawing;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MonoMac.Foundation;

namespace ASEva.UIMonoMac
{
    public class AppHandlerCreator
    {
        public static AppHandler Create()
        {
            return new AppHandlerMonoMac();
        }
    }

    class AppDelegate : Eto.Mac.AppDelegate
    {
        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }

    class AppHandlerMonoMac : AppHandler
    {
        public Application CreateApp(bool attach, out String uiBackend, out String webViewBackend)
        {
            var platform = new global::Eto.Mac.Platform();
            platform.Add<WebView.IHandler>(() => new WKWebViewHandler());
            platform.Add<UITimer.IHandler>(() => new UITimerHandler());
            platform.Add<LinkButton.IHandler>(() => new LinkButtonHandler());
            platform.Add<Label.IHandler>(() => new LabelHandler());
            platform.Add<PasswordBox.IHandler>(() => new PasswordBoxHandler());

            var app = new Application(platform);

            // CHECK: 支持无bundle运行
            if (!InBundle)
            {
                var nsApp = app.ControlObject as NSApplication;
                nsApp.ActivationPolicy = NSApplicationActivationPolicy.Regular;

                String icnsName = null;
                var targetLib = Assembly.GetEntryAssembly();
                if (targetLib != null)
                {
                    var names = targetLib.GetManifestResourceNames().ToList();
                    if (names.Contains("icon.icns")) icnsName = "icon.icns";
                    else icnsName = names.Find(s => s.EndsWith(".icns"));
                }
                if (icnsName != null)
                {
                    var instream = targetLib.GetManifestResourceStream(icnsName);
                    if (instream != null && instream.Length > 0)
                    {
                        var data = new byte[instream.Length];
                        instream.Read(data, 0, data.Length);
                        instream.Close();

                        nsApp.ApplicationIconImage = new NSImage(NSData.FromArray(data));
                    }
                }
            }

            // CHECK: 点击主窗口关闭按钮后令应用程序退出
            var appHandler = app.Handler as Eto.Mac.Forms.ApplicationHandler;
            appHandler.AppDelegate = new AppDelegate();

            SetContentExtensions.WindowInitializer = new InitWindowHandlerMonoMac();
            SetClientSizeExtensions.ClientSizeSetter = new SetClientSizeHandlerMonoMac();
            ASEva.UIEto.ImageConverter.Mode = ASEva.UIEto.ImageConverter.ConvertMode.AlphaScaleColorInverted;
            DefaultTextTableViewBackend.UpdateColorMode = DefaultTextTableViewBackend.InvalidateMode.EditCell;
            DefaultTextTableViewBackend.DefaultTextColor = Colors.Black;
            DefaultTextTableViewBackend.DefaultBackgroundColor = Color.FromArgb(0, 0, 0, 16);
            DefaultCheckableListBoxBackend.UpdateColorMode = DefaultCheckableListBoxBackend.InvalidateMode.EditCell;
            GLView.Factory = new GLViewFactoryMonoMac();
            SkiaView.Factory = new GLViewFactoryMonoMac();
            SkiaCanvasExtensions.DefaultFontName = "STHeiti";
            SkiaCanvasExtensions.DefaultFontSize = 13.0f;
            DefaultSimpleTreeViewBackend.DefaultBackgroundColor = Colors.Transparent;
            SnapshotExtensions.Handler = new SnapshotHandler();
            SnapshotExtensions.ScreenModeHandler = new ScreenSnapshotHandler();
            CheckableListBox.Factory = new CheckableListBoxFactoryMonoMac();
            TextTableView.Factory = new TextTableViewFactoryMonoMac();
            SimpleTreeView.Factory = new SimpleTreeViewFactoryMonoMac();
            TopMostExtensions.QueryInterface = new TopMostHandlerMonoMac();
            OxyPlotView.Factory = new OxyPlotViewFactoryMonoMac();

            FuncManager.Register("GetUIBackendAPIVersion", delegate { return APIInfo.GetAPIVersion(); });

            uiBackend = null;
            webViewBackend = "webkit2";
            return app;
        }

        public Font CreateDefaultFont()
        {
            return SystemFonts.Default();
        }

        public void RunApp(Application application, Form window, Form[] subWindows)
        {
            window.Closing += (o, e) => { if (e.Cancel) window.Visible = true; };
            
            // CHECK: 支持无bundle运行
            if (InBundle)
            {
                application.Run(window);
            }
            else
            {
                CrashReporter.Attach();
                NSSetUncaughtExceptionHandler(UncaughtExceptionHandler);
                EtoBundle.Init();
                // EtoFontManager.Install(); // 会出现"A shared NSFontManager instance already exists"

                var nsApp = application.ControlObject as NSApplication;
                var nsWindow = window.ControlObject as NSWindow;
                nsWindow.WillClose += delegate { nsApp.AbortModal(); };
                window.Visible = true;
                nsApp.RunModalForWindow(nsWindow);
            }
        }

        public Control ConvertControlToEto(object platformControl)
        {
            if (platformControl == null) return null;
            if (platformControl is NSView) return (platformControl as NSView).ToEto();
            else return null;
        }

        public object ConvertControlToPlatform(Control etoControl)
        {
            if (etoControl == null) return null;
            return etoControl.ToNative(true);
        }

        public WindowPanel ConvertWindowPanelToEto(object platformWindowPanel)
        {
            return null;
        }

        public ConfigPanel ConvertConfigPanelToEto(object platformConfigPanel)
        {
            return null;
        }

        public bool RunDialog(DialogPanel panel)
        {
            return false;
        }

        public Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<string, string>();
            table["MonoMac"] = ResourceLoader.LoadText("MonoMac.LICENSE");
            table["The OpenGL Extension Wrangler Library"] = ResourceLoader.LoadText("GLEW.LICENSE");
            return table;
        }

        public bool ShouldPassParent()
        {
            return true;
        }

        public bool CanParentReceiveChildEvents()
        {
            return true;
        }

        private bool InBundle
        {
            get
            {
                var executableFolderDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.GetFileName(executableFolderDir) == "MacOS";
            }
        }

		private delegate void UncaughtExceptionHandlerDelegate(IntPtr nsexceptionPtr);
		
		[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
		private static extern void NSSetUncaughtExceptionHandler(UncaughtExceptionHandlerDelegate handler);

		private static void UncaughtExceptionHandler(IntPtr nsexceptionPtr)
		{
			var nsexception = Runtime.GetNSObject<NSException>(nsexceptionPtr);
			if (nsexception != null)
			{
				if (EtoEnvironment.Platform.IsMono)
				{
					// mono includes full stack already
					throw new ObjCException(nsexception);
				}
				else
				{
					// .NET 5 does not include the full stack as it goes through native code.
					// Fortunately, .NET 5 does actually use the StackTrace property for its Exception.ToString() implementation,
					// so we can feed the stack to the exception object.
					var st = new System.Diagnostics.StackTrace(1); // skip UncaughtException method
					var ststr = st.ToString();
					throw new ObjCException(nsexception, st.ToString());
				}
			}
		}
    }
}