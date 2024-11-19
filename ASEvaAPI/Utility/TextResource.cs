using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System.Text;
using ASEva;

namespace ASEva.Utility
{
    #pragma warning disable CS1571
    /// \~English
    /// <summary>
    /// (api:app=2.1.0) Multi-language text resource
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.1.0) 多语言文本资源
    /// </summary>
    public class TextResource
    {
        /// \~English
        /// <summary>
        /// Load the resource
        /// </summary>
        /// <param name="xmlFileName">Resource name</param>
        /// <param name="languageCode">Language code, "en" is English, "ch" is Chinese, set to null to get by ASEva.Agency.GetAppLanguage internally</param>
        /// <returns>Multi-language text resource object, null if failed to load</returns>
        /// \~Chinese
        /// <summary>
        /// 加载多语言文本资源
        /// </summary>
        /// <param name="xmlFileName">资源文件名</param>
        /// <param name="languageCode">语言代号，en表示英文，ch表示中文，null则通过 ASEva.Agency.GetAppLanguage 获取</param>
        /// <returns>多语言文本资源对象，获取失败则返回null</returns>
        public static TextResource Load(String xmlFileName, String languageCode = null)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(xmlFileName);
            if (instream == null) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            instream.Close();

            return Load(data, languageCode);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.6.14) Load from binary data
        /// </summary>
        /// <param name="xmlFileData">XML binary data</param>
        /// <param name="languageCode">Language code, "en" is English, "ch" is Chinese, set to null to get by ASEva.Agency.GetAppLanguage internally</param>
        /// <returns>Multi-language text resource object, null if failed to load</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.6.14) 从XML文件数据加载多语言文本资源
        /// </summary>
        /// <param name="xmlFileData">XML文件数据</param>
        /// <param name="languageCode">语言代号，en表示英文，ch表示中文，null则通过 ASEva.Agency.GetAppLanguage 获取</param>
        /// <returns>多语言文本资源对象，获取失败则返回null</returns>
        public static TextResource Load(byte[] xmlFileData, String languageCode)
        {
            if (xmlFileData == null || xmlFileData.Length <= 3) return null;

            if (xmlFileData[0] == 0xEF && xmlFileData[1] == 0xBB && xmlFileData[2] == 0xBF)
            {
                var buf = new byte[xmlFileData.Length - 3];
                Array.Copy(xmlFileData, 3, buf, 0, buf.Length);
                xmlFileData = buf;
            }

            var xmlString = Encoding.UTF8.GetString(xmlFileData);
            if (xmlString == null) return null;

            var langCodes = new List<String>();

            var lang = String.IsNullOrEmpty(languageCode) ? Agency.GetAppLanguage() : languageCode;
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
            try { xml.LoadXml(xmlString); }
            catch (Exception) { return null; }

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

        /// \~English
        /// <summary>
        /// Get the text with the ID
        /// </summary>
        /// <value>Text for the specified ID</value>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID对应的文本
        /// </summary>
        /// <value>指定ID对应的文本</value>
        public String this[String id]
        {
            get
            {
                if (dict.ContainsKey(id)) return dict[id];
                else return null;
            }
        }

        /// \~English
        /// <summary>
        /// Get the format text with the ID and use it to create the final text
        /// </summary>
        /// <param name="id">Text ID</param>
        /// <param name="args">Arguments for the formats</param>
        /// <returns>Output text</returns>
        /// \~Chinese
        /// <summary>
        /// 以指定ID对应的文本作为格式描述，输出文本
        /// </summary>
        /// <param name="id">指定ID</param>
        /// <param name="args">格式描述中的参数值</param>
        /// <returns>输出文本</returns>
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