using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initWebGroupBox(GroupBox groupBox)
        {
            var layout = groupBox.SetContentAsColumnLayout();
            var layoutRow = layout.AddRowLayout();
            var webView = layout.AddControl(new WebView(), true, 500, 200) as WebView;

            layoutRow.AddButtonPanel(CommonImage.LoadResource("backward.png").ToEtoBitmap()).Click += delegate { webView.GoBack(); };
            layoutRow.AddButtonPanel(CommonImage.LoadResource("forward.png").ToEtoBitmap()).Click += delegate { webView.GoForward(); };
            layoutRow.AddSeparator();
            var textBox = layoutRow.AddControl(new TextBox(), true) as TextBox;
            layoutRow.AddButtonPanel(t["web-go-url"]).Click += delegate
            {
                if (!String.IsNullOrEmpty(textBox.Text))
                {
                    webView.Url = new Uri(textBox.Text.StartsWith("http") ? textBox.Text : ("http://" + textBox.Text));
                }
            };
            layoutRow.AddSeparator();
            layoutRow.AddButtonPanel(t["web-call-script"]).Click += delegate
            {
                webView.ExecuteScriptAsync("callScript()");
            };

            webView.LoadHtml(ResourceLoader.LoadText("index.html"));
        }
    }
}