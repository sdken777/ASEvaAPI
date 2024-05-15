using System;
using System.Linq;
using ASEva.Graph;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ASEva.UIEto
{
    class HistLineGraph : BaseGraph
    {
        protected override void InitializeModel()
        {
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Bottom,
            });
            model.Axes.Add(new CategoryAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Bottom,
                Angle = -45,
                IsAxisVisible = false,
            });
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                MajorGridlineStyle = LineStyle.Solid,
                TitleColor = OxyColors.DodgerBlue,
                Position = AxisPosition.Left,
            });
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Right,
                TitleColor = OxyColors.Orange,
                LabelFormatter = delegate {return "";},
                IsAxisVisible = false,
                
            });
            model.Series.Add(new HistogramSeries()
            {
                Title = "",
                FillColor = OxyColors.DodgerBlue,
                TrackerFormatString = "",
            });
            model.Series.Add(new LineSeries()
            {
                Title = "",
                Color = OxyColors.Orange,
                TrackerFormatString = "",
            });
            model.Annotations.Add(new PolygonAnnotation()
            {
                Fill = OxyColor.FromAColor(64, OxyColors.Green)
            });
        }

        protected override void UpdateModel(GraphData data)
        {
            model.Title = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is HistAndLineData) || !data.HasData())
            {
                model.Subtitle = "No data.";
                model.SubtitleColor = OxyColors.Black;
                model.Axes[0].Reset();
                model.Axes[1].Reset();
                model.Axes[2].Reset();
                model.Axes[3].Reset();
                (model.Series[0] as HistogramSeries).Items.Clear();
                (model.Series[1] as LineSeries).Points.Clear();
                (model.Annotations[0] as PolygonAnnotation).Points.Clear();
                InvalidatePlot();
                return;
            }

            var histLineData = data as HistAndLineData;
            var xValuesOrLabels = histLineData.GetXValuesOrLabels();
            if (xValuesOrLabels is HistLineXValues)
            {
                var xValues = xValuesOrLabels as HistLineXValues;
                model.Axes[0].IsAxisVisible = true;
                model.Axes[1].IsAxisVisible = false;
                model.Axes[0].Title = histLineData.GetXTitle();
                model.Axes[0].Minimum = xValues.Base;
                model.Axes[0].Maximum = xValues.Base + xValues.Count * xValues.Step;
            }
            else // HistLineXLabels
            {
                var xLabels = xValuesOrLabels as HistLineXLabels;
                var axis = model.Axes[1] as CategoryAxis;
                model.Axes[1].IsAxisVisible = true;
                model.Axes[0].IsAxisVisible = false;
                model.Axes[1].Title = histLineData.GetXTitle();
                axis.Labels.Clear();
                axis.Labels.AddRange(xLabels.Labels);
            }

            model.Axes[2].Title = histLineData.GetHistTitle();
            if (histLineData.IsLineEnabled())
            {
                model.Axes[3].IsAxisVisible = true;
                model.Axes[3].Title = histLineData.GetLineTitle();
            }
            else model.Axes[3].IsAxisVisible = false;

            if (model.Series[0].TrackerFormatString.Length == 0)
            {
                model.Series[0].TrackerFormatString = histLineData.GetHistTitle() + " = {7:0.000}";
            }
            if (histLineData.IsLineEnabled() && model.Series[1].TrackerFormatString.Length == 0)
            {
                model.Series[1].TrackerFormatString = histLineData.GetLineTitle() + " = {4:0.000}";
            }

            var samples = histLineData.GetSamples();
            double maximum = Double.NegativeInfinity;
            double minimum = Double.PositiveInfinity;
            foreach (var sample in samples)
            {
                maximum = Math.Max(maximum, sample.HistValue);
                minimum = Math.Min(minimum, sample.HistValue);
                if (histLineData.IsLineEnabled())
                {
                    maximum = Math.Max(maximum, sample.LineValue);
                    minimum = Math.Min(minimum, sample.LineValue);
                }
            }
            bool hasValue = maximum >= minimum;
            if (hasValue)
            {
                if (maximum == minimum)
                {
                    if (minimum > 0)
                    {
                        maximum = minimum * 1.25;
                        minimum = 0;
                    }
                    else if (minimum < 0)
                    {
                        minimum = minimum * 1.25;
                        maximum = 0;
                    }
                }
                else
                {
                    var maxreal = maximum + (maximum - minimum) * 0.02;
                    var minreal = minimum - (maximum - minimum) * 0.02;
                    maximum = maxreal;
                    minimum = minreal;

                    if (minimum > -0.001) minimum = -0.001;
                    if (maximum < 0.001) maximum = 0.001;
                }
            }
            model.Axes[2].Minimum = model.Axes[3].Minimum = minimum;
            model.Axes[2].Maximum = model.Axes[3].Maximum = maximum;

            (model.Series[0] as HistogramSeries).Items.Clear();
            (model.Series[1] as LineSeries).Points.Clear();
            for (int i = 0; i < samples.Length; i++)
            {
                double x1 = i + 0.1, x2 = i + 0.9;
                if (xValuesOrLabels is HistLineXValues)
                {
                    var xValues = xValuesOrLabels as HistLineXValues;
                    x1 = xValues.Base + (i + 0.1) * xValues.Step;
                    x2 = xValues.Base + (i + 0.9) * xValues.Step;
                }
                var area = samples[i].HistValue * (x2 - x1);
                (model.Series[0] as HistogramSeries).Items.Add(new HistogramItem(x1, x2, area, 1));

                if (histLineData.IsLineEnabled()) (model.Series[1] as LineSeries).Points.Add(new DataPoint(0.5 * (x1 + x2), samples[i].LineValue));
            }

            var polygon = model.Annotations[0] as PolygonAnnotation;
            polygon.Points.Clear();
            if (data.Definition.Validation != null && xValuesOrLabels is HistLineXValues)
            {
                var xValues = xValuesOrLabels as HistLineXValues;
                if (data.Definition.Validation is ValueAboveValidation)
                {
                    var indices = (data.Definition.Validation as ValueAboveValidation).GetHistLineValuesOKIndices(xValues);
                    if (indices.Length > 0)
                    {
                        double thisLeft = xValues.Base + xValues.Step * indices[0];
                        double thisRight = xValues.Base + xValues.Step * xValues.Count;
                        polygon.Points.Add(new DataPoint(thisLeft, maximum));
                        polygon.Points.Add(new DataPoint(thisRight, maximum));
                        polygon.Points.Add(new DataPoint(thisRight, minimum));
                        polygon.Points.Add(new DataPoint(thisLeft, minimum));
                    }
                }
                else if (data.Definition.Validation is ValueBelowValidation)
                {
                    var indices = (data.Definition.Validation as ValueBelowValidation).GetHistLineValuesOKIndices(xValues);
                    if (indices.Length > 0)
                    {
                        double thisLeft = xValues.Base;
                        double thisRight = xValues.Base + xValues.Step * (indices.Last() + 1);
                        polygon.Points.Add(new DataPoint(thisLeft, maximum));
                        polygon.Points.Add(new DataPoint(thisRight, maximum));
                        polygon.Points.Add(new DataPoint(thisRight, minimum));
                        polygon.Points.Add(new DataPoint(thisLeft, minimum));
                    }
                }
                else if (data.HasData())
                {
                    double left = xValues.Base;
                    double right = xValues.Base + xValues.Step * xValues.Count;
                    if (data.Definition.Validation is PolyAboveValidation)
                    {
                        var thresholds = (data.Definition.Validation as PolyAboveValidation).GetHistLineValuesThreshold(xValues);
                        for (int i = 0; i < thresholds.Length; i++)
                        {
                            thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
                        }

                        polygon.Points.Add(new DataPoint(right, maximum));
                        polygon.Points.Add(new DataPoint(left, maximum));
                        for (int i = 0; i < thresholds.Length; i++)
                        {
                            var thisX = left + xValues.Step * i;
                            var thisY = thresholds[i];

                            if (i == 0 || thresholds[i] != thresholds[i - 1]) polygon.Points.Add(new DataPoint(thisX, thisY));
                            polygon.Points.Add(new DataPoint(thisX + xValues.Step, thisY));
                        }
                    }
                    else if (data.Definition.Validation is PolyBelowValidation)
                    {
                        var thresholds = (data.Definition.Validation as PolyBelowValidation).GetHistLineValuesThreshold(xValues);
                        for (int i = 0; i < thresholds.Length; i++)
                        {
                            thresholds[i] = Math.Max(minimum, Math.Min(maximum, thresholds[i]));
                        }

                        polygon.Points.Add(new DataPoint(right, minimum));
                        polygon.Points.Add(new DataPoint(left, minimum));
                        for (int i = 0; i < thresholds.Length; i++)
                        {
                            var thisX = left + xValues.Step * i;
                            var thisY = thresholds[i];

                            if (i == 0 || thresholds[i] != thresholds[i - 1]) polygon.Points.Add(new DataPoint(thisX, thisY));
                            polygon.Points.Add(new DataPoint(thisX + xValues.Step, thisY));
                        }
                    }
                }
            }

            double? percentage = null;
            var vdResult = data.Validate(out percentage);
            if (vdResult == null)
            {
                model.SubtitleColor = OxyColors.Black;
                model.Subtitle = percentage == null ? null : (percentage.Value.ToString("F1") + "% OK");
            }
            else if (vdResult.Value)
            {
                model.SubtitleColor = OxyColors.Green;
                model.Subtitle = percentage == null ? "OK" : (percentage.Value.ToString("F1") + "% OK");
            }
            else
            {
                model.SubtitleColor = OxyColors.Red;
                model.Subtitle = percentage == null ? "NG" : (percentage.Value.ToString("F1") + "% OK");
            }
        }
    }
}