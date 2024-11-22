using ASEva;

namespace WinformWpfConverter
{
    public class WinformHost
    {
        public static void RegisterWpfFunctions()
        {
            if (wpfFunctionsRegistered) return;

            wpfFunctionsRegistered = true;

            FuncManager.Register("GetUISecondaryBackendCode", delegate { return "wpf"; });
            FuncManager.Register("GetUISecondaryBackendAPIVersion", delegate { return ASEva.UIWpf.APIInfo.GetAPIVersion(); });
            FuncManager.Register("GetUISecondaryBackendAPIThirdPartyNotices", delegate { return ASEva.UIWpf.APIInfo.GetThirdPartyNotices(); });
        }

        public static ASEva.UICoreWF.WindowPanel ConvertWindowPanel(object windowPanel)
        {
            if (!wpfFunctionsRegistered) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not ASEva.UIWpf.WindowPanel) return null;
            return new WpfEmbedderWindowPanel(windowPanel as ASEva.UIWpf.WindowPanel);
        }

        public static ASEva.UICoreWF.ConfigPanel ConvertConfigPanel(object configPanel)
        {
            if (!wpfFunctionsRegistered) return null;
            if (configPanel == null) return null;
            if (configPanel is not ASEva.UIWpf.ConfigPanel) return null;
            return new WpfEmbedderConfigPanel(configPanel as ASEva.UIWpf.ConfigPanel);
        }

        private static bool wpfFunctionsRegistered = false;
    }
}
