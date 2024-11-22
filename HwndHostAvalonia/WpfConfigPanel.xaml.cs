using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using ASEva.UIWpf;

namespace HwndHostAvalonia
{
    partial class WpfConfigPanel : ConfigPanel
    {
        public WpfConfigPanel(ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel)
        {
            InitializeComponent();
            this.avaloniaConfigPanel = avaloniaConfigPanel;
            embedder.Content = avaloniaConfigPanel;
            avaloniaConfigPanel.CloseRequested += delegate { Close(); };
        }

        public override void OnInitSize(string config)
        {
            avaloniaConfigPanel.OnInitSize(config);

            Width = (int)Math.Max(200, avaloniaConfigPanel.Width);
            Height = (int)Math.Max(50, avaloniaConfigPanel.Height);
        }

        public override void OnInit(string config)
        {
            avaloniaConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
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

                var frame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
                Dispatcher.PushFrame(frame);

                if (task.Status >= TaskStatus.RanToCompletion) break;
            }
        }

        static object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }

        public override Task OnHandleAsync()
        {
            return avaloniaConfigPanel.OnHandleAsync();
        }

        private ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel;
    }
}
