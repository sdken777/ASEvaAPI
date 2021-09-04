using System;
using System.Reflection;
using System.IO;
using Eto.Forms;

namespace ASEva.UIEto
{
    public interface AppHandler
    {
        Application CreateApp();
        void RunApp(Application application, Form window);
    }

    /// <summary>
    /// (api:eto=2.0.0) Eto应用程序初始化与运行
    /// </summary>
    public class App
    {
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

        public static void Run(Form window)
        {
            if (application != null)
            {
                application.Run(window);
            }
        }

        public static Application Instance
        {
            get { return application; }
        }

        private static AppHandler handler;
        private static Application application;
    }
}