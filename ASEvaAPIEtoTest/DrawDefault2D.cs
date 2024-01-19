using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using ASEva;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawDefault2D(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();
            var overlay = layout.AddControl(new OverlayLayout(), true, 200, 0) as OverlayLayout;
            drawableDefault2D = overlay.AddControl(new Drawable(), 0, 0, 0, 0) as Drawable;
            var buttonA = overlay.AddControl(new Button { Text = "A" }, 10, null, 10, null) as Button;
            var buttonB = overlay.AddControl(new Button { Text = "B", Visible = false }, 10, null, null, 10) as Button;
            var buttonC = overlay.AddControl(new Button { Text = "C", Visible = false }, null, 10, 10, null) as Button;
            var buttonD = overlay.AddControl(new Button { Text = "D", Visible = false }, null, 10, null, 10) as Button;

            buttonA.SetLogicalWidth(30);
            buttonB.SetLogicalWidth(30);
            buttonC.SetLogicalWidth(30);
            buttonD.SetLogicalWidth(30);

            buttonA.Click += delegate
            {
                buttonB.Visible = buttonC.Visible = buttonD.Visible = true;
            };
            buttonC.Click += delegate
            {
                if (overlayLinkButton != null) return;
                overlayLinkButton = new LinkButton{ Text = "ABCD", ToolTip = "ABCD" };
                overlay.AddControl(overlayLinkButton, null, 10, null, null);
            };

            drawableDefault2D.MouseDown += (o, e) =>
            {
                drawPoints.Add(e.GetLogicalPoint().ToCommon());
            };

            var image = Bitmap.FromResource("camera.png");
            drawableDefault2D.Paint += (o, args) =>
            {
                var g = args.Graphics;
                g.Clear(Colors.LightYellow);

                g.DrawLine(Colors.Black, 10, 100, 190, 100);
                g.DrawLine(Colors.Black, 10, 120, 190, 120);
                g.DrawLine(Colors.Black, 100, 10, 100, 190);
                g.DrawLine(new Pen(Colors.Red, 20), 110, 110, 180, 110);

                if (DateTime.Now.Second % 2 == 0) g.DrawString(t["draw-text"], g.ScaledDefaultFont(), Colors.Black, TextAnchor.BottomRight, 100, 120);
                else
                {
                    if (drawableTextBitmap == null) drawableTextBitmap = new TextBitmap(g, t["draw-text"], 1.0f, Colors.DarkBlue);
                    drawableTextBitmap.Draw(g, TextAnchor.BottomRight, 100, 120);
                }

                var textSize = g.MeasureString(g.ScaledDefaultFont(), t["draw-text"]);
                g.DrawRectangle(new Pen(Colors.Green), 100 - textSize.Width, 120 - textSize.Height, textSize.Width, textSize.Height);
                
                g.DrawImage(image, 80, 80);

                var pieAngle = (DateTime.Now - startTime).TotalMilliseconds * 0.1;
                pieAngle -= Math.Floor(pieAngle / 360) * 360;
                g.FillPie(Color.FromArgb(0, 128, 0, 128), 10, 10, 180, 180, -90, (float)pieAngle);

                foreach (var pt in drawPoints)
                {
                    g.FillEllipse(Colors.DarkBlue, pt.X - 1, pt.Y - 1, 2, 2);
                }
            };
        }

        private void loopDrawDefault2D()
        {
            drawableDefault2D.Invalidate();
        }

        private Drawable drawableDefault2D;
        private TextBitmap drawableTextBitmap;
        private List<FloatPoint> drawPoints = new List<FloatPoint>();
        private LinkButton overlayLinkButton;
    }
}