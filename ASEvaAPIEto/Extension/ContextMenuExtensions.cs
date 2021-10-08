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
        /// (api:eto=2.0.8) 设置面板右键菜单
        /// </summary>
        /// <param name="panel">面板</param>
        /// <returns>右键菜单对象，若已存在则返回null</returns>
        public static ContextMenu SetContextMenuAsNew(this Panel panel)
        {
            if (panel.ContextMenu != null) return null;
            var menu = new ContextMenu();
            panel.ContextMenu = menu;
            if (App.GetRunningUI() == "gtk")
            {
                panel.MouseDown += (o, args) =>
                {
                    if (args.Buttons.HasFlag(MouseButtons.Alternate)) menu.Show();
                };
            }
            return menu;
        }

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