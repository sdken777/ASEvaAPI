using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Eto.Forms;
using Eto.Wpf.Forms.Controls;

namespace ASEva.UIWpf
{
    /// <summary>
    /// WindowPanelEto.xaml 的交互逻辑
    /// </summary>
    public partial class WindowPanelEto : WindowPanel
    {
        public WindowPanelEto()
        {
            InitializeComponent();
        }

        public void SetEtoWindowPanel(UIEto.WindowPanel windowPanel)
        {
            etoWindowPanel = windowPanel;
            Content = windowPanel.ToNative(true);
        }

        public override void OnInitSize(String config)
        {
            etoWindowPanel.OnInitSize(config);

            var defaultSize = etoWindowPanel.OnGetDefaultSize();
            var minSize = etoWindowPanel.OnGetMinimumSize();
            minSize = new IntSize(Math.Max(100, minSize.Width), Math.Max(50, minSize.Height));

            Width = Math.Max(defaultSize.Width, minSize.Width);
            Height = Math.Max(defaultSize.Height, minSize.Height);
            MinWidth = minSize.Width;
            MinHeight = minSize.Height;
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
            var newContainerSize = new IntSize((int)Width, (int)Height);
            if (newContainerSize.Width > 0 && newContainerSize.Height > 0 &&
                (newContainerSize.Width != containerSize.Width || newContainerSize.Height != containerSize.Height))
            {
                etoWindowPanel.OnUpdateContainerSize(newContainerSize);
                containerSize = newContainerSize;
            }
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
            UIEto.ContainerExtensions.CloseRecursively(etoWindowPanel);
        }

        private UIEto.WindowPanel etoWindowPanel;
        private IntSize containerSize;
    }
}
