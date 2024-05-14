using System;
using ASEva.UIEto;
using Eto.Forms;
using OxyPlot;

namespace ASEva.UIWpf
{
    class OxyPlotViewBackendWpf : OxyPlot.Wpf.PlotView, OxyPlotViewBackend
    {
        public void SetModel(PlotModel model)
        {
            Model = model;
        }
    }

    class OxyPlotViewFactoryWpf : OxyPlotViewFactory
    {
        public void CreateOxyPlotViewBackend(out Control etoControl, out OxyPlotViewBackend backend)
        {
            var view = new OxyPlotViewBackendWpf();
            etoControl = view.ToEto();
            backend = view;
        }
    }
}