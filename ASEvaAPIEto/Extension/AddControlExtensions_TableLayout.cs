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
        /// 表布局添加一行
        /// </summary>
        /// <param name="tableLayout">表布局</param>
        /// <param name="expandHeight">是否纵向撑满</param>
        /// <returns>表布局的行</returns>
        public static TableRow AddRow(this TableLayout tableLayout, bool expandHeight = false)
        {
            var row = new TableRow { ScaleHeight = expandHeight };
            tableLayout.Rows.Add(row);
            return row;
        }

        /// <summary>
        /// 添加空间延横向撑满
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        public static void AddSpace(this TableRow tableRow)
        {
            tableRow.Cells.Add(new TableCell(new Panel(), true));
        }

        /// <summary>
        /// 添加文字标签至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this TableRow tableRow, String text, bool expandWidth = false)
        {
            if (text == null) text = "";
            var label = new Label { Text = text, Wrap = WrapMode.None };

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(label, VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return label;
        }

        /// <summary>
        /// 添加文字标签至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="alignment">文字对齐</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this TableRow tableRow, String text, TextAlignment alignment, bool expandWidth = false, bool fillHeight = false, int logicalWidth = 0, int logicalHeight = 0)
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

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(label, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return label;
        }

        /// <summary>
        /// 添加按键至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的按键对象</returns>
        public static Button AddButton(this TableRow tableRow, String text, bool expandWidth = false, bool fillHeight = false, int logicalWidth = 0, int logicalHeight = 0)
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

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(button, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return button;
        }

        /// <summary>
        /// 添加链接式按键至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <returns>创建的链接式按键对象</returns>
        public static LinkButton AddLinkButton(this TableRow tableRow, String text, bool expandWidth = false, bool fillHeight = false)
        {
            if (text == null) text = "";
            var button = new LinkButton { Text = text };

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(button, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return button;
        }

        /// <summary>
        /// 添加多选框至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="isChecked">初始状态是否勾选</param>
        /// <returns>创建的多选框对象</returns>
        public static CheckBox AddCheckBox(this TableRow tableRow, String text, bool expandWidth = false, bool fillHeight = false, bool isChecked = false)
        {
            if (text == null) text = "";
            var checkBox = new CheckBox { Text = text, Checked = isChecked };

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(checkBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return checkBox;
        }

        /// <summary>
        /// 添加单选框组至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="texts">各单选框的文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="selectedIndex">初始的选中单选框序号</param>
        /// <param name="logicalSpacing">单选框间的间隙</param>
        /// <returns>创建的单选框组对象</returns>
        public static RadioButtonList AddRadioButtonList(this TableRow tableRow, String[] texts, bool expandWidth = false, bool fillHeight = false, int selectedIndex = 0, int logicalSpacing = 8)
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

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(radioButtonList, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return radioButtonList;
        }

        /// <summary>
        /// 添加组合框至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="texts">组合框各选项的文字</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this TableRow tableRow, String[] texts, bool expandWidth = false, bool fillHeight = false, int selectedIndex = 0)
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

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(comboBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return comboBox;
        }

        /// <summary>
        /// 添加分组框至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="title">标题</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的分组框对象</returns>
        public static GroupBox AddGroupBox(this TableRow tableRow, String title, bool expandWidth = false, bool fillHeight = false, int logicalWidth = 0, int logicalHeight = 0)
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

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(groupBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return groupBox;
        }

        /// <summary>
        /// 添加控件至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="control">控件</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        public static Control AddControl(this TableRow tableRow, Control control, bool expandWidth = false, bool fillHeight = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            if (logicalWidth > 0)
            {
                control.SetLogicalWidth(logicalWidth);
            }
            if (logicalHeight > 0)
            {
                control.SetLogicalHeight(logicalHeight);
            }

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(control, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return control;
        }

        /// <summary>
        /// 添加行堆叠布局至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalSpacing">行堆叠布局中各控件的间隙</param>
        /// <returns>创建的行堆叠布局</returns>
        public static StackLayout AddRowStackLayout(this TableRow tableRow, bool expandWidth = false, bool fillHeight = false, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical };
            layout.Spacing = layout.Sizer(logicalSpacing);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(layout, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return layout;
        }

        /// <summary>
        /// 添加列堆叠布局至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalSpacing">列堆叠布局中各控件的间隙</param>
        /// <returns>创建的列堆叠布局</returns>
        public static StackLayout AddColumnStackLayout(this TableRow tableRow, bool expandWidth = false, bool fillHeight = false, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal };
            layout.Spacing = layout.Sizer(logicalSpacing);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(layout, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return layout;
        }

        /// <summary>
        /// 添加表布局至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalSpacingX">表布局中各控件的横向间隙</param>
        /// <param name="logicalSpacingY">表布局中各控件的纵向间隙</param>
        /// <returns>创建的表布局</returns>
        public static TableLayout AddTableLayout(this TableRow tableRow, bool expandWidth = false, bool fillHeight = false, int logicalSpacingX = 8, int logicalSpacingY = 8)
        {
            var layout = new TableLayout();
            layout.Spacing = new Size(layout.Sizer(logicalSpacingX), layout.Sizer(logicalSpacingY));

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(layout, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));

            return layout;
        }
    }
}
