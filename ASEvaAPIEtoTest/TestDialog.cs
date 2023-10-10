using System;
using ASEva.Utility;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class TestDialog : DialogPanel
    {
        public TestDialog(bool fixSize, bool withBorder, TextResource t)
        {
            Icon = Icon.FromResource("icon.png");
            Title = t["title-dialog"];
            if (fixSize) SetFixMode(800, 400, withBorder);
            else SetResizableMode(600, 300, 800, 400);

            var layout = this.SetContentAsRowLayout();
            layout.AddLinkButton(t["basic-client-size"], false).Click += (sender, args) =>
            {
                (sender as LinkButton).Text = ClientSize.Width + "x" + ClientSize.Height;
            };
            layout.AddLinkButton(t["basic-dialog-with-border"], false).Click += delegate
            {
                var dialog = new TestDialog(false, true, t);
                App.RunDialog(dialog);
            };
            layout.AddSpace();
            layout.AddButton(t["basic-close"]).Click += delegate
            {
                this.Close();
            };
        }
    }
}