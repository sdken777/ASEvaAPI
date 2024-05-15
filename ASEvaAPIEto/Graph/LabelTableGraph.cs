using System;
using ASEva.Graph;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ASEva.UIEto
{
    class LabelTableGraph : BaseGraph
    {
        protected override void InitializeModel()
        {
            dummyData[0, 0] = 0;

            model.Axes.Add(new CategoryAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Bottom,
                Angle = -45,
            });
            model.Axes.Add(new CategoryAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Position = AxisPosition.Left,
                Angle = -45,
            });
            model.Axes.Add(new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Palette = genPalette(),
            });
            model.Series.Add(new HeatMapSeries()
            {
                CoordinateDefinition = HeatMapCoordinateDefinition.Edge,
                Interpolate = false,
                X0 = -0.5,
                X1 = 0.5,
                Y0 = -0.5,
                Y1 = 0.5,
                Data = dummyData,
            });
        }

        protected override void UpdateModel(GraphData data)
        {
            model.Title = data == null ? "" : data.Definition.MainTitle;
            if (data == null || !(data is LabelTableData) || !data.HasData())
            {
                model.Axes[0].Reset();
                model.Axes[1].Reset();
                model.Axes[2].Reset();
                (model.Series[0] as HeatMapSeries).Data = dummyData;
                InvalidatePlot();
                return;
            }

            var labelData = data as LabelTableData;
            model.Axes[0].Title = labelData.GetXTitle();
            model.Axes[1].Title = labelData.GetYTitle();

            (model.Axes[0] as CategoryAxis).Labels.Clear();
            (model.Axes[0] as CategoryAxis).Labels.AddRange(labelData.GetXLabels());
            (model.Series[0] as HeatMapSeries).X1 = labelData.GetXLabelCount() - 0.5;

            (model.Axes[1] as CategoryAxis).Labels.Clear();
            (model.Axes[1] as CategoryAxis).Labels.AddRange(labelData.GetYLabels());
            (model.Series[0] as HeatMapSeries).Y1 = labelData.GetYLabelCount() - 0.5;

            switch (labelData.GetValueDirection())
            {
                case LabelTableValueDirection.Positive:
                    model.Axes[2].Minimum = labelData.GetDefaultValue();
                    model.Axes[2].Maximum = Math.Max(labelData.GetDefaultValue() + 0.001, labelData.GetValues().Max2D());
                    break;
                case LabelTableValueDirection.Negative:
                    model.Axes[2].Maximum = labelData.GetDefaultValue();
                    model.Axes[2].Minimum = Math.Min(labelData.GetDefaultValue() - 0.001, labelData.GetValues().Min2D());
                    break;
                case LabelTableValueDirection.Bidirectional:
                    {
                        var upperRange = labelData.GetValues().Max2D() - labelData.GetDefaultValue();
                        var lowerRange = labelData.GetDefaultValue() - labelData.GetValues().Min2D();
                        var range = Math.Max(lowerRange, upperRange);
                        model.Axes[2].Minimum = labelData.GetDefaultValue() - range;
                        model.Axes[2].Maximum = labelData.GetDefaultValue() + range;
                    }
                    break;
            }

            (model.Series[0] as HeatMapSeries).Data = labelData.GetValues();
        }

        private OxyPalette genPalette()
        {
            var colors = new OxyColor[17];
            colors[0] = OxyColor.FromArgb(255, 64, 192, 32);
            colors[1] = OxyColor.FromArgb(255, 72, 184, 32);
            colors[2] = OxyColor.FromArgb(255, 80, 176, 32);
            colors[3] = OxyColor.FromArgb(255, 88, 168, 32);
            colors[4] = OxyColor.FromArgb(255, 96, 160, 32);
            colors[5] = OxyColor.FromArgb(255, 104, 152, 32);
            colors[6] = OxyColor.FromArgb(255, 112, 144, 32);
            colors[7] = OxyColor.FromArgb(255, 120, 136, 32);
            colors[8] = OxyColor.FromArgb(255, 128, 128, 32);
            colors[9] = OxyColor.FromArgb(255, 136, 120, 32);
            colors[10] = OxyColor.FromArgb(255, 144, 112, 32);
            colors[11] = OxyColor.FromArgb(255, 152, 104, 32);
            colors[12] = OxyColor.FromArgb(255, 160, 96, 32);
            colors[13] = OxyColor.FromArgb(255, 168, 88, 32);
            colors[14] = OxyColor.FromArgb(255, 176, 80, 32);
            colors[15] = OxyColor.FromArgb(255, 184, 72, 32);
            colors[16] = OxyColor.FromArgb(255, 192, 64, 32);
            return new OxyPalette(colors);
        }

        private double[,] dummyData = new double[1, 1];
    }
}