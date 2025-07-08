using System;
using Avalonia;
using Avalonia.Headless;
using ASEva.UIAvalonia;

namespace GeneralHostAvalonia
{
    public class GeneralHost
    {
        public static void InitAvaloniaEnvironment(Func<object> appBuilderCreation)
        {
            if (avaloniaEnvInitialized) return;

            avaloniaEnvInitialized = true;

            var appBuilder = appBuilderCreation == null ? null : appBuilderCreation.Invoke() as AppBuilder;
            if (appBuilder == null) appBuilder = finishAppBuilder(AppBuilder.Configure<AvaloniaApplicationSimpleRoundTheme>());

            appBuilder.SetupWithoutStarting();

            var initializer = new HeadlessInitializer();
            initializer.Show();

            ASEva.FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });
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
            return appBuilder.UseSkia().UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false }).WithInterFont();
        }

        private static bool avaloniaEnvInitialized = false;
    }
}
