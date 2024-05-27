using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:eto=3.0.0) Vertical flow layout
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.0) 沿纵向添加的控件列表
    /// </summary>
    public class FlowLayout : Panel, FlowLayoutCallback
    {
        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowLayout()
        {
            Selectable = true;
            
            if (Factory == null) Factory = new DefaultFlowLayoutFactory();

            Control etoControl = null;
            Factory.CreateFlowLayoutBackend(this, out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
        }

        /// \~English
        /// <summary>
        /// Add control
        /// </summary>
        /// <param name="control">Target control object, do nothing if it's already in the layout</param>
        /// <param name="logicalHeight">Target logical height, at least 4</param>
        /// \~Chinese
        /// <summary>
        /// 在底部添加控件
        /// </summary>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void AddControl(Control control, int logicalHeight)
        {
            if (control == null) return;
            if (controls.Exists(c => c.Control.Equals(control))) return;
            logicalHeight = Math.Max(4, logicalHeight);
            controls.Add(new ControlContext{ Control = control, Visible = true });
            if (backend != null) backend.AddControl(control, logicalHeight);
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.1.1) Add control
        /// </summary>
        /// <param name="control">Target control object, do nothing if it's already in the layout</param>
        /// <param name="logicalHeight">Target logical height, at least 4</param>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.1.1) 在底部添加控件
        /// </summary>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void AddControl(ControlAndMouseSources control, int logicalHeight)
        {
            if (control == null || control.Control == null) return;
            if (controls.Exists(c => c.Control.Equals(control.Control))) return;
            logicalHeight = Math.Max(4, logicalHeight);
            controls.Add(new ControlContext{ Control = control.Control, Visible = true });
            if (backend != null)
            {
                backend.AddControl(control.Control, logicalHeight);
                if (control.MouseSources != null && !App.CanParentReceiveChildEvents)
                {
                    foreach (var source in control.MouseSources)
                    {
                        source.MouseDown += delegate { SelectControl(GetControlIndex(control.Control), true); };
                    }
                }
            }
        }

        /// \~English
        /// <summary>
        /// Insert control at index
        /// </summary>
        /// <param name="index">Specified control index (If it's over the bound, insert to index 0 or add to last position)</param>
        /// <param name="control">Target control object, do nothing if it's already in the layout</param>
        /// <param name="logicalHeight">Target logical height, at least 4</param>
        /// \~Chinese
        /// <summary>
        /// 在指定序号位置添加控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则添加至顶部或底部</param>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void InsertControl(int index, Control control, int logicalHeight)
        {
            if (control == null) return;
            if (controls.Exists(c => c.Control.Equals(control))) return;
            if (index >= controls.Count) AddControl(control, logicalHeight);
            else
            {
                index = Math.Max(0, index);
                logicalHeight = Math.Max(4, logicalHeight);
                controls.Insert(index, new ControlContext{ Control = control, Visible = true });
                if (backend != null) backend.InsertControl(index, control, logicalHeight);
            }
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.1.1) Insert control at index
        /// </summary>
        /// <param name="index">Specified control index (If it's over the bound, insert to index 0 or add to last position)</param>
        /// <param name="control">Target control object, do nothing if it's already in the layout</param>
        /// <param name="logicalHeight">Target logical height, at least 4</param>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.1.1) 在指定序号位置添加控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则添加至顶部或底部</param>
        /// <param name="control">目标控件，若已存在则不添加</param>
        /// <param name="logicalHeight">目标控件的高度，至少为4</param>
        public void InsertControl(int index, ControlAndMouseSources control, int logicalHeight)
        {
            if (control == null || control.Control == null) return;
            if (controls.Exists(c => c.Control.Equals(control.Control))) return;
            if (index >= controls.Count) AddControl(control, logicalHeight);
            else
            {
                index = Math.Max(0, index);
                logicalHeight = Math.Max(4, logicalHeight);
                controls.Insert(index, new ControlContext{ Control = control.Control, Visible = true });
                if (backend != null) 
                {
                    backend.InsertControl(index, control.Control, logicalHeight);
                    if (control.MouseSources != null && !App.CanParentReceiveChildEvents)
                    {
                        foreach (var source in control.MouseSources)
                        {
                            source.MouseDown += delegate { SelectControl(GetControlIndex(control.Control), true); };
                        }
                    }
                }
            }
        }

        /// \~English
        /// <summary>
        /// Get number of controls
        /// </summary>
        /// <returns>Number of controls</returns>
        /// \~Chinese
        /// <summary>
        /// 获取控件个数
        /// </summary>
        /// <returns>控件个数</returns>
        public int GetControlCount()
        {
            return controls.Count;
        }

        /// \~English
        /// <summary>
        /// Get index of the target control
        /// </summary>
        /// <param name="control">The target control</param>
        /// <returns>Index, -1 if it's not in the layout</returns>
        /// \~Chinese
        /// <summary>
        /// 获取指定控件的序号位置
        /// </summary>
        /// <param name="control">目标控件</param>
        /// <returns>序号位置，若不存在则返回-1</returns>
        public int GetControlIndex(Control control)
        {
            return controls.FindIndex(c => c.Control.Equals(control));
        }

        /// \~English
        /// <summary>
        /// Get control at the index
        /// </summary>
        /// <param name="index">Index of control</param>
        /// <returns>Target control, null if it's over the bound</returns>
        /// \~Chinese
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

        /// \~English
        /// <summary>
        /// Remove control at the index
        /// </summary>
        /// <param name="index">Index of control, do nothing if it's over the bound</param>
        /// \~Chinese
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
                if (backend != null) backend.RemoveControl(index);
            }
        }

        /// \~English
        /// <summary>
        /// Remove all controls
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 移除所有控件
        /// </summary>
        public void RemoveAllControls()
        {
            controls.Clear();
            selectedControl = null;
            if (backend != null) backend.RemoveAllControls();
        }

        /// \~English
        /// <summary>
        /// Set visibility of the control at the index
        /// </summary>
        /// <param name="index">Index of control, do nothing if over the bound</param>
        /// <param name="visible">Visibility of the control</param>
        /// \~Chinese
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
                if (backend != null) backend.SetControlVisible(index, visible);
            }
        }

        /// \~English
        /// <summary>
        /// Select the control at the index
        /// </summary>
        /// <param name="index">Index of control, do nothing if over the bound</param>
        /// <param name="invokeEvent">Whether trigger ControlSelected event if newly selected</param>
        /// \~Chinese
        /// <summary>
        /// 选中指定序号位置处的控件
        /// </summary>
        /// <param name="index">指定序号位置，超出范围则忽略</param>
        /// <param name="invokeEvent">新选中后是否触发ControlSelected事件</param>
        public void SelectControl(int index, bool invokeEvent)
        {
            if (Selectable && index >= 0 && index < controls.Count && index != GetSelectedControlIndex())
            {
                selectedControl = controls[index].Control;
                if (backend != null) backend.SelectControl(index);
                if (invokeEvent && ControlSelected != null) ControlSelected(this, null);
            }
        }
        
        /// \~English
        /// <summary>
        /// Get index of the selected control
        /// </summary>
        /// <returns>Index of selected control, -1 if no control is selected</returns>
        /// \~Chinese
        /// <summary>
        /// 获取选中控件所在序号位置
        /// </summary>
        /// <returns>序号位置，若无选中则返回-1</returns>
        public int GetSelectedControlIndex()
        {
            if (selectedControl == null) return -1;
            else return controls.FindIndex(c => c.Control.Equals(selectedControl));
        }

        /// \~English
        /// <summary>
        /// Trigger event while newly selected
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 新选中控件后触发事件
        /// </summary>
        public event EventHandler ControlSelected;

        /// \~English
        /// <summary>
        /// Whether selectable
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 是否可选中控件
        /// </summary>
        public bool Selectable { get; set; }

        public void OnControlClicked(int index)
        {
            if (Selectable) SelectControl(index, true);
        }

        public static FlowLayoutFactory Factory { private get; set; }

		private FlowLayoutBackend backend;

        class ControlContext
        {
            public Control Control { get; set; }
            public bool Visible { get; set; }
        }

        private List<ControlContext> controls = new List<ControlContext>();
        private Control selectedControl = null;
    }

	public interface FlowLayoutCallback
	{
        void OnControlClicked(int index);
	}

	public interface FlowLayoutBackend
	{
        void AddControl(Control control, int logicalHeight);
        void InsertControl(int index, Control control, int logicalHeight);
        void RemoveControl(int index);
        void RemoveAllControls();
        void SetControlVisible(int index, bool visible);
        void SelectControl(int index);
	}

	public interface FlowLayoutFactory
	{
		void CreateFlowLayoutBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayoutBackend backend);
	}

    public class DefaultFlowLayoutFactory : FlowLayoutFactory
    {
        public void CreateFlowLayoutBackend(FlowLayoutCallback callback, out Control etoControl, out FlowLayoutBackend backend)
        {
            var control = new DefaultFlowLayoutBackend(callback);
            etoControl = control;
            backend = control;
        }
    }

    class DefaultFlowLayoutBackend : Scrollable, FlowLayoutBackend
    {
        public DefaultFlowLayoutBackend(FlowLayoutCallback callback)
        {
            this.callback = callback;
            layout = this.SetContentAsColumnLayout(4, 4);
        }

        public void AddControl(Control control, int logicalHeight)
        {
            var panel = new Panel();
            panel.SetLogicalHeight(logicalHeight + 2);

            var borders = new Control[4];
            var cLayout = panel.SetContentAsColumnLayout(0, 0);
            borders[0] = cLayout.AddControl(new Panel(), false, 0, 1);
            var hLayout = cLayout.AddRowLayout(true, 0, VerticalAlignment.Stretch);
            borders[1] = hLayout.AddControl(new Panel(), false, 1, 0);
            hLayout.AddControl(control, true);
            borders[2] = hLayout.AddControl(new Panel(), false, 1, 0);
            borders[3] = cLayout.AddControl(new Panel(), false, 0, 1);

            var item = new StackLayoutItem(panel);
            ctxs.Add(new ControlContext{ Item = item, Visible = true, Borders = borders });
            layout.Items.Add(item);
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

            var borders = new Control[4];
            var cLayout = panel.SetContentAsColumnLayout(0, 0);
            borders[0] = cLayout.AddControl(new Panel(), false, 0, 1);
            var hLayout = cLayout.AddRowLayout(true, 0, VerticalAlignment.Stretch);
            borders[1] = hLayout.AddControl(new Panel(), false, 1, 0);
            hLayout.AddControl(control, true);
            borders[2] = hLayout.AddControl(new Panel(), false, 1, 0);
            borders[3] = cLayout.AddControl(new Panel(), false, 0, 1);

            var item = new StackLayoutItem(panel);
            ctxs.Insert(index, new ControlContext{ Item = item, Visible = true, Borders = borders });
            int visibleIndex = 0;
            foreach (var ctx in ctxs)
            {
                if (ctx.Item.Equals(item)) break;
                if (ctx.Visible) visibleIndex++;
            }
            layout.Items.Insert(visibleIndex, item);
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
            layout.Items.Clear();
            ctxs.Clear();
        }

        public void RemoveControl(int index)
        {
            if (ctxs[index].Item.Control.Equals(selectedControl)) selectedControl = null;
            layout.Items.Remove(ctxs[index].Item);
            ctxs.RemoveAt(index);
        }

        public void SelectControl(int index)
        {
            if (selectedControl != null)
            {
                var item = ctxs.Find(ctx => ctx.Item.Control.Equals(selectedControl));
                if (item != null)
                {
                    foreach (var control in item.Borders) control.BackgroundColor = Colors.Transparent;
                }
                selectedControl = null;
            }
            selectedControl = ctxs[index].Item.Control;
            foreach (var control in ctxs[index].Borders) control.BackgroundColor = Colors.Black;
        }

        public void SetControlVisible(int index, bool visible)
        {
            if (visible)
            {
                int visibleIndex = 0;
                for (int i = 0; i < index; i++)
                {
                    if (ctxs[i].Visible) visibleIndex++;
                }
                ctxs[index].Visible = true;
                layout.Items.Insert(visibleIndex, ctxs[index].Item);
            }
            else
            {
                layout.Items.Remove(ctxs[index].Item);
                ctxs[index].Visible = false;
            }
        }

        private class ControlContext
        {
            public StackLayoutItem Item { get; set; }
            public bool Visible { get; set; }
            public Control[] Borders { get; set; }
        }

        private FlowLayoutCallback callback;
        private StackLayout layout;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private Control selectedControl;
    }
}