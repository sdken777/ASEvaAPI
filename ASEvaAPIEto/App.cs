using System;
using System.Reflection;
using System.IO;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    public interface AppHandler
    {
        Application CreateApp();
        void RunApp(Application application, Form window);
        Font CreateDefaultFont();
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
            if (handler == null)
            {
                String dllFileName = null;
                String uiNamespace =  null;
                switch (ASEva.APIInfo.GetRunningOS())
                {
                case "windows":
                    dllFileName = "ASEvaAPICoreWF.dll";
                    uiNamespace = "UICoreWF";
                    break;
                case "linux":
                case "linuxarm":
                    dllFileName = "ASEvaAPIGtk.dll";
                    uiNamespace = "UIGtk";
                    break;
                }

                if (dllFileName != null)
                {
                    var workPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var dllFilePath = workPath + Path.DirectorySeparatorChar + dllFileName;
                    if (File.Exists(dllFilePath))
                    {
                        var stream = new FileStream(dllFilePath, FileMode.Open, FileAccess.Read);
                        var fileBinary = new byte[stream.Length];
                        stream.Read(fileBinary, 0, (int)stream.Length);
                        stream.Close();

                        var assembly = Assembly.Load(fileBinary);
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
                application = handler.CreateApp();
            }

            return application != null;
        }

        /// <summary>
        /// 运行应用程序
        /// </summary>
        /// <param name="window">主窗口</param>
        public static void Run(Form window)
        {
            if (application != null)
            {
                application.Run(window);
            }
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
            }
            sizeRatio = Math.Max(0.01f, sizeRatio);
            return new Font(defaultFont.Family, defaultFont.Size * sizeRatio, defaultFont.FontStyle, defaultFont.FontDecoration);
        }

        private static AppHandler handler;
        private static Application application;
        private static Font defaultFont;
    }
}