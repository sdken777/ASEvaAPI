using System;
using Eto.Forms;
using ASEva.Samples;
using ASEva.UIEto;
using SkiaSharp;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawSkia2D(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();
            var skiaView = layout.AddControl(new SkiaView(), true, 200, 0) as SkiaView;

            skiaView.Render += (o, args) =>
            {
                var c = args.Canvas;
                var textSize = new SKRect();
                var blackPaint = new SKPaint{ Color = SKColors.Black, IsAntialias = true };
                var defaultFontPaint = new SKPaint(c.GetDefaultFont()) { Color = SKColors.Black, IsAntialias = true };
                var centerFontPaint = new SKPaint(c.GetDefaultFont()) { Color = SKColors.Black, TextAlign = SKTextAlign.Center, IsAntialias = true };
                var wideLinePaint = new SKPaint{ Color = SKColors.Red, StrokeWidth = 20, StrokeCap = SKStrokeCap.Square, IsAntialias = true };
                var piePaint = new SKPaint{ Color = new SKColor(0, 128, 0, 128), IsAntialias = true };
                var textBoundPaint = new SKPaint{ Color = SKColors.Green, Style = SKPaintStyle.Stroke, IsAntialias = true };

                c.Clear(SKColors.LightYellow);
                c.DrawLine(10, 100, 190, 100, blackPaint);
                c.DrawLine(10, 120, 190, 120, blackPaint);
                c.DrawLine(100, 10, 100, 190, blackPaint);
                c.DrawLine(20, 110, 90, 110, wideLinePaint);
                defaultFontPaint.MeasureText(t["draw-text"], ref textSize);
                c.DrawText(t["draw-text"], 100, 100 + textSize.Height, defaultFontPaint);
                c.DrawRect(100, 100, textSize.Width, textSize.Height, textBoundPaint);
                c.DrawImage(CommonImage.LoadResource("camera.png").ToSKImage(), 80, 80);
                c.DrawArc(new SKRect(10, 10, 190, 190), -90, 270, true, piePaint);
                
                c.DrawLine(10, 210, 190, 215, blackPaint);
                c.DrawLine(10, 235, 190, 240, blackPaint);
                c.DrawText(t["draw-skia-anti-alias"], 100, 225 + textSize.Height / 2, centerFontPaint);
            };

            Closing += delegate
            {
                skiaView.Close();
            };
        }
    }
}