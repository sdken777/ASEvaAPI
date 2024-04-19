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
        void RunApp(Application application, Form mainWindow, Form[] subWindows);
        Font CreateDefaultFont();
        Control ConvertControlToEto(object platformControl);
        object ConvertControlToPlatform(Control etoControl);
        WindowPanel ConvertWindowPanelToEto(object platformWindowPanel);
        ConfigPanel ConvertConfigPanelToEto(object platformConfigPanel);
        bool RunDialog(DialogPanel panel);
        Dictionary<String, String> GetThirdPartyNotices();
        bool ShouldPassParent();
    }

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Utility functions for Eto application
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) Eto应用程序相关的实用函数
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
                AppDomain.CurrentDomain.UnhandledException += (o, args) => { TriggerFatalException(args); };
            }
            return application != null;
        }

        /// \~English
        /// <summary>
        /// Initialize application with the specified UI framework
        /// </summary>
        /// <param name="uiCode">UI framework code</param>
        /// <returns>Whether initialization is successfull</returns>
        /// \~Chinese
        /// <summary>
        /// 以指定UI框架初始化应用程序
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
        /// (api:app=3.0.1) Initialize GPU rendering options (Only the first call is valid)
        /// </summary>
        /// <param name="renderingDisabled">Whether to disable GPU rendering</param>
        /// <param name="onscreenRenderingEnabled">Whether to enable GPU onscreen rendering (If renderingDisabled is true, this setting is invalid)</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.0.1) 初始化GPU渲染选项，仅第一次调用有效
        /// </summary>
        /// <param name="renderingDisabled">是否禁用GPU渲染</param>
        /// <param name="onscreenRenderingEnabled">是否启用GPU在屏渲染，若renderingDisabled为true则此设定无效</param>
        public static void InitGPUOptions(bool renderingDisabled, bool onscreenRenderingEnabled)
        {
            if (!gpuOptionsInitialized)
            {
                gpuOptionsInitialized = true;
                GPUOptions.IsGPURenderingDisabled = renderingDisabled;
                GPUOptions.IsOnscreenGPURenderingEnabled = onscreenRenderingEnabled;
            }
        }

        /// \~English
        /// <summary>
        /// Run application
        /// </summary>
        /// <param name="mainWindow">The main window</param>
        /// \~Chinese
        /// <summary>
        /// 运行应用程序
        /// </summary>
        /// <param name="mainWindow">主窗口</param>
        public static void Run(Form mainWindow)
        {
            Run(mainWindow, null);
        }

        /// \~English
        /// <summary>
        /// Run application
        /// </summary>
        /// <param name="mainWindow">The main window</param>
        /// <param name="subWindows"></param>
        /// \~Chinese
        /// <summary>
        /// 运行应用程序
        /// </summary>
        /// <param name="mainWindow">主窗口</param>
        /// <param name="subWindows"></param>
        public static void Run(Form mainWindow, Form[] subWindows)
        {
            if (handler != null && application != null && mainWindow != null && firstFatalException == null)
            {
                mainWindow.Closed += delegate { mainWindow.CloseRecursively(); };

                exceptionTimer = new UITimer();
                exceptionTimer.Interval = 0.1;
                exceptionTimer.Elapsed += delegate
                {
                    if (firstFatalException != null)
                    {
                        exceptionTimer.Stop();
                        application.Quit();
                    }
                };
                exceptionTimer.Start();

                var validSubWindows = new List<Form>();
                if (subWindows != null)
                {
                    foreach (var form in subWindows)
                    {
                        if (form != null) validSubWindows.Add(form);
                    }
                }

                try
                {
                    handler.RunApp(application, mainWindow, validSubWindows.ToArray());
                }
                catch (Exception ex)
                {
                    if (firstFatalException == null) firstFatalException = ex;
                }

                exceptionTimer.Stop();
                exceptionTimer = null;

                if (firstFatalException != null)
                {
                    MessageBox.Show(firstFatalException.Message + "\n" + firstFatalException.StackTrace, MessageBoxType.Error);
                }
            }
        }

        /// \~English
        /// <summary>
        /// Get current running OS's code, which is the same as ASEva.APIInfo.GetRunningOS
        /// </summary>
        /// <returns>OS code, null if unrecognized</returns>
        /// \~Chinese
        /// <summary>
        /// 返回当前运行的OS代号，与 ASEva.APIInfo.GetRunningOS 结果一致
        /// </summary>
        /// <returns>OS代号，若无法识别返回null</returns>
        public static String GetRunningOS()
        {
            return ASEva.APIInfo.GetRunningOS();
        }

        /// \~English
        /// <summary>
        /// Get current running UI framework's code
        /// </summary>
        /// <returns>UI framework code, null if ASEva.UIEto.App.Init is not called or initialization failed</returns>
        /// \~Chinese
        /// <summary>
        /// 返回当前运行的UI框架代号
        /// </summary>
        /// <returns>UI框架代号，若未运行 ASEva.UIEto.App.Init 或初始化失败则返回null</returns>
        public static String GetRunningUI()
        {
            return runningUI;
        }

        /// \~English
        /// <summary>
        /// Get the backend code of current running UI
        /// </summary>
        /// <returns>UI backend code, null if ASEva.UIEto.App.Init is not called or initialization failed or there's no backend for current UI</returns>
        /// \~Chinese
        /// <summary>
        /// 返回当前运行UI的后端代号
        /// </summary>
        /// <returns>UI的后端代号，若未运行 ASEva.UIEto.App.Init 、或初始化失败、或当前运行UI无后端则返回null</returns>
        public static String GetUIBackend()
        {
            return uiBackend;
        }

        /// \~English
        /// <summary>
        /// Get current running UI and UI backend's third party license notices
        /// </summary>
        /// <returns>Key is title, value is notice</returns>
        /// \~Chinese
        /// <summary>
        /// 返回当前运行的UI框架和UI后端使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetUIBackendThirdPartyNotices()
        {
            return handler == null ? null : handler.GetThirdPartyNotices();
        }

        /// \~English
        /// <summary>
        /// Get current WebView backend code
        /// </summary>
        /// <returns>WebView backend code, null if ASEva.UIEto.App.Init is not called or initialization failed</returns>
        /// \~Chinese
        /// <summary>
        /// 返回当前WebView使用的后台框架代号
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
        /// Get default font
        /// </summary>
        /// <param name="sizeRatio">Ratio to the default size, default is 1</param>
        /// <returns>Font object</returns>
        /// \~Chinese
        /// <summary>
        /// 获取默认字体
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
                var font = FontLibraryEto.GetFont(defaultFont, targetSize);
                if (font != null) return font;
                else newFontFailed = true;
            }
            return SystemFonts.Default(targetSize);
        }

        /// \~English
        /// <summary>
        /// Get application's work path
        /// </summary>
        /// <value>Work path</value>
        /// \~Chinese
        /// <summary>
        /// 获取应用程序的工作路径
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
        /// Convert platform control to Eto control
        /// </summary>
        /// <param name="platformControl">Platform control</param>
        /// <returns>Eto control, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将平台特化控件转化为Eto控件
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
        /// Convert Eto control to platform control
        /// </summary>
        /// <param name="etoControl">Eto control</param>
        /// <returns>Platform control, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将Eto控件转化为平台特化控件
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
        /// Convert platform window panel to Eto window panel
        /// </summary>
        /// <param name="platformWindowPanel">Platform window panel</param>
        /// <returns>Eto window panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将平台特化窗口控件转化为Eto窗口控件
        /// </summary>
        /// <param name="platformWindowPanel">平台特化窗口控件</param>
        /// <returns>Eto窗口控件，若转化失败则返回null</returns>
        public static WindowPanel ConvertWindowPanelToEto(object platformWindowPanel)
        {
            if (handler == null || platformWindowPanel == null) return null;
            if (platformWindowPanel is WindowPanel) return platformWindowPanel as WindowPanel;
            else return handler.ConvertWindowPanelToEto(platformWindowPanel);
        }

        /// \~English
        /// <summary>
        /// Convert platform config panel to Eto config panel
        /// </summary>
        /// <param name="platformConfigPanel">Platform config panel</param>
        /// <returns>Eto config panel, null if conversion failed</returns>
        /// \~Chinese
        /// <summary>
        /// 将平台特化配置界面控件转化为Eto配置界面控件
        /// </summary>
        /// <param name="platformConfigPanel">平台特化配置界面控件</param>
        /// <returns>Eto配置界面控件，若转化失败则返回null</returns>
        public static ConfigPanel ConvertConfigPanelToEto(object platformConfigPanel)
        {
            if (handler == null || platformConfigPanel == null) return null;
            if (platformConfigPanel is ConfigPanel) return platformConfigPanel as ConfigPanel;
            else return handler.ConvertConfigPanelToEto(platformConfigPanel);
        }

        /// \~English
        /// <summary>
        /// Run dialog
        /// </summary>
        /// <param name="panel">Target dialog panel object</param>
        /// <returns>Whether the dialog is shown. You should use "Result" properties of the dialog panel object to get result</returns>
        /// \~Chinese
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="panel">对话框主面板</param>
        /// <returns>是否成功弹出，对话框的运行结果应通过主面板的各Result属性获取</returns>
        public static bool RunDialog(DialogPanel panel)
        {
            if (handler == null || panel == null || firstFatalException != null) return false;

            UITimer localTimer = null;
            if (exceptionTimer == null)
            {
                localTimer = new UITimer();
                localTimer.Interval = 0.1;
                localTimer.Elapsed += delegate
                {
                    if (firstFatalException != null)
                    {
                        localTimer.Stop();
                        panel.Close();
                    }
                };
                localTimer.Start();
            }

            try
            {
                var runOK = handler.RunDialog(panel);
                if (!runOK)
                {
                    var dialogEto = new AppDialogEto(panel);
                    dialogEto.MoveToCenter();
                    dialogEto.ShowModal();
                }
            }
            catch (Exception ex)
            {
                if (firstFatalException == null) firstFatalException = ex;
            }

            if (localTimer != null)
            {
                localTimer.Stop();
                localTimer = null;
                if (firstFatalException != null)
                {
                    MessageBox.Show(firstFatalException.Message + "\n" + firstFatalException.StackTrace, MessageBoxType.Error);
                }
            }

            return firstFatalException == null;
        }

        /// \~English
        /// <summary>
        /// It's recommended to use this function to pass parent argument while calling ShowDialog
        /// </summary>
        /// <param name="parent">"parent" argument</param>
        /// <returns>"parent" or null</returns>
        /// \~Chinese
        /// <summary>
        /// 调用ShowDialog时建议通过此函数传parent参数
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
        /// It's recommended to use this function to pass parent argument while calling ShowDialog
        /// </summary>
        /// <param name="parent">"parent" argument</param>
        /// <returns>"parent" or null</returns>
        /// \~Chinese
        /// <summary>
        /// 调用ShowDialog时建议通过此函数传parent参数
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
		/// Key press event of main window (Generally only for plugins)
		/// </summary>
		/// \~Chinese
		/// <summary>
		/// 主窗口的按键事件（一般仅供插件使用）
		/// </summary>
		public static event EventHandler<KeyEventArgs> KeyDown;

		/// \~English
		/// <summary>
        /// Workflow should use this function to manually trigger the KeyDown event
        /// </summary>
		/// \~Chinese
        /// <summary>
        /// 主流程应使用此函数手动触发KeyDown事件
        /// </summary>
        public static void TriggerKeyDown(object sender, KeyEventArgs args)
        {
            KeyDown?.Invoke(sender, args);
        }

		/// \~English
		/// <summary>
        /// Whether a fatal exception occurred, and the application will terminate immediately
        /// </summary>
		/// \~Chinese
        /// <summary>
        /// 是否发生了致命异常，应用程序将立即强制退出
        /// </summary>
        public static bool FatalException
        {
            get { return firstFatalException != null; }
        }

        public static void TriggerFatalException(UnhandledExceptionEventArgs args)
        {
            var exObj = args.ExceptionObject;
            if (exObj is TargetInvocationException) exObj = (exObj as TargetInvocationException).InnerException;

            Exception ex = null;
            if (exObj is Exception) ex = exObj as Exception;
            else ex = new Exception("Unknown exception.");

            if (args.IsTerminating)
            {
                try
                {
                    var sep = Path.DirectorySeparatorChar;
                    var logDirPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + sep + "SpadasFiles" + sep + "log";
                    if (!Directory.Exists(logDirPath)) Directory.CreateDirectory(logDirPath);

                    var errorFilePath = logDirPath + sep + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_Exception.txt";
                    using (var writer = new StreamWriter(errorFilePath))
                    {
                        writer.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }

                    Console.WriteLine("Exception message written to: " + errorFilePath);
                }
                catch (Exception) {}
            }
            else if (firstFatalException == null) firstFatalException = ex;
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
            case "macosarm":
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
        private static Exception firstFatalException = null;
        private static UITimer exceptionTimer = null;
        private static bool gpuOptionsInitialized = false;
    }
}