using System;
using ASEva.UIEto;
using Eto.Forms;
using OxyPlot;

namespace ASEva.UIMonoMac
{
    class OxyPlotViewBackendMonoMac : OxyPlot.Xamarin.Mac.PlotView, OxyPlotViewBackend
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

    class OxyPlotViewFactoryMonoMac : OxyPlotViewFactory
    {
        public void CreateOxyPlotViewBackend(out Control etoControl, out OxyPlotViewBackend backend)
        {
            var view = new OxyPlotViewBackendMonoMac();
            etoControl = view.ToEto();
            backend = view;
        }
    }
}