using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class EtoEmbedWindow : Window
    {
        public EtoEmbedWindow()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language);

            var panel = new ASEvaAPIEtoTest.EtoTestPanel(Program.Language, false);
            embedder.EtoControl = panel;

            panel.RequestClose += delegate { this.Close(); };
            panel.RequestFullScreen += delegate { this.WindowState = WindowState.FullScreen; };
            Closing += delegate { panel.StopTimer(); };
        }

        private LanguageSwitch language;
    }
}