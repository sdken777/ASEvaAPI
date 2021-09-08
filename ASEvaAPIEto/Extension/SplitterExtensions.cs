using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.2) 方便操作分屏控件的扩展
    /// </summary>
    public static class SplitterExtensions
    {
        /// <summary>
        /// 设置面板1
        /// </summary>
        /// <param name="splitter">分屏控件</param>
        /// <returns>面板1对象</returns>
        public static Panel SetPanel1(this Splitter splitter)
        {
            if (splitter.Panel1 != null) return null;
            var panel = new Panel();
            splitter.Panel1 = panel;
            return panel;
        }

        /// <summary>
        /// 设置面板2
        /// </summary>
        /// <param name="splitter">分屏控件</param>
        /// <returns>面板2对象</returns>
        public static Panel SetPanel2(this Splitter splitter)
        {
            if (splitter.Panel2 != null) return null;
            var panel = new Panel();
            splitter.Panel2 = panel;
            return panel;
        }
    }
}