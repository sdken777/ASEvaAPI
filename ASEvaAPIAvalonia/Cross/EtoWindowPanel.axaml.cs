using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;
using System.Threading.Tasks;

namespace ASEva.UIAvalonia
{
    partial class EtoWindowPanel : WindowPanel
    {
        public EtoWindowPanel()
        {
            InitializeComponent();
        }

        public void SetEtoWindowPanel(UIEto.WindowPanel windowPanel)
        {
            embedder.EtoControl = windowPanel;
            this.etoWindowPanel = windowPanel;
        }

        public override string OnGetConfig()
        {
            return etoWindowPanel.OnGetConfig();
        }

        public override Task OnHandleModal()
        {
            etoWindowPanel.OnHandleModal();
            return Task.CompletedTask;
        }

        public override void OnInit(string config)
        {
            etoWindowPanel.OnInit(config);
        }

        public override void OnInitSize(string config)
        {
            etoWindowPanel.OnInitSize(config);

            var defaultSize = etoWindowPanel.OnGetDefaultSize();
            var minSize = etoWindowPanel.OnGetMinimumSize();

            this.Width = defaultSize.Width;
            this.Height = defaultSize.Height;
            this.MinWidth = minSize.Width;
            this.MinHeight = minSize.Height;
        }

        public override void OnInputData(object data)
        {
            etoWindowPanel.OnInputData(data);
        }

        public override void OnRelease()
        {
            etoWindowPanel.OnRelease();
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

        public override Task OnHandleAsync()
        {
            return etoWindowPanel.OnHandleAsync();
        }

        private UIEto.WindowPanel etoWindowPanel;
    }
}