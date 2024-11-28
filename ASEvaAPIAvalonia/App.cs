using System;
using System.Collections.Generic;
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
                        subscribeGlobalEvents();
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
                    catch (Exception ex) { Dump.Exception(ex); token.Cancel(); }
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
                Dump.Exception(ex);
                if (fatalException == null) fatalException = ex;

                try { appLifetime.MainWindow?.Hide(); }
                catch (Exception ex2) { Dump.Exception(ex2); }
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
        /// (api:avalonia=1.2.5) Run the dialog action
        /// </summary>
        /// <param name="action">The dialog action, with the active window as argument</param>
        /// <param name="owner">Owner of the dialog, null to use main window</param>
        /// <returns>Whether the dialog action is invoked</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.2.5) 运行对话框函数
        /// </summary>
        /// <param name="action">对话框函数，最顶层活动窗口作为输入参数</param>
        /// <param name="owner">对话框所有者，若使用主窗口则设为null</param>
        /// <returns>对话框函数是否已执行</returns>
        public static async Task<bool> RunDialog(Func<Window, Task> action, object owner = null)
        {
            Window activeWindow = null;
            if (owner == null)
            {
                if (App.MainWindow != null) activeWindow = await App.MainWindow.GetActiveWindow();
                if (activeWindow == null)
                {
                    foreach (var window in allWindows)
                    {
                        if (window.IsActive)
                        {
                            activeWindow = window;
                            break;
                        }
                    }
                }
            }
            else if (owner is Control) activeWindow = await (owner as Control).GetActiveWindow();
            else if (owner is Window) activeWindow = await (owner as Window).GetActiveWindow();

            if (activeWindow == null) return false;

            var etoControls = new Eto.Forms.Control[0];
            if (activeWindow != null)
            {
                etoControls = EtoEmbedder.ExtractControls(activeWindow);
                foreach (var control in etoControls) control.Enabled = false;
            }

            await Task.Delay(10); // 避免EtoEmbedder加载失败

            await action.Invoke(activeWindow);
            foreach (var control in etoControls) control.Enabled = true;
            return true;
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.2) Show message box
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.2) 显示消息框
        /// </summary>
        public static async void ShowMessageBox(String message, String caption = "", MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxButtons buttons = MessageBoxButtons.OK)
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
                    var task = box.Show(null, buttons); // Don't await
                    appBuilder.Instance.Run(token.Token);
                }
                catch (Exception ex) { Dump.Exception(ex); }
            }
            else
            {
                await RunDialog((window) => box.Show(window, buttons));
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
                catch (Exception e) { Dump.Exception(e);}
            }
            else if (fatalException == null) fatalException = ex;
        }

        private static void subscribeGlobalEvents()
        {
            Window.WindowOpenedEvent.AddClassHandler(typeof(Window), (sender, args) =>
            {
                Window window = (Window)sender;
                if (!allWindows.Contains(window)) allWindows.Add(window);
            });
            Window.WindowClosedEvent.AddClassHandler(typeof(Window), (sender, args) =>
            {
                Window window = (Window)sender;
                allWindows.Remove(window);
            });
        }

        private class EtoRunDialogHandler : RunDialogHandler
        {
            public async Task<bool> RunDialog(DialogPanel panel)
            {
                if (App.appBuilder == null) return false;

                var dialog = new EtoEmbedDialog(panel);
                if (App.appLifetime.MainWindow == null) App.Run(dialog);
                else await App.RunDialog(dialog.ShowDialog);
                return true;
            }
        }

        private static bool initAppInvoked = false;
        private static AppBuilder appBuilder = null;
        private static ClassicDesktopStyleApplicationLifetime appLifetime = null;
        private static DispatcherTimer exceptionTimer = null;
        private static Exception fatalException = null;
        private static List<Window> allWindows = new List<Window>();
    }
}