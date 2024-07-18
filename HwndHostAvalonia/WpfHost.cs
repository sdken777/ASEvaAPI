using System;

namespace HwndHostAvalonia
{
    public class WpfHost : CommonHost
    {
        public static ASEva.UIWpf.WindowPanel ConvertWindowPanel(object windowPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (windowPanel == null) return null;
            if (windowPanel is not ASEva.UIAvalonia.WindowPanel) return null;
            return new WpfWindowPanel(windowPanel as ASEva.UIAvalonia.WindowPanel);
        }

        public static ASEva.UIWpf.ConfigPanel ConvertConfigPanel(object configPanel)
        {
            if (!avaloniaEnvInitialized) return null;
            if (configPanel == null) return null;
            if (configPanel is not ASEva.UIAvalonia.ConfigPanel) return null;
            return new WpfConfigPanel(configPanel as ASEva.UIAvalonia.ConfigPanel);
        }
    }
}
