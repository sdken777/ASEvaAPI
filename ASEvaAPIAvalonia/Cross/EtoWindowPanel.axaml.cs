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
            return etoWindowPanel?.OnGetConfig() ?? "";
        }

        public override Task OnHandleModal()
        {
            etoWindowPanel?.OnHandleModal();
            return Task.CompletedTask;
        }

        public override void OnInit(string config)
        {
            etoWindowPanel?.OnInit(config);
        }

        public override void OnInitSize(string config)
        {
            if (etoWindowPanel == null) return;

            etoWindowPanel.OnInitSize(config);

            var defaultSize = etoWindowPanel.OnGetDefaultSize();
            var minSize = etoWindowPanel.OnGetMinimumSize();
            minSize = new IntSize(Math.Max(100, minSize.Width), Math.Max(50, minSize.Height));

            this.Width = Math.Max(defaultSize.Width, minSize.Width);
            this.Height = Math.Max(defaultSize.Height, minSize.Height);
            this.MinWidth = minSize.Width;
            this.MinHeight = minSize.Height;
        }

        public override void OnInputData(object data)
        {
            etoWindowPanel?.OnInputData(data);
        }

        public override void OnRelease()
        {
            if (etoWindowPanel == null) return;
            etoWindowPanel.OnRelease();
            UIEto.ContainerExtensions.CloseRecursively(etoWindowPanel);
        }

        public override void OnResetData()
        {
            etoWindowPanel?.OnResetData();
        }

        public override void OnStartSession()
        {
            etoWindowPanel?.OnStartSession();
        }

        public override void OnStopSession()
        {
            etoWindowPanel?.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            var newContainerSize = new IntSize((int)Width, (int)Height);
            if (newContainerSize.Width > 0 && newContainerSize.Height > 0 &&
                (newContainerSize.Width != containerSize.Width || newContainerSize.Height != containerSize.Height))
            {
                etoWindowPanel?.OnUpdateContainerSize(newContainerSize);
                containerSize = newContainerSize;
            }
            etoWindowPanel?.OnUpdateUI();
        }

        public override Task OnHandleAsync()
        {
            return etoWindowPanel?.OnHandleAsync() ?? Task.CompletedTask;
        }

        private UIEto.WindowPanel? etoWindowPanel;
        private IntSize containerSize;
    }
}