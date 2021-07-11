using System;
using Eto.Forms;

namespace ASEva.Eto
{
    /// <summary>
    /// (api:eto=1.0.0) 方便添加控件的扩展
    /// </summary>
    public static class AddControlExtensions
    {
        /// <summary>
        /// 添加文字至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否延布局方向撑满</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this StackLayout stackLayout, String text, bool expand = false)
        {
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
        /// 添加控件至堆叠布局
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        /// <param name="control">控件</param>
        /// <param name="expand">是否延布局方向撑满</param>
        public static void AddControl(this StackLayout stackLayout, Control control, bool expand = false)
        {
            if (control is GroupBox)
            {
                (control as GroupBox).SetTitleFontToContent();
            }

            if (stackLayout.Orientation == Orientation.Vertical)
            {
                stackLayout.Items.Add(new StackLayoutItem(control, HorizontalAlignment.Stretch, expand));
            }
            else
            {
                stackLayout.Items.Add(new StackLayoutItem(control, VerticalAlignment.Center, expand));
            }
        }

        /// <summary>
        /// 添加空间延布局方向撑满
        /// </summary>
        /// <param name="stackLayout">堆叠布局</param>
        public static void AddSpace(this StackLayout stackLayout)
        {
            stackLayout.Items.Add(new StackLayoutItem(new Panel(), true));
        }

        /// <summary>
        /// 添加文字至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="text">文字</param>
        /// <param name="expand">是否横向撑满</param>
        /// <returns>创建的文字标签对象</returns>
        public static Label AddLabel(this TableRow tableRow, String text, bool expand = false)
        {
            var label = new Label { Text = text, Wrap = WrapMode.None };

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(label, VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expand));

            return label;
        }

        /// <summary>
        /// 添加控件至表布局的行
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        /// <param name="control">控件</param>
        /// <param name="expand">是否横向撑满</param>
        public static void AddControl(this TableRow tableRow, Control control, bool expand = false)
        {
            if (control is GroupBox)
            {
                (control as GroupBox).SetTitleFontToContent();
            }

            var stackLayout = new StackLayout() { Orientation = Orientation.Horizontal };
            stackLayout.Items.Add(new StackLayoutItem(control, VerticalAlignment.Center, true));

            tableRow.Cells.Add(new TableCell(stackLayout, expand));
        }

        /// <summary>
        /// 添加空间延横向撑满
        /// </summary>
        /// <param name="tableRow">表布局的行</param>
        public static void AddSpace(this TableRow tableRow)
        {
            tableRow.Cells.Add(new TableCell(new Panel(), true));
        }
    }
}
