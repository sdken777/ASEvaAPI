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
                c.Clear(SKColors.LightYellow);
                c.DrawLine(10, 100, 190, 100, new SKPaint{ Color = SKColors.Black });
                c.DrawLine(10, 120, 190, 120, new SKPaint{ Color = SKColors.Black });
                c.DrawLine(100, 10, 100, 190, new SKPaint{ Color = SKColors.Black });
                c.DrawLine(20, 110, 90, 110, new SKPaint{ Color = SKColors.Red, StrokeWidth = 20, StrokeCap = SKStrokeCap.Square });
                c.DrawText(t["draw-text"], 100, 120, new SKPaint(c.GetDefaultFont()) { Color = SKColors.Black });
                c.DrawImage(CommonImage.LoadResource("camera.png").ToSKImage(), 80, 80);
                c.DrawArc(new SKRect(10, 10, 190, 190), -90, 270, true, new SKPaint{ Color = new SKColor(0, 128, 0, 128), StrokeWidth = 2 });
                c.DrawLine(10, 210, 190, 215, new SKPaint{ Color = SKColors.Black });
                c.DrawLine(10, 230, 190, 235, new SKPaint{ Color = SKColors.Black });
                c.DrawText(t["draw-skia-anti-alias"], 100, 230, new SKPaint(c.GetDefaultFont()) { Color = SKColors.Black, TextAlign = SKTextAlign.Center });
            };

            Closing += delegate
            {
                skiaView.Close();
            };
        }
    }
}