using System;

namespace HwndHostAvalonia
{
    public class WinformHost : CommonHost
    {
        public static ASEva.UICoreWF.WindowPanel ConvertWindowPanel(object windowPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not ASEva.UIAvalonia.WindowPanel) return null;
            return new WinformWindowPanel(windowPanel as ASEva.UIAvalonia.WindowPanel);
        }

        public static ASEva.UICoreWF.ConfigPanel ConvertConfigPanel(object configPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (configPanel == null) return null;
            if (configPanel is not ASEva.UIAvalonia.ConfigPanel) return null;
            return new WinformConfigPanel(configPanel as ASEva.UIAvalonia.ConfigPanel);
        }
    }
}
