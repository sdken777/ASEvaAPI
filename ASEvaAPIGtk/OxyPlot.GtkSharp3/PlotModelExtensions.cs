// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotModelExtensions.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides extension methods to the <see cref="PlotModel" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.GtkSharp
{
    static class PlotModelExtensions
    {
        public static string ToSvg(this PlotModel model, double width, double height, bool isDocument)
        {
            var rc = new GraphicsRenderContext { RendersToScreen = false };
            return SvgExporter.ExportToString(model, width, height, isDocument, rc);
        }
    }
}