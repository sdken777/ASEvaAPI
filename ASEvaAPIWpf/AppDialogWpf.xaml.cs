using System;
using System.Windows;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using ASEva;
using ASEva.Samples;
using ASEva.UIEto;

namespace ASEva.UIWpf
{
    public partial class AppDialogWpf : System.Windows.Window
    {
        public AppDialogWpf(FrameworkElement element, DialogPanel dialogPanel)
        {
            InitializeComponent();

            this.panel = dialogPanel;
            this.panelElement = element;

            WindowStyle = dialogPanel.WithBorder ? System.Windows.WindowStyle.SingleBorderWindow : System.Windows.WindowStyle.None;
            ResizeMode = dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode ? ResizeMode.CanResize : ResizeMode.NoResize;

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

            SourceInitialized += (o, e) =>
            {
                if (dialogPanel.Mode == DialogPanel.DialogMode.ResizableMode)
                {
                    var windowHandle = new WindowInteropHelper(this).Handle;
                    if (windowHandle != IntPtr.Zero)
                    {
                        SetWindowLong(windowHandle, GWL_STYLE, GetWindowLong(windowHandle, GWL_STYLE) & ~WS_MINIMIZEBOX);
                    }
                }
            };
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

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MINIMIZEBOX = 0x20000; // minimize button

        private DialogPanel panel;
        private FrameworkElement panelElement;
    }
}
