using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ASEva;
using ASEva.Utility;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class CrossAvaloniaWindow : Window
    {
        public CrossAvaloniaWindow()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");

            if (!Program.DesignerMode)
            {
                var webView = new Eto.Forms.WebView();
                etoEmbedder.EtoControl = webView;
                Loaded += delegate { webView.LoadHtml(ResourceLoader.LoadText("index.html")); };
            }

            buttonAvaloniaDialog.Click += buttonAvaloniaDialog_Click;
            buttonEtoDialog.Click += buttonEtoDialog_Click;
        }

        private async void buttonAvaloniaDialog_Click(object sender, RoutedEventArgs args)
        {
            var avaloniaDialog = new CrossAvaloniaWindow();
            avaloniaDialog.Title = language["menu-avalonia-dialog"];
            await App.RunDialog(avaloniaDialog.ShowDialog, this);
        }

        private async void buttonEtoDialog_Click(object sender, RoutedEventArgs args)
        {
            await ASEva.UIEto.App.RunDialog(new CrossEtoPanel());
        }

        private LanguageSwitch language;
    }
}