using System;
using System.Collections.Generic;
using System.Threading;
using Gtk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649, CS1571

    /// \~English
    /// <summary>
    /// (api:gtk=2.0.0) Base class of graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 图表显示控件基类
    /// </summary>
    public class BaseGraph : Box, GraphPanel
    {
        public BaseGraph(IntPtr raw) : base(raw)
        {}

        /// \~English
        /// <summary>
        /// Graph data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 图表数据
        /// </summary>
        public new GraphData Data { get; set; }

        /// \~English
        /// <summary>
        /// Event of graph selecting
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 图表控件被选择事件
        /// </summary>
        public event EventHandler GraphSelected;

        /// \~English
        /// <summary>
        /// Set control's scale
        /// </summary>
        /// <param name="scale">Control's scale, which ranges 1~8</param>
        /// \~Chinese
        /// <summary>
        /// 设置图表控件大小
        /// </summary>
        /// <param name="scale">控件大小，1为最小，8为最大</param>
        public void SetSize(int scale)
        {
            scale = Math.Max(1, Math.Min(8, scale));

            int width = 0, height = 0;
            switch (scale)
            {
                case 1:
                    width = 300;
                    height = 199;
                    break;
                case 2:
                    width = 400;
                    height = 268;
                    break;
                case 3:
                    width = 500;
                    height = 337;
                    break;
                case 4:
                    width = 600;
                    height = 406;
                    break;
                case 5:
                    width = 700;
                    height = 475;
                    break;
                case 6:
                    width = 800;
                    height = 544;
                    break;
                case 7:
                    width = 900;
                    height = 613;
                    break;
                case 8:
                    width = 1000;
                    height = 682;
                    break;
            }

            if (IsHeightFixed()) height = 61;

            SetSizeRequest(width, height);
        }

        /// \~English
        /// <summary>
        /// Create graph control
        /// </summary>
        /// <param name="definition">Graph definition</param>
        /// <param name="scale">Scale, which ranges 1~8</param>
        /// <returns>Created graph control</returns>
        /// \~Chinese
        /// <summary>
        /// 创建图表控件
        /// </summary>
        /// <param name="definition">图表定义</param>
        /// <param name="scale">控件大小，1为最小，8为最大</param>
        /// <returns>新创建的图表控件</returns>
        public static BaseGraph CreateGraphControl(GraphDefinition definition, int scale)
        {
            var defID = definition.GetID();
            if (ControlTypeTable.ContainsKey(defID))
            {
                var controlType = ControlTypeTable[defID];
                var graph = (BaseGraph)controlType.Assembly.CreateInstance(controlType.ToString());
                if (graph != null)
                {
                    graph.SetSize(scale);
                    return graph;
                }
            }
            BaseGraph defaultGraph = null;
            switch (definition.Type)
            {
                case GraphType.SingleValue:
                    defaultGraph = new ValueGraph();
                    break;
                case GraphType.ScatterPoints:
                    defaultGraph = new ScatterPointsGraph();
                    break;
                case GraphType.MatrixTable:
                    defaultGraph = new MatrixTableGraph();
                    break;
                case GraphType.LabelTable:
                    defaultGraph = new LabelTableGraph();
                    break;
                case GraphType.HistAndLine:
                    defaultGraph = new HistLineGraph();
                    break;
                default:
                    return null;
            }

            defaultGraph.SetSize(scale);
            return defaultGraph;
        }

        /// \~English
        /// <summary>
        /// Register to use the specified type of control for graph data visualization
        /// </summary>
        /// <param name="graphDefinitionID">Graph definition ID</param>
        /// <param name="controlType">Control type, which should be derived class of ASEva.UIGtk.BaseGraph </param>
        /// \~Chinese
        /// <summary>
        /// 注册指定ID的图表使用自定义可视化控件
        /// </summary>
        /// <param name="graphDefinitionID">图表定义ID</param>
        /// <param name="controlType">控件类型，必须为 ASEva.UIGtk.BaseGraph 的子类</param>
        public static void RegisterGraphControl(int graphDefinitionID, Type controlType)
        {
            if (controlType != null) ControlTypeTable[graphDefinitionID] = controlType;
        }

        /// \~English
        /// <summary>
        /// Whether control's height is fixed
        /// </summary>
        /// <returns>Whether control's height is fixed</returns>
        /// \~Chinese
        /// <summary>
        /// 控件高度是否固定
        /// </summary>
        /// <returns>控件高度是否固定</returns>
        public virtual bool IsHeightFixed() { return false; }

        /// \~English
        /// <summary>
        /// Update UI
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 控件显示更新
        /// </summary>
        public virtual void UpdateUIWithData() { }

        /// \~English
        /// <summary>
        /// (api:gtk=2.0.8) Release resources
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:gtk=2.0.8) 释放相关资源
        /// </summary>
        public virtual void Close() { }

        /// \~English
        /// <summary>
        /// Select the control
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 选择图表控件
        /// </summary>
        protected void HandleGraphSelected()
        {
            if (clickEvent != null) clickEvent.Set();
            if (GraphSelected != null) GraphSelected(this, null);
        }

        public void UpdateWithGraphData(GraphData data)
        {
            Data = data;
            UpdateUIWithData();
        }

        public int? GetFixedHeight()
        {
            if (IsHeightFixed()) return 61;
            else return null;
        }

        public void ReleaseResources()
        {
            Close();
        }

        public void UseClickEvent(ManualResetEventSlim ev)
        {
            clickEvent = ev;
        }

        private ManualResetEventSlim clickEvent;

        private static Dictionary<int, Type> ControlTypeTable = new Dictionary<int, Type>();
    }
}
