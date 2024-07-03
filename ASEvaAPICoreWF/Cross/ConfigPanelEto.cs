using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class ConfigPanelEto : ConfigPanel
    {
        public ConfigPanelEto(UIEto.ConfigPanel etoConfigPanel)
        {
            this.etoConfigPanel = etoConfigPanel;

            var winformPanel = etoConfigPanel.ToNative(true);
            Controls.Add(winformPanel);
            winformPanel.Dock = DockStyle.Fill;

            etoConfigPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(String config)
        {
            etoConfigPanel.OnInitSize(config);

            var size = etoConfigPanel.OnGetSize();
            Width = size.Width * DeviceDpi / 96;
            Height = size.Height * DeviceDpi / 96;
        }

        public override void OnInit(String config)
        {
            etoConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
            etoConfigPanel.OnRelease();
        }

        public override void OnUpdateUI()
        {
            etoConfigPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            etoConfigPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return etoConfigPanel.OnHandleAsync();
        }

        private UIEto.ConfigPanel etoConfigPanel;
    }
}
