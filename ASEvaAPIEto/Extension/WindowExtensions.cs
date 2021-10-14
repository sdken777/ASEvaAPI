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
            if (screen == null) screen = Screen.PrimaryScreen;
            var bound = screen.Bounds;
            window.Location = new Point((int)bound.X + Math.Max(0, (int)((bound.Width - window.Width) / 2)), (int)bound.Y + Math.Max(0, (int)((bound.Height - window.Height) / 2)));
        }
    }
}