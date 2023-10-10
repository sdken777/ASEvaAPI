using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.9.1) 沿纵向添加的二维控件列表
    /// </summary>
    public class FlowLayout2D : Panel, FlowLayoutCallback
    {
        /// <summary>
        /// 构造函数，默认控件宽度为300
        /// </summary>
        public FlowLayout2D()
        {
            init(300);
        }

        /// <summary>
        /// 构造函数，并指定默认控件宽度
        /// </summary>
        /// <param name="controlLogicalWidth">默认控件宽度，至少为4</param>
        public FlowLayout2D(int controlLogicalWidth)
        {
            init(controlLogicalWidth);
        }

        private void init(int controlLogicalWidth)
        {
            if (Factory == null) Factory = new DefaultFlowLayout2DFactory();

            Control etoControl = null;
            Factory.CreateFlowLayout2DBackend(this, out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
            if (backend != null) backend.SetControlWidth(controlLogicalWidth);

            SizeChanged += delegate
            {
                if (delayTimer != null)
                {
                    delayTimer.Stop();
                    delayTimer = null;
                }
                delayTimer = new UITimer();
                delayTimer.Interval = 0.1;
                delayTimer.Elapsed += delegate
                {
                    if (backend != null) backend.UpdateControlsLayout(this.GetLogicalSize());
                };
                delayTimer.Start();
            };
        }

        /// <summary>
        /// 在底部添加控件
        /// </summary>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void AddControl(Control control, int logicalHeight)
        {
            if (controls.Exists(c => c.Control.Equals(control))) return;
            logicalHeight = Math.Max(4, logicalHeight);
            controls.Add(new ControlContext{ Control = control, Visible = true });
            if (backend != null)
            {
                backend.AddControl(control, logicalHeight);
                backend.UpdateControlsLayout(this.GetLogicalSize());
            }
        }

        /// <summary>
        /// 在指定序号位置添加控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则添加至顶部或底部</param>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void InsertControl(int index, Control control, int logicalHeight)
        {
            if (controls.Exists(c => c.Control.Equals(control))) return;
            if (index >= controls.Count) AddControl(control, logicalHeight);
            else
            {
                index = Math.Max(0, index);
                logicalHeight = Math.Max(4, logicalHeight);
                controls.Insert(index, new ControlContext{ Control = control, Visible = true });
                if (backend != null)
                {
                    backend.InsertControl(index, control, logicalHeight);
                    backend.UpdateControlsLayout(this.GetLogicalSize());
                }
            }
        }

        /// <summary>
        /// 更新控件尺寸
        /// </summary>
        /// <param name="controlLogicalWidth">默认控件宽度，至少为4</param>
        /// <param name="controlsLogicalHeight">需要更新的所有控件的高度表，键为控件的序号位置，值为控件高度，至少为4</param>
        public void UpdateControlsSize(int controlLogicalWidth, Dictionary<int, int> controlsLogicalHeight)
        {
            if (backend != null)
            {
                backend.SetControlWidth(Math.Max(4, controlLogicalWidth));
                foreach (var pair in controlsLogicalHeight)
                {
                    if (pair.Key < 0 || pair.Key >= controls.Count) continue;
                    backend.SetControlHeight(pair.Key, Math.Max(4, pair.Value));
                }
                backend.UpdateControlsLayout(this.GetLogicalSize());
            }
        }

        /// <summary>
        /// 获取控件个数
        /// </summary>
        /// <returns>控件个数</returns>
        public int GetControlCount()
        {
            return controls.Count;
        }

        /// <summary>
        /// 获取指定控件的序号位置
        /// </summary>
        /// <param name="control">目标控件</param>
        /// <returns>序号位置，若不存在则返回-1</returns>
        public int GetControlIndex(Control control)
        {
            return controls.FindIndex(c => c.Equals(control));
        }

        /// <summary>
        /// 获取指定序号位置上的控件
        /// </summary>
        /// <param name="index">指定序号位置</param>
        /// <returns>目标控件，若超出范围则返回null</returns>
        public Control GetControlAt(int index)
        {
            if (index >= 0 && index < controls.Count)
            {
                return controls[index].Control;
            }
            else return null;
        }

        /// <summary>
        /// 移除指定序号位置处的控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则不移除</param>
        public void RemoveControl(int index)
        {
            if (index >= 0 && index < controls.Count)
            {
                if (controls[index].Control.Equals(selectedControl)) selectedControl = null;
                controls.RemoveAt(index);
                if (backend != null)
                {
                    backend.RemoveControl(index);
                    backend.UpdateControlsLayout(this.GetLogicalSize());
                }
            }
        }

        /// <summary>
        /// 移除所有控件
        /// </summary>
        public void RemoveAllControls()
        {
            controls.Clear();
            selectedControl = null;
            if (backend != null) backend.RemoveAllControls();
        }

        /// <summary>
        /// 设置指定序号位置处的控件是否可见
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则忽略</param>
        /// <param name="visible">是否可见</param>
        public void SetControlVisible(int index, bool visible)
        {
            if (index >= 0 && index < controls.Count && controls[index].Visible != visible)
            {
                controls[index].Visible = visible;
                if (backend != null)
                {
                    backend.SetControlVisible(index, visible);
                    backend.UpdateControlsLayout(this.GetLogicalSize());
                }
            }
        }

        /// <summary>
        /// (api:eto=2.9.2) 获取鼠标所在控件的序号位置
        /// </summary>
        /// <returns>鼠标所在控件的序号位置，-1表示鼠标未在任何控件上</returns>
        public int GetControlWithMouse()
        {
            if (backend != null) return backend.GetControlWithMouse();
            else return -1;
        }

        /// <summary>
        /// 选中指定序号位置处的控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则忽略</param>
        /// <param name="invokeEvent">新选中后是否触发ControlSelected事件</param>
        public void SelectControl(int index, bool invokeEvent)
        {
            if (index >= 0 && index < controls.Count && index != GetSelectedControlIndex())
            {
                selectedControl = controls[index].Control;
                if (backend != null) backend.SelectControl(index);
                if (invokeEvent && ControlSelected != null) ControlSelected(this, null);
            }
        }
        
        /// <summary>
        /// 获取选中控件所在序号位置
        /// </summary>
        /// <returns>序号位置，若无选中则返回-1</returns>
        public int GetSelectedControlIndex()
        {
            if (selectedControl == null) return -1;
            else return controls.FindIndex(c => c.Control.Equals(selectedControl));
        }

        /// <summary>
        /// 新选中控件后触发事件
        /// </summary>
        public event EventHandler ControlSelected;

        public void OnControlClicked(int index)
        {
            SelectControl(index, true);
        }

        public static FlowLayout2DFactory Factory { private get; set; }

		private FlowLayout2DBackend backend;

        class ControlContext
        {
            public Control Control { get; set; }
            public bool Visible { get; set; }
        }

        private List<ControlContext> controls = new List<ControlContext>();
        private Control selectedControl = null;
        private UITimer delayTimer = null;
    }

	public interface FlowLayout2DBackend
	{
        void SetControlWidth(int logicalWidth);
        void SetControlHeight(int index, int logicalHeight);
        void UpdateControlsLayout(Size containerLogicalSize);
        void AddControl(Control control, int logicalHeight);
        void InsertControl(int index, Control control, int logicalHeight);
        void RemoveControl(int index);
        void RemoveAllControls();
        void SetControlVisible(int index, bool visible);
        void SelectControl(int index);
        int GetControlWithMouse();
	}

	public interface FlowLayout2DFactory
	{
		void CreateFlowLayout2DBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayout2DBackend backend);
	}

    class DefaultFlowLayout2DFactory : FlowLayout2DFactory
    {
        public void CreateFlowLayout2DBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayout2DBackend backend)
        {
            var control = new DefaultFlowLayout2DBackend(callback);
            etoControl = control;
            backend = control;
        }
    }

    public class DefaultFlowLayout2DBackend : Scrollable, FlowLayout2DBackend
    {
        public DefaultFlowLayout2DBackend(FlowLayoutCallback callback)
        {
            this.callback = callback;
            mainLayout = this.SetContentAsRowLayout(3, 6, VerticalAlignment.Top);
        }
 
        public void SetControlWidth(int logicalWidth)
        {
            controlWidth = logicalWidth;
        }

        public void SetControlHeight(int index, int logicalHeight)
        {
            ctxs[index].Item.Control.SetLogicalHeight(logicalHeight + 2);
            ctxs[index].LogicalHeight = logicalHeight;
        }

        public void UpdateControlsLayout(Size containerLogicalSize)
        {
            int containerWidth = 0, containerHeight = 0;
            if (FixedScrollBarSize != null)
            {
                containerWidth = containerLogicalSize.Width - FixedScrollBarSize.Value;
                containerHeight = containerLogicalSize.Height - FixedScrollBarSize.Value;
            }
            else
            {
                try
                {
                    containerWidth = Math.Min(containerLogicalSize.Width, (int)(VisibleRect.Width / Pixel.Scale - 6));
                    containerHeight = Math.Min(containerLogicalSize.Height, (int)(VisibleRect.Height / Pixel.Scale - 6));
                }
                catch (Exception) {}
            }
            if (containerWidth < 8 || containerHeight < 8) return;

            int itemWidth = controlWidth + 8;

            var identifierMap = new Dictionary<int, ControlContext>();
            var targetIdentifiers = new List<List<int>>();
            var colIdentifiers = new List<int>();
            int colHeight = 0;
            for (int i = 0; i < ctxs.Count; i++)
            {
                var ctx = ctxs[i];
                if (!ctx.Visible) continue;

                var itemHeight = ctx.LogicalHeight + 8;
                if (itemHeight >= containerHeight) continue;

                if (colHeight + itemHeight >= containerHeight)
                {
                    targetIdentifiers.Add(colIdentifiers);
                    colIdentifiers = new List<int>(); 
                    colHeight = 0;
                }

                identifierMap[ctx.Identifier] = ctx;
                colIdentifiers.Add(ctx.Identifier);
                colHeight += itemHeight;
            }
            if (colIdentifiers.Count > 0) targetIdentifiers.Add(colIdentifiers);

            mainLayout.SuspendLayout();

            if (curIdentifiers.Count > 0)
            {
                int i = curIdentifiers.Count - 1;
                while (i >= 0)
                {
                    if (i >= targetIdentifiers.Count)
                    {
                        curIdentifiers.RemoveAt(i);
                        mainLayout.Items.RemoveAt(i);
                    }
                    else
                    {
                        var curColIdentifiers = curIdentifiers[i];
                        var colLayout = mainLayout.Items[i].Control as StackLayout;
                        int j = curColIdentifiers.Count - 1;
                        colLayout.SuspendLayout();
                        while (j >= 0)
                        {
                            if (!targetIdentifiers[i].Contains(curColIdentifiers[j]))
                            {
                                curColIdentifiers.RemoveAt(j);
                                colLayout.Items.RemoveAt(j);
                            }
                            j--;
                        }
                        colLayout.ResumeLayout();
                    }
                    i--;
                }
            }

            for (int i = 0; i < targetIdentifiers.Count; i++)
            {
                if (i >= curIdentifiers.Count)
                {
                    var colLayout = mainLayout.AddColumnLayout(false, 6);
                    colLayout.SuspendLayout();
                    foreach (var identifier in targetIdentifiers[i])
                    {
                        var ctx = identifierMap[identifier];
                        ctx.Item.Control.SetLogicalWidth(controlWidth + 2);
                        colLayout.Items.Add(ctx.Item);
                    }
                    colLayout.ResumeLayout();
                    curIdentifiers.Add(targetIdentifiers[i]);
                }
                else
                {
                    var curColIdentifiers = curIdentifiers[i];
                    var colLayout = mainLayout.Items[i].Control as StackLayout;
                    int insertIndex = 0;
                    colLayout.SuspendLayout();
                    foreach (var identifier in targetIdentifiers[i])
                    {
                        var ctx = identifierMap[identifier];
                        ctx.Item.Control.SetLogicalWidth(controlWidth + 2);
                        if (curColIdentifiers.Contains(identifier))
                        {
                            insertIndex = curColIdentifiers.IndexOf(identifier) + 1;
                            continue;
                        }
                        if (insertIndex >= colLayout.Items.Count)
                        {
                            colLayout.Items.Add(ctx.Item);
                            curColIdentifiers.Add(identifier);
                        }
                        else
                        {
                            colLayout.Items.Insert(insertIndex, ctx.Item);
                            curColIdentifiers.Insert(insertIndex, identifier);
                        }
                        insertIndex++;
                    }
                    colLayout.ResumeLayout();
                }
            }

            mainLayout.ResumeLayout();
        }

        public void AddControl(Control control, int logicalHeight)
        {
            var panel = new Panel();
            panel.SetLogicalHeight(logicalHeight + 2);
            panel.SetContentAsColumnLayout(1, 0).AddControl(control, true);
            var item = new StackLayoutItem(panel);
            ctxs.Add(new ControlContext{ Identifier = ++identifierCount, Item = item, LogicalHeight = logicalHeight, Visible = true });
            panel.MouseDown += (obj, args) =>
            {
                for (int i = 0; i < ctxs.Count; i++)
                {
                    if (ctxs[i].Item.Control.Equals(obj))
                    {
                        callback.OnControlClicked(i);
                        return;
                    }
                }
            };
        }

        public void InsertControl(int index, Control control, int logicalHeight)
        {
            var panel = new Panel();
            panel.SetLogicalHeight(logicalHeight + 2);
            panel.SetContentAsColumnLayout(1, 0).AddControl(control, true);
            var item = new StackLayoutItem(panel);
            ctxs.Insert(index, new ControlContext{ Identifier = ++identifierCount, Item = item, LogicalHeight = logicalHeight, Visible = true });
            int visibleIndex = 0;
            foreach (var ctx in ctxs)
            {
                if (ctx.Item.Equals(item)) break;
                if (ctx.Visible) visibleIndex++;
            }
            panel.MouseDown += (obj, args) =>
            {
                for (int i = 0; i < ctxs.Count; i++)
                {
                    if (ctxs[i].Item.Control.Equals(obj))
                    {
                        callback.OnControlClicked(i);
                        return;
                    }
                }
            };
        }

        public void RemoveAllControls()
        {
            selectedControl = null;
            mainLayout.Items.Clear();
            ctxs.Clear();
            curIdentifiers.Clear();
        }

        public void RemoveControl(int index)
        {
            if (ctxs[index].Item.Control.Equals(selectedControl)) selectedControl = null;
            ctxs.RemoveAt(index);
        }

        public void SelectControl(int index)
        {
            if (selectedControl != null)
            {
                selectedControl.BackgroundColor = Colors.Transparent;
                selectedControl = null;
            }
            selectedControl = ctxs[index].Item.Control;
            selectedControl.BackgroundColor = Colors.Black;
        }

        public void SetControlVisible(int index, bool visible)
        {
            ctxs[index].Visible = visible;
        }

        public int GetControlWithMouse()
        {
            foreach (var ctx in ctxs)
            {
                if (!ctx.Visible) continue;
                var mouse = ctx.Item.Control.GetMouseLogicalPoint();
                var size = ctx.Item.Control.GetLogicalSize();
                if (mouse.X > 0 && mouse.Y > 0 && mouse.X < size.Width && mouse.Y < size.Height) return ctxs.IndexOf(ctx);
            }
            return -1;
        }

        private class ControlContext
        {
            public int Identifier { get; set; }
            public StackLayoutItem Item { get; set; }
            public int LogicalHeight { get; set; }
            public bool Visible { get; set; }
        }

        private FlowLayoutCallback callback;
        private StackLayout mainLayout;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private List<List<int>> curIdentifiers = new List<List<int>>();
        private Control selectedControl;
        private int controlWidth;

        private int identifierCount = 0;

        public static int? FixedScrollBarSize { private get; set; }
    }
}