using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// (api:corewf=2.0.0) FlowLayoutPanel子控件宽度自适应
    /// </summary>
    public class FlowPanelResizer
    {
        public static void Resize(FlowLayoutPanel panel)
        {
            var dpiRatio = (float)panel.DeviceDpi / 96;

            if (panel.Width < dpiRatio * 50) return;

            foreach (Control c in panel.Controls)
            {
                c.Width = (int)(panel.Width - dpiRatio * (8 + (panel.VerticalScroll.Visible ? 20 : 0)));
            }
        }
    }
}
