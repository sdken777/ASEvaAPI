using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva.Utility
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 字符串与数据相互转换
    /// </summary>
    public class StringParsing
    {
        public static double? StringToValue(String text)
        {
            if (text == null || text.Length == 0) return null;
            double ret;
            if (!Double.TryParse(text, out ret)) return null;
            return ret;
        }

        public static double[] StringToSignalValues(String text)
        {
            if (text == null || text.Length == 0) return null;
            var values = text.Split(',');
            if (values.Length == 0) return null;
            var ret = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (!Double.TryParse(values[i], out ret[i])) return null;
            }
            return ret;
        }

        public static String SignalValuesToString(double[] values)
        {
            if (values == null || values.Length == 0) return null;
            else
            {
                var text = "";
                foreach (var val in values) text += text.Length == 0 ? val.ToString() : ("," + val);
                return text;
            }
        }

        public static List<String> StringToSignalValueStrings(String text)
        {
            if (text == null || text.Length == 0) return null;
            var comps = text.Split(',');
            if (comps.Length == 0) return null;
            double dummy;
            foreach (var c in comps)
            {
                if (!Double.TryParse(c, out dummy)) return null;
            }
            return comps.ToList();
        }

        public static String SignalValueStringsToString(List<String> list)
        {
            if (list == null || list.Count == 0) return "";
            var text = "";
            double dummy;
            foreach (var c in list)
            {
                if (!Double.TryParse(c, out dummy)) return "";
                if (text.Length > 0) text += ",";
                text += c;
            }
            return text;
        }
    }
}
