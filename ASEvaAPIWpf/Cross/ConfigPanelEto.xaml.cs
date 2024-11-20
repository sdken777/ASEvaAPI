using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Eto.Forms;

namespace ASEva.UIWpf
{
    /// <summary>
    /// ConfigPanelEto.xaml 的交互逻辑
    /// </summary>
    partial class ConfigPanelEto : ConfigPanel
    {
        public ConfigPanelEto()
        {
            InitializeComponent();
        }

        public void SetEtoConfigPanel(UIEto.ConfigPanel configPanel)
        {
            etoConfigPanel = configPanel;
            Content = configPanel.ToNative(true);
            configPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(String config)
        {
            if (etoConfigPanel == null) return;

            etoConfigPanel.OnInitSize(config);

            var size = etoConfigPanel.OnGetSize();
            Width = Math.Max(200, size.Width);
            Height = Math.Max(50, size.Height);
        }

        public override void OnInit(String config)
        {
            etoConfigPanel?.OnInit(config);
        }

        public override void OnRelease()
        {
            if (etoConfigPanel == null) return;
            etoConfigPanel.OnRelease();
            UIEto.ContainerExtensions.CloseRecursively(etoConfigPanel);
        }

        public override void OnUpdateUI()
        {
            etoConfigPanel?.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            etoConfigPanel?.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return etoConfigPanel?.OnHandleAsync() ?? Task.CompletedTask;
        }

        private UIEto.ConfigPanel? etoConfigPanel;
    }
}
