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
            MinimumSize = this.Sizer(600, 300);
            Size = this.Sizer(800, 400);
            Resizable = withBorder;
            Title = "";

            var layout = this.SetContentAsRowLayout();
            layout.AddLinkButton(t["basic-client-size"], true).Click += (sender, args) =>
            {
                (sender as LinkButton).Text = ClientSize.Width + "x" + ClientSize.Height;
            };
            if (withBorder)
            {
                layout.AddButton(t["basic-freeze-size"]).Click += delegate
                {
                    Resizable = false;
                };
            }
            layout.AddButton(t["basic-close"]).Click += delegate
            {
                this.Close();
            };
        }
    }
}