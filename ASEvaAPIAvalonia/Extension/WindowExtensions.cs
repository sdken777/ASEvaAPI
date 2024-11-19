using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.2.3) Extensions for getting window information
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.2.3) 方便获取窗口信息的扩展
    /// </summary>
    public static class WindowExtensions
    {
        /// \~English
        /// <summary>
        /// Get the parent window of the specified control
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取控件所属窗口
        /// </summary>
        public static Window? GetParentWindow(this Control control)
        {
            if (control == null) return null;
            return TopLevel.GetTopLevel(control) as Window;
        }

        /// \~English
        /// <summary>
        /// Get the top active window (dialog) of the specified window
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取窗口的最顶层活动窗口(对话框)
        /// </summary>
        public static async Task<Window?> GetActiveWindow(this Window window)
        {
            var activeWindow = getFirstActiveWindow(window);
            if (activeWindow == null || (lastActiveWindow != null && lastActiveWindow != activeWindow))
            {
                await Task.Delay(DelayTime);
                activeWindow = getFirstActiveWindow(window);
                if (activeWindow == null) return null;
            }

            lastActiveWindow = activeWindow;
            return activeWindow;
        }

        /// \~English
        /// <summary>
        /// Get the top active window (dialog) of the window owning the specified control
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取控件所属窗口的最顶层活动窗口(对话框)
        /// </summary>
        public static async Task<Window?> GetActiveWindow(this Control control)
        {
            var window = control.GetParentWindow();
            return window == null ? null : await window.GetActiveWindow();
        }

        private static Window? getFirstActiveWindow(Window window)
        {
            if (window.IsActive) return window;
            foreach (var dialog in window.OwnedWindows)
            {
                var activeWindow = getFirstActiveWindow(dialog);
                if (activeWindow != null) return activeWindow;
            }
            return null;
        }

        private static Window? lastActiveWindow = null;
        private const int DelayTime = 100; // ms
    }
}