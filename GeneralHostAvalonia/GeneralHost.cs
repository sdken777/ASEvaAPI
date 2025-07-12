using System;
using System.Threading;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input.Platform;
using Avalonia.Controls.Platform;
using ASEva.UIAvalonia;

namespace GeneralHostAvalonia
{
    public class GeneralHost
    {
        public static void InitAvaloniaEnvironment(Func<object> appBuilderCreation)
        {
            if (avaloniaEnvInitialized) return;

            avaloniaEnvInitialized = true;

            var originSyncCtx = SynchronizationContext.Current;

            var appBuilder = appBuilderCreation == null ? null : appBuilderCreation.Invoke() as AppBuilder;
            if (appBuilder == null) appBuilder = finishAppBuilder(AppBuilder.Configure<AvaloniaApplicationSimpleRoundTheme>());

            appBuilder.SetupWithoutStarting();

            var initializer = new HeadlessInitializer();
            initializer.Show();

            SynchronizationContext.SetSynchronizationContext(originSyncCtx);

            App.DialogHandler = new DialogHandler();

            ASEva.FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });

            Window.WindowOpenedEvent.AddClassHandler(typeof(Window), (o, e) =>
            {
                var window = (Window)o;
                if (EtoHostPanelCommon.GetEtoPanel(window) != null) return;

                var etoWindow = new FormHost(window);
                var common = new EtoHostPanelCommon(window, etoWindow.MainPanel);
                common.Initialize();

                etoWindow.Closed += delegate
                {
                    common.StopTimer();
                    etoWindow.SystemClose = true;
                    common.CloseAvaloniaWindow();
                };

                etoWindow.Show();
            });
        }

        public static void InitAvaloniaEnvironmentSimple(bool useRoundTheme)
        {
            if (avaloniaEnvInitialized) return;
            
            if (useRoundTheme)
            {
                InitAvaloniaEnvironment(() => finishAppBuilder(AppBuilder.Configure<AvaloniaApplicationSimpleRoundTheme>()));
            }
            else
            {
                InitAvaloniaEnvironment(() => finishAppBuilder(AppBuilder.Configure<AvaloniaApplicationSimpleTheme>()));
            }
        }

        public static ASEva.UIEto.WindowPanel ConvertWindowPanel(object windowPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not WindowPanel) return null;
            return new EtoHostWindowPanel(windowPanel as WindowPanel);
        }

        public static ASEva.UIEto.ConfigPanel ConvertConfigPanel(object configPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (configPanel == null) return null;
            if (configPanel is not ConfigPanel) return null;
            return new EtoHostConfigPanel(configPanel as ConfigPanel);
        }

        private static AppBuilder finishAppBuilder(AppBuilder appBuilder)
        {
            return appBuilder.UseSkia().UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false }).WithInterFont().AfterPlatformServicesSetup(delegate
            {
                AvaloniaLocator locator = null;
                foreach (var m in typeof(AvaloniaLocator).GetMembers())
                {
                    if (m.Name == "get_CurrentMutable" && m is MethodInfo methodInfo)
                    {
                        locator = methodInfo.Invoke(null, null) as AvaloniaLocator;
                    }
                }
                if (locator == null) return;

                var registerClipboard = new AvaloniaLocator.RegistrationHelper<IClipboard>(locator);
                registerClipboard.ToSingleton<Clipboard>();

                var registerStorageProviderFactory = new AvaloniaLocator.RegistrationHelper<IStorageProviderFactory>(locator);
                registerStorageProviderFactory.ToSingleton<StorageProviderFactory>();   
            });
        }

        private class FormHost : Eto.Forms.Form
        {
            public FormHost(Window avaloniaWindow)
            {
                Title = avaloniaWindow.Title;
                if (avaloniaWindow.CanResize)
                {
                    ASEva.UIEto.SetClientSizeExtensions.SetClientSize(this, (int)avaloniaWindow.Width, (int)avaloniaWindow.Height);
                    ASEva.UIEto.SetClientSizeExtensions.SetMinimumClientSize(this, (int)avaloniaWindow.MinWidth, (int)avaloniaWindow.MinHeight);
                    Resizable = true;
                }
                else
                {
                    ASEva.UIEto.SetClientSizeExtensions.SetClientSize(this, (int)avaloniaWindow.Width, (int)avaloniaWindow.Height);
                    WindowStyle = avaloniaWindow.SystemDecorations == SystemDecorations.None ? Eto.Forms.WindowStyle.None : Eto.Forms.WindowStyle.Default;
                    Resizable = false;
                }

                MainPanel = ASEva.UIEto.SetContentAsControlExtension.SetContentAsControl(this, new Eto.Forms.Panel(), 0);

                avaloniaWindow.Closing += delegate
                {
                    if (!SystemClose) Close();
                };
            }

            public Eto.Forms.Panel MainPanel { get; private set; }

            public bool SystemClose { private get; set; }
        }

        private static bool avaloniaEnvInitialized = false;
    }
}
