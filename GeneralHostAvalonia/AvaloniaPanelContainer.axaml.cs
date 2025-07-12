using System;
using Avalonia;
using Avalonia.Controls;

namespace GeneralHostAvalonia
{
    partial class AvaloniaPanelContainer : Window
    {
        public AvaloniaPanelContainer(Panel panel)
        {
            InitializeComponent();
            Width = Math.Max(Double.IsNormal(panel.Width) ? panel.Width : 100, Double.IsNormal(panel.Bounds.Width) ? panel.Bounds.Width : 100);
            Height = Math.Max(Double.IsNormal(panel.Height) ? panel.Height : 100, Double.IsNormal(panel.Bounds.Height) ? panel.Bounds.Height : 100);
            Content = panel;

            SizeChanged += delegate
            {
                panel.Width = Width;
                panel.Height = Height;
            };
        }
    }
}