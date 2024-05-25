using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// \~English
    /// <summary>
    /// (api:eto=3.1.0) Extensions for conversion of setting tool tip
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.1.0) 方便设置tool tip的扩展
    /// </summary>
    public static class SetToolTipExtensions
    {
        public static void SetToolTip(this Control control, String tooltip)
        {
            if (String.IsNullOrWhiteSpace(tooltip)) tooltip = null;
            if (tooltip == null)
            {
                if (String.IsNullOrEmpty(control.ToolTip)) return;
            }
            else
            {
                if (control.ToolTip != null && control.ToolTip == tooltip) return;
            }
            control.ToolTip = tooltip;
            if (Handler != null) Handler.SetToolTip(control, tooltip);
        }

        public interface SetToolTipHandler
        {
            void SetToolTip(Control control, String tooltip);
        }

        public static SetToolTipHandler Handler { private get; set; }
    }
}