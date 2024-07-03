using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class WindowPanelEto : WindowPanel
    {
        public WindowPanelEto(UIEto.WindowPanel etoWindowPanel)
        {
            this.etoWindowPanel = etoWindowPanel;

            var winformPanel = etoWindowPanel.ToNative(true);
            Controls.Add(winformPanel);
            winformPanel.Dock = DockStyle.Fill;
        }

        public override void OnInitSize(String config)
        {
            etoWindowPanel.OnInitSize(config);

            var defaultSize = etoWindowPanel.OnGetDefaultSize();
            var minSize = etoWindowPanel.OnGetMinimumSize();

            Width = defaultSize.Width * DeviceDpi / 96;
            Height = defaultSize.Height * DeviceDpi / 96;
            MinimumSize = new Size(minSize.Width * DeviceDpi / 96, minSize.Height * DeviceDpi / 96);
        }

        public override void OnInit(String config)
        {
            etoWindowPanel.OnInit(config);
        }

        public override String OnGetConfig()
        {
            return etoWindowPanel.OnGetConfig();
        }

        public override void OnInputData(object data)
        {
            etoWindowPanel.OnInputData(data);
        }

        public override void OnResetData() 
        {
            etoWindowPanel.OnResetData();
        }

        public override void OnStartSession()
        {
            etoWindowPanel.OnStartSession();
        }

        public override void OnStopSession()
        {
            etoWindowPanel.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            etoWindowPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            etoWindowPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return etoWindowPanel.OnHandleAsync();
        }

        public override void OnRelease()
        {
            etoWindowPanel.OnRelease();
        }

        private UIEto.WindowPanel etoWindowPanel;
    }
}
