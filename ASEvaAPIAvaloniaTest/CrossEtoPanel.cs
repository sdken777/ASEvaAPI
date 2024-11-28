using System;
using Eto.Forms;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEvaAPIAvaloniaTest
{
    class CrossEtoPanel : DialogPanel
    {
        public CrossEtoPanel()
        {
            Title = Program.Texts["menu-eto-dialog"];
            this.SetFixMode(700, 400, true);

            var mainLayout = this.SetContentAsColumnLayout();
            var topLayout = mainLayout.AddRowLayout();
            var webView = mainLayout.AddControl(new WebView(), true) as WebView;

            topLayout.AddLinkButton(Program.Texts["menu-avalonia-dialog"]).Click += async delegate
            {
                var avaloniaDialog = new CrossAvaloniaWindow();
                avaloniaDialog.Title = Program.Texts["menu-avalonia-dialog"];
                await ASEva.UIAvalonia.App.RunDialog(avaloniaDialog.ShowDialog);
            };

            topLayout.AddLinkButton(Program.Texts["menu-eto-dialog"]).Click += async delegate
            {
                await App.RunDialog(new CrossEtoPanel());
            };

            LoadComplete += delegate
            {
                webView.LoadHtml(ResourceLoader.LoadText("index.html"));
            };
        }
    }
}