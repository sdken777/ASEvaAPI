using System;
using System.Collections.Generic;

namespace ASEva.Graph
{
    /// <summary>
    /// (api:app=2.0.0) 图表数据的范围区间内验证方式
    /// </summary>
    public class ValueInRangeValidation : GraphValidation
    {
        public ValueInRangeValidation()
        {
            lower = upper = 0;
        }

        public ValueInRangeValidation(double lower, double upper)
        {
            this.lower = lower;
            this.upper = upper;
        }

        public ValueInRangeValidation(double lower, double upper, double okPercentage)
        {
            this.lower = lower;
            this.upper = upper;
            this.okPercentage = okPercentage;
        }

        public void GetRange(out double lower, out double upper)
        {
            lower = this.lower;
            upper = this.upper;
        }

        public GraphValidationType GetValidationType()
        {
            return GraphValidationType.ValueInRange;
        }

        public List<string> ToStringList()
        {
            var list = new List<String>();
            list.Add("ValueInRange");
            list.Add(lower.ToString());
            list.Add(upper.ToString());
            list.Add(okPercentage == null ? "null" : okPercentage.Value.ToString());
            return list;
        }

        public bool FromStringList(List<string> stringList)
        {
            if (stringList.Count < 4) return false;

            lower = Convert.ToDouble(stringList[1]);
            upper = Convert.ToDouble(stringList[2]);

            double tmp = 0;
            if (Double.TryParse(stringList[3], out tmp)) okPercentage = tmp;
            else okPercentage = null;

            return true;
        }

        public bool? Validate(GraphData data, out double? percentage)
        {
            if (data.HasData())
            {
                if (data is SingleValueData)
                {
                    var val = (data as SingleValueData).GetValue();
                    if (val >= lower && val <= upper)
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
            }
            percentage = null;
            return null;
        }

        private double lower;
        private double upper;
        private double? okPercentage;
    }
}
