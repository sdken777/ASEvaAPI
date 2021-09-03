using System;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.0) 方便设置面板Content的扩展 
    /// </summary>
    public static class SetContentExtensions
    {
        /// <summary>
        /// 设置行堆叠布局至面板Content
        /// </summary>
        /// <param name="panel">面板</param>
        /// <param name="logicalPadding">行堆叠布局的边缘空隙</param>
        /// <param name="logicalSpacing">行堆叠布局中各控件的间隙</param>
        /// <returns>创建的行堆叠布局</returns>
        public static StackLayout SetContentAsRowStackLayout(this Panel panel, int logicalPadding = 8, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Vertical };
            layout.Padding = layout.Sizer(logicalPadding);
            layout.Spacing = layout.Sizer(logicalSpacing);
            panel.Content = layout;
            return layout;
        }

        /// <summary>
        /// 设置列堆叠布局至面板Content
        /// </summary>
        /// <param name="panel">面板</param>
        /// <param name="logicalPadding">列堆叠布局的边缘空隙</param>
        /// <param name="logicalSpacing">列堆叠布局中各控件的间隙</param>
        /// <returns>创建的列堆叠布局</returns>
        public static StackLayout SetContentAsColumnStackLayout(this Panel panel, int logicalPadding = 8, int logicalSpacing = 8)
        {
            var layout = new StackLayout { Orientation = Orientation.Horizontal };
            layout.Padding = layout.Sizer(logicalPadding);
            layout.Spacing = layout.Sizer(logicalSpacing);
            panel.Content = layout;
            return layout;
        }

        /// <summary>
        /// 设置表布局至面板Content
        /// </summary>
        /// <param name="panel">面板</param>
        /// <param name="logicalPadding">表布局的边缘空隙</param>
        /// <param name="logicalSpacingX">表布局中各控件的横向间隙</param>
        /// <param name="logicalSpacingY">表布局中各控件的纵向间隙</param>
        /// <returns>创建的表布局</returns>
        public static TableLayout SetContentAsTableLayout(this Panel panel, int logicalPadding = 8, int logicalSpacingX = 8, int logicalSpacingY = 8)
        {
            var layout = new TableLayout();
            layout.Padding = layout.Sizer(logicalPadding);
            layout.Spacing = new Size(layout.Sizer(logicalSpacingX), layout.Sizer(logicalSpacingY));
            panel.Content = layout;
            return layout;
        }

        /// <summary>
        /// 设置像素布局至面板Content
        /// </summary>
        /// <param name="panel">面板</param>
        /// <returns>创建的像素布局</returns>
        public static PixelLayout SetContentAsPixelLayout(this Panel panel)
        {
            var layout = new PixelLayout();
            panel.Content = layout;
            return layout;
        }
    }
}