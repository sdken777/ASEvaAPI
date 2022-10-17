using System;
using ASEva;
using Eto.Drawing;
using SkiaSharp;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.8.14) 方便进行结构体转换操作的扩展
    /// </summary>
    public static class ConvertExtensions
    {
        public static IntSize ToCommon(this Size size)
        {
            return new IntSize(size.Width, size.Height);
        }
        public static IntSize ToCommon(this SKSizeI size)
        {
            return new IntSize(size.Width, size.Height);
        }
        public static Size ToEto(this IntSize size)
        {
            return new Size(size.Width, size.Height);
        }
        public static Size ToEto(this SKSizeI size)
        {
            return new Size(size.Width, size.Height);
        }
        public static SKSizeI ToSkia(this IntSize size)
        {
            return new SKSizeI(size.Width, size.Height);
        }
        public static SKSizeI ToSkia(this Size size)
        {
            return new SKSizeI(size.Width, size.Height);
        }

        public static FloatSize ToCommon(this SizeF size)
        {
            return new FloatSize(size.Width, size.Height);
        }
        public static FloatSize ToCommon(this SKSize size)
        {
            return new FloatSize(size.Width, size.Height);
        }
        public static SizeF ToEto(this FloatSize size)
        {
            return new SizeF(size.Width, size.Height);
        }
        public static SizeF ToEto(this SKSize size)
        {
            return new SizeF(size.Width, size.Height);
        }
        public static SKSize ToSkia(this FloatSize size)
        {
            return new SKSize(size.Width, size.Height);
        }
        public static SKSize ToSkia(this SizeF size)
        {
            return new SKSize(size.Width, size.Height);
        }

        public static IntPoint ToCommon(this Point pt)
        {
            return new IntPoint(pt.X, pt.Y);
        }
        public static IntPoint ToCommon(this SKPointI pt)
        {
            return new IntPoint(pt.X, pt.Y);
        }
        public static Point ToEto(this IntPoint pt)
        {
            return new Point(pt.X, pt.Y);
        }
        public static Point ToEto(this SKPointI pt)
        {
            return new Point(pt.X, pt.Y);
        }
        public static SKPointI ToSkia(this IntPoint pt)
        {
            return new SKPointI(pt.X, pt.Y);
        }
        public static SKPointI ToSkia(this Point pt)
        {
            return new SKPointI(pt.X, pt.Y);
        }

        public static FloatPoint ToCommon(this PointF pt)
        {
            return new FloatPoint(pt.X, pt.Y);
        }
        public static FloatPoint ToCommon(this SKPoint pt)
        {
            return new FloatPoint(pt.X, pt.Y);
        }
        public static PointF ToEto(this FloatPoint pt)
        {
            return new PointF(pt.X, pt.Y);
        }
        public static PointF ToEto(this SKPoint pt)
        {
            return new PointF(pt.X, pt.Y);
        }
        public static SKPoint ToSkia(this FloatPoint pt)
        {
            return new SKPoint(pt.X, pt.Y);
        }
        public static SKPoint ToSkia(this PointF pt)
        {
            return new SKPoint(pt.X, pt.Y);
        }

        public static IntRect ToCommon(this Rectangle rect)
        {
            return new IntRect(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static IntRect ToCommon(this SKRectI rect)
        {
            return new IntRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public static Rectangle ToEto(this IntRect rect)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static Rectangle ToEto(this SKRectI rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public static SKRectI ToSkia(this IntRect rect)
        {
            return new SKRectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
        public static SKRectI ToSkia(this Rectangle rect)
        {
            return new SKRectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static FloatRect ToCommon(this RectangleF rect)
        {
            return new FloatRect(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static FloatRect ToCommon(this SKRect rect)
        {
            return new FloatRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public static RectangleF ToEto(this FloatRect rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static RectangleF ToEto(this SKRect rect)
        {
            return new RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public static SKRect ToSkia(this FloatRect rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
        public static SKRect ToSkia(this RectangleF rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static ColorRGBA ToCommon(this Color color)
        {
            return new ColorRGBA((byte)color.Rb, (byte)color.Gb, (byte)color.Bb, (byte)color.Ab);
        }
        public static ColorRGBA ToCommon(this SKColor color)
        {
            return new ColorRGBA(color.Red, color.Green, color.Blue, color.Alpha);
        }
        public static Color ToEto(this ColorRGBA color)
        {
            return Color.FromArgb(color.R, color.G, color.B, color.A);
        }
        public static Color ToEto(this SKColor color)
        {
            return Color.FromArgb(color.Red, color.Green, color.Blue, color.Alpha);
        }
        public static SKColor ToSkia(this ColorRGBA color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }
        public static SKColor ToSkia(this Color color)
        {
            return new SKColor((byte)color.Rb, (byte)color.Gb, (byte)color.Bb, (byte)color.Ab);
        }
    }
}