using System;
using System.Windows.Forms;

namespace ASEva.UICoreWF
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:corewf=2.0.0) NumericUpDown控件设置值
    /// </summary>
    public class NumericSetter
    {
        public static void Set(NumericUpDown control, double value)
        {
            control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, (decimal)value));
        }
    }
}
