using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.0) Set the background color of the control (solve the problem that the background color is invalid when the control is disabled)
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.0) 设置控件的背景颜色（解决禁用时背景颜色失效问题）
    /// </summary>
    public class BackgroundColorSetter
    {
        public BackgroundColorSetter(TemplatedControl control)
        {
            this.control = control;
            disabledKey = control.GetType().ToString().Split('.').Last() + "BackgroundDisabled";

            style = new Style();
            style.Resources[disabledKey] = control.Background;
            control.Styles.Add(style);
        }

        public IBrush Background
        {
            set
            {
                if (control.Background is SolidColorBrush b1 && value is SolidColorBrush b2 && b1.Color == b2.Color) return;
                control.Background = value;
                style.Resources[disabledKey] = value;
            }
        }

        private TemplatedControl control;
        private Style style;
        private String disabledKey;
    }
}