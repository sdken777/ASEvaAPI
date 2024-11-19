using System;
using ASEva.Graph;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ASEva.UIEto
{
    class ScatterPointsGraph : BaseGraph
    {
        protected override void InitializeModel()
        {
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            });
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Left,
            });
            model.Series.Add(new ScatterSeries()
            {
                Title = "",
                MarkerType = MarkerType.Diamond,
                MarkerStrokeThickness = 0,
                MarkerSize = 2,
                MarkerFill = OxyColors.MediumPurple,
            });
            model.Annotations.Add(new PolygonAnnotation());
        }

        protected override void UpdateModel(GraphData data)
        {
            model.Title = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is ScatterPointsData scatterPointsData) || !data.HasData())
            {
                model.Subtitle = "No data.";
                model.SubtitleColor = OxyColors.Black;
                model.Axes[0].Reset();
                model.Axes[1].Reset();
                (model.Series[0] as ScatterSeries)?.Points.Clear();
                (model.Annotations[0] as PolygonAnnotation)?.Points.Clear();
                InvalidatePlot();
                return;
            }

            model.Axes[0].Title = scatterPointsData.GetXTitle();
            model.Axes[1].Title = scatterPointsData.GetYTitle();

            var xRange = scatterPointsData.GetXRange();
            model.Axes[0].Minimum = xRange.lower;
            model.Axes[0].Maximum = xRange.upper;

            var yRange = scatterPointsData.GetYRange();
            model.Axes[1].Minimum = yRange.lower;
            model.Axes[1].Maximum = yRange.upper;

            var points = scatterPointsData.GetPoints();
            var series = model.Series[0] as ScatterSeries;
            series?.Points.Clear();
            for (int i = 0; i < points.Length; i++)
            {
                series?.Points.Add(new ScatterPoint(points[i].X, points[i].Y));
            }

            OxyColor color = OxyColors.Black;
            FloatPoint[]? outline = null;
            if (data.Definition.Validation != null)
            {
                if (data.Definition.Validation is OutlineInsideValidation oiv)
                {
                    color = OxyColors.LimeGreen;
                    outline = oiv.GetOutline();
                }
                else if (data.Definition.Validation is OutlineOutsideValidation oov)
                {
                    color = OxyColors.Red;
                    outline = oov.GetOutline();
                }
            }
            (model.Annotations[0] as PolygonAnnotation)?.Points.Clear();
            if (outline != null)
            {
                var polygon = model.Annotations[0] as PolygonAnnotation;
                if (polygon != null) polygon.Fill = OxyColor.FromAColor(64, color);
                foreach (var pt in outline) polygon?.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            double? percentage = null;
            var vdResult = data.Validate(out percentage);
            if (vdResult == null)
            {
                model.SubtitleColor = OxyColors.Black;
                model.Subtitle = percentage == null ? null : (getPercentageText(percentage.Value) + "% OK");
            }
            else if (vdResult.Value)
            {
                model.SubtitleColor = OxyColors.Green;
                model.Subtitle = percentage == null ? "OK" : (getPercentageText(percentage.Value) + "% OK");
            }
            else
            {
                model.SubtitleColor = OxyColors.Red;
                model.Subtitle = percentage == null ? "NG" : (getPercentageText(percentage.Value) + "% OK");
            }
        }

        private String getPercentageText(double percentage)
        {
            return percentage >= 100 ? percentage.ToString("F0") : percentage.ToString("F1");
        }
    }
}