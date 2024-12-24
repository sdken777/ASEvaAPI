using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using ASEva;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        private void initDrawDefault2D(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();
            var overlay = layout.AddControl(new OverlayLayout(), true, 200, 0);
            drawableDefault2D = overlay.AddControl<Drawable>(new Drawable(), 0, 0, 0, 0);

            Button buttonA = new Button { Text = "A" }, buttonB = new Button { Text = "B" }, buttonC = new Button { Text = "C" }, buttonD = new Button { Text = "D" };
            buttonA.SetLogicalWidth(30); buttonB.SetLogicalWidth(30); buttonC.SetLogicalWidth(30); buttonD.SetLogicalWidth(30);
            var linkButton = new LinkButton{ Text = "ABCD", BackgroundColor = Colors.LightYellow };
            linkButton.SetToolTip("ABCD");
            overlay.AddControl(buttonA, 10, null, 10, null);

            buttonA.Click += delegate
            {
                overlay.AddControl(buttonB, null, 10, 10, null);
                overlay.AddControl(buttonC, null, 10, null, 10);
                overlay.AddControl(buttonD, 10, null, null, 10);
            };
            buttonB.Click += delegate
            {
                overlay.AddControl(linkButton, null, 10, null, null);
            };
            buttonC.Click += delegate
            {
                overlay.RemoveControl(linkButton);
            };
            buttonD.Click += delegate
            {
                overlay.RemoveControl(buttonB);
                overlay.RemoveControl(buttonC);
                overlay.RemoveControl(buttonD);
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
    }
}