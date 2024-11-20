using ASEva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformWpfConverter
{
    public class WpfHost
    {
        public static void RegisterWinformFunctions()
        {
            if (winformFunctionsRegistered) return;

            winformFunctionsRegistered = true;

            FuncManager.Register("GetUISecondaryBackendCode", delegate { return "corewf"; });
            FuncManager.Register("GetUISecondaryBackendAPIVersion", delegate { return ASEva.UICoreWF.APIInfo.GetAPIVersion(); });
            FuncManager.Register("GetUISecondaryBackendAPIThirdPartyNotices", delegate { return ASEva.UICoreWF.APIInfo.GetThirdPartyNotices(); });
        }

        public static ASEva.UIWpf.WindowPanel? ConvertWindowPanel(object windowPanel)
        {
            if (!winformFunctionsRegistered) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not ASEva.UICoreWF.WindowPanel winformWindowPanel) return null;
            return new WinformEmbedderWindowPanel(winformWindowPanel);
        }

        public static ASEva.UIWpf.ConfigPanel? ConvertConfigPanel(object configPanel)
        {
            if (!winformFunctionsRegistered) return null;
            if (configPanel == null) return null;
            if (configPanel is not ASEva.UICoreWF.ConfigPanel winformConfigPanel) return null;
            return new WinformEmbedderConfigPanel(winformConfigPanel);
        }

        private static bool winformFunctionsRegistered = false;
    }
}
