using System;
using System.Xml;

namespace ASEva.Utility
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:app=3.7.0) XML attribute parsing
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) XML属性解析
    /// </summary>
    public class AttributeParser(XmlElement element)
    {
        private XmlAttributeCollection a = element.Attributes;

        public bool ParseBool(String key, String trueValue, bool defaultValue)
        {
            var v = a[key];
            if (v == null) return defaultValue;
            else return v.Value == trueValue;
        }

        public int ParseInt(String key, int defaultValue)
        {
            var v = a[key];
            if (v == null) return defaultValue;
            int output;
            if (Int32.TryParse(v.Value, out output)) return output;
            else return defaultValue;
        }

        public long ParseLong(String key, long defaultValue)
        {
            var v = a[key];
            if (v == null) return defaultValue;
            long output;
            if (Int64.TryParse(v.Value, out output)) return output;
            else return defaultValue;
        }

        public double ParseDouble(String key, double defaultValue)
        {
            var v = a[key];
            if (v == null) return defaultValue;
            double output;
            if (Double.TryParse(v.Value, out output)) return output;
            else return defaultValue;
        }

        public String? ParseString(String key, String? defaultValue)
        {
            var v = a[key];
            if (v == null) return defaultValue;
            var text = v.Value;
            return text == "null" ? null : text;
        }

        public String? ParseMessageID(String key)
        {
            var v = a[key];
            if (v == null) return null;
            var text = v.Value;
            if (text.Length == 0 || text == "null") return null;
            var comps = text.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (comps.Length != 2) return null;
            return text;
        }

        public String? ParseSignalID(String key)
        {
            var v = a[key];
            if (v == null) return null;
            var text = v.Value;
            if (text.Length == 0 || text == "null") return null;
            var comps = text.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (comps.Length != 3) return null;
            return text;
        }

        public double[]? ParseSignalValue(String key, bool optional)
        {
            var v = a[key];

            double[]? ret = null;
            if (!optional) ret = [0];
            if (v == null) return ret;

            String text = v.Value;
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
            var v = a[key];
            if (v == null) return defaultValue;
            try
            {
                var obj = Enum.Parse(type, v.Value);
                return obj == null ? defaultValue : obj;
            }
            catch (Exception ex) { Dump.Exception(ex); return defaultValue; }
        }

        public FloatPoint? ParsePoint(String key, FloatPoint? defaultPoint)
        {
            var v = a[key];
            if (v == null) return defaultPoint;

            var comps = v.Value.Split(',');
            if (comps.Length < 2) return defaultPoint;

            float x, y;
            if (!Single.TryParse(comps[0], out x) ||
                !Single.TryParse(comps[1], out y)) return defaultPoint;
            return new FloatPoint(x, y);
        }

        public ColorRGBA ParseColor(String key, ColorRGBA defaultColor)
        {
            var v = a[key];
            if (v == null) return defaultColor;

            var comps = v.Value.Split(',');
            if (comps.Length < 3) return defaultColor;

            int r, g, b;
            if (!Int32.TryParse(comps[0], out r) ||
                !Int32.TryParse(comps[1], out g) ||
                !Int32.TryParse(comps[2], out b)) return defaultColor;
            return new ColorRGBA((byte)r, (byte)g, (byte)b, 255);
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.0) XML attribute writer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.0) XML属性输出
    /// </summary>
    public class AttributeWriter(XmlElement element)
    {
        private XmlDocument x = element.OwnerDocument;
        private XmlAttributeCollection a = element.Attributes;

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

        public void WriteString(String key, String? value)
        {
            a.Append(x.CreateAttribute(key)).Value = value == null ? "null" : value;
        }

        public void WriteSignalValue(String key, double[]? values)
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
