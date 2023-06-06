using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class FlowLayoutFactoryCoreWF : FlowLayoutFactory
    {
        public void CreateFlowLayoutBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayoutBackend backend)
        {
            var panel = new FlowLayoutBackendCoreWF(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }

    class FlowLayoutBackendCoreWF : System.Windows.Forms.FlowLayoutPanel, FlowLayoutBackend
    {
        public FlowLayoutBackendCoreWF(FlowLayoutCallback callback)
        {
            FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AutoScroll = true;

            this.callback = callback;
            this.timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += delegate { updateToPanel(); };
            timer.Enabled = true;
        }

        public void AddControl(Control control, int logicalHeight)
        {
            control.SetLogicalHeight(logicalHeight);
            var winformControl = control.ToNative(true) as System.Windows.Forms.Panel;

            if (winformControl == null)
            {
                control = new Panel { Content = control };
                control.SetLogicalHeight(logicalHeight);
                winformControl = control.ToNative(true) as System.Windows.Forms.Panel;
            }

            winformControl.Width = 1;
            winformControl.Margin = new System.Windows.Forms.Padding(2);
            ctxs.Add(new ControlContext { EtoControl = control, WinformControl = winformControl, Visible = true });
            control.MouseDown += (obj, args) =>
            {
                callback.OnControlClicked(ctxs.FindIndex(c => c.EtoControl.Equals(obj)));
            };
        }

        public void InsertControl(int index, Control control, int logicalHeight)
        {
            control.SetLogicalHeight(logicalHeight);
            var winformControl = control.ToNative(true) as System.Windows.Forms.Panel;

            if (winformControl == null)
            {
                control = new Panel { Content = control };
                control.SetLogicalHeight(logicalHeight);
                winformControl = control.ToNative(true) as System.Windows.Forms.Panel;
            }

            winformControl.Width = 1;
            winformControl.Margin = new System.Windows.Forms.Padding(2);
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
            removedFromPanel = true;
        }

        public void RemoveControl(int index)
        {
            Controls.Remove(ctxs[index].WinformControl);
            ctxs.RemoveAt(index);
            removedFromPanel = true;
        }

        public void SelectControl(int index)
        {
            if (selectedControl != null)
            {
                selectedControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                selectedControl = null;
            }
            selectedControl = ctxs[index].WinformControl;
            selectedControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        public void SetControlVisible(int index, bool visible)
        {
            ctxs[index].Visible = visible;
            if (!visible)
            {
                Controls.Remove(ctxs[index].WinformControl);
                removedFromPanel = true;
            }
        }

        private void updateToPanel()
        {
            bool added = false;
            int index = 0;
            foreach (var ctx in ctxs)
            {
                if (ctx.Visible)
                {
                    if (!Controls.Contains(ctx.WinformControl))
                    {
                        Controls.Add(ctx.WinformControl);
                        Controls.SetChildIndex(ctx.WinformControl, index);
                        added = true;
                        break;
                    }
                    index++;
                }
            }

            bool resized = false;
            if (Width > 0 && Height > 0 && (Width != lastSize.Width || Height != lastSize.Height))
            {
                lastSize = Size;
                resized = true;
            }

            if (added || resized || removedFromPanel)
            {
                removedFromPanel = false;

                if (lastSize.Width < 50) return;
                foreach (var ctx in ctxs)
                {
                    ctx.EtoControl.Width = lastSize.Width - 6 - (VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0);
                }
            }
        }

        private class ControlContext
        {
            public System.Windows.Forms.Panel WinformControl { get; set; }
            public Control EtoControl { get; set; }
            public bool Visible { get; set; }
        }

        private FlowLayoutCallback callback;
        private System.Windows.Forms.Timer timer;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private System.Windows.Forms.Panel selectedControl = null;
        private bool removedFromPanel = false;
        private System.Drawing.Size lastSize;
    }
}
