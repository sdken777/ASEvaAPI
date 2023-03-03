using System;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.9.6) 文本位图，可替代Graphics.DrawString以提高效率
    /// </summary>
    public class TextBitmap
    {
        /// <summary>
        /// 创建文本位图（仅支持默认字体）
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="text">文本</param>
        /// <param name="sizeRatio">相对字体默认大小的比例，默认为1</param>
        /// <param name="color">绘制颜色</param>
        public TextBitmap(Graphics g, String text, float sizeRatio, Color color)
        {
            if (String.IsNullOrEmpty(text)) text = " ";
            sizeRatio = Math.Max(0.1f, sizeRatio);

            var font = g.ScaledDefaultFont(sizeRatio * LargeScale);
            var largeLogicalSize = g.MeasureString(font, text);

            var largeBitmapWidth = (int)Math.Ceiling(largeLogicalSize.Width);
            largeBitmapWidth += largeBitmapWidth % 2 == 0 ? 2 : 1;
            var largeBitmapHeight = (int)Math.Ceiling(largeLogicalSize.Height);
            largeBitmapHeight += largeBitmapHeight % 2 == 0 ? 2 : 1;

            var largeBitmap = new Bitmap(largeBitmapWidth, largeBitmapHeight, PixelFormat.Format32bppRgba);
            using (var bitmapGraphics = new Graphics(largeBitmap))
            {
                bitmapGraphics.DrawString(text, font, color, TextAnchor.Center, largeBitmapWidth / 2, largeBitmapHeight / 2);
            }

            var largeCommonImage = ImageConverter.ConvertFromBitmap(largeBitmap);

            var bitmapWidth = largeBitmapWidth / 2;
            var bitmapHeight = largeBitmapHeight / 2;

            var commonImage = CommonImage.Create(bitmapWidth, bitmapHeight, true);
            unsafe
            {
                fixed (byte* srcData = &largeCommonImage.Data[0], dstData = &commonImage.Data[0])
                {
                    for (int v = 0; v < bitmapHeight; v++)
                    {
                        byte* srcRow1 = &srcData[2 * v * bitmapWidth * 8];
                        byte* srcRow2 = &srcData[(2 * v + 1) * bitmapWidth * 8];
                        byte* dstRow = &dstData[v * bitmapWidth * 4];
                        for (int u = 0; u < bitmapWidth; u++)
                        {
                            dstRow[4 * u] = (byte)(((int)srcRow1[8 * u] + (int)srcRow1[8 * u + 4] + (int)srcRow2[8 * u] + (int)srcRow2[8 * u + 4]) >> 2);
                            dstRow[4 * u + 1] = (byte)(((int)srcRow1[8 * u + 1] + (int)srcRow1[8 * u + 5] + (int)srcRow2[8 * u + 1] + (int)srcRow2[8 * u + 5]) >> 2);
                            dstRow[4 * u + 2] = (byte)(((int)srcRow1[8 * u + 2] + (int)srcRow1[8 * u + 6] + (int)srcRow2[8 * u + 2] + (int)srcRow2[8 * u + 6]) >> 2);
                            dstRow[4 * u + 3] = (byte)(((int)srcRow1[8 * u + 3] + (int)srcRow1[8 * u + 7] + (int)srcRow2[8 * u + 3] + (int)srcRow2[8 * u + 7]) >> 2);
                        }
                    }
                }
            }

            bitmap = ImageConverter.ConvertToBitmap(commonImage) as Bitmap;

            this.text = text;
            this.font = font;
            this.color = color;
        }

        /// <summary>
        /// 通过图形对象绘制文本位图
        /// </summary>
        /// <param name="g">图形对象</param>
        /// <param name="anchor">文本锚点坐标位置类型</param>
        /// <param name="logicalX">文本锚点X轴逻辑坐标</param>
        /// <param name="logicalY">文本锚点Y轴逻辑坐标</param>
        public void Draw(Graphics g, TextAnchor anchor, int logicalX, int logicalY)
        {
            var dx = (float)logicalX;
            var dy = (float)logicalY;
                
            switch (anchor)
            {
                case TextAnchor.TopLeft:
                    break;
                case TextAnchor.LeftCenter:
                    dy -= (float)bitmap.Height / BitmapScale / 2;
                    break;
                case TextAnchor.BottomLeft:
                    dy -= (float)bitmap.Height / BitmapScale;
                    break;
                case TextAnchor.TopCenter:
                    dx -= (float)bitmap.Width / BitmapScale / 2;
                    break;
                case TextAnchor.Center:
                    dx -= (float)bitmap.Width / BitmapScale / 2;
                    dy -= (float)bitmap.Height / BitmapScale / 2;
                    break;
                case TextAnchor.BottomCenter:
                    dx -= (float)bitmap.Width / BitmapScale / 2;
                    dy -= (float)bitmap.Height / BitmapScale;
                    break;
                case TextAnchor.TopRight:
                    dx -= (float)bitmap.Width / BitmapScale;
                    break;
                case TextAnchor.RightCenter:
                    dx -= (float)bitmap.Width / BitmapScale;
                    dy -= (float)bitmap.Height / BitmapScale / 2;
                    break;
                case TextAnchor.BottomRight:
                    dx -= (float)bitmap.Width / BitmapScale;
                    dy -= (float)bitmap.Height / BitmapScale;
                    break;
            }

            g.SaveTransform();

            g.TranslateTransform(dx, dy);
            g.ScaleTransform(1.0f / BitmapScale);

            g.ImageInterpolation = ImageInterpolation.Medium;
            g.DrawImage(bitmap, 0, 0);

            g.RestoreTransform();
        }

        /// <summary>
        /// 获取当前文本
        /// </summary>
        public String Text { get { return text; }}

        /// <summary>
        /// 获取当前字体
        /// </summary>
        public Font Font { get { return font; }}

        /// <summary>
        /// 获取当前绘制颜色
        /// </summary>
        public Color Color { get { return color; }}

        private String text;
        private Font font;
        private Color color;
        private Bitmap bitmap;

        private const int BitmapScale = 3;
        private const int LargeScale = BitmapScale * 2;
    }
}