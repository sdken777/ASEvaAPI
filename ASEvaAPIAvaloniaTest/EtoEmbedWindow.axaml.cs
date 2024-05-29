using System;
using ASEva;
using Avalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class EtoEmbedWindow : Window
    {
        public EtoEmbedWindow() // For designer
        {
            InitializeComponent();
        }

        public EtoEmbedWindow(Language language)
        {
            InitializeComponent();

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(language, false);
            embedder.EtoControl = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.WindowState = WindowState.FullScreen; };
            Closing += delegate { panel.StopTimer(); };
        }
    }
}