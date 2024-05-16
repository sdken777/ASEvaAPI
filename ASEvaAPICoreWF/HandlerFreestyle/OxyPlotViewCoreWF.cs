using System;
using ASEva.UIEto;
using Eto.Forms;
using OxyPlot;

namespace ASEva.UICoreWF
{
    class OxyPlotViewBackendCoreWF : OxyPlot.WindowsForms.PlotView, OxyPlotViewBackend
    {
        public void SetModel(PlotModel model)
        {
            Model = model;
        }

        public void InvalidatePlot()
        {
            InvalidatePlot(true);
        }
    }

    class OxyPlotViewFactoryCoreWF : OxyPlotViewFactory
    {
        public void CreateOxyPlotViewBackend(out Control etoControl, out OxyPlotViewBackend backend)
        {
            var view = new OxyPlotViewBackendCoreWF();
            etoControl = view.ToEto();
            backend = view;
        }
    }
}