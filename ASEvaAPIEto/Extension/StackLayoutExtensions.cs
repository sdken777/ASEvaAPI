using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Extensions for stack layout
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 方便操作堆叠布局的扩展
    /// </summary>
    public static class StackLayoutExtensions
    {
        /// \~English
        /// <summary>
        /// Add a panel to fill the remaining space
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// \~Chinese
        /// <summary>
        /// 添加空间延布局方向撑满
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        public static void AddSpace(this StackLayout stackLayout)
        {
            stackLayout.Items.Add(new StackLayoutItem(new Panel(), true));
        }

        /// \~English
        /// <summary>
        /// Add a panel with the specified size
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="logicalSize">Space size</param>
        /// \~Chinese
        /// <summary>
        /// 延布局方向添加指定大小空间
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="logicalSize">指定大小空间</param>
        public static void AddSpace(this StackLayout stackLayout, int logicalSize)
        {
            if (stackLayout.Orientation == Orientation.Horizontal)
            {
                stackLayout.Items.Add(new StackLayoutItem(new Panel{ Width = stackLayout.Sizer(logicalSize) }));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(new Panel{ Height = stackLayout.Sizer(logicalSize) }));
            }
        }

        /// \~English
        /// <summary>
        /// Add separator
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// \~Chinese
        /// <summary>
        /// 添加分割线
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        public static void AddSeparator(this StackLayout stackLayout)
        {
            if (stackLayout.Orientation == Orientation.Horizontal)
            {
                stackLayout.Items.Add(new StackLayoutItem(new Panel{ Width = 1, BackgroundColor = Colors.LightGrey }, VerticalAlignment.Stretch));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(new Panel{ Height = 1, BackgroundColor = Colors.LightGrey }, HorizontalAlignment.Stretch));
            }
        }

        /// \~English
        /// <summary>
        /// Add label
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Text</param>
        /// <param name="expand">Whether to expand</param>
        /// <returns>Created label object</returns>
        /// \~Chinese
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
            stackLayout.Items.Add(new StackLayoutItem(label, expand));
            return label;
        }

        /// \~English
        /// <summary>
        /// Add label with the specified alignment and size
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Text</param>
        /// <param name="alignment">Text alignment</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Create label object</returns>
        /// \~Chinese
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
            if (logicalWidth > 0) label.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) label.SetLogicalHeight(logicalHeight);
            stackLayout.Items.Add(new StackLayoutItem(label, expand));
            return label;
        }

        /// \~English
        /// <summary>
        /// Add text button
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Text</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created text button object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加文字按键至堆叠布局
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
            if (logicalWidth > 0) button.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) button.SetLogicalHeight(logicalHeight);
            stackLayout.Items.Add(new StackLayoutItem(button, expand));
            return button;
        }
        /// \~English
        /// <summary>
        /// Add image button
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="image">Image</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created image button object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加图像按键至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="image">图像</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <returns>创建的按键对象</returns>
        public static Button AddButton(this StackLayout stackLayout, Image image, bool expand = false, int logicalWidth = 0, int logicalHeight = 0)
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
            stackLayout.Items.Add(new StackLayoutItem(button, expand));
            return button;
        }

        /// \~English
        /// <summary>
        /// Add text button panel
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Test</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <param name="logicalPadding">Logical padding between border and text</param>
        /// <returns>Created button panel object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加面板式文字按键至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <param name="logicalPadding">按钮边框与文字的间距</param>
        /// <returns>创建的面板式按钮对象</returns>
        public static ButtonPanel AddButtonPanel(this StackLayout stackLayout, String text, bool expand = false, int logicalWidth = 0, int logicalHeight = 0, int logicalPadding = 4)
        {
            if (text == null) text = "";
            var panel = new ButtonPanel(text, logicalPadding);
            stackLayout.AddControl(panel, expand, logicalWidth, logicalHeight);
            return panel;
        }
        /// \~English
        /// <summary>
        /// Add image button panel
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="image">Image</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <param name="logicalPadding">Logical padding between border and image</param>
        /// <returns>Created button panel object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加面板式图像按键至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="image">图像</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <param name="logicalPadding">按钮边框与图像的间距</param>
        /// <returns>创建的面板式按钮对象</returns>
        public static ButtonPanel AddButtonPanel(this StackLayout stackLayout, Bitmap image, bool expand = false, int logicalWidth = 0, int logicalHeight = 0, int logicalPadding = 4)
        {
            if (image == null) return AddButtonPanel(stackLayout, "", expand, logicalWidth, logicalHeight);
            if (Pixel.Scale != 1)
            {
                var w = Math.Max(1, (int)(image.Width * Pixel.Scale));
                var h = Math.Max(1, (int)(image.Height * Pixel.Scale));
                image = new Bitmap(image, w, h, ImageInterpolation.High);
            }
            var panel = new ButtonPanel(image, logicalPadding);
            stackLayout.AddControl(panel, expand, logicalWidth, logicalHeight);
            return panel;
        }

        /// \~English
        /// <summary>
        /// Add link button
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Text</param>
        /// <param name="expand">Whether to expand</param>
        /// <returns>Created link button object</returns>
        /// \~Chinese
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
            stackLayout.Items.Add(new StackLayoutItem(button, expand));
            return button;
        }

        /// \~English
        /// <summary>
        /// Add check box
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="text">Text</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="isChecked">Initial check state</param>
        /// <returns>Created check box object</returns>
        /// \~Chinese
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
            stackLayout.Items.Add(new StackLayoutItem(checkBox, expand));
            return checkBox;
        }

        /// \~English
        /// <summary>
        /// Add radio button list
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="texts">Text of each item</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="selectedIndex">Initial selected item index</param>
        /// <param name="logicalSpacing">Space (in logical size) between items</param>
        /// <returns>Created radio button list</returns>
        /// \~Chinese
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
            foreach (var text in texts) radioButtonList.Items.Add(text);

            if (selectedIndex >= 0 && selectedIndex < texts.Length) radioButtonList.SelectedIndex = selectedIndex;
            else radioButtonList.SelectedIndex = 0;

            stackLayout.Items.Add(new StackLayoutItem(radioButtonList, expand));
            return radioButtonList;
        }

        /// \~English
        /// <summary>
        /// Add combo box
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="texts">Text of each item (can be null)</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="selectedIndex">Initial selected index</param>
        /// <returns>Created combo box object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加组合框至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="texts">组合框各选项的文字，可为空</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this StackLayout stackLayout, String[] texts, bool expand = false, int selectedIndex = 0)
        {
            if (texts == null) texts = new String[0];

            var comboBox = new ComboBox { ReadOnly = true };
            foreach (var text in texts) comboBox.Items.Add(text);

            if (selectedIndex >= 0 && selectedIndex < texts.Length) comboBox.SelectedIndex = selectedIndex;
            else if (texts.Length > 0) comboBox.SelectedIndex = 0;

            stackLayout.Items.Add(new StackLayoutItem(comboBox, expand));
            return comboBox;
        }

        /// \~English
        /// <summary>
        /// Add combo box
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="texts">Text of each item (can be null)</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="selectedIndex">Initial selected index</param>
        /// <returns>Created combo box object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加组合框至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="texts">组合框各选项的文字，可为空</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="selectedIndex">初始的选项序号</param>
        /// <returns>创建的组合框对象</returns>
        public static ComboBox AddComboBox(this StackLayout stackLayout, String[] texts, int logicalWidth, int selectedIndex = 0)
        {
            if (texts == null) texts = new String[0];

            var comboBox = new ComboBox { ReadOnly = true };
            foreach (var text in texts) comboBox.Items.Add(text);

            if (logicalWidth > 0) comboBox.SetLogicalWidth(logicalWidth);

            if (selectedIndex >= 0 && selectedIndex < texts.Length) comboBox.SelectedIndex = selectedIndex;
            else if (texts.Length > 0) comboBox.SelectedIndex = 0;

            stackLayout.Items.Add(new StackLayoutItem(comboBox, false));
            return comboBox;
        }

        /// \~English
        /// <summary>
        /// Add group box
        /// </summary>
        /// <param name="stackLayout">Stack layout</param>
        /// <param name="title">Title</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <returns>Created group box object</returns>
        /// \~Chinese
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
            if (logicalWidth > 0) groupBox.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) groupBox.SetLogicalHeight(logicalHeight);
            stackLayout.Items.Add(new StackLayoutItem(groupBox, expand));
            return groupBox;
        }

        /// \~English
        /// <summary>
        /// Add control
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="control">Control object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// \~Chinese
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
            if (logicalWidth > 0) control.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) control.SetLogicalHeight(logicalHeight);
            stackLayout.Items.Add(new StackLayoutItem(control, expand));
            return control;
        }

        /// \~English
        /// <summary>
        /// Add horizontal child stack layout
        /// </summary>
        /// <param name="stackLayout">Parent stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the child layout</param>
        /// <param name="alignment">Alignment of controls in the child layout</param>
        /// <returns>Created child StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加横向堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalSpacing">横向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">横向堆叠布局中各控件的纵向对齐方式</param>
        /// <returns>创建的横向堆叠布局</returns>
        public static StackLayout AddRowLayout(this StackLayout stackLayout, bool expand = false, int logicalSpacing = 8, VerticalAlignment alignment = VerticalAlignment.Center)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal, VerticalContentAlignment = alignment };
            layout.Spacing = layout.Sizer(logicalSpacing);
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add horizontal child stack layout with the specified size
        /// </summary>
        /// <param name="stackLayout">Parent stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the child layout</param>
        /// <param name="alignment">Alignment of controls in the child layout</param>
        /// <returns>Created child StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加横向堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <param name="logicalSpacing">横向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">横向堆叠布局中各控件的纵向对齐方式</param>
        /// <returns>创建的横向堆叠布局</returns>
        public static StackLayout AddRowLayout(this StackLayout stackLayout, bool expand, int logicalWidth, int logicalHeight, int logicalSpacing, VerticalAlignment alignment = VerticalAlignment.Center)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal, VerticalContentAlignment = alignment };
            if (logicalWidth > 0) layout.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) layout.SetLogicalHeight(logicalHeight);
            layout.Spacing = layout.Sizer(logicalSpacing);
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add vertical child stack layout
        /// </summary>
        /// <param name="stackLayout">Parent stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the child layout</param>
        /// <param name="alignment">Alignment of controls in the child layout</param>
        /// <returns>Created StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加纵向堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalSpacing">纵向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">纵向堆叠布局中各控件的横向对齐方式</param>
        /// <returns>创建的纵向堆叠布局</returns>
        public static StackLayout AddColumnLayout(this StackLayout stackLayout, bool expand = false, int logicalSpacing = 8, HorizontalAlignment alignment = HorizontalAlignment.Stretch)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical, HorizontalContentAlignment = alignment };
            layout.Spacing = layout.Sizer(logicalSpacing);
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add vertical child stack layout with the specified size
        /// </summary>
        /// <param name="stackLayout">Parent stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <param name="logicalSpacing">Space (in logical size) between controls in the child layout</param>
        /// <param name="alignment">Alignment of controls in the child layout</param>
        /// <returns>Created StackLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加纵向堆叠布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <param name="logicalSpacing">纵向堆叠布局中各控件的间隙</param>
        /// <param name="alignment">纵向堆叠布局中各控件的横向对齐方式</param>
        /// <returns>创建的纵向堆叠布局</returns>
        public static StackLayout AddColumnLayout(this StackLayout stackLayout, bool expand, int logicalWidth, int logicalHeight, int logicalSpacing, HorizontalAlignment alignment = HorizontalAlignment.Stretch)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical, HorizontalContentAlignment = alignment };
            if (logicalWidth > 0) layout.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) layout.SetLogicalHeight(logicalHeight);
            layout.Spacing = layout.Sizer(logicalSpacing);
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add table layout
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalSpacingX">Space (in logical size) between controls along X axis</param>
        /// <param name="logicalSpacingY">Space (in logical size) between controls along Y axis</param>
        /// <returns>Created TableLayout object</returns>
        /// \~Chinese
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
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }

        /// \~English
        /// <summary>
        /// Add table layout with the specified size
        /// </summary>
        /// <param name="stackLayout">Stack layout object</param>
        /// <param name="expand">Whether to expand</param>
        /// <param name="logicalWidth">Initial logical width, 0 as not to set</param>
        /// <param name="logicalHeight">Initial logical height, 0 as not to set</param>
        /// <param name="logicalSpacingX">Space (in logical size) between controls along X axis</param>
        /// <param name="logicalSpacingY">Space (in logical size) between controls along Y axis</param>
        /// <returns>Created TableLayout object</returns>
        /// \~Chinese
        /// <summary>
        /// 添加表布局至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <param name="logicalWidth">初始宽度，0表示不设置</param>
        /// <param name="logicalHeight">初始高度，0表示不设置</param>
        /// <param name="logicalSpacingX">表布局中各控件的横向间隙</param>
        /// <param name="logicalSpacingY">表布局中各控件的纵向间隙</param>
        /// <returns>创建的表布局</returns>
        public static TableLayout AddTableLayout(this StackLayout stackLayout, bool expand, int logicalWidth, int logicalHeight, int logicalSpacingX, int logicalSpacingY)
        {
            var layout = new TableLayout();
            if (logicalWidth > 0) layout.SetLogicalWidth(logicalWidth);
            if (logicalHeight > 0) layout.SetLogicalHeight(logicalHeight);
            layout.Spacing = new Size(layout.Sizer(logicalSpacingX), layout.Sizer(logicalSpacingY));
            stackLayout.Items.Add(new StackLayoutItem(layout, expand));
            return layout;
        }
    }
}
