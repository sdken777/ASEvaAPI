using System;
using Avalonia.Controls;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.1.6) Extensions for getting window information
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.1.6) 方便获取窗口信息的扩展
    /// </summary>
    public static class WindowExtensions
    {
        /// \~English
        /// <summary>
        /// Get the top active dialog of the specified window
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取窗口的最顶层活动对话框
        /// </summary>
        public static Window GetActiveDialog(this Window window)
        {
            var target = window;
            while (true)
            {
                var firstActiveDialog = getFirstActiveDialog(target);
                if (firstActiveDialog == null) return target;
                else target = firstActiveDialog;
            }
        }

        private static Window getFirstActiveDialog(Window window)
        {
            foreach (var dialog in window.OwnedWindows)
            {
                if (dialog.IsActive) return dialog;
            }
            return null;
        }
    }
}