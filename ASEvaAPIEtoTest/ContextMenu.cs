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
            menu.AddButtonItem(t["menu-fullscreen"]).Click += delegate { this.MaximizeToFullScreen(); };
            menu.AddButtonItem(t["menu-exit"]).Click += delegate { Close(); };
            menu.AddSeparator();
            var subMenu = menu.AddButtonItem(t["menu-sub"]);
            subMenu.AddButtonItem(t.Format("menu-sub-item", "A"));
            subMenu.AddSeparator();
            subMenu.AddButtonItem(t.Format("menu-sub-item", "B"));
            menu.AddSeparator();
            menu.AddCheckItem(t.Format("menu-check", "A"));
            menu.AddCheckItem(t.Format("menu-check", "B"));
            menu.AddSeparator();
            menu.AddRadioItems(new String[] { t.Format("menu-radio", "A"), t.Format("menu-radio", "B") });
            menu.AddSeparator();
            menu.AddButtonItem(t["menu-snapshot"]).Click += delegate
            {
                var snapshot = this.Snapshot();
                if (snapshot == null) MessageBox.Show(t["menu-snapshot-failed"], MessageBoxType.Error);
                else
                {
                    var dialog = new ImageDialog(snapshot);
                    App.RunDialog(dialog);
                }
            };
            menu.AddButtonItem(t["menu-snapshot-screen"]).Click += delegate
            {
                var snapshot = this.SnapshotFromScreen();
                if (snapshot == null) MessageBox.Show(t["menu-snapshot-failed"], MessageBoxType.Error);
                else
                {
                    var dialog = new ImageDialog(snapshot);
                    App.RunDialog(dialog);
                }
            };
        }
    }
}