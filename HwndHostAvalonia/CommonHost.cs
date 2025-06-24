using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ASEva.UIAvalonia;

namespace HwndHostAvalonia
{
    public class CommonHost
    {
        public static void InitAvaloniaEnvironment(Func<object> appBuilderCreation)
        {
            if (avaloniaEnvInitialized) return;

            avaloniaEnvInitialized = true;

            var appBuilder = appBuilderCreation == null ? null : appBuilderCreation.Invoke() as AppBuilder;
            if (appBuilder == null) appBuilder = AppBuilder.Configure<AvaloniaApplicationSimpleTheme>().UsePlatformDetect().WithInterFont();

            var appLifetime = new ClassicDesktopStyleApplicationLifetime();
            appBuilder.SetupWithLifetime(appLifetime);

            ASEva.FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });
        }

        public static void InitAvaloniaEnvironmentSimple(bool useRoundTheme)
        {
            if (avaloniaEnvInitialized) return;
            
            if (useRoundTheme)
            {
                InitAvaloniaEnvironment(() => AppBuilder.Configure<AvaloniaApplicationSimpleRoundTheme>().UsePlatformDetect().WithInterFont());
            }
            else
            {
                InitAvaloniaEnvironment(() => AppBuilder.Configure<AvaloniaApplicationSimpleTheme>().UsePlatformDetect().WithInterFont());
            }
        }

        protected static bool avaloniaEnvInitialized = false;
    }
}
