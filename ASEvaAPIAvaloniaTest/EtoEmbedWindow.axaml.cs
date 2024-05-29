using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class EtoEmbedWindow : Window
    {
        public EtoEmbedWindow() // For designer
        {
            InitializeComponent();
            this.AddToResources(MainWindow.Texts);
        }

        public EtoEmbedWindow(Language language)
        {
            InitializeComponent();
            this.AddToResources(MainWindow.Texts);

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(language, false);
            embedder.EtoControl = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.WindowState = WindowState.FullScreen; };
            Closing += delegate { panel.StopTimer(); };
        }
    }
}