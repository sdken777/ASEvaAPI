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
        public static Window GetParentWindow(this Control control)
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
        public static async Task<Window> GetActiveWindow(this Window window)
        {
            if (window == null) return null;

            var activeWindow = getActiveWindow(window);
            if (activeWindow == null) return null;

            if (lastActiveWindow != null && lastActiveWindow != activeWindow)
            {
                await Task.Delay(DelayTime);
                activeWindow = getActiveWindow(window);
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
        public static async Task<Window> GetActiveWindow(this Control control)
        {
            if (control == null) return null;
            var window = control.GetParentWindow();
            return window == null ? null : await window.GetActiveWindow();
        }

        private static Window getActiveWindow(Window window)
        {
            var target = window;
            while (true)
            {
                var firstActiveWindow = getFirstActiveWindow(target);
                if (firstActiveWindow == null) return target;
                else target = firstActiveWindow;
            }
        }

        private static Window getFirstActiveWindow(Window window)
        {
            if (window == null || window.OwnedWindows == null) return null;
            foreach (var dialog in window.OwnedWindows)
            {
                if (dialog.IsActive) return dialog;
            }
            return null;
        }

        private static Window lastActiveWindow = null;
        private const int DelayTime = 100; // ms
    }
}