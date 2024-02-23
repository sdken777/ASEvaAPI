using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=2.0.2) Extensions for table layout
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=2.0.2) 方便操作表布局的扩展
    /// </summary>
    public static class TableLayoutExtensions
    {
        /// \~English
        /// <summary>
        /// Add a row to table layout
        /// </summary>
        /// <param name="tableLayout">Table layout object</param>
        /// <param name="expandHeight">Whether to expand</param>
        /// <returns>Row object of table layout</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Add a panel to fill the remaining space
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// \~Chinese
        /// <summary>
        /// 添加空间延横向撑满
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        public static void AddSpace(this TableRow tableRow)
        {
            tableRow.Cells.Add(new TableCell(new Panel(), true));
        }

        /// \~English
        /// <summary>
        /// Add label
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="text">Text</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <returns>Created label object</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Add label with the specified alignment and size
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="text">Text</param>
        /// <param name="alignment">Text alignment</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Create label object</returns>
        /// \~Chinese
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
            if (logicalWidth > 0) label.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) label.SetLogicalHeight(logicalHeight);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(label, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return label;
        }

        /// \~English
        /// <summary>
        /// Add text button
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="text">Text</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created text button object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加文字按键至表布局的行
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
            if (logicalWidth > 0) button.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) button.SetLogicalHeight(logicalHeight);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(button, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return button;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.0.4) Add image button
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="image">Image</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created image button object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.0.4) 添加图像按键至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="image">图像</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的按键对象</returns>
        public static Button AddButton(this TableRow tableRow, Image image, bool expandWidth = false, bool fillHeight = false, int logicalWidth = 0, int logicalHeight = 0)
        {
            var button = new Button();
            if (image != null)
            {
                if (Pixel.Scale == 1) button.Image = image;
                else
                {
                    var w = Math.Max(1, (int)(image.Width * Pixel.Scale));
                    var h = Math.Max(1, (int)(image.Height * Pixel.Scale));
                    button.Image = new Bitmap(image, w, h, ImageInterpolation.High);
                }
                button.ImagePosition = ButtonImagePosition.Above;
            }
            else button.Text = "";
            if (logicalWidth > 0) button.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) button.SetLogicalHeight(logicalHeight);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(button, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return button;
        }

        /// \~English
        /// <summary>
        /// Add link button
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="text">Text</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <returns>Created link button object</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Add check box
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="text">Text</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="isChecked">Initial check state</param>
        /// <returns>Created check box object</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Add radio button list
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="texts">Text of each item</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="selectedIndex">Initial selected item index</param>
        /// <param name="logicalSpacing">Space (in logical size) between items</param>
        /// <returns>Created radio button list</returns>
        /// \~Chinese
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
            foreach (var text in texts) radioButtonList.Items.Add(text);
            if (selectedIndex >= 0 && selectedIndex < texts.Length) radioButtonList.SelectedIndex = selectedIndex;
            else radioButtonList.SelectedIndex = 0;

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(radioButtonList, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return radioButtonList;
        }

        /// \~English
        /// <summary>
        /// Add combo box
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="texts">Text of each item (can be null)</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="selectedIndex">Initial selected index</param>
        /// <returns>Created combo box object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加组合框至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="texts">组合框各选项的文字，可为空</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this TableRow tableRow, String[] texts, bool expandWidth = false, bool fillHeight = false, int selectedIndex = 0)
        {
            if (texts == null) texts = new String[0];

            var comboBox = new ComboBox { ReadOnly = true };
            foreach (var text in texts) comboBox.Items.Add(text);
            
            if (selectedIndex >= 0 && selectedIndex < texts.Length) comboBox.SelectedIndex = selectedIndex;
            else if (texts.Length > 0) comboBox.SelectedIndex = 0;

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(comboBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return comboBox;
        }

        /// \~English
        /// <summary>
        /// (api:eto=2.14.3) Add combo box
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="texts">Text of each item (can be null)</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="selectedIndex">Initial selected index</param>
        /// <returns>Created combo box object</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=2.14.3) 添加组合框至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="texts">组合框各选项的文字，可为空</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this TableRow tableRow, String[] texts, int logicalWidth, bool fillHeight = false, int selectedIndex = 0)
        {
            if (texts == null) texts = new String[0];

            var comboBox = new ComboBox { ReadOnly = true };
            foreach (var text in texts) comboBox.Items.Add(text);

            if (logicalWidth > 0) comboBox.SetLogicalWidth(logicalWidth);
            
            if (selectedIndex >= 0 && selectedIndex < texts.Length) comboBox.SelectedIndex = selectedIndex;
            else if (texts.Length > 0) comboBox.SelectedIndex = 0;

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(comboBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, false));
            return comboBox;
        }

        /// \~English
        /// <summary>
        /// Add group box
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="title">Title</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created group box object</returns>
        /// \~Chinese
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
            if (logicalWidth > 0) groupBox.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) groupBox.SetLogicalHeight(logicalHeight);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(groupBox, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return groupBox;
        }

        /// \~English
        /// <summary>
        /// Add control
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="control">Control object</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// \~Chinese
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
            if (logicalWidth > 0) control.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) control.SetLogicalHeight(logicalHeight);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(control, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return control;
        }

        /// \~English
        /// <summary>
        /// Add horizontal stack layout
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the layout</param>
        /// <param name="alignment">Alignment of controls in the layout</param>
        /// <returns>Created StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加横向堆叠布局至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalSpacing">横向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">横向堆叠布局中各控件的纵向对齐方式</param>
        /// <returns>创建的横向堆叠布局</returns>
        public static StackLayout AddRowLayout(this TableRow tableRow, bool expandWidth = false, bool fillHeight = false, int logicalSpacing = 8, VerticalAlignment alignment = VerticalAlignment.Center)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal, VerticalContentAlignment = alignment };
            layout.Spacing = layout.Sizer(logicalSpacing);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(layout, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add vertical stack layout
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the layout</param>
        /// <param name="alignment">Alignment of controls in the layout</param>
        /// <returns>Created StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加纵向堆叠布局至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="expandWidth">是否横向撑满</param>
        /// <param name="fillHeight">是否纵向填满</param>
        /// <param name="logicalSpacing">纵向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">纵向堆叠布局中各控件的横向对齐方式</param>
        /// <returns>创建的纵向堆叠布局</returns>
        public static StackLayout AddColumnLayout(this TableRow tableRow, bool expandWidth = false, bool fillHeight = false, int logicalSpacing = 8, HorizontalAlignment alignment = HorizontalAlignment.Stretch)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical, HorizontalContentAlignment = alignment };
            layout.Spacing = layout.Sizer(logicalSpacing);

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(layout, fillHeight ? VerticalAlignment.Stretch : VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expandWidth));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add child table layout
        /// </summary>
        /// <param name="tableRow">Row object of table layout</param>
        /// <param name="expandWidth">Whether to expand horizontally</param>
        /// <param name="fillHeight">Whether to fill vertically</param>
        /// <param name="logicalSpacingX">Space (in logical size) between controls along X axis</param>
        /// <param name="logicalSpacingY">Space (in logical size) between controls along Y axis</param>
        /// <returns>Created child TableLayout object</returns>
        /// \~Chinese
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
