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
            Content = panel;
        }
    }
}