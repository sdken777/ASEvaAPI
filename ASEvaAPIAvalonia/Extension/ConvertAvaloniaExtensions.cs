using System;
using ASEva;
using Avalonia;
using Avalonia.Media;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.8) Extensions for conversion between structures
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.8) 方便进行结构体转换操作的扩展
    /// </summary>
    public static class ConvertAvaloniaExtensions
    {
        public static IntSize ToCommon(this PixelSize size)
        {
            return new IntSize(size.Width, size.Height);
        }

        public static PixelSize ToAvalonia(this IntSize size)
        {
            return new PixelSize(size.Width, size.Height);
        }

        public static FloatSize ToCommon(this Size size)
        {
            return new FloatSize((float)size.Width, (float)size.Height);
        }

        public static Size ToAvalonia(this FloatSize size)
        {
            return new Size(size.Width, size.Height);
        }

        public static IntPoint ToCommon(this PixelPoint pt)
        {
            return new IntPoint(pt.X, pt.Y);
        }

        public static PixelPoint ToAvalonia(this IntPoint pt)
        {
            return new PixelPoint(pt.X, pt.Y);
        }

        public static FloatPoint ToCommon(this Point pt)
        {
            return new FloatPoint((float)pt.X, (float)pt.Y);
        }

        public static Point ToAvalonia(this FloatPoint pt)
        {
            return new Point(pt.X, pt.Y);
        }

        public static IntRect ToCommon(this PixelRect rect)
        {
            return new IntRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static PixelRect ToAvalonia(this IntRect rect)
        {
            return new PixelRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static FloatRect ToCommon(this Rect rect)
        {
            return new FloatRect((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        public static Rect ToAvalonia(this FloatRect rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static ColorRGBA ToCommon(this Color color)
        {
            return new ColorRGBA(color.R, color.G, color.B, color.A);
        }

        public static Color ToAvalonia(this ColorRGBA color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}