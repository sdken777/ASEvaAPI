using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    public interface AppHandler
    {
        Application CreateApp(out String uiBackend, out String webViewBackend);
        void RunApp(Application application, Form window);
        Font CreateDefaultFont();
        Control ConvertControlToEto(object platformControl);
        object ConvertControlToPlatform(Control etoControl);
        bool RunDialog(DialogPanel panel);
        Dictionary<String, String> GetThirdPartyNotices();
        bool ShouldPassParent();
    }

    /// \~English
    /// <summary>
    /// (api:eto=2.0.0) Utility functions for Eto application
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.0.0) Eto应用程序相关的实用函数
    /// </summary>
    public class App
    {
        /// \~English
        /// <summary>
        /// Initialize application
        /// </summary>
        /// <returns>Whether initialization is successfull</returns>
        /// \~Chinese
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <returns>是否成功</returns>
        public static bool Init()
        {
            if (!initAppInvoked)
            {
                var availableUICodes = getAvailableUICodes();
                if (availableUICodes == null) return false;
                initApp(availableUICodes[0]);
            }
            return application != null;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.7) Initialize application with the specified UI framework
        /// </summary>
        /// <param name="uiCode">UI framework code</param>
        /// <returns>Whether initialization is successfull</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.7) 以指定UI框架初始化应用程序
        /// </summary>
        /// <param name="uiCode">UI框架代号</param>
        /// <returns>是否成功</returns>
        public static bool Init(String uiCode)
        {
            if (String.IsNullOrEmpty(uiCode))
            {
                return Init();
            }
            if (!initAppInvoked)
            {
                var availableUICodes = getAvailableUICodes();
                if (availableUICodes == null || !availableUICodes.Contains(uiCode)) return false;
                initApp(uiCode);
            }
            return application != null;
        }

        /// \~English
        /// <summary>
        /// Run application
        /// </summary>
        /// <param name="window">The main window</param>
        /// \~Chinese
        /// <summary>
        /// 运行应用程序
        /// </summary>
        /// <param name="window">主窗口</param>
        public static void Run(Form window)
        {
            if (handler != null && application != null && window != null)
            {
                window.KeyDown += (o, e) => { KeyDown?.Invoke(o, e); };
                window.Closed += delegate { window.CloseRecursively(); };
                handler.RunApp(application, window);
            }
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.7) Get current running OS's code, which is the same as ASEva.APIInfo.GetRunningOS
        /// </summary>
        /// <returns>OS code, null if unrecognized</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.7) 返回当前运行的OS代号，与 ASEva.APIInfo.GetRunningOS 结果一致
        /// </summary>
        /// <returns>OS代号，若无法识别返回null</returns>
        public static String GetRunningOS()
        {
            return ASEva.APIInfo.GetRunningOS();
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.7) Get current running UI framework's code
        /// </summary>
        /// <returns>UI framework code, null if ASEva.UIEto.App.Init is not called or initialization failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.7) 返回当前运行的UI框架代号
        /// </summary>
        /// <returns>UI框架代号，若未运行 ASEva.UIEto.App.Init 或初始化失败则返回null</returns>
        public static String GetRunningUI()
        {
            return runningUI;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.4.0) Get the backend code of current running UI
        /// </summary>
        /// <returns>UI backend code, null if ASEva.UIEto.App.Init is not called or initialization failed or there's no backend for current UI</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.4.0) 返回当前运行UI的后端代号
        /// </summary>
        /// <returns>UI的后端代号，若未运行 ASEva.UIEto.App.Init 、或初始化失败、或当前运行UI无后端则返回null</returns>
        public static String GetUIBackend()
        {
            return uiBackend;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.11.0) Get current running UI and UI backend's third party license notices
        /// </summary>
        /// <returns>Key is title, value is notice</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.11.0) 返回当前运行的UI框架和UI后端使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetUIBackendThirdPartyNotices()
        {
            return handler == null ? null : handler.GetThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.3.4) Get current WebView backend code
        /// </summary>
        /// <returns>WebView backend code, null if ASEva.UIEto.App.Init is not called or initialization failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.3.4) 返回当前WebView使用的后台框架代号
        /// </summary>
        /// <returns>当前WebView使用的后台框架代号，若未运行 ASEva.UIEto.App.Init 或初始化失败则返回null</returns>
        public static String GetWebViewBackend()
        {
            return webViewBackend;
        }

        /// \~English
        /// <summary>
        /// Application instance
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 应用程序对象
        /// </summary>
        public static Application Instance
        {
            get { return application; }
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.6) Get default font
        /// </summary>
        /// <param name="sizeRatio">Ratio to the default size, default is 1</param>
        /// <returns>Font object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.6) 获取默认字体
        /// </summary>
        /// <param name="sizeRatio">相对字体默认大小的比例，默认为1</param>
        /// <returns>默认字体</returns>
        public static Font DefaultFont(float sizeRatio = 1)
        {
            if (handler != null && defaultFont == null)
            {
                defaultFont = handler.CreateDefaultFont();
                defaultFontSize = defaultFont.Size;
            }

            if (defaultFont.Size == 0) defaultFont = SystemFonts.Default(defaultFontSize);

            if (sizeRatio == 1) return defaultFont;
            sizeRatio = Math.Max(0.01f, sizeRatio);

            var targetSize = defaultFont.Size * sizeRatio;
            if (!newFontFailed)
            {
                var font = FontLibrary.GetFont(defaultFont, targetSize);
                if (font != null) return font;
                else newFontFailed = true;
            }
            return SystemFonts.Default(targetSize);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.1.0) Get application's work path
        /// </summary>
        /// <value>Work path</value>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.1.0) 获取应用程序的工作路径
        /// </summary>
        /// <value>工作路径</value>
        public static String WorkPath
        {
            get
            {
                var workDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                if (Path.GetFileName(workDir) == "MacOS")
                {
                    var parentDir1 = Path.GetDirectoryName(workDir);
                    if (Path.GetFileName(parentDir1) == "Contents")
                    {
                        var parentDir2 = Path.GetDirectoryName(parentDir1);
                        if (Path.GetExtension(parentDir2) == ".app")
                        {
                            workDir = Path.GetDirectoryName(parentDir2);
                        }
                    }
                }
                return workDir;
            }
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.0) Convert platform control to Eto control
        /// </summary>
        /// <param name="platformControl">Platform control</param>
        /// <returns>Eto control, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.0) 将平台特化控件转化为Eto控件
        /// </summary>
        /// <param name="platformControl">平台特化控件</param>
        /// <returns>Eto控件，若转化失败则返回null</returns>
        public static Control ConvertControlToEto(object platformControl)
        {
            if (handler == null || platformControl == null) return null;
            return handler.ConvertControlToEto(platformControl);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.0) Convert Eto control to platform control
        /// </summary>
        /// <param name="etoControl">Eto control</param>
        /// <returns>Platform control, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.0) 将Eto控件转化为平台特化控件
        /// </summary>
        /// <param name="etoControl">Eto控件</param>
        /// <returns>平台特化控件，若转化失败则返回null</returns>
        public static object ConvertControlToPlatform(Control etoControl)
        {
            if (handler == null || etoControl == null) return null;
            return handler.ConvertControlToPlatform(etoControl);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.0) Run dialog
        /// </summary>
        /// <param name="panel">Target dialog panel object</param>
        /// <returns>Whether the dialog is shown. You should use "Result" properties of the dialog panel object to get result</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.0) 弹出对话框
        /// </summary>
        /// <param name="panel">对话框主面板</param>
        /// <returns>是否成功弹出，对话框的运行结果应通过主面板的各Result属性获取</returns>
        public static bool RunDialog(DialogPanel panel)
        {
            if (handler == null || panel == null) return false;
            
            var runOK = handler.RunDialog(panel);
            if (runOK) return true;

            var dialogEto = new AppDialogEto(panel);
            dialogEto.MoveToCenter();
            dialogEto.ShowModal();
            return true;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.11.2) It's recommended to use this function to pass parent argument while calling ShowDialog
        /// </summary>
        /// <param name="parent">"parent" argument</param>
        /// <returns>"parent" or null</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.11.2) 调用ShowDialog时建议通过此函数传parent参数
        /// </summary>
        /// <param name="parent">parent参数</param>
        /// <returns>返回parent或null</returns>
        public static Window PassParent(Window parent)
        {
            if (handler == null || !handler.ShouldPassParent()) return null;
            else return parent;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.11.2) It's recommended to use this function to pass parent argument while calling ShowDialog
        /// </summary>
        /// <param name="parent">"parent" argument</param>
        /// <returns>"parent" or null</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.11.2) 调用ShowDialog时建议通过此函数传parent参数
        /// </summary>
        /// <param name="parent">parent参数</param>
        /// <returns>返回parent或null</returns>
        public static Control PassParent(Control parent)
        {
            if (handler == null || !handler.ShouldPassParent()) return null;
            else return parent;
        }

		/// \~English
		/// <summary>
		/// (api:eto=2.11.4) Key press event of main window
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// (api:eto=2.11.4) 主窗口的按键事件
		/// </summary>
		public static event EventHandler<KeyEventArgs> KeyDown;

		/// \~English
		/// <summary>
        /// (api:eto=2.11.4) When ASEva.UIEto.App.Run is not used, you can use this function to manually trigger the KeyDown event
        /// </summary>
		/// \~Chinese
        /// <summary>
        /// (api:eto=2.11.4) 未使用 ASEva.UIEto.App.Run 启动时可使用此函数手动触发KeyDown事件
        /// </summary>
        public static void TriggerKeyDown(object sender, KeyEventArgs args)
        {
            KeyDown?.Invoke(sender, args);
        }

        private static String[] getAvailableUICodes()
        {
            var osCode = GetRunningOS();
            if (osCode == null) return null;

            switch (osCode)
            {
            case "windows":
                return new String[] { "corewf", "wpf" };
            case "linux":
            case "linuxarm":
                return new String[] { "gtk" };
            case "macos":
                return new String[] { "monomac" };
            default:
                return null;
            }
        }

        private static void initApp(String uiCode)
        {
            initAppInvoked = true;

            if (handler == null)
            {
                String dllFileName = null;
                String uiNamespace =  null;
                switch (uiCode)
                {
                case "corewf":
                    dllFileName = "ASEvaAPICoreWF.dll";
                    uiNamespace = "UICoreWF";
                    break;
                case "wpf":
                    dllFileName = "ASEvaAPIWpf.dll";
                    uiNamespace = "UIWpf";
                    break;
                case "gtk":
                    dllFileName = "ASEvaAPIGtk.dll";
                    uiNamespace = "UIGtk";
                    break;
                case "monomac":
                    dllFileName = "ASEvaAPIMonoMac.dll";
                    uiNamespace = "UIMonoMac";
                    break;
                }

                if (dllFileName != null)
                {
                    var workPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var dllFilePath = workPath + Path.DirectorySeparatorChar + dllFileName;
                    if (File.Exists(dllFilePath))
                    {
                        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

                        Assembly assembly = null;
                        foreach (var target in loadedAssemblies)
                        {
                            if (target.FullName.StartsWith("Microsoft.GeneratedCode")) continue;
                            String assemblyLocation = null;
                            try { assemblyLocation = target.Location; }
                            catch (Exception) { continue; }
                            if (assemblyLocation == dllFilePath || assemblyLocation == dllFileName) assembly = target;
                        }
                        if (assembly == null) 
                        {
                            try { assembly = Assembly.LoadFrom(dllFilePath); }
                            catch {}
                        }

                        if (assembly != null)
                        {
                            var handlerCreator = assembly.GetType("ASEva." + uiNamespace + ".AppHandlerCreator");
                            if (handlerCreator != null)
                            {
                                handler = (AppHandler)handlerCreator.InvokeMember("Create", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, new object[] { });
                            }
                        }
                    }
                }
            }

            if (handler != null && application == null)
            {
                application = handler.CreateApp(out uiBackend, out webViewBackend);
                if (application != null) runningUI = uiCode;
            }
        }

        private static AppHandler handler = null;
        private static Application application = null;
        private static String runningUI = null;
        private static String uiBackend = null;
        private static String webViewBackend = null;
        private static Font defaultFont = null;
        private static float defaultFontSize = 0;
        private static bool newFontFailed = false;
        private static bool initAppInvoked = false;
    }
}