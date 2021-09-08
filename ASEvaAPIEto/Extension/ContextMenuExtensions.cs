using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.3) 方便操作右键菜单的扩展
    /// </summary>
    public static class ContextMenuExtensions
    {
        /// <summary>
        /// 添加分割线
        /// </summary>
        /// <param name="menu">右键菜单</param>
        public static void AddSeparator(this ContextMenu menu)
        {
            menu.Items.Add(new SeparatorMenuItem());
        }

        /// <summary>
        /// (api:eto=2.0.4) 添加按键项
        /// </summary>
        /// <param name="menu">右键菜单</param>
        /// <param name="text">菜单项文字</param>
        /// <param name="image">菜单项文字前的图标，默认为null</param>
        /// <returns></returns>
        public static ButtonMenuItem AddButtonItem(this ContextMenu menu, String text, Image image = null)
        {
            if (text == null) text = "";
            var item = new ButtonMenuItem { Text = text };
            if (image != null) item.Image = image;
            menu.Items.Add(item);
            return item;
        }

        /// <summary>
        /// 添加多选项
        /// </summary>
        /// <param name="menu">右键菜单</param>
        /// <param name="text">菜单项文字</param>
        /// <returns>多选项对象</returns>
        public static CheckMenuItem AddCheckItem(this ContextMenu menu, String text)
        {
            if (text == null) text = "";
            var item = new CheckMenuItem { Text = text };
            menu.Items.Add(item);
            return item;
        }

        /// <summary>
        /// 添加一组单选项
        /// </summary>
        /// <param name="menu">右键菜单</param>
        /// <param name="texts">各菜单项文字</param>
        /// <returns>一组单选项对象</returns>
        public static RadioMenuItem[] AddRadioItems(this ContextMenu menu, String[] texts)
        {
            if (texts == null) return null;

            var list = new List<RadioMenuItem>();
            RadioMenuItem lastRadioItem = null;
            foreach (var text in texts)
            {
                var item = new RadioMenuItem(lastRadioItem) { Text = text == null ? "" : text };
                menu.Items.Add(item);
                list.Add(item);
                lastRadioItem = item;
            }
            return list.ToArray();
        }
    }
}