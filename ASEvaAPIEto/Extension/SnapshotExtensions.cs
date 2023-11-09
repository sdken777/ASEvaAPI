using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=2.10.3) Extensions for querying control's snapshot
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.10.3) 获取控件快照的扩展
    /// </summary>
    public static class SnapshotExtensions
    {
        /// \~English
        /// <summary>
        /// Query snapshot by directly rendering to image
        /// </summary>
        /// <param name="control">Target control object</param>
        /// <returns>Queried snapshot, return null if unsupported or query failed</returns>
        /// \~Chinese
        /// <summary>
        /// 获取控件快照（直接渲染图像方式）
        /// </summary>
        /// <param name="control">目标控件</param>
        /// <returns>控件快照图像，若不支持或获取失败则返回null</returns>
        public static CommonImage Snapshot(this Control control)
        {
            if (control == null || Handler == null) return null;
            
            var rawImage = Handler.Snapshot(control);
            if (rawImage == null) return null;

            return resizeToControlSize(rawImage, control);
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.11.6) Query snapshot by downloading data from screen buffer
        /// </summary>
        /// <param name="control">Target control object</param>
        /// <returns>Queried snapshot, return null if unsupported or query failed</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.11.6) 获取控件快照（从屏幕缓存下载方式）
        /// </summary>
        /// <param name="control">目标控件</param>
        /// <returns>控件快照图像，若不支持或获取失败则返回null</returns>
        public static CommonImage SnapshotFromScreen(this Control control)
        {
            if (control == null || ScreenModeHandler == null) return null;
            
            var rawImage = ScreenModeHandler.Snapshot(control);
            if (rawImage == null) return null;

            return resizeToControlSize(rawImage, control);
        }

        private static CommonImage resizeToControlSize(CommonImage rawImage, Control control)
        {
            var targetWidth = control.GetLogicalWidth();
            if (control is Window)
            {
                var content = (control as Window).Content;
                if (content == null) return null;
                else targetWidth = content.GetLogicalWidth();
            }

            if (rawImage.Width == targetWidth) return rawImage;

            return rawImage.Resize(targetWidth);
        }

        public interface SnapshotHandler
        {
            CommonImage Snapshot(Control control);
        }

        public static SnapshotHandler Handler { private get; set; }
        public static SnapshotHandler ScreenModeHandler { private get; set; }
    }
}