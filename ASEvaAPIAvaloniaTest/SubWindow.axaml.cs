using System;
using ASEva;
using Avalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class SubWindow : Window
    {
        public SubWindow() // For designer
        {
            InitializeComponent();
        }

        public SubWindow(Language language, bool onscreenRendering)
        {
            InitializeComponent();

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(language, onscreenRendering);
            embedder.EtoControl = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.WindowState = WindowState.FullScreen; };
            Closing += delegate { panel.StopTimer(); };
        }
    }
}