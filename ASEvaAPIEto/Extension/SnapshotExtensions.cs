using System;
using Eto.Forms;
using Eto.Drawing;
using ASEva.Samples;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.10.3) 获取控件快照的扩展
    /// </summary>
    public static class SnapshotExtensions
    {
        /// <summary>
        /// 获取控件快照
        /// </summary>
        /// <param name="control">目标控件</param>
        /// <returns>控件快照图像，若不支持或获取失败则返回null</returns>
        public static CommonImage Snapshot(this Control control)
        {
            if (control == null || Handler == null) return null;
            else return Handler.Snapshot(control);
        }

        public interface SnapshotHandler
        {
            CommonImage Snapshot(Control control);
        }

        public static SnapshotHandler Handler { private get; set; }
    }
}