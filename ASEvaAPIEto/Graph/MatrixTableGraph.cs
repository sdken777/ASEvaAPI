using System;
using ASEva.Graph;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ASEva.UIEto
{
    class MatrixTableGraph : BaseGraph
    {
        protected override void InitializeModel()
        {
            dummyData[0, 0] = 0;

            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Bottom,
            });
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Left,
            });
            model.Axes.Add(new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Palette = OxyPalettes.Jet(500),
            });
            model.Series.Add(new HeatMapSeries()
            {
                CoordinateDefinition = HeatMapCoordinateDefinition.Edge,
                Interpolate = true,
                X0 = 0,
                X1 = 100,
                Y0 = 0,
                Y1 = 100,
                Data = dummyData,
            });
            model.Annotations.Add(new PolygonAnnotation()
            {
                Fill = OxyColors.Transparent,
                StrokeThickness = 2,
            });
        }

        protected override void UpdateModel(GraphData data)
        {
            model.Title = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is MatrixTableData) || !data.HasData())
            {
                model.Subtitle = "No data.";
                model.SubtitleColor = OxyColors.Black;
                model.Axes[0].Reset();
                model.Axes[1].Reset();
                model.Axes[2].Reset();
                (model.Series[0] as HeatMapSeries).Data = dummyData;
                (model.Annotations[0] as PolygonAnnotation).Points.Clear();
                InvalidatePlot();
                return;
            }

            var matrixData = data as MatrixTableData;
            model.Axes[0].Title = matrixData.GetXTitle();
            model.Axes[1].Title = matrixData.GetYTitle();

            var xRange = matrixData.GetXRange();
            double xMin = xRange.Base, xMax = xRange.Base + xRange.Step * xRange.Count;
            model.Axes[0].Minimum = xMin;
            model.Axes[0].Maximum = xMax;
            (model.Series[0] as HeatMapSeries).X0 = xMin;
            (model.Series[0] as HeatMapSeries).X1 = xMax;

            var yRange = matrixData.GetYRange();
            double yMin = yRange.Base, yMax = yRange.Base + yRange.Step * yRange.Count;
            model.Axes[1].Minimum = yMin;
            model.Axes[1].Maximum = yMax;
            (model.Series[0] as HeatMapSeries).Y0 = yMin;
            (model.Series[0] as HeatMapSeries).Y1 = yMax;

            var values = matrixData.GetValues();
            (model.Series[0] as HeatMapSeries).Data = values;

            OxyColor color = OxyColors.Black;
            FloatPoint[] outline = null;
            String subTitlePrefix = "";
            if (data.Definition.Validation != null)
            {
                if (data.Definition.Validation is OutlineInsideValidation)
                {
                    color = OxyColors.LimeGreen;
                    outline = (data.Definition.Validation as OutlineInsideValidation).GetOutline();
                }
                else if (data.Definition.Validation is OutlineOutsideValidation)
                {
                    color = OxyColors.Red;
                    outline = (data.Definition.Validation as OutlineOutsideValidation).GetOutline();
                }
                else if (data.Definition.Validation is ValueBelowValidation)
                {
                    subTitlePrefix = "≤ " + (data.Definition.Validation as ValueBelowValidation).GetThreshold() + " : ";
                }
                else if (data.Definition.Validation is ValueAboveValidation)
                {
                    subTitlePrefix = "≥ " + (data.Definition.Validation as ValueAboveValidation).GetThreshold() + " : ";
                }
            }
            (model.Annotations[0] as PolygonAnnotation).Points.Clear();
            if (outline != null)
            {
                var polygon = model.Annotations[0] as PolygonAnnotation;
                polygon.Stroke = color;
                foreach (var pt in outline) polygon.Points.Add(new DataPoint(pt.X, pt.Y));
            }

            double? percentage = null;
            var vdResult = data.Validate(out percentage);
            if (vdResult == null)
            {
                model.SubtitleColor = OxyColors.Black;
                model.Subtitle = percentage == null ? null : (subTitlePrefix + getPercentageText(percentage.Value) + "% OK");
            }
            else if (vdResult.Value)
            {
                model.SubtitleColor = OxyColors.Green;
                model.Subtitle = subTitlePrefix + (percentage == null ? "OK" : (getPercentageText(percentage.Value) + "% OK"));
            }
            else
            {
                model.SubtitleColor = OxyColors.Red;
                model.Subtitle = subTitlePrefix + (percentage == null ? "NG" : (getPercentageText(percentage.Value) + "% OK"));
            }
        }

        private String getPercentageText(double percentage)
        {
            return percentage >= 100 ? percentage.ToString("F0") : percentage.ToString("F1");
        }

        private double[,] dummyData = new double[1, 1];
    }
}