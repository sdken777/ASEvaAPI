using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.UIEto;
using ASEva.Utility;

namespace ASEvaAPIAvaloniaTest
{
    class CrossEtoPanel : DialogPanel
    {
        public CrossEtoPanel()
        {
            Title = Program.Texts["menu-eto-dialog"];
            Icon = Icon.FromResource("icon.png");
            this.SetFixMode(700, 400, true);

            var mainLayout = this.SetContentAsColumnLayout();
            var topLayout = mainLayout.AddRowLayout();
            var webView = mainLayout.AddControl(new WebView(), true) as WebView;

            topLayout.AddLinkButton(Program.Texts["menu-avalonia-dialog"]).Click += async delegate
            {
                var avaloniaDialog = new CrossAvaloniaWindow();
                avaloniaDialog.Title = Program.Texts["menu-avalonia-dialog"];
                await ASEva.UIAvalonia.App.RunDialog(avaloniaDialog);
            };

            topLayout.AddLinkButton(Program.Texts["menu-eto-dialog"]).Click += async delegate
            {
                await App.RunDialog(new CrossEtoPanel());
            };

            topLayout.AddLinkButton(Program.Texts["eto-show-question-box"]).Click += async delegate
            {
                await App.ShowMessageBox(await App.ShowQuestionBox(Program.Texts["eto-show-question-msg"]) ? "TRUE" : "FALSE");
            };
            
            topLayout.AddLinkButton(Program.Texts["eto-show-color-picker"]).Click += async delegate
            {
                color = await App.ShowColorDialog(null, color);
            };

            LoadComplete += delegate
            {
                webView.LoadHtml(ResourceLoader.LoadText("index.html"));
            };
        }

        private Eto.Drawing.Color color = new Eto.Drawing.Color();
    }
}