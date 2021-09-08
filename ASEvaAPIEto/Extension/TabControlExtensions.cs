using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.2) 方便操作选项卡控件的扩展
    /// </summary>
    public static class TabControlExtensions
    {
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