using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for tab control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便操作选项卡控件的扩展
    /// </summary>
    public static class TabControlExtensions
    {
        /// \~English
        /// <summary>
        /// Add page to tab control
        /// </summary>
        /// <param name="tabControl">Tabl control object</param>
        /// <param name="title">Page title</param>
        /// <returns>Page object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加页至选项卡控件
        /// </summary>
        /// <param name="tabControl">选项卡控件</param>
        /// <param name="title">页标题</param>
        /// <returns>页对象</returns>
        public static TabPage AddPage(this TabControl tabControl, String title)
        {
            if (title == null) title = "";
            var page = new TabPage { Text = title };
            tabControl.Pages.Add(page);
            return page;
        }
    }
}