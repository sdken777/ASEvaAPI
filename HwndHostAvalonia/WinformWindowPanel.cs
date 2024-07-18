using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ASEva.UICoreWF;

namespace HwndHostAvalonia
{
    partial class WinformWindowPanel : WindowPanel
    {
        public WinformWindowPanel(ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel)
        {
            InitializeComponent();
            this.avaloniaWindowPanel = avaloniaWindowPanel;

            var embedder = new AvaloniaEmbedder();
            embedder.Content = avaloniaWindowPanel;

            var elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = embedder;
            Controls.Add(elementHost);
        }

        public override void OnInitSize(string config)
        {
            avaloniaWindowPanel.OnInitSize(config);

            var minWidth = (int)(Math.Max(100, avaloniaWindowPanel.MinWidth) * DeviceDpi / 96);
            var minHeight = (int)(Math.Max(50, avaloniaWindowPanel.MinHeight) * DeviceDpi / 96);
            var defaultWidth = (int)(avaloniaWindowPanel.Width * DeviceDpi / 96);
            var defaultHeight = (int)(avaloniaWindowPanel.Height * DeviceDpi / 96);

            Width = Math.Max(minWidth, defaultWidth);
            Height = Math.Max(minHeight, defaultHeight);
            MinimumSize = new(minWidth, minHeight);
        }

        public override void OnInit(string config)
        {
            avaloniaWindowPanel.OnInit(config);
        }

        public override string OnGetConfig()
        {
            return avaloniaWindowPanel.OnGetConfig();
        }

        public override void OnInputData(object data)
        {
            avaloniaWindowPanel.OnInputData(data);
        }

        public override void OnResetData()
        {
            avaloniaWindowPanel.OnResetData();
        }

        public override void OnStartSession()
        {
            avaloniaWindowPanel.OnStartSession();
        }

        public override void OnStopSession()
        {
            avaloniaWindowPanel.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            avaloniaWindowPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            var task = avaloniaWindowPanel.OnHandleModal();
            while (true)
            {
                Thread.Sleep(1);
                Application.DoEvents();
                if (task.Status >= TaskStatus.RanToCompletion) break;
            }
        }

        public override Task OnHandleAsync()
        {
            return avaloniaWindowPanel.OnHandleAsync();
        }

        public override void OnRelease()
        {
            avaloniaWindowPanel.OnRelease();
        }

        private ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel;
    }
}
