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

            fastMode = Pixel.RealConsistency;
            bitmapScale = fastMode ? 1 : BitmapScale;

            var font = g.ScaledDefaultFont(sizeRatio * bitmapScale);
            var logicalSize = g.MeasureString(font, text);

            var bitmapWidth = (int)Math.Ceiling(logicalSize.Width);
            bitmapWidth += bitmapWidth % 2 == 0 ? 0 : 1;
            var bitmapHeight = (int)Math.Ceiling(logicalSize.Height);
            bitmapHeight += bitmapHeight % 2 == 0 ? 0 : 1;

            bitmap = new Bitmap(bitmapWidth, bitmapHeight, PixelFormat.Format32bppRgba);
            using (var bitmapGraphics = new Graphics(bitmap))
            {
                if (ModifyInterface != null) ModifyInterface.Modify(bitmapGraphics);
                bitmapGraphics.DrawString(text, font, color, TextAnchor.Center, bitmapWidth / 2, bitmapHeight / 2);
            }

            this.text = text;
            this.sizeRatio = sizeRatio;
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
                    dy -= (float)bitmap.Height / bitmapScale / 2;
                    break;
                case TextAnchor.BottomLeft:
                    dy -= (float)bitmap.Height / bitmapScale;
                    break;
                case TextAnchor.TopCenter:
                    dx -= (float)bitmap.Width / bitmapScale / 2;
                    break;
                case TextAnchor.Center:
                    dx -= (float)bitmap.Width / bitmapScale / 2;
                    dy -= (float)bitmap.Height / bitmapScale / 2;
                    break;
                case TextAnchor.BottomCenter:
                    dx -= (float)bitmap.Width / bitmapScale / 2;
                    dy -= (float)bitmap.Height / bitmapScale;
                    break;
                case TextAnchor.TopRight:
                    dx -= (float)bitmap.Width / bitmapScale;
                    break;
                case TextAnchor.RightCenter:
                    dx -= (float)bitmap.Width / bitmapScale;
                    dy -= (float)bitmap.Height / bitmapScale / 2;
                    break;
                case TextAnchor.BottomRight:
                    dx -= (float)bitmap.Width / bitmapScale;
                    dy -= (float)bitmap.Height / bitmapScale;
                    break;
            }

            var originInterpolationMode = g.ImageInterpolation;

            if (fastMode)
            {
                g.ImageInterpolation = ImageInterpolation.None;
                g.DrawImage(bitmap, (float)Math.Round(dx), (float)Math.Round(dy));
            }
            else
            {
                g.SaveTransform();
                g.TranslateTransform(dx, dy);
                g.ScaleTransform(1.0f / bitmapScale);

                if (ImageInterpolationMode != null) g.ImageInterpolation = ImageInterpolationMode.Value;
                else g.ImageInterpolation = ImageInterpolation.Default;
                g.DrawImage(bitmap, 0, 0);

                g.RestoreTransform();
            }

            g.ImageInterpolation = originInterpolationMode;
        }

        /// <summary>
        /// 获取当前文本
        /// </summary>
        public String Text { get { return text; }}

        /// <summary>
        /// (api:eto=2.9.8) 获取当前字体相对于默认大小比例
        /// </summary>
        public float SizeRatio { get { return sizeRatio; }}

        /// <summary>
        /// 获取当前绘制颜色
        /// </summary>
        public Color Color { get { return color; }}

        private String text;
        private float sizeRatio;
        private Color color;
        private Bitmap bitmap;
        private int bitmapScale;
        private bool fastMode;

        private const int BitmapScale = 3;

        public interface ModifyBitmapGraphics
        {
            void Modify(Graphics graphics);
        }

        public static ModifyBitmapGraphics ModifyInterface { private get; set; }

        public static ImageInterpolation? ImageInterpolationMode { private get; set; }
    }
}