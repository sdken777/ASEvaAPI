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
                var workPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                foreach (var filePath in Directory.GetFiles(workPath))
                {
                    var fileName = Path.GetFileName(filePath);
                    if (fileName.StartsWith("ASEvaAPI") && fileName.EndsWith(".dll"))
                    {
                        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        var fileBinary = new byte[stream.Length];
                        stream.Read(fileBinary, 0, (int)stream.Length);
                        stream.Close();

                        var assembly = Assembly.Load(fileBinary);
                        if (assembly != null)
                        {
                            var uiType = Path.GetFileNameWithoutExtension(filePath).Substring(8);
                            var handlerCreator = assembly.GetType("ASEva.UI" + uiType + ".AppHandlerCreator");
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