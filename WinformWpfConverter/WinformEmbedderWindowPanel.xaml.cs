using System;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace WinformWpfConverter
{
    /// <summary>
    /// WinformEmbedderWindowPanel.xaml 的交互逻辑
    /// </summary>
    partial class WinformEmbedderWindowPanel : ASEva.UIWpf.WindowPanel
    {
        public WinformEmbedderWindowPanel(ASEva.UICoreWF.WindowPanel winformWindowPanel)
        {
            InitializeComponent();
            this.winformWindowPanel = winformWindowPanel;

            winformHost.Child = winformWindowPanel;
        }

        public override void OnInitSize(string config)
        {
            winformWindowPanel.OnInitSize(config);

            var minWidth = Math.Max(100, winformWindowPanel.MinimumSize.Width * 96 / winformWindowPanel.DeviceDpi);
            var minHeight = Math.Max(50, winformWindowPanel.MinimumSize.Height * 96 / winformWindowPanel.DeviceDpi);
            var defaultWidth = winformWindowPanel.Width * 96 / winformWindowPanel.DeviceDpi;
            var defaultHeight = winformWindowPanel.Height * 96 / winformWindowPanel.DeviceDpi;

            Width = Math.Max(minWidth, defaultWidth);
            Height = Math.Max(minHeight, defaultHeight);
            MinWidth = minWidth;
            MinHeight = minHeight;
        }

        public override void OnInit(string config)
        {
            winformWindowPanel.OnInit(config);
        }

        public override string OnGetConfig()
        {
            return winformWindowPanel.OnGetConfig();
        }

        public override void OnInputData(object data)
        {
            winformWindowPanel.OnInputData(data);
        }

        public override void OnResetData()
        {
            winformWindowPanel.OnResetData();
        }

        public override void OnStartSession()
        {
            winformWindowPanel.OnStartSession();
        }

        public override void OnStopSession()
        {
            winformWindowPanel.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            winformWindowPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            winformWindowPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return winformWindowPanel.OnHandleAsync();
        }

        public override void OnRelease()
        {
            winformWindowPanel.OnRelease();
        }

        private ASEva.UICoreWF.WindowPanel winformWindowPanel;
    }
}
