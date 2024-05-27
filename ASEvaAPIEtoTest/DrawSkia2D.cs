using System;
using System.Collections.Generic;
using Eto.Forms;
using ASEva;
using ASEva.UIEto;
using SkiaSharp;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        private void initDrawSkia2D(TabPage tabPage, bool disableGPU, bool onscreenRendering = false)
        {
            var layout = tabPage.SetContentAsColumnLayout();
            var skiaView = layout.AddControl(new SkiaView(null, disableGPU, onscreenRendering), true, 200, 0) as SkiaView;
            if (!disableGPU)
            {
                layout.AddLinkButton(t["draw-gl-detail"]).Click += delegate
                {
                    if (skiaView.ContextInfo != null)
                    {
                        var info = skiaView.ContextInfo.Value;
                        var rowTexts = new List<String>();
                        rowTexts.Add(t.Format("draw-gl-info-version", info.version));
                        rowTexts.Add(t.Format("draw-gl-info-vendor", info.vendor));
                        rowTexts.Add(t.Format("draw-gl-info-renderer", info.renderer));
                        rowTexts.Add(t.Format("draw-gl-info-extensions", String.Join('\n', info.ToExtensionList())));
                        App.RunDialog(new InfoDialog(t["draw-gl-info-title"], String.Join('\n', rowTexts)));
                    }
                };
            }

            skiaView.MouseDown += (o, e) =>
            {
                skiaDrawPoints.Add(e.GetLogicalPoint().ToCommon());
            };

            var image = ImageConverter.ConvertFromBitmap(Eto.Drawing.Bitmap.FromResource("camera.png")).ToSKImage();
            skiaView.Render += (o, args) =>
            {
                var c = args.Canvas;
                var blackPaint = new SKPaint{ Color = SKColors.Black, IsAntialias = true };
                var wideLinePaint = new SKPaint{ Color = SKColors.Red, StrokeWidth = 20, StrokeCap = SKStrokeCap.Square, IsAntialias = true };
                var piePaint = new SKPaint{ Color = new SKColor(0, 128, 0, 128), IsAntialias = true };
                var textBoundPaint = new SKPaint{ Color = SKColors.Green, Style = SKPaintStyle.Stroke, IsAntialias = true };
                var pointPaint = new SKPaint{ Color = SKColors.DarkBlue, StrokeWidth = 2, IsAntialias = true };

                c.Clear(SKColors.LightYellow);
                c.DrawLine(10, 100, 190, 100, blackPaint);
                c.DrawLine(10, 120, 190, 120, blackPaint);
                c.DrawLine(100, 10, 100, 190, blackPaint);
                c.DrawLine(110, 110, 180, 110, wideLinePaint);
                c.DrawString(t["draw-text"], c.GetDefaultFont(), SKColors.Black, TextAnchor.BottomRight, 100, 120);
                var textSize = c.MeasureString(t["draw-text"], c.GetDefaultFont());
                c.DrawRect(100 - textSize.Width, 120 - textSize.Height, textSize.Width, textSize.Height, textBoundPaint);
                c.DrawImage(image, 80, 80);

                var pieAngle = (DateTime.Now - startTime).TotalMilliseconds * 0.1;
                pieAngle -= Math.Floor(pieAngle / 360) * 360;
                c.DrawArc(new SKRect(10, 10, 190, 190), -90, (float)pieAngle, true, piePaint);
                
                c.DrawLine(10, 210, 190, 215, blackPaint);
                c.DrawLine(10, 235, 190, 240, blackPaint);
                c.DrawString(t["draw-skia-anti-alias"], c.GetDefaultFont(), SKColors.Black, TextAnchor.Center, 100, 225);

                foreach (var pt in skiaDrawPoints)
                {
                    c.DrawPoint(pt.X, pt.Y, pointPaint);
                }
            };
            skiaViews.Add(skiaView);
        }

        private void loopDrawSkia2D()
        {
            foreach (var skiaView in skiaViews) skiaView.QueueRender();
        }

        private List<SkiaView> skiaViews = new List<SkiaView>();
        private List<FloatPoint> skiaDrawPoints = new List<FloatPoint>();
    }
}