using System;
using System.Xml;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) XML属性解析
    /// </summary>
    public class AttributeParser
    {
        private XmlAttributeCollection a;

        public AttributeParser(XmlElement root)
        {
            a = root.Attributes;
        }

        public bool ParseBool(String key, String trueValue, bool defaultValue)
        {
            if (a[key] == null) return defaultValue;
            else return a[key].Value == trueValue;
        }

        public int ParseInt(String key, int defaultValue)
        {
            if (a[key] == null) return defaultValue;
            int output;
            if (Int32.TryParse(a[key].Value, out output)) return output;
            else return defaultValue;
        }

        public long ParseLong(String key, long defaultValue)
        {
            if (a[key] == null) return defaultValue;
            long output;
            if (Int64.TryParse(a[key].Value, out output)) return output;
            else return defaultValue;
        }

        public double ParseDouble(String key, double defaultValue)
        {
            if (a[key] == null) return defaultValue;
            double output;
            if (Double.TryParse(a[key].Value, out output)) return output;
            else return defaultValue;
        }

        public String ParseString(String key, String defaultValue)
        {
            if (a[key] == null) return defaultValue;
            var text = a[key].Value;
            return text == "null" ? null : text;
        }

        public String ParseMessageID(String key)
        {
            if (a[key] == null) return null;
            var text = a[key].Value;
            if (text.Length == 0 || text == "null") return null;
            var comps = text.Split(':');
            if (comps.Length != 2) return null;
            return comps[0].ToLower() + ":" + comps[1];
        }

        public String ParseSignalID(String key)
        {
            if (a[key] == null) return null;
            var text = a[key].Value;
            if (text.Length == 0 || text == "null") return null;
            var comps = text.Split(':');
            if (comps.Length != 3) return null;
            return comps[0].ToLower() + ":" + comps[1] + ":" + comps[2];
        }

        public double[] ParseSignalValue(String key, bool optional)
        {
            double[] ret = null;
            if (!optional) ret = new double[1] { 0 };
            if (a[key] == null) return ret;

            String text = a[key].Value;
            if (text.Length == 0 || text == "null") return ret;

            var values = text.Split(',');
            if (values.Length == 0) return ret;

            var buf = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                double tmp;
                if (Double.TryParse(values[i], out tmp)) buf[i] = tmp;
                else return ret;
            }

            ret = buf;
            return ret;
        }

        public object ParseEnum(String key, Type type, object defaultValue)
        {
            if (a[key] == null) return defaultValue;
            try
            {
                var obj = Enum.Parse(type, a[key].Value);
                return obj == null ? defaultValue : obj;
            }
            catch (Exception) { return defaultValue; }
        }

        public FloatPoint? ParsePoint(String key, FloatPoint? defaultPoint)
        {
            if (a[key] == null) return defaultPoint;

            var comps = a[key].Value.Split(',');
            if (comps.Length < 2) return defaultPoint;

            float x, y;
            if (!Single.TryParse(comps[0], out x) ||
                !Single.TryParse(comps[1], out y)) return defaultPoint;
            return new FloatPoint(x, y);
        }

        public ColorRGBA ParseColor(String key, ColorRGBA defaultColor)
        {
            if (a[key] == null) return defaultColor;

            var comps = a[key].Value.Split(',');
            if (comps.Length < 3) return defaultColor;

            int r, g, b;
            if (!Int32.TryParse(comps[0], out r) ||
                !Int32.TryParse(comps[1], out g) ||
                !Int32.TryParse(comps[2], out b)) return defaultColor;
            return new ColorRGBA((byte)r, (byte)g, (byte)b, 255);
        }
    }

    /// <summary>
    /// (api:app=2.0.0) XML属性输出
    /// </summary>
    public class AttributeWriter
    {
        private XmlDocument x;
        private XmlAttributeCollection a;

        /// <summary>
        /// 基于XML元素节点创建
        /// </summary>
        /// <param name="element">XML元素节点</param>
        public AttributeWriter(XmlElement element)
        {
            x = element.OwnerDocument;
            a = element.Attributes;
        }

        public AttributeWriter(XmlDocument xml, XmlElement root)
        {
            x = xml;
            a = root.Attributes;
        }

        public void WriteBool(String key, bool value, String trueValue, String falseValue)
        {
            a.Append(x.CreateAttribute(key)).Value = value ? trueValue : falseValue;
        }

        public void WriteInt(String key, int value)
        {
            a.Append(x.CreateAttribute(key)).Value = value.ToString();
        }

        public void WriteLong(String key, long value)
        {
            a.Append(x.CreateAttribute(key)).Value = value.ToString();
        }

        public void WriteDouble(String key, double value)
        {
            a.Append(x.CreateAttribute(key)).Value = value.ToString();
        }

        public void WriteString(String key, String value)
        {
            a.Append(x.CreateAttribute(key)).Value = value == null ? "null" : value;
        }

        public void WriteSignalValue(String key, double[] values)
        {
            if (values == null) a.Append(x.CreateAttribute(key)).Value = "null";
            else
            {
                var text = "";
                foreach (var val in values) text += text.Length == 0 ? val.ToString() : ("," + val);
                a.Append(x.CreateAttribute(key)).Value = text;
            }
        }

        public void WritePoint(String key, FloatPoint? value)
        {
            a.Append(x.CreateAttribute(key)).Value = value == null ? "null" : (value.Value.X + "," + value.Value.Y);
        }

        public void WriteColor(String key, ColorRGBA value)
        {
            a.Append(x.CreateAttribute(key)).Value = value.R + "," + value.G + "," + value.B;
        }
    }
}
