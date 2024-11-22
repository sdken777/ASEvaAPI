using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for splitter
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便操作分屏控件的扩展
    /// </summary>
    public static class SplitterExtensions
    {
        /// \~English
        /// <summary>
        /// Set panel 1
        /// </summary>
        /// <param name="splitter">Splitter object</param>
        /// <returns>Panel 1 object</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Set panel 2
        /// </summary>
        /// <param name="splitter">Splitter object</param>
        /// <returns>Panel 2 object</returns>
        /// \~Chinese
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