using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;
using System.Threading.Tasks;

namespace ASEva.UIAvalonia
{
    partial class EtoConfigPanel : ConfigPanel
    {
        public EtoConfigPanel()
        {
            InitializeComponent();
        }

        public void SetEtoConfigPanel(UIEto.ConfigPanel configPanel)
        {
            embedder.EtoControl = configPanel;
            this.etoConfigPanel = configPanel;
            configPanel.CloseRequested += delegate { Close(); };
        }

        public override Task OnHandleModal()
        {
            etoConfigPanel.OnHandleModal();
            return Task.CompletedTask;
        }

        public override void OnInit(string config)
        {
            etoConfigPanel.OnInit(config);
        }

        public override void OnInitSize(string config)
        {
            etoConfigPanel.OnInitSize(config);
            
            var size = etoConfigPanel.OnGetSize();
            this.Width = Math.Max(200, size.Width);
            this.Height = Math.Max(50, size.Height);
        }

        public override void OnRelease()
        {
            etoConfigPanel.OnRelease();
            UIEto.ContainerExtensions.CloseRecursively(etoConfigPanel);
        }

        public override void OnUpdateUI()
        {
            etoConfigPanel.OnUpdateUI();
        }

        public override Task OnHandleAsync()
        {
            return etoConfigPanel.OnHandleAsync();
        }

        private UIEto.ConfigPanel etoConfigPanel;
    }
}