using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WinformWpfConverter
{
    partial class WpfEmbedderConfigPanel : ASEva.UICoreWF.ConfigPanel
    {
        public WpfEmbedderConfigPanel(ASEva.UIWpf.ConfigPanel wpfConfigPanel)
        {
            InitializeComponent();

            this.wpfConfigPanel = wpfConfigPanel;
            this.wpfConfigPanelWidth = wpfConfigPanel.Width;
            this.wpfConfigPanelHeight = wpfConfigPanel.Height;

            var elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = wpfConfigPanel;
            Controls.Add(elementHost);

            wpfConfigPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(string config)
        {
            wpfConfigPanel.OnInitSize(config);

            if (wpfConfigPanel.Width > 0 && wpfConfigPanel.Width < 10000) wpfConfigPanelWidth = wpfConfigPanel.Width;
            if (wpfConfigPanel.Height > 0 && wpfConfigPanel.Height < 10000) wpfConfigPanelHeight = wpfConfigPanel.Height;

            Width = (int)(Math.Max(200, wpfConfigPanelWidth) * DeviceDpi / 96);
            Height = (int)(Math.Max(50, wpfConfigPanelHeight) * DeviceDpi / 96);
        }

        public override void OnInit(string config)
        {
            wpfConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
            wpfConfigPanel.OnRelease();
        }

        public override void OnUpdateUI()
        {
            wpfConfigPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            wpfConfigPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return wpfConfigPanel.OnHandleAsync();
        }

        private ASEva.UIWpf.ConfigPanel wpfConfigPanel;
        private double wpfConfigPanelWidth, wpfConfigPanelHeight;
    }
}
