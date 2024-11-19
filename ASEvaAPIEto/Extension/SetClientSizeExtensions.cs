using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for settings client size of window or dialog
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便设置窗口或对话框的用户区域
    /// </summary>
    public static class SetClientSizeExtensions
    {
        /// \~English
        /// <summary>
        /// Set client size of window or dialog
        /// </summary>
        /// <param name="window">Window or dialog object</param>
        /// <param name="logicalWidth">Logical width</param>
        /// <param name="logicalHeight">Logical height</param>
        /// \~Chinese
        /// <summary>
        /// 设置窗口或对话框的用户区域尺寸
        /// </summary>
        /// <param name="window">窗口或对话框对象</param>
        /// <param name="logicalWidth">宽度</param>
        /// <param name="logicalHeight">高度</param>
        public static void SetClientSize(this Window window, int logicalWidth, int logicalHeight)
        {
            if (ClientSizeSetter != null) ClientSizeSetter.SetClientSize(window, logicalWidth, logicalHeight);
            else window.ClientSize = window.Sizer(logicalWidth, logicalHeight);
        }

        /// \~English
        /// <summary>
        /// Set minimum client size of window or dialog
        /// </summary>
        /// <param name="window">Window or dialog object</param>
        /// <param name="logicalWidth">Logical width</param>
        /// <param name="logicalHeight">Logical height</param>
        /// \~Chinese
        /// <summary>
        /// 设置窗口或对话框的用户区域的最小尺寸
        /// </summary>
        /// <param name="window">窗口或对话框对象</param>
        /// <param name="logicalWidth">最小宽度</param>
        /// <param name="logicalHeight">最小高度</param>
        public static void SetMinimumClientSize(this Window window, int logicalWidth, int logicalHeight)
        {
            if (ClientSizeSetter != null) ClientSizeSetter.SetMinimumClientSize(window, logicalWidth, logicalHeight);
            else window.MinimumSize = window.Sizer(logicalWidth, logicalHeight);
        }

        public static SetClientSizeHandler? ClientSizeSetter { private get; set; }
    }

    public interface SetClientSizeHandler
    {
        void SetClientSize(Window window, int logicalWidth, int logicalHeight);
        void SetMinimumClientSize(Window window, int logicalWidth, int logicalHeight);
    }
}