using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initDrawGroupBox(GroupBox groupBox)
        {
            var layoutRow = groupBox.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            var layoutOverlay = layoutRow.AddControl(new OverlayLayout(), false, 200, 200) as OverlayLayout;
            initDrawOverlay(layoutOverlay);

            var layoutImages = layoutRow.AddColumnLayout();
            initDrawImages(layoutImages);
        }

        private void initDrawOverlay(OverlayLayout layout)
        {
            var drawable = layout.AddControl(new Drawable(), 0, 0, 0, 0) as Drawable;
            layout.AddControl(new Button { Text = "A"}, 10, null, 10, null);
            layout.AddControl(new Button { Text = "B"}, 10, null, null, 10);
            layout.AddControl(new Button { Text = "C"}, null, 10, 10, null);
            layout.AddControl(new Button { Text = "D"}, null, 10, null, 10);

            drawable.Paint += drawable_Paint;
        }

        private void drawable_Paint(object o, PaintEventArgs a)
        {
            var g = a.Graphics;
            g.Clear(Colors.White);
            g.DrawLine(Colors.Black, 10, 100, 190, 100);
            g.DrawLine(Colors.Black, 10, 120, 190, 120);
            g.DrawLine(Colors.Black, 100, 10, 100, 190);
            g.DrawLine(new Pen(Colors.Red, 20), 20, 110, 90, 110);
            g.DrawText(g.ScaledDefaultFont(), Colors.Black, 100, 100, t["draw-text"]);
            g.DrawImage(Icon.FromResource("camera.png"), 80, 80);
            g.FillPie(Color.FromArgb(0, 128, 0, 128), 10, 10, 180, 180, -90, 270);
        }

        private void initDrawImages(StackLayout layout)
        {
            var imageView3 = layout.AddControl(new ImageView(), false, 100, 100) as ImageView;
            var imageView4 = layout.AddControl(new ImageView(), false, 100, 100) as ImageView;

            var image3 = CommonImage.Create(100, 100, false);
            var image4 = CommonImage.Create(100, 100, true);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    image3.Data[i * image3.RowBytes + j * 3] = (byte)(50 + j * 2); // B
                    image3.Data[i * image3.RowBytes + j * 3 + 1] = (byte)(250 - j * 2); // G
                    image3.Data[i * image3.RowBytes + j * 3 + 2] = 128; // R

                    image4.Data[i * image4.RowBytes + j * 4] = 128; // B
                    image4.Data[i * image4.RowBytes + j * 4 + 1] = 128; // G
                    image4.Data[i * image4.RowBytes + j * 4 + 2] = (byte)(100 + i); // R
                    image4.Data[i * image4.RowBytes + j * 4 + 3] = (byte)(200 - j * 2); // A
                }
            }
            imageView3.Image = ASEva.UIEto.ImageConverter.ConvertToBitmap(image3) as Bitmap;
            imageView4.Image = ASEva.UIEto.ImageConverter.ConvertToBitmap(image4) as Bitmap;
        }
    }
}