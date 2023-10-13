using System;
using SkiaSharp;

namespace ASEva.UIEto
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.7.0) 方便操作Skia画布的扩展
    /// </summary>
    public static class SkiaCanvasExtensions
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 获取默认字体
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="sizeScale">相对于默认尺寸的比例</param>
        public static SKFont GetDefaultFont(this SKCanvas canvas, float sizeScale = 1.0f)
        {
            if (sizeScale <= 0) sizeScale = 1;
            if (String.IsNullOrEmpty(DefaultFontName)) return FontLibrary.GetSKFont();
            else
            {
                if (DefaultFontSize == 0) return FontLibrary.GetSKFont(DefaultFontName);
                else return FontLibrary.GetSKFont(DefaultFontName, DefaultFontSize * sizeScale, SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.16) 获取指定字体
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="sizeScale">相对于默认尺寸的比例</param>
        /// <param name="weight">字重</param>
        /// <param name="width">宽度</param>
        /// <param name="slant">倾斜度</param>
        /// <returns></returns>
        public static SKFont GetFont(this SKCanvas canvas, String fontName, float sizeScale = 1.0f, SKFontStyleWeight weight = SKFontStyleWeight.Normal, SKFontStyleWidth width = SKFontStyleWidth.Normal, SKFontStyleSlant slant = SKFontStyleSlant.Upright)
        {
            if (sizeScale <= 0) sizeScale = 1;
            if (String.IsNullOrEmpty(fontName)) return FontLibrary.GetSKFont();
            else
            {
                if (DefaultFontSize == 0) return FontLibrary.GetSKFont(fontName);
                else return FontLibrary.GetSKFont(fontName, DefaultFontSize * sizeScale, weight, width, slant);
            }
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.7.1) 指定锚点绘制文本
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="color">文字颜色</param>
        /// <param name="anchor">文本锚点坐标位置类型</param>
        /// <param name="logicalX">文本锚点X轴逻辑坐标</param>
        /// <param name="logicalY">文本锚点Y轴逻辑坐标</param>
        public static void DrawString(this SKCanvas canvas, String text, SKFont font, SKColor color, TextAnchor anchor, int logicalX, int logicalY)
        {
            SKTextAlign align = SKTextAlign.Left;
            switch (anchor)
            {
                case TextAnchor.TopLeft:
                case TextAnchor.LeftCenter:
                case TextAnchor.BottomLeft:
                    align = SKTextAlign.Left;
                    break;
                case TextAnchor.TopCenter:
                case TextAnchor.Center:
                case TextAnchor.BottomCenter:
                    align = SKTextAlign.Center;
                    break;
                case TextAnchor.TopRight:
                case TextAnchor.RightCenter:
                case TextAnchor.BottomRight:
                    align = SKTextAlign.Right;
                    break;
            }

            var fontPaint = new SKPaint(font){ Color = color, TextAlign = align, IsAntialias = true };
            var testTextRect = new SKRect();
            fontPaint.MeasureText(testText, ref testTextRect);

            int yOffset = 0;
            switch (anchor)
            {
                case TextAnchor.TopLeft:
                case TextAnchor.TopCenter:
                case TextAnchor.TopRight:
                    yOffset = (int)testTextRect.Top;
                    break;
                case TextAnchor.LeftCenter:
                case TextAnchor.Center:
                case TextAnchor.RightCenter:
                    yOffset = (int)((testTextRect.Top + testTextRect.Bottom) * 0.5);
                    break;
                case TextAnchor.BottomLeft:
                case TextAnchor.BottomCenter:
                case TextAnchor.BottomRight:
                    yOffset = (int)testTextRect.Bottom;
                    break;
            }

            canvas.DrawText(text, new SKPoint(logicalX, logicalY - yOffset), fontPaint);
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.7.1) 测量文本的尺寸
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <returns>文本的逻辑尺寸</returns>
        public static SKSize MeasureString(this SKCanvas canvas, String text, SKFont font)
        {
            var fontPaint = new SKPaint(font);
            var textRect = new SKRect();
            var testTextRect = new SKRect();
            fontPaint.MeasureText(text, ref textRect);
            fontPaint.MeasureText(testText, ref testTextRect);
            return new SKSize(textRect.Width, testTextRect.Height);
        }

		public static String DefaultFontName { private get; set; }
		public static float DefaultFontSize { private get; set; }

        private static String testText = "0O口";
    }
}