using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=2.13.5) Extensions for make window full screen
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.13.5) 全屏化窗口的扩展
    /// </summary>
    public static class FullScreenExtensions
    {
        /// \~English
        /// <summary>
        /// Make window full screen
        /// </summary>
        /// <param name="window">Target window object</param>
        /// \~Chinese
        /// <summary>
        /// 全屏化窗口
        /// </summary>
        /// <param name="window">目标窗口</param>
        public static void MaximizeToFullScreen(this Window window)
        {
            if (window == null) return;
            if (Handler == null)
            {
                if (window.Maximizable) window.Maximize();
                return;
            }
            Handler.FullScreen(window);
        }

        public interface FullScreenHandler
        {
            void FullScreen(Window window);
        }

        public static FullScreenHandler Handler { private get; set; }
    }
}