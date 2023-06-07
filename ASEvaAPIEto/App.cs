using System;
using System.Reflection;
using System.IO;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    public interface AppHandler
    {
        Application CreateApp(out String uiBackend, out String webViewBackend);
        void RunApp(Application application, Form window);
        Font CreateDefaultFont();
        Control ConvertControlToEto(object platformControl);
        object ConvertControlToPlatform(Control etoControl);
        bool RunDialog(DialogPanel panel);
    }

    /// <summary>
    /// (api:eto=2.0.0) Eto应用程序
    /// </summary>
    public class App
    {
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

        /// <summary>
        /// 运行应用程序
        /// </summary>
        /// <param name="window">主窗口</param>
        public static void Run(Form window)
        {
            if (handler != null && application != null && window != null)
            {
                window.Closed += delegate { window.CloseRecursively(); };
                handler.RunApp(application, window);
            }
        }

        /// <summary>
        /// (api:eto=2.0.7) 返回当前运行的OS代号，与 ASEva.APIInfo.GetRunningOS 结果一致
        /// </summary>
        /// <returns>OS代号，若无法识别返回null</returns>
        public static String GetRunningOS()
        {
            return ASEva.APIInfo.GetRunningOS();
        }

        /// <summary>
        /// (api:eto=2.0.7) 返回当前运行的UI框架代号
        /// </summary>
        /// <returns>UI框架代号，若未运行 ASEva.UIEto.App.Init 或初始化失败则返回null</returns>
        public static String GetRunningUI()
        {
            return runningUI;
        }

        /// <summary>
        /// (api:eto=2.4.0) 返回当前运行UI的后端代号
        /// </summary>
        /// <returns>UI的后端代号，若未运行 ASEva.UIEto.App.Init 、或初始化失败、或当前运行UI无后端则返回null</returns>
        public static String GetUIBackend()
        {
            return uiBackend;
        }

        /// <summary>
        /// (api:eto=2.3.4) 返回当前WebView使用的后台框架代号
        /// </summary>
        /// <returns>当前WebView使用的后台框架代号，若未运行 ASEva.UIEto.App.Init 或初始化失败则返回null</returns>
        public static String GetWebViewBackend()
        {
            return webViewBackend;
        }

        /// <summary>
        /// 应用程序对象
        /// </summary>
        public static Application Instance
        {
            get { return application; }
        }

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
    }
}