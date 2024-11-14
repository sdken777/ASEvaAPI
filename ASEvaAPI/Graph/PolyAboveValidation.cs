using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva.Graph
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Validation of whether the data is above the poly line
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 图表数据的折线上方验证方式
    /// </summary>
    public class PolyAboveValidation : GraphValidation
    {
        public PolyAboveValidation()
        {
            poly = new FloatPoint[0];
        }

        public PolyAboveValidation(FloatPoint[] polyline)
        {
            if (polyline == null || polyline.Length == 0)
            {
                poly = new FloatPoint[0];
                return;
            }

            var sortList = new List<float>();
            foreach (var pt in polyline)
            {
                sortList.Add(pt.X);
            }

            sortList.Sort();

            poly = new FloatPoint[polyline.Length];
            for (int i = 0; i < polyline.Length; i++)
            {
                var x = sortList[i];
                poly[i].X = x;
                poly[i].Y = polyline.First(pt => pt.X == x).Y;
            }
        }

        public PolyAboveValidation(FloatPoint[] polyline, double okPercentage)
        {
            if (polyline == null || polyline.Length == 0)
            {
                poly = new FloatPoint[0];
                return;
            }

            var sortList = new List<float>();
            foreach (var pt in polyline)
            {
                sortList.Add(pt.X);
            }

            sortList.Sort();

            poly = new FloatPoint[polyline.Length];
            for (int i = 0; i < polyline.Length; i++)
            {
                var x = sortList[i];
                poly[i].X = x;
                poly[i].Y = polyline.First(pt => pt.X == x).Y;
            }

            this.okPercentage = okPercentage;
        }

        public GraphValidationType GetValidationType()
        {
            return GraphValidationType.PolyAbove;
        }

        public List<string> ToStringList()
        {
            var list = new List<String>();
            list.Add("PolyAbove");
            list.Add(poly.Length.ToString());
            list.Add(okPercentage == null ? "null" : okPercentage.Value.ToString());
            foreach (var pt in poly)
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

            poly = new FloatPoint[ptCount];
            for (int i = 0; i < ptCount; i++)
            {
                float x, y;
                if (Single.TryParse(stringList[3 + 2 * i], out x) && Single.TryParse(stringList[4 + 2 * i], out y))
                {
                    poly[i].X = x;
                    poly[i].Y = y;
                }
            }

            return true;
        }

        public bool? Validate(GraphData data, out double? percentage)
        {
            if (data.HasData())
            {
                if (data is HistAndLineData hist && hist.GetXValuesOrLabels() is HistLineXValues xValues)
                {
                    var samples = hist.GetSamples();
                    var thresholds = GetHistLineValuesThreshold(xValues);

                    int okCount = 0;
                    for (int i = 0; i < xValues.Count; i++)
                    {
                        if (samples[i].HistValue >= thresholds[i]) okCount++;
                    }

                    percentage = (double)okCount * 100 / xValues.Count;
                    return okPercentage == null ? null : (bool?)(percentage >= okPercentage);
                }
            }
            percentage = null;
            return null;
        }

        public double[] GetHistLineValuesThreshold(HistLineXValues values)
        {
            var output = new double[values.Count];
            if (poly.Length == 0) return output;

            for (int i = 0; i < values.Count; i++)
            {
                var x = ((double)i + 0.5) * values.Step + values.Base;
                if (x < poly[0].X) output[i] = poly[0].Y;
                else if (x >= poly.Last().X) output[i] = poly.Last().Y;
                else
                {
                    for (int j = 0; j < poly.Length - 1; j++)
                    {
                        var p1 = poly[j];
                        var p2 = poly[j + 1];
                        if (x >= p1.X && x < p2.X)
                        {
                            output[i] = ((x - p1.X) * p2.Y + (p2.X - x) * p1.Y) / (p2.X - p1.X);
                            break;
                        }
                    }
                }
            }

            return output;
        }

        private FloatPoint[] poly;
        private double? okPercentage;
    }
}
