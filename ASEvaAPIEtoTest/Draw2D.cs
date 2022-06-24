using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDraw2D(StackLayout layout)
        {
            var overlay = layout.AddControl(new OverlayLayout(), true, 200, 0) as OverlayLayout;
            var drawable = overlay.AddControl(new Drawable(), 0, 0, 0, 0) as Drawable;
            overlay.AddControl(new Button { Text = "A"}, 10, null, 10, null);
            overlay.AddControl(new Button { Text = "B"}, 10, null, null, 10);
            overlay.AddControl(new Button { Text = "C"}, null, 10, 10, null);
            overlay.AddControl(new Button { Text = "D"}, null, 10, null, 10);

            drawable.Paint += (o, args) =>
            {
                var g = args.Graphics;
                g.Clear(Colors.LightYellow);
                g.DrawLine(Colors.Black, 10, 100, 190, 100);
                g.DrawLine(Colors.Black, 10, 120, 190, 120);
                g.DrawLine(Colors.Black, 100, 10, 100, 190);
                g.DrawLine(new Pen(Colors.Red, 20), 20, 110, 90, 110);
                g.DrawText(g.ScaledDefaultFont(), Colors.Black, 100, 100, t["draw-text"]);
                var textSize = g.MeasureString(g.ScaledDefaultFont(), t["draw-text"]);
                g.DrawRectangle(new Pen(Colors.Green), 100, 100, textSize.Width, textSize.Height);
                g.DrawImage(Icon.FromResource("camera.png"), 80, 80);
                g.FillPie(Color.FromArgb(0, 128, 0, 128), 10, 10, 180, 180, -90, 270);
            };
        }
    }
}