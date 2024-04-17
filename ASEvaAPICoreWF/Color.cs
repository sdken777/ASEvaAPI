using System;
using System.Drawing;
using ASEva;

namespace ASEva.UICoreWF
{
    /// \~English
    /// <summary>
    /// (api:corewf=3.0.0) Color type conversion
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:corewf=3.0.0) 颜色类型转换
    /// </summary>
    public class ColorConv
    {
        public static Color Conv(ColorRGBA target)
        {
            return Color.FromArgb(target.A, target.R, target.G, target.B);
        }

        public static Color Conv(ColorRGBA target, byte alpha)
        {
            return Color.FromArgb(alpha, target.R, target.G, target.B);
        }

        public static ColorRGBA Conv(Color target)
        {
            return new ColorRGBA(target.R, target.G, target.B, target.A);
        }
    }
}
