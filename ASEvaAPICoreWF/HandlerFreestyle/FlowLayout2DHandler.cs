using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using ASEva.Utility;

namespace ASEva.UICoreWF
{
    class FlowLayout2DFactoryCoreWF : FlowLayout2DFactory
    {
        public void CreateFlowLayout2DBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayout2DBackend backend)
        {
            var panel = new FlowLayout2DBackendCoreWF(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }

    class FlowLayout2DBackendCoreWF : System.Windows.Forms.FlowLayoutPanel, FlowLayout2DBackend
    {
        public FlowLayout2DBackendCoreWF(FlowLayoutCallback callback)
        {
            FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AutoScroll = true;

            this.callback = callback;
            this.timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += delegate { updateToPanel(); };
            timer.Enabled = true;
        }

        public void SetControlWidth(int logicalWidth)
        {
            controlWidth = logicalWidth;
            foreach (var ctx in ctxs)
            {
                ctx.EtoControl.SetLogicalWidth(controlWidth);
            }
        }

        public void SetControlHeight(int index, int logicalHeight)
        {
            ctxs[index].EtoControl.SetLogicalHeight(logicalHeight);
        }

        public void UpdateControlsLayout(Size containerLogicalSize)
        {
        }

        public void AddControl(Control control, int logicalHeight)
        {
            control.SetLogicalSize(controlWidth, logicalHeight);
            var winformControl = control.ToNative(true);
            winformControl.Margin = new System.Windows.Forms.Padding(4);
            ctxs.Add(new ControlContext { EtoControl = control, WinformControl = winformControl, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void InsertControl(int index, Control control, int logicalHeight)
        {
            control.SetLogicalSize(controlWidth, logicalHeight);
            var winformControl = control.ToNative(true);
            winformControl.Margin = new System.Windows.Forms.Padding(4);
            ctxs.Insert(index, new ControlContext { EtoControl = control, WinformControl = winformControl, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void RemoveAllControls()
        {
            Controls.Clear();
            ctxs.Clear();
        }

        public void RemoveControl(int index)
        {
            Controls.Remove(ctxs[index].WinformControl);
            ctxs.RemoveAt(index);
        }

        public void SelectControl(int index)
        {
            if (selectedControl != null)
            {
                setBorderStyle(selectedControl, System.Windows.Forms.BorderStyle.None);
                selectedControl = null;
            }
            selectedControl = ctxs[index].WinformControl;
            setBorderStyle(selectedControl, System.Windows.Forms.BorderStyle.FixedSingle);
        }

        public void SetControlVisible(int index, bool visible)
        {
            ctxs[index].Visible = visible;
            if (!visible)
            {
                Controls.Remove(ctxs[index].WinformControl);
            }
        }

        public int GetControlWithMouse()
        {
            foreach (var ctx in ctxs)
            {
                if (!ctx.Visible) continue;
                var mouse = ctx.EtoControl.GetMouseLogicalPoint();
                var size = ctx.EtoControl.GetLogicalSize();
                if (mouse.X > 0 && mouse.Y > 0 && mouse.X < size.Width && mouse.Y < size.Height)
                {
                    return ctxs.IndexOf(ctx);
                }
            }
            return -1;
        }

        private void updateToPanel()
        {
            if (!Visible) return;

            try
            {
                int index = 0;
                foreach (var ctx in ctxs)
                {
                    if (ctx.Visible)
                    {
                        if (!Controls.Contains(ctx.WinformControl))
                        {
                            Controls.Add(ctx.WinformControl);
                            Controls.SetChildIndex(ctx.WinformControl, index);
                            break;
                        }
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Dump.Exception(ex);
                if (timer != null)
                {
                    timer.Stop();
                    timer = null;
                }
            }
        }

        private void setBorderStyle(System.Windows.Forms.Control control, System.Windows.Forms.BorderStyle border)
        {
            if (control is System.Windows.Forms.Panel) (control as System.Windows.Forms.Panel).BorderStyle = border;
            else if (control is System.Windows.Forms.UserControl) (control as System.Windows.Forms.UserControl).BorderStyle = border;
        }

        private class ControlContext
        {
            public System.Windows.Forms.Control WinformControl { get; set; }
            public Control EtoControl { get; set; }
            public bool Visible { get; set; }
        }

        private FlowLayoutCallback callback;
        private System.Windows.Forms.Timer timer;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private System.Windows.Forms.Control selectedControl = null;
        private int controlWidth;
    }
}
