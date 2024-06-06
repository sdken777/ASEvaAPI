using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class InfoDialog : Window
    {
        public InfoDialog()
        {
            InitializeComponent();
        }

        public InfoDialog(String title, String text)
        {
            InitializeComponent();

            Title = title;
            textBlock.Text = text;
        }
    }
}