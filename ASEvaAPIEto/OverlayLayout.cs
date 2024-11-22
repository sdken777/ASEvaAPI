using System;
using System.Collections.Generic;
using Eto.Forms;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Overlay layout
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 可覆盖的布局
    /// </summary>
    public class OverlayLayout : PixelLayout
    {
        public OverlayLayout()
        {
            SizeChanged += this_SizeChanged;
        }

        /// \~English
        /// <summary>
        /// Add control
        /// </summary>
        /// <param name="control">The control object</param>
        /// <param name="topLogicalPadding">Space between top bound and the control, null as not related</param>
        /// <param name="bottomLogicalPadding">Space between bottom bound and the control, null as not related</param>
        /// <param name="leftLogicalPadding">Space between left bound and the control, null as not related</param>
        /// <param name="rightLogicalPadding">Space between right bound and the control, null as not related</param>
        /// <returns>新添加的控件</returns>
        /// \~Chinese
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
            if (control is GLView)
            {
                if (!(control as GLView).SupportOverlay) return null;
            }
            if (control is SkiaView)
            {
                if (!(control as SkiaView).SupportOverlay) return null;
            }

            if (paddingTable.ContainsKey(control))
            {
                RemoveControl(control);
                return AddControl(control, topLogicalPadding, bottomLogicalPadding, leftLogicalPadding, rightLogicalPadding);
            }

            var padding = new ControlPadding();
            padding.Top = topLogicalPadding;
            padding.Bottom = bottomLogicalPadding;
            padding.Left = leftLogicalPadding;
            padding.Right = rightLogicalPadding;
            paddingTable[control] = padding;
            
            if (DelayHandleControl)
            {
                Add(control, 0, 0);
                if (sizeInitialized) handleControl(control, false);
            }
            else
            {
                handleControl(control, true);
                if (sizeInitialized) handleAllControlsLater(0.02);
            }
            return control;
        }

        /// \~English
        /// <summary>
        /// Remove control
        /// </summary>
        /// <param name="control">The control</param>
        /// \~Chinese
        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="control">控件</param>
        public void RemoveControl(Control control)
        {
            if (paddingTable.ContainsKey(control)) paddingTable.Remove(control);
            Remove(control);
        }

        /// \~English
        /// <summary>
        /// Update control's position (Generally used after manual modification of the control's size)
        /// </summary>
        /// <param name="control">The control</param>
        /// \~Chinese
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
            if (sizeInitialized && DelayHandleControl)
            {
                handleAllControlsLater(0.1);
            }
            else
            {
                foreach (var control in paddingTable.Keys) handleControl(control, false);
            }
            sizeInitialized = true;
        }

        private void handleAllControlsLater(double interval)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            timer = new UITimer();
            timer.Interval = interval;
            timer.Elapsed += delegate
            {
                timer.Stop();
                timer = null;
                foreach (var control in paddingTable.Keys) handleControl(control, false);
            };
            timer.Start();
        }

        private void handleControl(Control control, bool add)
        {
            var padding = paddingTable[control];
            var visible = true; // at least 16x16

            // set size
            if (padding.Top != null && padding.Bottom != null)
            {
                var targetHeight = (int)(this.Height - (padding.Top.Value + padding.Bottom.Value) * Pixel.Scale) + (ExpandControlSize && padding.Bottom.Value == 0 ? 1 : 0);
                if (targetHeight < 16) visible = false;
                else control.Height = targetHeight;
            }

            if (visible && padding.Left != null && padding.Right != null)
            {
                var targetWidth = (int)(this.Width - (padding.Left.Value + padding.Right.Value) * Pixel.Scale) + (ExpandControlSize && padding.Right.Value == 0 ? 1 : 0);
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
                else posx = this.Width - (int)(padding.Right.Value * Pixel.Scale) - control.Width;
            }
            else posx = (int)(padding.Left.Value * Pixel.Scale);

            int posy = 0;
            if (padding.Top == null)
            {
                if (padding.Bottom == null) posy = (this.Height - control.Height) / 2;
                else posy = this.Height - (int)(padding.Bottom.Value * Pixel.Scale) - control.Height;
            }
            else posy = (int)(padding.Top.Value * Pixel.Scale);

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
        private UITimer timer = null;
        private bool sizeInitialized = false;

        public static bool DelayHandleControl { private get; set; }
        public static bool ExpandControlSize { private get; set; }
    }
}