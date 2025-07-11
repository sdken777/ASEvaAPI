using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ASEva.UICoreWF;

namespace HwndHostAvalonia
{
    partial class WinformConfigPanel : ConfigPanel
    {
        public WinformConfigPanel(ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel)
        {
            InitializeComponent();
            this.avaloniaConfigPanel = avaloniaConfigPanel;

            var embedder = new AvaloniaEmbedder();
            embedder.Content = avaloniaConfigPanel;

            var elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = embedder;
            Controls.Add(elementHost);

            this.embedder = embedder;
            WinformControlMap.Add(embedder, this);

            avaloniaConfigPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(string config)
        {
            avaloniaConfigPanel.OnInitSize(config);

            Width = (int)(Math.Max(200, avaloniaConfigPanel.Width) * DeviceDpi / 96);
            Height = (int)(Math.Max(50, avaloniaConfigPanel.Height) * DeviceDpi / 96);
        }

        public override void OnInit(string config)
        {
            avaloniaConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
            WinformControlMap.Remove(embedder);
            avaloniaConfigPanel.OnRelease();
        }

        public override void OnUpdateUI()
        {
            avaloniaConfigPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            var task = avaloniaConfigPanel.OnHandleModal();
            while (true)
            {
                Thread.Sleep(1);
                Application.DoEvents();
                if (task.Status >= TaskStatus.RanToCompletion) break;
            }
        }

        public override Task OnHandleAsync()
        {
            return avaloniaConfigPanel.OnHandleAsync();
        }

        private ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel;
        private AvaloniaEmbedder embedder;
    }
}
