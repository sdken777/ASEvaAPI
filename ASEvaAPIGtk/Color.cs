using System;
using ASEva;
using Gdk;

namespace ASEva.UIGtk
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 颜色类型转换
    /// </summary>
    public class ColorConv
    {
        public static RGBA Conv(ColorRGBA target)
        {
            var k = 1.0 / 255;
            return new RGBA
            {
                Red = k * target.R,
                Green = k * target.G,
                Blue = k * target.B,
                Alpha = k * target.A
            };
        }

        public static RGBA Conv(ColorRGBA target, byte alpha)
        {
            var k = 1.0 / 255;
            return new RGBA
            {
                Red = k * target.R,
                Green = k * target.G,
                Blue = k * target.B,
                Alpha = k * alpha,
            };
        }

        public static ColorRGBA Conv(RGBA target)
        {
            return new ColorRGBA((byte)(255 * target.Red), (byte)(255 * target.Green), (byte)(255 * target.Blue), (byte)(255 * target.Alpha));
        }

        public static Cairo.Color ConvCairo(ColorRGBA target)
        {
            var rgba = Conv(target);
            return new Cairo.Color(rgba.Red, rgba.Green, rgba.Blue, rgba.Alpha);
        }

        public static Cairo.Color ConvCairo(ColorRGBA target, byte alpha)
        {
            var rgba = Conv(target, alpha);
            return new Cairo.Color(rgba.Red, rgba.Green, rgba.Blue, rgba.Alpha);
        }

        public static ColorRGBA ConvCairo(Cairo.Color target)
        {
            var rgba = new RGBA() {Red = target.R, Green = target.G, Blue = target.B, Alpha = target.A};
            return Conv(rgba);
        }
    }
}
