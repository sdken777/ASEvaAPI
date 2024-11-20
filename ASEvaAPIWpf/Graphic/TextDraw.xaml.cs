using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ASEva.UIEto;

namespace ASEva.UIWpf
{
    partial class TextDraw : UserControl
    {
        public TextDraw()
        {
            InitializeComponent();
            RealPixelScale = 0;
        }

        public GLTextTask[] Texts { private get; set; } = [];
        public float RealPixelScale { get; private set; }

        public void QueueRender()
        {
            if (lastRenderWithTexts || Texts.Length > 0) InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            RealPixelScale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;

            if (Texts.Length == 0)
            {
                lastRenderWithTexts = false;
                return;
            }

            lastRenderWithTexts = true;

            var clipGeometry = new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight));
            drawingContext.PushClip(clipGeometry);

            foreach (var task in Texts)
            {
                if (String.IsNullOrEmpty(task.text)) continue;

                var fontName = String.IsNullOrEmpty(task.fontName) ? "Microsoft Yahei" : task.fontName;
                var fontSize = (task.sizeScale <= 0 ? 1.0f : task.sizeScale) * 11.0f;

                var brush = new SolidColorBrush(Color.FromArgb(task.alpha == 0 ? (byte)255 : task.alpha, task.red, task.green, task.blue));
                var text = new FormattedText(task.text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontName), fontSize, brush, 96);
                var textSize = new Size((int)text.Width, (int)text.Height);

                int posX = task.posX;
                int posY = task.posY;
                if (task.isRealPos)
                {
                    posX = (int)((float)posX / RealPixelScale);
                    posY = (int)((float)posY / RealPixelScale);
                }

                int fullWidth = (int)textSize.Width;
                int fullHeight = (int)textSize.Height;
                int halfWidth = (int)(textSize.Width / 2);
                int halfHeight = (int)(textSize.Height / 2);
                switch (task.anchor)
                {
                    case TextAnchor.TopLeft:
                        break;
                    case TextAnchor.LeftCenter:
                        posY -= halfHeight;
                        break;
                    case TextAnchor.BottomLeft:
                        posY -= fullHeight;
                        break;
                    case TextAnchor.TopCenter:
                        posX -= halfWidth;
                        break;
                    case TextAnchor.Center:
                        posX -= halfWidth;
                        posY -= halfHeight;
                        break;
                    case TextAnchor.BottomCenter:
                        posX -= halfWidth;
                        posY -= fullHeight;
                        break;
                    case TextAnchor.TopRight:
                        posX -= fullWidth;
                        break;
                    case TextAnchor.RightCenter:
                        posX -= fullWidth;
                        posY -= halfHeight;
                        break;
                    case TextAnchor.BottomRight:
                        posX -= fullWidth;
                        posY -= fullHeight;
                        break;
                    default:
                        break;
                }

                drawingContext.DrawText(text, new Point(posX, posY));
            }

            drawingContext.Pop();
        }

        private bool lastRenderWithTexts = false;
    }
}
