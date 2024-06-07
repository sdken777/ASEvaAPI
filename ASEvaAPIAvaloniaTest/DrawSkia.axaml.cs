using System;
using System.Collections.Generic;
using System.IO;
using ASEva.UIAvalonia;
using ASEva.Utility;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawSkia : Panel
    {
        public DrawSkia()
        {
            InitializeComponent();

            using (var stream = new MemoryStream(ResourceLoader.Load("camera.png")))
            {
                image = new Bitmap(stream).ToCommonImage().ToSKImage();
            }
        }

        public void OnLoop()
        {
            skiaView.QueueRender();
        }

        private void skiaView_Render(object sender, SkiaRenderEventArgs e)
        {
            var c = e.Canvas;
            var blackPaint = new SKPaint{ Color = SKColors.Black, IsAntialias = true };
            var wideLinePaint = new SKPaint{ Color = SKColors.Red, StrokeWidth = 20, StrokeCap = SKStrokeCap.Square, IsAntialias = true };
            var piePaint = new SKPaint{ Color = new SKColor(0, 128, 0, 128), IsAntialias = true };
            var textBoundPaint = new SKPaint{ Color = SKColors.Green, Style = SKPaintStyle.Stroke, IsAntialias = true };
            var pointPaint = new SKPaint{ Color = SKColors.DarkBlue, StrokeWidth = 2, IsAntialias = true };

            c.DrawLine(10, 100, 190, 100, blackPaint);
            c.DrawLine(10, 120, 190, 120, blackPaint);
            c.DrawLine(100, 10, 100, 190, blackPaint);
            c.DrawLine(110, 110, 180, 110, wideLinePaint);
            c.DrawString(Program.Texts["draw-text"], c.GetDefaultFont(), SKColors.Black, AlignmentX.Right, AlignmentY.Bottom, 100, 120);
            var textSize = c.MeasureString(Program.Texts["draw-text"], c.GetDefaultFont());
            c.DrawRect(100 - textSize.Width, 120 - textSize.Height, textSize.Width, textSize.Height, textBoundPaint);
            c.DrawImage(image, 80, 80);

            var pieAngle = (DateTime.Now - startTime).TotalMilliseconds * 0.1;
            pieAngle -= Math.Floor(pieAngle / 360) * 360;
            c.DrawArc(new SKRect(10, 10, 190, 190), -90, (float)pieAngle, true, piePaint);
            
            c.DrawLine(10, 210, 190, 215, blackPaint);
            c.DrawLine(10, 235, 190, 240, blackPaint);
            c.DrawString(Program.Texts["draw-skia-anti-alias"], c.GetDefaultFont(), SKColors.Black, AlignmentX.Center, AlignmentY.Center, 100, 225);

            foreach (var pt in drawPoints)
            {
                c.DrawPoint(pt, pointPaint);
            }
        }

        private void skiaView_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            drawPoints.Add(e.GetPosition(skiaView).ToCommon().ToSkia());
        }

        private SKImage image;
        private DateTime startTime = DateTime.Now;
        private List<SKPoint> drawPoints = new List<SKPoint>();
    }
}