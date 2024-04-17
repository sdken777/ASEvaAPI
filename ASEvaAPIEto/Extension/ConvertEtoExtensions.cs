using System;
using ASEva;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for conversion between structures
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便进行结构体转换操作的扩展
    /// </summary>
    public static class ConvertEtoExtensions
    {
        public static IntSize ToCommon(this Size size)
        {
            return new IntSize(size.Width, size.Height);
        }

        public static Size ToEto(this IntSize size)
        {
            return new Size(size.Width, size.Height);
        }

        public static FloatSize ToCommon(this SizeF size)
        {
            return new FloatSize(size.Width, size.Height);
        }

        public static SizeF ToEto(this FloatSize size)
        {
            return new SizeF(size.Width, size.Height);
        }

        public static IntPoint ToCommon(this Point pt)
        {
            return new IntPoint(pt.X, pt.Y);
        }

        public static Point ToEto(this IntPoint pt)
        {
            return new Point(pt.X, pt.Y);
        }

        public static FloatPoint ToCommon(this PointF pt)
        {
            return new FloatPoint(pt.X, pt.Y);
        }

        public static PointF ToEto(this FloatPoint pt)
        {
            return new PointF(pt.X, pt.Y);
        }

        public static IntRect ToCommon(this Rectangle rect)
        {
            return new IntRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Rectangle ToEto(this IntRect rect)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static FloatRect ToCommon(this RectangleF rect)
        {
            return new FloatRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static RectangleF ToEto(this FloatRect rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static ColorRGBA ToCommon(this Color color)
        {
            return new ColorRGBA((byte)color.Rb, (byte)color.Gb, (byte)color.Bb, (byte)color.Ab);
        }

        public static Color ToEto(this ColorRGBA color)
        {
            return Color.FromArgb(color.R, color.G, color.B, color.A);
        }
    }
}