using System;
using System.Windows;
using System.Windows.Media;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIWpf
{
    /// <summary>
    /// AppDialogWpf.xaml 的交互逻辑
    /// </summary>
    public partial class AppDialogWpf : System.Windows.Window
    {
        public AppDialogWpf(FrameworkElement element, DialogPanel dialogPanel)
        {
            InitializeComponent();

            this.panel = dialogPanel;
            this.panelElement = element;

            WindowStyle = dialogPanel.WithBorder ? System.Windows.WindowStyle.SingleBorderWindow : System.Windows.WindowStyle.None;
            ResizeMode = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode ? ResizeMode.CanResize : ResizeMode.CanMinimize;

            if (dialogPanel.Title != null) Title = dialogPanel.Title;

            if (dialogPanel.Icon != null)
            {
                var iconFrame = dialogPanel.Icon.GetFrame(1);
                if (iconFrame != null && iconFrame.Bitmap != null) Icon = iconFrame.Bitmap.ControlObject as ImageSource;
            }

            Content = element;
            Width = dialogPanel.DefaultSize.Width;
            Height = dialogPanel.DefaultSize.Height;

            panel.OnDialogClose += Panel_OnDialogClose;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dw = (int)(ActualWidth - panelElement.ActualWidth);
            var dh = (int)(ActualHeight - panelElement.ActualHeight);

            Width = panel.DefaultSize.Width + dw;
            Height = panel.DefaultSize.Height + dh;
            MinWidth = panel.MinSize.Width + dw;
            MinHeight = panel.MinSize.Height + dh;
        }

        private void Panel_OnDialogClose(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            panel.OnClosing();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            panel.CloseRecursively();
        }

        private DialogPanel panel;
        private FrameworkElement panelElement;
    }
}
