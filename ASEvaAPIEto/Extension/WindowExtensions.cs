using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.9) 方便操作窗口的扩展
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// 将窗口移动至屏幕中央
        /// </summary>
        /// <param name="window">窗口</param>
        /// <param name="screen">目标屏幕，空表示主屏幕</param>
        public static void MoveToCenter(this Window window, Screen screen = null)
        {
            RectangleF? bound = null;
            if (screen == null) screen = Screen.PrimaryScreen;
            if (screen != null)
            {
                try { bound = screen.Bounds; }
                catch (Exception) {}
            }
            if (bound == null)
            {
                bound = new RectangleF(0, 0, 1280, 720);
            }
            
            var width = window.Width;
            var height = window.Height;
            if (width <= 0 || height <= 0)
            {
                width = window.ClientSize.Width;
                height = window.ClientSize.Height;
            }

            window.Location = new Point((int)bound.Value.X + Math.Max(0, (int)((bound.Value.Width - width) / 2)), (int)bound.Value.Y + Math.Max(0, (int)((bound.Value.Height - height) / 2)));
        }
    }
}