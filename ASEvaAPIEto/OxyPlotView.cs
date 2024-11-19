using System;
using Eto.Forms;
using OxyPlot;

namespace ASEva.UIEto
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:eto=3.0.4) OxyPlot graph control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:eto=3.0.4) OxyPlot图表控件
    /// </summary>
    public class OxyPlotView : Panel
    {
        /// \~English
        /// <summary>
        /// Constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 构造函数
        /// </summary>
        public OxyPlotView()
        {
            Control? etoControl = null;
            if (Factory != null) Factory.CreateOxyPlotViewBackend(out etoControl, out backend);
            if (etoControl != null) Content = etoControl;
        }

        /// \~English
        /// <summary>
        /// Set OxyPlot model, and refresh drawing
        /// </summary>
        /// <param name="model">OxyPlot model</param>
        /// \~Chinese
        /// <summary>
        /// 设置OxyPlot模型，并刷新显示
        /// </summary>
        /// <param name="model">OxyPlot模型</param>
        public void SetModel(PlotModel model)
        {
            if (backend != null) backend.SetModel(model);
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.0.4) Refresh drawing
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.0.4) 刷新绘制
        /// </summary>
        public void InvalidatePlot()
        {
            if (backend != null) backend.InvalidatePlot();
        }

        public static OxyPlotViewFactory? Factory { private get; set; }

		private OxyPlotViewBackend? backend;
    }

	public interface OxyPlotViewBackend
	{
        void SetModel(PlotModel model);
        void InvalidatePlot();
	}

	public interface OxyPlotViewFactory
	{
		void CreateOxyPlotViewBackend(out Control etoControl, out OxyPlotViewBackend backend);
	}
}