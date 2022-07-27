using System;
using ASEva.Utility;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class TestDialog : Dialog
    {
        public TestDialog(bool withBorder, TextResource t)
        {
            WindowStyle = withBorder ? WindowStyle.Default : WindowStyle.None;
            Icon = Icon.FromResource("icon.png");
            Title = t["title-dialog"];
            this.SetClientSize(800, 400);
            this.SetMinimumClientSize(600, 300);
            Resizable = withBorder;

            var layout = this.SetContentAsRowLayout();
            layout.AddLinkButton(t["basic-client-size"], true).Click += (sender, args) =>
            {
                (sender as LinkButton).Text = ClientSize.Width + "x" + ClientSize.Height;
            };
            layout.AddButton(t["basic-close"]).Click += delegate
            {
                this.Close();
            };
        }
    }
}