using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WinformWpfConverter
{
    partial class WpfEmbedderWindowPanel : ASEva.UICoreWF.WindowPanel
    {
        public WpfEmbedderWindowPanel(ASEva.UIWpf.WindowPanel wpfWindowPanel)
        {
            InitializeComponent();

            this.wpfWindowPanel = wpfWindowPanel;
            this.wpfWindowPanelWidth = wpfWindowPanel.Width;
            this.wpfWindowPanelHeight = wpfWindowPanel.Height;
            wpfWindowPanel.Background = System.Windows.SystemColors.ControlBrush;

            var elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = wpfWindowPanel;
            Controls.Add(elementHost);
        }

        public override void OnInitSize(string config)
        {
            wpfWindowPanel.OnInitSize(config);

            if (wpfWindowPanel.Width > 0 && wpfWindowPanel.Width < 10000) wpfWindowPanelWidth = wpfWindowPanel.Width;
            if (wpfWindowPanel.Height > 0 && wpfWindowPanel.Height < 10000) wpfWindowPanelHeight = wpfWindowPanel.Height;

            var minWidth = (int)(Math.Max(100, wpfWindowPanel.MinWidth) * DeviceDpi / 96);
            var minHeight = (int)(Math.Max(50, wpfWindowPanel.MinHeight) * DeviceDpi / 96);
            var defaultWidth = (int)(wpfWindowPanelWidth * DeviceDpi / 96);
            var defaultHeight = (int)(wpfWindowPanelHeight * DeviceDpi / 96);

            Width = Math.Max(minWidth, defaultWidth);
            Height = Math.Max(minHeight, defaultHeight);
            MinimumSize = new(minWidth, minHeight);
        }

        public override void OnInit(string config)
        {
            wpfWindowPanel.OnInit(config);
        }

        public override string OnGetConfig()
        {
            return wpfWindowPanel.OnGetConfig();
        }

        public override void OnInputData(object data)
        {
            wpfWindowPanel.OnInputData(data);
        }

        public override void OnResetData()
        {
            wpfWindowPanel.OnResetData();
        }

        public override void OnStartSession()
        {
            wpfWindowPanel.OnStartSession();
        }

        public override void OnStopSession()
        {
            wpfWindowPanel.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            wpfWindowPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            wpfWindowPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return wpfWindowPanel.OnHandleAsync();
        }

        public override void OnRelease()
        {
            wpfWindowPanel.OnRelease();
        }

        private ASEva.UIWpf.WindowPanel wpfWindowPanel;
        private double wpfWindowPanelWidth, wpfWindowPanelHeight;
    }
}
