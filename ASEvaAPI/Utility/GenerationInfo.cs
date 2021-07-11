using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) Generation的状态信息
    /// </summary>
    public enum GenerationProcessStatus
    {
        /// <summary>
        /// 未知（在较老版本中输出的Generation不包括状态信息）
        /// </summary>
        Unknown,

        /// <summary>
        /// 处理完毕
        /// </summary>
        Finished,

        /// <summary>
        /// 未处理完毕（在处理过程中手动停止或卡死自动跳过导致）
        /// </summary>
        NotFinished,
    }

    /// <summary>
    /// (api:app=2.0.0) 读写Generation的信息文件(info.xml)
    /// </summary>
    public class GenerationInfo
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// Generation ID
        /// </summary>
        public String GenerationID { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        public GenerationProcessStatus ProcessStatus { get; set; }

        /// <summary>
        /// 样本别名表
        /// </summary>
        public Dictionary<string, string> SampleAlias { get; set; }

        /// <summary>
        /// 创建Generation的软件版本信息（用于回溯）
        /// </summary>
        public Dictionary<string, Version> Versions { get; set; }

        /// <summary>
        /// Generation更新记录
        /// </summary>
        public Dictionary<DateTime, string> UpdateLogs { get; set; }

        private GenerationInfo()
        {
        }

        /// <summary>
        /// 创建信息文件对象（仅创建对象，不写入文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="generationID">Generation ID</param>
        /// <param name="status">状态信息</param>
        /// <param name="versions">生成Generation的软件版本信息</param>
        /// <returns>返回创建的对象</returns>
        public static GenerationInfo Create(String filePath, String generationID, GenerationProcessStatus status, Dictionary<string, Version> versions)
        {
            return Create(filePath, generationID, status, null, versions, null);
        }

        /// <summary>
        /// 创建信息文件对象（仅创建对象，不写入文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="generationID">Generation ID</param>
        /// <param name="status">状态信息</param>
        /// <param name="sampleAlias">样本别名表</param>
        /// <param name="versions">生成Generation的软件版本信息</param>
        /// <param name="updateLogs">Generation更新记录</param>
        /// <returns>返回创建的对象</returns>
        public static GenerationInfo Create(String filePath, String generationID, GenerationProcessStatus status, Dictionary<string, string> sampleAlias, Dictionary<string, Version> versions, Dictionary<DateTime, string> updateLogs)
        {
            if (filePath == null || filePath.Length == 0 || generationID == null || generationID.Length == 0 || status == GenerationProcessStatus.Unknown) return null;

            var info = new GenerationInfo();
            info.FilePath = filePath;
            info.GenerationID = generationID;
            info.ProcessStatus = status;
            info.SampleAlias = sampleAlias;
            info.Versions = versions;
            info.UpdateLogs = updateLogs;

            return info;
        }

        /// <summary>
        /// 从已有信息文件中读取
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回创建的对象</returns>
        public static GenerationInfo Load(String filePath)
        {
            if (filePath == null || filePath.Length == 0) return null;

            var defaultGenID = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(filePath));
            if (defaultGenID == null || defaultGenID.Length == 0) return null;

            GenerationInfo info = null;
            try
            {
                var xml = new XmlDocument();
                xml.Load(filePath);
                var attribs = xml.DocumentElement.Attributes;

                info = new GenerationInfo();

                if (attribs["gen_id"] != null)
                {
                    info.GenerationID = attribs["gen_id"].Value;
                }
                else
                {
                    info.GenerationID = defaultGenID;
                }

                if (attribs["finished"] != null)
                {
                    info.ProcessStatus = attribs["finished"].Value == "yes" ? GenerationProcessStatus.Finished : GenerationProcessStatus.NotFinished;
                }
                else
                {
                    info.ProcessStatus = GenerationProcessStatus.Unknown;
                }

                info.SampleAlias = new Dictionary<string, string>();
                var aliasNodes = xml.DocumentElement.GetElementsByTagName("alias");
                foreach (XmlElement aliasNode in aliasNodes)
                {
                    info.SampleAlias[aliasNode.Attributes["sample"].Value] = aliasNode.InnerText;
                }

                info.Versions = new Dictionary<string, Version>();
                var versionNodes = xml.DocumentElement.GetElementsByTagName("version");
                foreach (XmlElement versionNode in versionNodes)
                {
                    info.Versions[versionNode.Attributes["key"].Value] = Version.Parse(versionNode.InnerText);
                }

                info.UpdateLogs = new Dictionary<DateTime, string>();
                var updateNodes = xml.DocumentElement.GetElementsByTagName("update");
                foreach (XmlElement updateNode in updateNodes)
                {
                    info.UpdateLogs[DateTime.ParseExact(updateNode.Attributes["time"].Value, "yyyy-MM-dd-HH-mm-ss", null)] = updateNode.InnerText;
                }
            }
            catch (Exception) { }

            if (info == null)
            {
                info = new GenerationInfo()
                {
                    GenerationID = defaultGenID,
                    ProcessStatus = GenerationProcessStatus.Unknown,
                };
            }

            info.FilePath = filePath;
            return info;
        }

        /// <summary>
        /// 保存信息文件
        /// </summary>
        public void Save()
        {
            if (FilePath == null) return;
            if (ProcessStatus == GenerationProcessStatus.Unknown) return;

            try
            {
                var root = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            }
            catch (Exception) { return; }

            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));
            var rootNode = xml.AppendChild(xml.CreateElement("root")) as XmlElement;

            rootNode.Attributes.Append(xml.CreateAttribute("gen_id")).Value = GenerationID;
            rootNode.Attributes.Append(xml.CreateAttribute("finished")).Value = ProcessStatus == GenerationProcessStatus.Finished ? "yes" : "no";

            if (SampleAlias != null)
            {
                foreach (var alias in SampleAlias)
                {
                    var aliasNode = rootNode.AppendChild(xml.CreateElement("alias")) as XmlElement;
                    aliasNode.Attributes.Append(xml.CreateAttribute("sample")).Value = alias.Key;
                    aliasNode.InnerText = alias.Value;
                }
            }

            if (Versions != null)
            {
                foreach (var item in Versions)
                {
                    var versionNode = rootNode.AppendChild(xml.CreateElement("version")) as XmlElement;
                    versionNode.Attributes.Append(xml.CreateAttribute("key")).Value = item.Key;
                    versionNode.InnerText = item.Value.ToString();
                }
            }

            if (UpdateLogs != null)
            {
                foreach (var update in UpdateLogs)
                {
                    var updateNode = rootNode.AppendChild(xml.CreateElement("update")) as XmlElement;
                    updateNode.Attributes.Append(xml.CreateAttribute("time")).Value = update.Key.ToString("yyyy-MM-dd-HH-mm-ss");
                    updateNode.InnerText = update.Value;
                }
            }

            xml.Save(FilePath);
        }
    }
}
