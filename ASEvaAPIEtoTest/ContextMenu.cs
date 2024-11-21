using System;
using System.Threading;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        public event EventHandler? RequestFullScreen;
        public event EventHandler? RequestClose;

        private void initContextMenu(ContextMenu menu)
        {
            menu.AddButtonItem(t["menu-fullscreen"]).Click += delegate { RequestFullScreen?.Invoke(this, EventArgs.Empty); };
            menu.AddButtonItem(t["menu-exit"]).Click += delegate { RequestClose?.Invoke(this, EventArgs.Empty); };
            menu.AddSeparator();
            var subMenu = menu.AddButtonItem(t["menu-sub"]);
            subMenu.AddButtonItem(t.Format("menu-sub-item", "A"));
            subMenu.AddSeparator();
            subMenu.AddButtonItem(t.Format("menu-sub-item", "B"));
            menu.AddSeparator();
            menu.AddCheckItem(t.Format("menu-check", "A"));
            menu.AddCheckItem(t.Format("menu-check", "B"));
            menu.AddSeparator();
            menu.AddRadioItems([t.Format("menu-radio", "A"), t.Format("menu-radio", "B")]);
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
            menu.AddSeparator();
            menu.AddButtonItem(t["menu-exception-main"]).Click += delegate
            {
                String[] arr = [];
                Console.WriteLine(arr[0]);
            };
            menu.AddButtonItem(t["menu-exception-sub"]).Click += delegate
            {
                var thread = new Thread(() =>
                {
                    String[] arr = [];
                    Console.WriteLine(arr[0]);
                });
                thread.Start();
            };
        }
    }
}