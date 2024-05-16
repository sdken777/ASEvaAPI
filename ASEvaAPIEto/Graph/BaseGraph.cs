using System;
using System.Threading;
using Eto.Drawing;
using OxyPlot;

namespace ASEva.UIEto
{
    #pragma warning disable CS0618

    class BaseGraph : OxyPlotView, GraphPanel
    {
        public BaseGraph()
        {
            BackgroundColor = Colors.White;

            InitializeModel();
            model.DefaultFontSize = model.DefaultFontSize * 0.8;
            model.TitleFontSize = model.TitleFontSize * 0.7;
            model.SubtitleFontSize = model.SubtitleFontSize * 0.8;
            model.Padding = new OxyThickness(0, 4, 4, 0);
            model.MouseDown += delegate { click.Set(); };
            SetModel(model);
        }

        public void UpdateWithGraphData(GraphData data)
        {
            UpdateModel(data);
            InvalidatePlot();
        }

        public int? GetFixedHeight()
        {
            return null;
        }

        public void ReleaseResources()
        {
        }

        public void UseClickEvent(ManualResetEventSlim ev)
        {
            click = ev;
        }

        protected virtual void InitializeModel()
        {
        }

        protected virtual void UpdateModel(GraphData data)
        {
        }

        protected PlotModel model = new PlotModel();
        protected bool chinese = Agency.GetAppLanguage() == "ch";

        private ManualResetEventSlim click;
    }
}