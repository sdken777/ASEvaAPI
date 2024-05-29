using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CustomMessageBox.Avalonia;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.0) Utility functions for Avalonia application
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.0) Avalonia应用程序相关的实用函数
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
                initAppInvoked = true;
                if (EtoInitializer.Validate())
                {
                    appBuilder = AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont();
                    if (appBuilder != null)
                    {
                        appLifetime = new ClassicDesktopStyleApplicationLifetime();
                        appLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
                        appBuilder.SetupWithLifetime(appLifetime);
                        EtoInitializer.Initialize();

                        AppDomain.CurrentDomain.UnhandledException += (o, args) => { triggerFatalException(args); };

                        FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
                        FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
                        FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });
                    }
                }
            }
            return appBuilder != null;
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
        public static void Run(Window mainWindow)
        {
            if (appBuilder == null || appBuilder.Instance == null || mainWindow == null) return;

            var token = new CancellationTokenSource();
            mainWindow.Closed += delegate { token.Cancel(); };

            fatalException = null;

            exceptionTimer = new DispatcherTimer();
            exceptionTimer.Interval = TimeSpan.FromSeconds(0.1);
            exceptionTimer.Tick += delegate
            {
                if (fatalException != null)
                {
                    exceptionTimer.Stop();
                    try { mainWindow.Close(); }
                    catch (Exception) { token.Cancel(); }
                }
            };
            exceptionTimer.Start();

            try
            {
                if (!mainWindow.IsVisible) mainWindow.Show();
                appLifetime.MainWindow = mainWindow;
                appBuilder.Instance.Run(token.Token);
            }
            catch (Exception ex)
            {
                if (fatalException == null) fatalException = ex;
            }

            appLifetime.MainWindow = null;
            exceptionTimer.Stop();
            exceptionTimer = null;

            if (fatalException != null)
            {
                ShowMessageBox(fatalException.Message + "\n" + fatalException.StackTrace, "", MessageBoxIcon.Error);
            }
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.2) Show message box (mainly for calling outside App.Run)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.2) 显示消息框（主要针对App.Run之外时使用）
        /// </summary>
        public static void ShowMessageBox(String message, String caption = "", MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            var box = new MessageBox(message, caption, icon);
            if (appLifetime.MainWindow == null)
            {
                box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                box.Show(null, buttons);
                App.Run(box);
            }
            else
            {
                box.Show(appLifetime.MainWindow, buttons);
            }
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

        private static void triggerFatalException(UnhandledExceptionEventArgs args)
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
            else if (fatalException == null) fatalException = ex;
        }

        private static bool initAppInvoked = false;
        private static AppBuilder appBuilder = null;
        private static ClassicDesktopStyleApplicationLifetime appLifetime = null;
        private static DispatcherTimer exceptionTimer = null;
        private static Exception fatalException = null;
    }
}