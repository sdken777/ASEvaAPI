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
        private void initUtilGroupBox(GroupBox groupBox)
        {
            var layout = groupBox.SetContentAsColumnLayout();
            var layoutRow = layout.AddRowLayout();
            var webView = layout.AddControl(new WebView(), true, 500, 200) as WebView;

            layoutRow.AddButtonPanel(Bitmap.FromResource("backward.png")).Click += delegate { webView.GoBack(); };
            layoutRow.AddButtonPanel(Bitmap.FromResource("forward.png")).Click += delegate { webView.GoForward(); };
            layoutRow.AddSeparator();
            var textBox = layoutRow.AddControl(new TextBox(), true) as TextBox;
            var buttonGo = layoutRow.AddButtonPanel(t["util-web-go-url"]);
            layoutRow.AddSeparator();
            layoutRow.AddButtonPanel(t["util-web-call-script"]).Click += delegate { webView.ExecuteScriptAsync("callScript()"); };

            buttonGo.Enabled = false;
            buttonGo.DefaultBackgroundColor = Colors.LightGrey;
            buttonGo.Click += delegate
            {
                if (!String.IsNullOrEmpty(textBox.Text))
                {
                    webView.Url = new Uri(textBox.Text.StartsWith("http") ? textBox.Text : ("http://" + textBox.Text));
                }
            };
            textBox.TextChanged += delegate
            {
                buttonGo.Enabled = textBox.Text.Length > 0;
            };

            webView.LoadHtml(ResourceLoader.LoadText("index.html"));
        }
    }
}