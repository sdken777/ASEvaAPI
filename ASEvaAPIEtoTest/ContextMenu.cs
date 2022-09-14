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
        private void initContextMenu(ContextMenu menu)
        {
            menu.AddButtonItem(t["menu-button"], CommonImage.LoadResource("menu-button.png").ToEtoBitmap()).Click += delegate { MessageBox.Show(App.WorkPath, ""); };
            var subMenu = menu.AddButtonItem(t["menu-sub"]);
            subMenu.AddButtonItem(t.Format("menu-sub-item", "A"));
            subMenu.AddSeparator();
            subMenu.AddButtonItem(t.Format("menu-sub-item", "B"));
            menu.AddSeparator();
            menu.AddCheckItem(t.Format("menu-check", "A"));
            menu.AddCheckItem(t.Format("menu-check", "B"));
            menu.AddSeparator();
            menu.AddRadioItems(new String[] { t.Format("menu-radio", "A"), t.Format("menu-radio", "B") });
        }
    }
}