using System;

namespace HwndHostAvalonia
{
    public class WinformHost : CommonHost
    {
        public static ASEva.UICoreWF.WindowPanel? ConvertWindowPanel(object windowPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel) return null;
            return new WinformWindowPanel(avaloniaWindowPanel);
        }

        public static ASEva.UICoreWF.ConfigPanel? ConvertConfigPanel(object configPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (configPanel == null) return null;
            if (configPanel is not ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel) return null;
            return new WinformConfigPanel(avaloniaConfigPanel);
        }
    }
}
