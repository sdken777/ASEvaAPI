using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.Samples;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawImage : Panel
    {
        public DrawImage()
        {
            InitializeComponent();

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
            imageView3.Source = image3.ToAvaloniaBitmap();
            imageView4.Source = image4.ToAvaloniaBitmap();
        }
    }
}