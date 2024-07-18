using Avalonia;
using ASEva.UIAvalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace HwndHostAvalonia
{
    public class CommonHost
    {
        public static void InitAvaloniaEnvironment()
        {
            if (avaloniaEnvInitialized) return;

            avaloniaEnvInitialized = true;

            var appBuilder = AppBuilder.Configure<AvaloniaApplicationSimpleTheme>().UsePlatformDetect().WithInterFont();
            var appLifetime = new ClassicDesktopStyleApplicationLifetime();
            appBuilder.SetupWithLifetime(appLifetime);

            ASEva.FuncManager.Register("GetAvaloniaAPIVersion", delegate { return APIInfo.GetAPIVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaLibVersion", delegate { return APIInfo.GetAvaloniaLibVersion(); });
            ASEva.FuncManager.Register("GetAvaloniaAPIThirdPartyNotices", delegate { return APIInfo.GetThirdPartyNotices(); });
        }

        protected static bool avaloniaEnvInitialized = false;
    }
}
