using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace ASEva.UIEto
{
    /// <summary>
    /// (api:eto=2.9.0) 沿纵向添加的控件列表
    /// </summary>
    public class FlowLayout : Panel, FlowLayoutCallback
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowLayout()
        {
            if (Factory == null) Factory = new DefaultFlowLayoutFactory();

            Control etoControl = null;
            Factory.CreateFlowLayoutBackend(this, out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
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
            if (backend != null) backend.AddControl(control, logicalHeight);
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
                if (backend != null) backend.InsertControl(index, control, logicalHeight);
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
                if (backend != null) backend.RemoveControl(index);
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
                if (backend != null) backend.SetControlVisible(index, visible);
            }
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
            panel.SetContentAsColumnLayout(1, 0).AddControl(control, true);
            var item = new StackLayoutItem(panel);
            ctxs.Add(new ControlContext{ Item = item, Visible = true });
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
            panel.SetContentAsColumnLayout(1, 0).AddControl(control, true);
            var item = new StackLayoutItem(panel);
            ctxs.Insert(index, new ControlContext{ Item = item, Visible = true });
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
                selectedControl.BackgroundColor = Colors.Transparent;
                selectedControl = null;
            }
            selectedControl = ctxs[index].Item.Control;
            selectedControl.BackgroundColor = Colors.Black;
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
        }

        private FlowLayoutCallback callback;
        private StackLayout layout;
        private List<ControlContext> ctxs = new List<ControlContext>();
        private Control selectedControl;
    }
}