using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.9.10) 方便获取控件所在窗口或对话框是否在顶层的扩展
    /// </summary>
    public static class TopMostExtensions
    {
        /// <summary>
        /// 获取控件所在窗口或对话框是否在顶层
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns>所在窗口或对话框是否在顶层</returns>
        public static bool IsTopMost(this Control control)
        {
            if (control == null) return false;
            if (QueryInterface != null) return QueryInterface.IsTopMost(control);
            else return control.ParentWindow != null && control.ParentWindow.Topmost;
        }

        public interface QueryTopMost
        {
            bool IsTopMost(Control control);
        }

        public static QueryTopMost QueryInterface { private get; set; }
    }
}