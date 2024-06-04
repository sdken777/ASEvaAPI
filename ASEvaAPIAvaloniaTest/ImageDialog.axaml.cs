using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Media;

namespace ASEvaAPIAvaloniaTest
{
    partial class ImageDialog : Window
    {
        public ImageDialog()
        {
            InitializeComponent();
        }

        public ImageDialog(IImage targetImage)
        {
            InitializeComponent();
            
            image.Source = targetImage;
            Width = targetImage.Size.Width;
            Height = targetImage.Size.Height;
        }
    }
}