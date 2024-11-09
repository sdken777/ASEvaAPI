using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ASEva.UIEto;
using ASEva.Utility;
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
            return Init(() => AppBuilder.Configure<AvaloniaApplication>().UsePlatformDetect().WithInterFont());
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.12) Initialize application with the specified AppBuilder
        /// </summary>
        /// <param name="appBuilderCreation">The function to create AppBuilder object</param>
        /// <returns>Whether initialization is successfull</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.12) 使用指定的AppBuilder进行应用程序初始化
        /// </summary>
        /// <param name="appBuilderCreation">创建AppBuilder对象的函数</param>
        /// <returns>是否成功</returns>
        public static bool Init(Func<AppBuilder> appBuilderCreation)
        {
            if (!initAppInvoked)
            {
                initAppInvoked = true;
                if (EtoInitializer.Validate())
                {
                    appBuilder = appBuilderCreation?.Invoke();
                    if (appBuilder != null)
                    {
                        appLifetime = new ClassicDesktopStyleApplicationLifetime();
                        appLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
                        appBuilder.SetupWithLifetime(appLifetime);
                        EtoInitializer.Initialize(new EtoRunDialogHandler());

                        var etoInitWindow = new EtoInitWindow();
                        etoInitWindow.Show();
                        var token = new CancellationTokenSource();
                        etoInitWindow.Closed += delegate { token.Cancel(); };
                        appLifetime.MainWindow = etoInitWindow;
                        appBuilder.Instance.Run(token.Token);
                        appLifetime.MainWindow = null;

                        if (EtoInitializer.InitializeResult.Value)
                        {
                            AppDomain.CurrentDomain.UnhandledException += (o, args) => { triggerFatalException(args); };

                            FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
                            FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
                            FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });
                        }
                        else appBuilder = null;
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
            if (appBuilder == null || appBuilder.Instance == null || message == null) return;

            var box = new MessageBox(message, caption, icon);
            if (appLifetime.MainWindow == null)
            {
                var token = new CancellationTokenSource();
                box.Closed += delegate { token.Cancel(); };
                box.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                try
                {
                    box.Show(null, buttons);
                    appBuilder.Instance.Run(token.Token);
                }
                catch (Exception) {}
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
                var workDir = EntryFolder.Path;
                if (workDir == null) return null;
                
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
        /// (api:avalonia=1.0.13) Get current main window
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.13) 获取当前主窗口
        /// </summary>
        public static Window MainWindow
        {
            get
            {
                return appLifetime?.MainWindow;
            }
        } 

        /// \~English
        /// <summary>
        /// (api:avalonia=1.1.1) Get initialization result of Eto framework, null means not initialized yet
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.1.1) 获取Eto初始化结果，null表示还未进行初始化
        /// </summary>
        public static bool? EtoInitializeResult
        {
            get
            {
                return EtoInitializer.InitializeResult;
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

        private class EtoRunDialogHandler : RunDialogHandler
        {
            public async Task<bool> RunDialog(DialogPanel panel)
            {
                if (App.appBuilder == null) return false;

                var dialog = new EtoEmbedDialog(panel);
                if (App.appLifetime.MainWindow == null) App.Run(dialog);
                else await dialog.ShowDialog(App.appLifetime.MainWindow.GetActiveDialog());
                return true;
            }
        }

        private static bool initAppInvoked = false;
        private static AppBuilder appBuilder = null;
        private static ClassicDesktopStyleApplicationLifetime appLifetime = null;
        private static DispatcherTimer exceptionTimer = null;
        private static Exception fatalException = null;
    }
}