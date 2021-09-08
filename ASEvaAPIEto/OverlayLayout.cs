using System;
using System.Collections.Generic;
using Eto.Forms;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.0.4) 可覆盖的布局
    /// </summary>
    public class OverlayLayout : PixelLayout
    {
        public OverlayLayout()
        {
            SizeChanged += this_SizeChanged;
        }

        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="topLogicalPadding">控件与顶部间隔，null表示不关联</param>
        /// <param name="bottomLogicalPadding">控件与底部间隔，null表示不关联</param>
        /// <param name="leftLogicalPadding">控件与左侧间隔，null表示不关联</param>
        /// <param name="rightLogicalPadding">控件与右侧间隔，null表示不关联</param>
        /// <returns>新添加的控件</returns>
        public Control AddControl(Control control, int? topLogicalPadding, int? bottomLogicalPadding, int? leftLogicalPadding, int? rightLogicalPadding)
        {
            ControlPadding padding = null;
            if (!paddingTable.ContainsKey(control)) paddingTable[control] = new ControlPadding();
            padding = paddingTable[control];

            padding.Top = topLogicalPadding;
            padding.Bottom = bottomLogicalPadding;
            padding.Left = leftLogicalPadding;
            padding.Right = rightLogicalPadding;

            handleControl(control, true);
            return control;
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="control">控件</param>
        public void RemoveControl(Control control)
        {
            if (paddingTable.ContainsKey(control)) paddingTable.Remove(control);
            Remove(control);
        }

        /// <summary>
        /// 更新控件位置（一般在手动修改控件大小后调用）
        /// </summary>
        /// <param name="control">控件</param>
        public void UpdatePosition(Control control)
        {
            if (paddingTable.ContainsKey(control)) handleControl(control, false);
        }

        private void this_SizeChanged(object sender, EventArgs e)
        {
            foreach (var control in paddingTable.Keys) handleControl(control, false);
        }

        private void handleControl(Control control, bool add)
        {
            var padding = paddingTable[control];
            var visible = true; // at least 16x16

            // set size
            if (padding.Top != null && padding.Bottom != null)
            {
                var targetHeight = (int)(this.Height - (padding.Top.Value + padding.Bottom.Value) * SizerExtensions.PixelScale);
                if (targetHeight < 16) visible = false;
                else control.Height = targetHeight;
            }

            if (visible && padding.Left != null && padding.Right != null)
            {
                var targetWidth = (int)(this.Width - (padding.Left.Value + padding.Right.Value) * SizerExtensions.PixelScale);
                if (targetWidth < 16) visible = false;
                else control.Width = targetWidth;
            }

            if (!visible)
            {
                control.Visible = false;
                if (add) Add(control, 0, 0);
                return;
            }
            else control.Visible = true;

            // set position
            int posx = 0;
            if (padding.Left == null)
            {
                if (padding.Right == null) posx = (this.Width - control.Width) / 2;
                else posx = this.Width - (int)(padding.Right.Value * SizerExtensions.PixelScale) - control.Width;
            }
            else posx = (int)(padding.Left.Value * SizerExtensions.PixelScale);

            int posy = 0;
            if (padding.Top == null)
            {
                if (padding.Bottom == null) posy = (this.Height - control.Height) / 2;
                else posy = this.Height - (int)(padding.Bottom.Value * SizerExtensions.PixelScale) - control.Height;
            }
            else posy = (int)(padding.Top.Value * SizerExtensions.PixelScale);

            if (add) Add(control, posx, posy);
            else Move(control, posx, posy);
        }

        private class ControlPadding
        {
            public int? Top { get; set; }
            public int? Bottom { get; set; }
            public int? Left { get; set; }
            public int? Right { get; set; }
        }

        private Dictionary<Control, ControlPadding> paddingTable = new Dictionary<Control, ControlPadding>();
    }
}