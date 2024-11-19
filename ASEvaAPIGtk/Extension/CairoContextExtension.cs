using System;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Extension methods for Cairo painting
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) Cairo绘图工具扩展方法
    /// </summary>
    public static class CairoContextExtension
    {
        public static void SetSourceColor(this Cairo.Context cc, ColorRGBA color)
        {
            cc.SetSourceColor(ColorConv.ConvCairo(color));
        }

        public static void DrawLine(this Cairo.Context cc, double x1, double y1, double x2, double y2)
        {
            cc.MoveTo(x1, y1);
            cc.LineTo(x2, y2);
            cc.Stroke();
        }

        public static void DrawLine(this Cairo.Context cc, FloatPoint p1, FloatPoint p2)
        {
            cc.MoveTo(p1.X, p1.Y);
            cc.LineTo(p2.X, p2.Y);
            cc.Stroke();
        }

        public static void DrawLines(this Cairo.Context cc, FloatPoint[] points)
        {
            if (points.Length < 2) return;

            cc.MoveTo(points[0].X, points[0].Y);
            for (int i = 1; i < points.Length; i++) cc.LineTo(points[i].X, points[i].Y);
            cc.Stroke();
        }

        public static void DrawPolygon(this Cairo.Context cc, FloatPoint[] points)
        {
            if (points.Length < 2) return;

            cc.MoveTo(points[0].X, points[0].Y);
            for (int i = 1; i < points.Length; i++) cc.LineTo(points[i].X, points[i].Y);
            cc.LineTo(points[0].X, points[0].Y);
            cc.Stroke();
        }

        public static void FillPolygon(this Cairo.Context cc, FloatPoint[] points)
        {
            if (points.Length < 2) return;

            cc.MoveTo(points[0].X, points[0].Y);
            for (int i = 1; i < points.Length; i++) cc.LineTo(points[i].X, points[i].Y);
            cc.LineTo(points[0].X, points[0].Y);
            cc.Fill();
        }

        public static void DrawCircle(this Cairo.Context cc, double centerX, double centerY, double radius)
        {
            cc.MoveTo(centerX + radius, centerY);
            cc.Arc(centerX, centerY, radius, 0, 2 * Math.PI);
            cc.Stroke();
        }

        public static void DrawCircle(this Cairo.Context cc, FloatPoint center, double radius)
        {
            cc.MoveTo(center.X + radius, center.Y);
            cc.Arc(center.X, center.Y, radius, 0, 2 * Math.PI);
            cc.Stroke();
        }

        public static void FillCircle(this Cairo.Context cc, double centerX, double centerY, double radius)
        {
            cc.MoveTo(centerX + radius, centerY);
            cc.Arc(centerX, centerY, radius, 0, 2 * Math.PI);
            cc.Fill();
        }

        public static void FillCircle(this Cairo.Context cc, FloatPoint center, double radius)
        {
            cc.MoveTo(center.X + radius, center.Y);
            cc.Arc(center.X, center.Y, radius, 0, 2 * Math.PI);
            cc.Fill();
        }

        public static void DrawRectangle(this Cairo.Context cc, double rectX, double rectY, double rectWidth, double rectHeight)
        {
            cc.Rectangle(rectX, rectY, rectWidth, rectHeight);
            cc.Stroke();
        }

        public static void DrawRectangle(this Cairo.Context cc, FloatRect rect)
        {
            cc.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            cc.Stroke();
        }

        public static void FillRectangle(this Cairo.Context cc, double rectX, double rectY, double rectWidth, double rectHeight)
        {
            cc.Rectangle(rectX, rectY, rectWidth, rectHeight);
            cc.Fill();
        }

        public static void FillRectangle(this Cairo.Context cc, FloatRect rect)
        {
            cc.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            cc.Fill();
        }

        public static String NotoFontName { get { return "Noto Sans CJK SC"; } }
    }
}