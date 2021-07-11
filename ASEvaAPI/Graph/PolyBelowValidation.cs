using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva.Graph
{
    /// <summary>
    /// (api:app=2.0.0) 图表数据的折线下方验证方式
    /// </summary>
    public class PolyBelowValidation : GraphValidation
    {
        public PolyBelowValidation()
        {
            poly = new FloatPoint[0];
        }

        public PolyBelowValidation(FloatPoint[] polyline)
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

        public PolyBelowValidation(FloatPoint[] polyline, double okPercentage)
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
            return GraphValidationType.PolyBelow;
        }

        public List<string> ToStringList()
        {
            var list = new List<String>();
            list.Add("PolyBelow");
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
            var ptCount = Convert.ToInt32(stringList[1]);
            if (stringList.Count != 3 + ptCount * 2) return false;

            double tmp = 0;
            if (Double.TryParse(stringList[2], out tmp)) okPercentage = tmp;
            else okPercentage = null;

            poly = new FloatPoint[ptCount];
            for (int i = 0; i < ptCount; i++)
            {
                poly[i].X = Convert.ToSingle(stringList[3 + 2 * i]);
                poly[i].Y = Convert.ToSingle(stringList[4 + 2 * i]);
            }

            return true;
        }

        public bool? Validate(GraphData data, out double? percentage)
        {
            if (data.HasData())
            {
                if (data is HistAndLineData)
                {
                    var hist = data as HistAndLineData;
                    var xValues = hist.GetXValuesOrLabels() as HistLineXValues;
                    var samples = hist.GetSamples();
                    var thresholds = GetHistLineValuesThreshold(xValues);

                    int okCount = 0;
                    for (int i = 0; i < xValues.Count; i++)
                    {
                        if (samples[i].HistValue <= thresholds[i]) okCount++;
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
