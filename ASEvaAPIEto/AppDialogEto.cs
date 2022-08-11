using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    class AppDialogEto : Dialog
    {
        public AppDialogEto(DialogPanel panel)
        {
            WindowStyle = panel.WithBorder ? WindowStyle.Default : WindowStyle.None;
            Title = panel.Title == null ? "" : panel.Title;
            if (panel.Icon != null) Icon = panel.Icon;
            if (panel.Mode == DialogPanel.DialogMode.FixMode)
            {
                this.SetClientSize(panel.MinSize.Width, panel.MinSize.Height);
                this.SetMinimumClientSize(panel.MinSize.Width, panel.MinSize.Height);
                Resizable = false;
            }
            else if (panel.Mode == DialogPanel.DialogMode.ResizableMode)
            {
                this.SetClientSize(panel.DefaultSize.Width, panel.DefaultSize.Height);
                this.SetMinimumClientSize(panel.MinSize.Width, panel.MinSize.Height);
                Resizable = true;
            }
            else Resizable = false;

            Content = panel;

            panel.OnDialogClose += delegate
            {
                Close();
            };

            Closing += delegate
            {
                panel.OnClosing();
            };
        }
    }
}