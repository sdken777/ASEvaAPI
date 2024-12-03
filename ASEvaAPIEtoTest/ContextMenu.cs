using System;
using System.Threading;
using ASEva;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        public event EventHandler RequestFullScreen;
        public event EventHandler RequestClose;

        private void initContextMenu(ContextMenu menu)
        {
            menu.AddButtonItem(t["menu-fullscreen"]).Click += delegate { RequestFullScreen?.Invoke(this, null); };
            menu.AddButtonItem(t["menu-exit"]).Click += delegate { RequestClose?.Invoke(this, null); };
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
                if (snapshot == null) App.ShowMessageBox(t["menu-snapshot-failed"], null, LogLevel.Error);
                else
                {
                    var dialog = new ImageDialog(snapshot);
                    App.RunDialog(dialog);
                }
            };
            menu.AddButtonItem(t["menu-snapshot-screen"]).Click += delegate
            {
                var snapshot = this.SnapshotFromScreen();
                if (snapshot == null) App.ShowMessageBox(t["menu-snapshot-failed"], null, LogLevel.Error);
                else
                {
                    var dialog = new ImageDialog(snapshot);
                    App.RunDialog(dialog);
                }
            };
            menu.AddSeparator();
            menu.AddButtonItem(t["menu-exception-main"]).Click += delegate
            {
                String a = null;
                Console.WriteLine(a.Length.ToString());
            };
            menu.AddButtonItem(t["menu-exception-sub"]).Click += delegate
            {
                var thread = new Thread(() =>
                {
                    String a = null;
                    Console.WriteLine(a.Length.ToString());
                });
                thread.Start();
            };
        }
    }
}