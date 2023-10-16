using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=2.0.4) Extensions for graphics object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.0.4) 方便操作图形对象的扩展
    /// </summary>
    public static class GraphicsExtensions
    {
        /// \~English
        /// <summary>
        /// Set the scale of graphics the same as logical units
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// \~Chinese
        /// <summary>
        /// 设置绘图尺度，令所有相关绘制函数的输入尺寸都为logical尺寸
        /// </summary>
        /// <param name="g">图形对象</param>
        public static void SetScaleForLogical(this Graphics g)
        {
            g.ScaleTransform(Pixel.Scale);
        }

        /// \~English
        /// <summary>
        /// After calling SetScaleForLogical, use returned Font object by this function to draw texts
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="f">Original font</param>
        /// <returns>Scaled font object</returns>
        /// \~Chinese
        /// <summary>
        /// 调用SetScaleForLogical后，绘制文字需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="f">原字体</param>
        /// <returns>可用于绘制的字体</returns>
        public static Font ScaledFont(this Graphics g, Font f)
        {
            if (Pixel.Scale == 1) return f;
            else return FontLibrary.GetFont(f.Family, f.Size / Pixel.Scale, f.FontStyle, f.FontDecoration);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.6) After calling SetScaleForLogical, if you want to use default font, use returned Font object by this function to draw texts
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="sizeRatio">Ratio to the default scale, default is 1</param>
        /// <returns>Scaled font object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.6) 调用SetScaleForLogical后，以默认字体绘制需要使用此函数返回的字体
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="sizeRatio">相对字体默认大小的比例，默认为1</param>
        /// <returns>可用于绘制的默认字体</returns>
        public static Font ScaledDefaultFont(this Graphics g, float sizeRatio = 1)
        {
            var f = App.DefaultFont(sizeRatio);
            if (Pixel.Scale == 1) return f;
            else return FontLibrary.GetFont(f, f.Size / Pixel.Scale);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.12) Draw text with the specified anchor point
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="text">Text</param>
        /// <param name="font">Font</param>
        /// <param name="color">Text color</param>
        /// <param name="anchor">Anchor type</param>
        /// <param name="logicalX">X-coordinate of anchor point</param>
        /// <param name="logicalY">Y-coordinate of anchor point</param>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.12) 指定锚点绘制文本
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="color">文字颜色</param>
        /// <param name="anchor">文本锚点坐标位置类型</param>
        /// <param name="logicalX">文本锚点X轴逻辑坐标</param>
        /// <param name="logicalY">文本锚点Y轴逻辑坐标</param>
        public static void DrawString(this Graphics g, String text, Font font, Color color, TextAnchor anchor, int logicalX, int logicalY)
        {
            var textSize = g.MeasureString(font, text);

            int xOffset = 0;
            switch (anchor)
            {
                case TextAnchor.TopLeft:
                case TextAnchor.LeftCenter:
                case TextAnchor.BottomLeft:
                    xOffset = 0;
                    break;
                case TextAnchor.TopCenter:
                case TextAnchor.Center:
                case TextAnchor.BottomCenter:
                    xOffset = (int)(-textSize.Width / 2) - 1;
                    break;
                case TextAnchor.TopRight:
                case TextAnchor.RightCenter:
                case TextAnchor.BottomRight:
                    xOffset = (int)-textSize.Width - 1;
                    break;
            }

            int yOffset = 0;
            switch (anchor)
            {
                case TextAnchor.TopLeft:
                case TextAnchor.TopCenter:
                case TextAnchor.TopRight:
                    yOffset = 0;
                    break;
                case TextAnchor.LeftCenter:
                case TextAnchor.Center:
                case TextAnchor.RightCenter:
                    yOffset = (int)(-textSize.Height / 2) - 1;
                    break;
                case TextAnchor.BottomLeft:
                case TextAnchor.BottomCenter:
                case TextAnchor.BottomRight:
                    yOffset = (int)-textSize.Height - 1;
                    break;
            }

            g.DrawText(font, color, logicalX + xOffset, logicalY + yOffset, text);
        }
    }
}