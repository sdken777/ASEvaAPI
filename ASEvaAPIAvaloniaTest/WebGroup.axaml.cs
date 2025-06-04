using System;
using ASEva;
using ASEva.Utility;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ASEvaAPIAvaloniaTest
{
    partial class WebGroup : Panel
    {
        public WebGroup()
        {
            InitializeComponent();
            new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");
            
            if (!Program.DesignerMode)
            {
                etoWebView = new Eto.Forms.WebView();
                webView.EtoControl = etoWebView;

                Loaded += delegate { etoWebView.LoadHtml(ResourceLoader.LoadText("index.html")); };
            }
        }

        private void buttonBackward_Click(object sender, RoutedEventArgs e)
        {
            etoWebView?.GoBack();
        }

        private void buttonForward_Click(object sender, RoutedEventArgs e)
        {
            etoWebView?.GoForward();
        }

        private void textUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonGoUrl.IsEnabled = textUrl.Text.Length > 0;
        }

        private void buttonGoUrl_Click(object sender, RoutedEventArgs e)
        {
            if (etoWebView!= null && !String.IsNullOrEmpty(textUrl.Text))
            {
                etoWebView.Url = new Uri(textUrl.Text.StartsWith("http") ? textUrl.Text : ("http://" + textUrl.Text));
            }
        }

        private void buttonCallScript_Click(object sender, RoutedEventArgs e)
        {
            etoWebView?.ExecuteScriptAsync("callScript()");
        }

        private Eto.Forms.WebView etoWebView;
    }
}