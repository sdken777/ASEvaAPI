using System;
using ASEva.UIEto;
using Eto.Forms;
using OxyPlot;

namespace ASEva.UIGtk
{
    class OxyPlotViewBackendGtk : OxyPlot.GtkSharp.PlotView, OxyPlotViewBackend
    {
        public void SetModel(PlotModel model)
        {
            Model = model;
        }
    }

    class OxyPlotViewFactoryGtk : OxyPlotViewFactory
    {
        public void CreateOxyPlotViewBackend(out Control etoControl, out OxyPlotViewBackend backend)
        {
            var view = new OxyPlotViewBackendGtk();
            etoControl = view.ToEto();
            backend = view;
        }
    }
}