using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.0) 方便添加控件的扩展
    /// </summary>
    public static partial class AddControlExtensions
    {
        /// <summary>
        /// 添加空间延布局方向撑满
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        public static void AddSpace(this StackLayout stackLayout)
        {
            stackLayout.Items.Add(new StackLayoutItem(new Panel(), true));
        }

        /// <summary>
        /// 添加文字标签至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this StackLayout stackLayout, String text, bool expand = false)
        {
            if (text == null) text = "";
            var label = new Label { Text = text, Wrap = WrapMode.None };
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(label, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(label, VerticalAlignment.Center, expand));
            }
            return label;
        }

        /// <summary>
        /// 添加文字标签至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="alignment">文字对齐</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this StackLayout stackLayout, String text, TextAlignment alignment, bool expand = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            if (text == null) text = "";
            var label = new Label { Text = text, Wrap = WrapMode.None, TextAlignment = alignment, VerticalAlignment = VerticalAlignment.Center };
            if (logicalWidth > 0)
            {
                label.SetLogicalWidth(logicalWidth);
            }
            if (logicalHeight > 0)
            {
                label.SetLogicalHeight(logicalHeight);
            }
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(label, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(label, VerticalAlignment.Center, expand));
            }
            return label;
        }

        /// <summary>
        /// 添加按键至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的按键对象</returns>
        public static Button AddButton(this StackLayout stackLayout, String text, bool expand = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            if (text == null) text = "";
            var button = new Button { Text = text };
            if (logicalWidth > 0)
            {
                button.SetLogicalWidth(logicalWidth);
            }
            if (logicalHeight > 0)
            {
                button.SetLogicalHeight(logicalHeight);
            }
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(button, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(button, VerticalAlignment.Center, expand));
            }
            return button;
        }

        /// <summary>
        /// 添加链接式按键至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <returns>创建的链接式按键对象</returns>
        public static LinkButton AddLinkButton(this StackLayout stackLayout, String text, bool expand = false)
        {
            if (text == null) text = "";
            var button = new LinkButton { Text = text };
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(button, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(button, VerticalAlignment.Center, expand));
            }
            return button;
        }

        /// <summary>
        /// 添加多选框至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="isChecked">初始状态是否勾选</param>
        /// <returns>创建的多选框对象</returns>
        public static CheckBox AddCheckBox(this StackLayout stackLayout, String text, bool expand = false, bool isChecked = false)
        {
            if (text == null) text = "";
            var checkBox = new CheckBox { Text = text, Checked = isChecked };
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(checkBox, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(checkBox, VerticalAlignment.Center, expand));
            }
            return checkBox;
        }

        /// <summary>
        /// 添加单选框组至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="texts">各单选框的文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="selectedIndex">初始的选中单选框序号</param>
        /// <param name="logicalSpacing">单选框间的间隙</param>
        /// <returns>创建的单选框组对象</returns>
        public static RadioButtonList AddRadioButtonList(this StackLayout stackLayout, String[] texts, bool expand = false, int selectedIndex = 0, int logicalSpacing = 8)
        {
            if (texts == null || texts.Length == 0) return null;

            var radioButtonList = new RadioButtonList();
            radioButtonList.Spacing = new Size(radioButtonList.Sizer(logicalSpacing), radioButtonList.Sizer(logicalSpacing));
            foreach (var text in texts)
            {
                radioButtonList.Items.Add(text);
            }

            if (selectedIndex >= 0 && selectedIndex < texts.Length)
            {
                radioButtonList.SelectedIndex = selectedIndex;
            }
            else
            {
                radioButtonList.SelectedIndex = 0;
            }

            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(radioButtonList, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(radioButtonList, VerticalAlignment.Center, expand));
            }
            return radioButtonList;
        }

        /// <summary>
        /// 添加组合框至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="texts">组合框各选项的文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this StackLayout stackLayout, String[] texts, bool expand = false, int selectedIndex = 0)
        {
            if (texts == null || texts.Length == 0) return null;

            var comboBox = new ComboBox { ReadOnly = true };
            foreach (var text in texts)
            {
                comboBox.Items.Add(text);
            }

            if (selectedIndex >= 0 && selectedIndex < texts.Length)
            {
                comboBox.SelectedIndex = selectedIndex;
            }
            else
            {
                comboBox.SelectedIndex = 0;
            }

            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(comboBox, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(comboBox, VerticalAlignment.Center, expand));
            }
            return comboBox;
        }

        /// <summary>
        /// 添加分组框至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="title">标题</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的分组框对象</returns>
        public static GroupBox AddGroupBox(this StackLayout stackLayout, String title, bool expand = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            if (title == null) title = "";
            var groupBox = new GroupBox { Text = title };
            if (logicalWidth > 0)
            {
                groupBox.SetLogicalWidth(logicalWidth);
            }
            if (logicalHeight > 0)
            {
                groupBox.SetLogicalHeight(logicalHeight);
            }
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(groupBox, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(groupBox, VerticalAlignment.Center, expand));
            }
            return groupBox;
        }

        /// <summary>
        /// 添加控件至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="control">控件</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        public static Control AddControl(this StackLayout stackLayout, Control control, bool expand = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            if (logicalWidth > 0)
            {
                control.SetLogicalWidth(logicalWidth);
            }
            if (logicalHeight > 0)
            {
                control.SetLogicalHeight(logicalHeight);
            }
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(control, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(control, VerticalAlignment.Center, expand));
            }
            return control;
        }

        /// <summary>
        /// 添加行堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalSpacing">行堆叠布局中各控件的间隙</param>
        /// <returns>创建的行堆叠布局</returns>
        public static StackLayout AddRowStackLayout(this StackLayout stackLayout, bool expand = false, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical };
            layout.Spacing = layout.Sizer(logicalSpacing);
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, VerticalAlignment.Center, expand));
            }
            return layout;
        }

        /// <summary>
        /// 添加列堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalSpacing">列堆叠布局中各控件的间隙</param>
        /// <returns>创建的列堆叠布局</returns>
        public static StackLayout AddColumnStackLayout(this StackLayout stackLayout, bool expand = false, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal };
            layout.Spacing = layout.Sizer(logicalSpacing);
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, VerticalAlignment.Center, expand));
            }
            return layout;
        }

        /// <summary>
        /// 添加表布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalSpacingX">表布局中各控件的横向间隙</param>
        /// <param name="logicalSpacingY">表布局中各控件的纵向间隙</param>
        /// <returns>创建的表布局</returns>
        public static TableLayout AddTableLayout(this StackLayout stackLayout, bool expand = false, int logicalSpacingX = 8, int logicalSpacingY = 8)
        {
            var layout = new TableLayout();
            layout.Spacing = new Size(layout.Sizer(logicalSpacingX), layout.Sizer(logicalSpacingY));
            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(layout, VerticalAlignment.Center, expand));
            }
            return layout;
        }
    }
}
