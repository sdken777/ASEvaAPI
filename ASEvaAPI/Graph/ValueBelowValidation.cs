using System;
using System.Collections.Generic;

namespace ASEva.Graph
{
    /// <summary>
    /// (api:app=2.0.0) 图表数据的阈值下方验证方式
    /// </summary>
    public class ValueBelowValidation : GraphValidation
    {
        public ValueBelowValidation()
        {
            thresh = 0;
        }

        public ValueBelowValidation(double threshold)
        {
            this.thresh = threshold;
        }

        public ValueBelowValidation(double threshold, double okPercentage)
        {
            this.thresh = threshold;
            this.okPercentage = okPercentage;
        }

        public double GetThreshold()
        {
            return thresh;
        }

        public GraphValidationType GetValidationType()
        {
            return GraphValidationType.ValueBelow;
        }

        public List<string> ToStringList()
        {
            var list = new List<String>();
            list.Add("ValueBelow");
            list.Add(thresh.ToString());
            list.Add(okPercentage == null ? "null" : okPercentage.Value.ToString());
            return list;
        }

        public bool FromStringList(List<string> stringList)
        {
            if (stringList.Count < 3) return false;
            if (!Double.TryParse(stringList[1], out thresh)) return false;

            double tmp = 0;
            if (Double.TryParse(stringList[2], out tmp)) okPercentage = tmp;
            else okPercentage = null;

            return true;
        }

        public bool? Validate(GraphData data, out double? percentage)
        {
            if (data.HasData())
            {
                if (data is SingleValueData)
                {
                    if ((data as SingleValueData).GetValue() <= thresh)
                    {
                        percentage = null;
                        return true;
                    }
                    else
                    {
                        percentage = null;
                        return false;
                    }
                }
                else if (data is HistAndLineData)
                {
                    var hist = data as HistAndLineData;
                    var xValues = hist.GetXValuesOrLabels() as HistLineXValues;
                    var samples = hist.GetSamples();
                    var okIndices = GetHistLineValuesOKIndices(xValues);

                    double totalSum = 0;
                    for (int i = 0; i < xValues.Count; i++) totalSum += samples[i].HistValue;

                    double okSum = 0;
                    for (int i = 0; i < okIndices.Length; i++) okSum += samples[okIndices[i]].HistValue;

                    percentage = okSum * 100 / totalSum;
                    return okPercentage == null ? null : (bool?)(percentage >= okPercentage);
                }
                else if (data is MatrixTableData)
                {
                    var mat = data as MatrixTableData;
                    var values = mat.GetValues();
                    double totalSum = values.Length;

                    double okSum = 0;
                    foreach (var val in values)
                    {
                        if (val <= thresh) okSum++;
                    }

                    percentage = okSum * 100 / totalSum;
                    return okPercentage == null ? null : (bool?)(percentage >= okPercentage);
                }
            }
            percentage = null;
            return null;
        }

        public int[] GetHistLineValuesOKIndices(HistLineXValues values)
        {
            int endIndex = (int)Math.Round((thresh - values.Base) / values.Step) - 1;
            var list = new List<int>();
            for (int i = 0; i <= endIndex; i++) list.Add(i);
            return list.ToArray();
        }

        private double thresh;
        private double? okPercentage;
    }
}
