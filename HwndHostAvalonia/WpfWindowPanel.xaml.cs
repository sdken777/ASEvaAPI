using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using ASEva.UIWpf;

namespace HwndHostAvalonia
{
    partial class WpfWindowPanel : WindowPanel
    {
        public WpfWindowPanel(ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel)
        {
            InitializeComponent();
            this.avaloniaWindowPanel = avaloniaWindowPanel;
            embedder.Content = avaloniaWindowPanel;
        }

        public override void OnInitSize(string config)
        {
            avaloniaWindowPanel.OnInitSize(config);

            var minWidth = (int)Math.Max(100, avaloniaWindowPanel.MinWidth);
            var minHeight = (int)Math.Max(50, avaloniaWindowPanel.MinHeight);
            var defaultWidth = (int)avaloniaWindowPanel.Width;
            var defaultHeight = (int)avaloniaWindowPanel.Height;

            Width = Math.Max(minWidth, defaultWidth);
            Height = Math.Max(minHeight, defaultHeight);
            MinWidth = minWidth;
            MinHeight = minHeight;
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

                var frame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
                Dispatcher.PushFrame(frame);

                if (task.Status >= TaskStatus.RanToCompletion) break;
            }
        }

        static object? ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
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
