using System;
using System.Collections.Generic;
using SkiaSharp;
using Avalonia.Media;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.8) Extensions for Skia canvas
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.8) 方便操作Skia画布的扩展
    /// </summary>
    public static class SkiaCanvasExtensions
    {
        /// \~English
        /// <summary>
        /// Get default font
        /// </summary>
        /// <param name="canvas">Skia canvas object</param>
        /// <param name="sizeScale">Ratio to the default size</param>
        /// \~Chinese
        /// <summary>
        /// 获取默认字体
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="sizeScale">相对于默认尺寸的比例</param>
        public static SKFont GetDefaultFont(this SKCanvas canvas, float sizeScale = 1.0f)
        {
#if ASEVA_API_BUNDLE_MODE
            if (sizeScale <= 0) sizeScale = 1;
            return FontLibrarySkia.GetSKFont(null, sizeScale, SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
#else
            return ASEva.UIEto.SkiaCanvasExtensions.GetDefaultFont(canvas, sizeScale);
#endif
        }

        /// \~English
        /// <summary>
        /// Get the specified font object
        /// </summary>
        /// <param name="canvas">Skia canvas object</param>
        /// <param name="fontName">Font name</param>
        /// <param name="sizeScale">Ratio to the default size</param>
        /// <param name="weight">Font weight</param>
        /// <param name="width">Font width</param>
        /// <param name="slant">Font slant</param>
        /// <returns>Font object</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定字体
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="sizeScale">相对于默认尺寸的比例</param>
        /// <param name="weight">字重</param>
        /// <param name="width">宽度</param>
        /// <param name="slant">倾斜度</param>
        /// <returns>字体对象</returns>
        public static SKFont GetFont(this SKCanvas canvas, String fontName, float sizeScale = 1.0f, SKFontStyleWeight weight = SKFontStyleWeight.Normal, SKFontStyleWidth width = SKFontStyleWidth.Normal, SKFontStyleSlant slant = SKFontStyleSlant.Upright)
        {
#if ASEVA_API_BUNDLE_MODE
            if (sizeScale <= 0) sizeScale = 1;
            return FontLibrarySkia.GetSKFont(fontName, sizeScale, weight, width, slant);
#else
            return ASEva.UIEto.SkiaCanvasExtensions.GetFont(canvas, fontName, sizeScale, weight, width, slant);
#endif
        }

        /// \~English
        /// <summary>
        /// Draw text with the specified anchor
        /// </summary>
        /// <param name="canvas">Skia canvas object</param>
        /// <param name="text">Text</param>
        /// <param name="font">Font</param>
        /// <param name="color">Text color</param>
        /// <param name="anchorX">Anchor point's type along X-coordinate</param>
        /// <param name="anchorY">Anchor point's type along Y-coordinate</param>
        /// <param name="posX">X-coordinate of anchor point</param>
        /// <param name="posY">Y-coordinate of anchor point</param>
        /// \~Chinese
        /// <summary>
        /// 指定锚点绘制文本
        /// </summary>
        /// <param name="canvas">Skia画布</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="color">文字颜色</param>
        /// <param name="anchorX">X轴上的文本锚点坐标位置类型</param>
        /// <param name="anchorY">Y轴上的文本锚点坐标位置类型</param>
        /// <param name="posX">文本锚点X轴逻辑坐标</param>
        /// <param name="posY">文本锚点Y轴逻辑坐标</param>
        public static void DrawString(this SKCanvas canvas, String text, SKFont font, SKColor color, AlignmentX anchorX, AlignmentY anchorY, int posX, int posY)
        {
            SKTextAlign align = SKTextAlign.Left;
            switch (anchorX)
            {
                case AlignmentX.Left:
                    align = SKTextAlign.Left;
                    break;
                case AlignmentX.Center:
                    align = SKTextAlign.Center;
                    break;
                case AlignmentX.Right:
                    align = SKTextAlign.Right;
                    break;
            }

            var fontPaint = new SKPaint(font){ Color = color, TextAlign = align, IsAntialias = true };
            var testTextRect = new SKRect();
            fontPaint.MeasureText(testText, ref testTextRect);

            int yOffset = 0;
            switch (anchorY)
            {
                case AlignmentY.Top:
                    yOffset = (int)testTextRect.Top;
                    break;
                case AlignmentY.Center:
                    yOffset = (int)((testTextRect.Top + testTextRect.Bottom) * 0.5);
                    break;
                case AlignmentY.Bottom:
                    yOffset = (int)testTextRect.Bottom;
                    break;
            }

            canvas.DrawText(text, new SKPoint(posX, posY - yOffset), fontPaint);
        }

        /// \~English
        /// <summary>
        /// Measure text's size
        /// </summary>
        /// <param name="canvas">Skia canvas object</param>
        /// <param name="text">Text</param>
        /// <param name="font">Font</param>
        /// <returns>Text's logical size</returns>
        /// \~Chinese
        /// <summary>
        /// 测量文本的尺寸
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

        private static String testText = "0O口";

#if ASEVA_API_BUNDLE_MODE
        private class FontLibrarySkia
        {
            public static SKFont GetSKFont()
            {
                if (skDefault == null) skDefault = new SKFont();
                return skDefault;
            }

            public static SKFont GetSKFont(String fontName, float scale, SKFontStyleWeight weight, SKFontStyleWidth width, SKFontStyleSlant slant)
            {
                if (fontName == null) fontName = GetSKFont().Typeface.FamilyName;
                var size = GetSKFont().Size * scale;
                var key = fontName + ":" + scale + ":" + weight.ToString() + ":" + width.ToString() + ":" + slant.ToString();
                if (!skLibrary.ContainsKey(key))
                {
                    try
                    {
                        var newFont = new SKFont(SKTypeface.FromFamilyName(fontName, weight, width, slant), size);
                        skLibrary[key] = newFont;
                    }
                    catch (Exception)
                    {
                        skLibrary[key] = null;
                    }
                }
                return skLibrary[key];
            }

            private static SKFont skDefault = null;
            private static Dictionary<String, SKFont> skLibrary = new Dictionary<String, SKFont>();
        }
#endif
    }
}