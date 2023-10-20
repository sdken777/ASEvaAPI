using System;
using System.Xml;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) XML utility
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) XML相关功能类
    /// </summary>
    public class Xml
    {
        /// \~English
        /// <summary>
        /// Create a XmlDocument object with a root node
        /// </summary>
        /// <param name="rootTag">Tag name of root node</param>
        /// <returns>Created XmlDocument object</returns>
        /// \~Chinese
        /// <summary>
        /// 创建一个包含根节点的空XML文档
        /// </summary>
        /// <param name="rootTag">根节点的标签名</param>
        /// <returns>新创建的XML文档</returns>
        public static XmlDocument Create(String rootTag)
        {
            if (rootTag == null || rootTag.Length == 0) rootTag = "root";

            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));
            xml.AppendChild(xml.CreateElement(rootTag));

            return xml;
        }
    }
}
