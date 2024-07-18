using System;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace WinformWpfConverter
{
    /// <summary>
    /// WinformEmbedderConfigPanel.xaml 的交互逻辑
    /// </summary>
    partial class WinformEmbedderConfigPanel : ASEva.UIWpf.ConfigPanel
    {
        public WinformEmbedderConfigPanel(ASEva.UICoreWF.ConfigPanel winformConfigPanel)
        {
            InitializeComponent();
            this.winformConfigPanel = winformConfigPanel;

            winformHost.Child = winformConfigPanel;

            winformConfigPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(string config)
        {
            winformConfigPanel.OnInitSize(config);

            Width = Math.Max(200, winformConfigPanel.Width * 96 / winformConfigPanel.DeviceDpi);
            Height = Math.Max(50, winformConfigPanel.Height * 96 / winformConfigPanel.DeviceDpi);
        }

        public override void OnInit(string config)
        {
            winformConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
            winformConfigPanel.OnRelease();
        }

        public override void OnUpdateUI()
        {
            winformConfigPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            winformConfigPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return winformConfigPanel.OnHandleAsync();
        }

        private ASEva.UICoreWF.ConfigPanel winformConfigPanel;
    }
}
