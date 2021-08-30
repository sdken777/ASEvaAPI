using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System.Text;
using ASEva;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.1.0) 多语言文本资源
    /// </summary>
    public class TextResource
    {
        public static TextResource Load(String xmlFileName)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(xmlFileName);
            if (instream == null) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            instream.Close();

            var xmlString = Encoding.UTF8.GetString(data);
            if (xmlString == null) return null;

            var langCodes = new List<String>();

            var lang = Agency.GetAppLanguage();
            if (lang == null || lang == "en")
            {
                langCodes.Add("en");
                langCodes.Add("ch");
            }
            else if (lang == "ch")
            {
                langCodes.Add("ch");
                langCodes.Add("en");
            }
            else return null;

            var xml = new XmlDocument();
            xml.LoadXml(xmlString);

            var output = new TextResource();
            foreach (XmlElement elem in xml.DocumentElement.GetElementsByTagName("t"))
            {
                if (elem.Attributes["id"] == null) continue;

                var id = elem.Attributes["id"].Value;
                if (String.IsNullOrEmpty(id) || output.dict.ContainsKey(id)) continue;

                String text = null;
                foreach (var code in langCodes)
                {
                    var codeNodes = elem.GetElementsByTagName(code);
                    if (codeNodes.Count > 0)
                    {
                        text = codeNodes[0].InnerText;
                        break;
                    }
                }

                if (text != null) output.dict[id] = text;
            }

            return output;
        }

        private TextResource()
        {
            dict = new Dictionary<string, string>();
        }

        public String this[String id]
        {
            get
            {
                if (dict.ContainsKey(id)) return dict[id];
                else return null;
            }
        }

        public String Format(String id, params object[] args)
        {
            if (dict.ContainsKey(id))
            {
                return String.Format(dict[id], args);
            }
            else return null;
        }

        private Dictionary<String, String> dict;
    }
}