using System;
using System.Collections.Generic;

namespace ASEva.Graph
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Validation of whether the data is inside the outline
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 图表数据的轮廓内验证方式
    /// </summary>
    public class OutlineInsideValidation : GraphValidation
    {
        public OutlineInsideValidation()
        {
            outline = new FloatPoint[0];
        }

        public OutlineInsideValidation(FloatPoint[] outline)
        {
            if (outline == null)
            {
                this.outline = new FloatPoint[0];
                return;
            }

            var list = new List<FloatPoint>();
            foreach (var pt in outline)
            {
                if (!list.Contains(pt)) list.Add(pt);
            }
            this.outline = list.ToArray();
        }

        public OutlineInsideValidation(FloatPoint[] outline, double okPercentage)
        {
            if (outline == null)
            {
                this.outline = new FloatPoint[0];
                return;
            }

            var list = new List<FloatPoint>();
            foreach (var pt in outline)
            {
                if (!list.Contains(pt)) list.Add(pt);
            }
            this.outline = list.ToArray();

            this.okPercentage = okPercentage;
        }

        public FloatPoint[] GetOutline()
        {
            var output = new FloatPoint[outline.Length];
            Array.Copy(outline, output, outline.Length);
            return output;
        }

        public GraphValidationType GetValidationType()
        {
            return GraphValidationType.OutlineInside;
        }

        public List<string> ToStringList()
        {
            var list = new List<String>();
            list.Add("OutlineInside");
            list.Add(outline.Length.ToString());
            list.Add(okPercentage == null ? "null" : okPercentage.Value.ToString());
            foreach (var pt in outline)
            {
                list.Add(pt.X.ToString());
                list.Add(pt.Y.ToString());
            }
            return list;
        }

        public bool FromStringList(List<string> stringList)
        {
            if (stringList.Count < 3) return false;

            int ptCount;
            if (!Int32.TryParse(stringList[1], out ptCount)) return false;
            if (stringList.Count != 3 + ptCount * 2) return false;

            double tmp = 0;
            if (Double.TryParse(stringList[2], out tmp)) okPercentage = tmp;
            else okPercentage = null;

            outline = new FloatPoint[ptCount];
            for (int i = 0; i < ptCount; i++)
            {
                float x, y;
                if (Single.TryParse(stringList[3 + 2 * i], out x) && Single.TryParse(stringList[4 + 2 * i], out y))
                {
                    outline[i].X = x;
                    outline[i].Y = y;
                }
            }

            return true;
        }

        public bool? Validate(GraphData data, out double? percentage)
        {
            if (data.HasData())
            {
                if (data is ScatterPointsData spData)
                {
                    var pts = spData.GetPoints();
                    int insideCount = 0;
                    foreach (var pt in pts)
                    {
                        if (IsPointInPolygon(outline, pt)) insideCount++;
                    }
                    percentage = (double)insideCount * 100 / pts.Length;
                    return okPercentage == null ? null : (bool?)(percentage >= okPercentage);
                }
                else if (data is MatrixTableData mtData)
                {
                    var vals = mtData.GetValues();
                    var xRange = mtData.GetXRange();
                    var yRange = mtData.GetYRange();
                    double insideSum = 0;
                    double totalSum = 0;
                    if (xRange != null && yRange != null)
                    {
                        for (int i = 0; i < xRange.Count; i++)
                        {
                            var x = (float)(((double)i + 0.5) * xRange.Step + xRange.Base);
                            for (int j = 0; j < yRange.Count; j++)
                            {
                                var y = (float)(((double)j + 0.5) * yRange.Step + yRange.Base);
                                if (IsPointInPolygon(outline, new FloatPoint(x, y))) insideSum += vals[i, j];
                                totalSum += vals[i, j];
                            }
                        }
                    }
                    if (totalSum <= 0)
                    {
                        percentage = null;
                        return null;
                    }
                    else
                    {
                        percentage = insideSum * 100 / totalSum;
                        return okPercentage == null ? null : (bool?)(percentage >= okPercentage);
                    }
                }
            }
            percentage = null;
            return null;
        }

        private bool IsPointInPolygon(FloatPoint[] polygon, FloatPoint testPoint)
        {
            bool result = false;
            int j = polygon.Length - 1;
            for (int i = 0; i < polygon.Length; i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        private FloatPoint[] outline;
        private double? okPercentage;
    }
}
