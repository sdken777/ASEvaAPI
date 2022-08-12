using System;
using System.Drawing;
using System.Windows.Forms;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
    partial class AppDialogCoreWF : Form
    {
        public AppDialogCoreWF(Control control, DialogPanel dialogPanel)
        {
            InitializeComponent();

            this.panel = dialogPanel;
            this.panelControl = control;

            var dpiScale = (float)DeviceDpi / 96;
            ClientSize = new Size((int)(dpiScale * dialogPanel.DefaultSize.Width), (int)(dpiScale * dialogPanel.DefaultSize.Height));

            FormBorderStyle = dialogPanel.WithBorder ?
                (dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog) :
                FormBorderStyle.None;
            MaximizeBox = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode;

            if (dialogPanel.Title != null) Text = dialogPanel.Title;
            if (dialogPanel.Icon != null) Icon = dialogPanel.Icon.ControlObject as Icon;

            Controls.Add(control);
            control.Dock = DockStyle.Fill;

            Shown += AppDialogCoreWF_Shown;
            dialogPanel.OnDialogClose += DialogPanel_OnDialogClose;
        }

        private void AppDialogCoreWF_Shown(object sender, EventArgs e)
        {
            var dw = Width - panelControl.Width;
            var dh = Height - panelControl.Height;

            var dpiScale = (float)DeviceDpi / 96;
            var minWidth = (int)(dpiScale * panel.MinSize.Width) + dw;
            var minHeight = (int)(dpiScale * panel.MinSize.Height) + dh;
            MinimumSize = new Size(minWidth, minHeight);
        }

        private void DialogPanel_OnDialogClose(object sender, EventArgs e)
        {
            Close();
        }

        private void AppDialogCoreWF_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel.OnClosing();
        }

        private void AppDialogCoreWF_FormClosed(object sender, FormClosedEventArgs e)
        {
            panel.CloseRecursively();
        }

        private DialogPanel panel;
        private Control panelControl;
    }
}
