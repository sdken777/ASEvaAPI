using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=2.0.3) Extensions for creating context menu
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.0.3) 方便操作右键菜单的扩展
    /// </summary>
    public static class ContextMenuExtensions
    {
        /// \~English
        /// <summary>
        /// (api:eto=2.0.8) Create a new context menu for the panel
        /// </summary>
        /// <param name="panel">Target panel</param>
        /// <returns>Context menu object, null if it already exists</returns>
        /// \~Chinese
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
            if (ShouldAddMouseDownEvent)
            {
                panel.MouseDown += (o, args) =>
                {
                    if (args.Buttons.HasFlag(MouseButtons.Alternate))
                    {
                        menu.Show();
                        args.Handled = true;
                    }
                };
            }
            return menu;
        }

        /// \~English
        /// <summary>
        /// Add separator item
        /// </summary>
        /// <param name="menu">Context menu</param>
        /// \~Chinese
        /// <summary>
        /// 添加分割线
        /// </summary>
        /// <param name="menu">右键菜单</param>
        public static void AddSeparator(this ContextMenu menu)
        {
            menu.Items.Add(new SeparatorMenuItem());
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.4) Add button item
        /// </summary>
        /// <param name="menu">Context menu</param>
        /// <param name="text">Item text</param>
        /// <param name="image">Item icon before the text, default is null</param>
        /// <returns>Created button item object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.4) 添加按键项
        /// </summary>
        /// <param name="menu">右键菜单</param>
        /// <param name="text">菜单项文字</param>
        /// <param name="image">菜单项文字前的图标，默认为null</param>
        /// <returns>按键项对象</returns>
        public static ButtonMenuItem AddButtonItem(this ContextMenu menu, String text, Image image = null)
        {
            if (text == null) text = "";
            var item = new ButtonMenuItem { Text = text };
            if (image != null) item.Image = image;
            menu.Items.Add(item);
            return item;
        }

        /// \~English
        /// <summary>
        /// Add check box item
        /// </summary>
        /// <param name="menu">Context menu</param>
        /// <param name="text">Item text</param>
        /// <returns>Create check box item object</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Add radio box items
        /// </summary>
        /// <param name="menu">Context menu</param>
        /// <param name="texts">Text of each item</param>
        /// <returns>A group of radio box item objects</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// (api:eto=2.8.10) Add separator item in sub menu
        /// </summary>
        /// <param name="subMenu">Sub menu</param>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.10) 在子菜单中添加分割线
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        public static void AddSeparator(this ButtonMenuItem subMenu)
        {
            subMenu.Items.Add(new SeparatorMenuItem());
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.10) Add button item in sub menu
        /// </summary>
        /// <param name="subMenu">Sub menu</param>
        /// <param name="text">Item text</param>
        /// <param name="image">Item icon before the text, default is null</param>
        /// <returns>Button item object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.10) 在子菜单中添加按键项
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        /// <param name="text">菜单项文字</param>
        /// <param name="image">菜单项文字前的图标，默认为null</param>
        /// <returns>按键项对象</returns>
        public static ButtonMenuItem AddButtonItem(this ButtonMenuItem subMenu, String text, Image image = null)
        {
            if (text == null) text = "";
            var item = new ButtonMenuItem { Text = text };
            if (image != null) item.Image = image;
            subMenu.Items.Add(item);
            return item;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.10) Add check box item in sub menu
        /// </summary>
        /// <param name="subMenu">Sub menu</param>
        /// <param name="text">Item text</param>
        /// <returns>Check box item object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.10) 在子菜单中添加多选项
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        /// <param name="text">菜单项文字</param>
        /// <returns>多选项对象</returns>
        public static CheckMenuItem AddCheckItem(this ButtonMenuItem subMenu, String text)
        {
            if (text == null) text = "";
            var item = new CheckMenuItem { Text = text };
            subMenu.Items.Add(item);
            return item;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.8.10) Add radio box items in sub menu
        /// </summary>
        /// <param name="subMenu">Sub menu</param>
        /// <param name="texts">Text of each item</param>
        /// <returns>A group of radio box item objects</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.8.10) 在子菜单中添加一组单选项
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        /// <param name="texts">各菜单项文字</param>
        /// <returns>一组单选项对象</returns>
        public static RadioMenuItem[] AddRadioItems(this ButtonMenuItem subMenu, String[] texts)
        {
            if (texts == null) return null;

            var list = new List<RadioMenuItem>();
            RadioMenuItem lastRadioItem = null;
            foreach (var text in texts)
            {
                var item = new RadioMenuItem(lastRadioItem) { Text = text == null ? "" : text };
                subMenu.Items.Add(item);
                list.Add(item);
                lastRadioItem = item;
            }
            return list.ToArray();
        }

        public static bool ShouldAddMouseDownEvent { private get; set; }
    }
}