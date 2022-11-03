using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) Session的meta信息
    /// </summary>
    public class SessionMeta
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// Session ID
        /// </summary>
        public DateTime ID { get; set; }

        /// <summary>
        /// Session GUID
        /// </summary>
        public String GUID { get; set; }

        /// <summary>
        /// Session长度，单位秒
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// CPU时间模型
        /// </summary>
        public CPUTimeModel CPUTimeModel { get; set; }

        /// <summary>
        /// 主机Posix时间模型
        /// </summary>
        public PosixTimeModel HostPosixModel { get; set; }

        /// <summary>
        /// 卫星Posix时间模型
        /// </summary>
        public PosixTimeModel GNSSPosixModel { get; set; }

        /// <summary>
        /// 主机是否与授时服务器同步
        /// </summary>
        public bool HostSync { get; set; }

        /// <summary>
        /// Session的注释说明
        /// </summary>
        public String Comment { get; set; }

        /// <summary>
        /// Session的截取ID，origin表示原始数据
        /// </summary>
        public String Pick { get; set; }

        /// <summary>
        /// Session的截取属性列表
        /// </summary>
        public Dictionary<String, String> PickProperties { get; set; }

        /// <summary>
        /// Session的属性
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// 采集Session的软件版本信息（用于回溯）
        /// </summary>
        public Dictionary<string, Version> Versions { get; set; }

        /// <summary>
        /// 按本地时间表示的主机Posix时间模型的session开始时间
        /// </summary>
        public DateTime? StartTimeLocal
        {
            get
            {
                if (HostPosixModel == null) return null;
                else
                {
                    var origin = new DateTime(1970, 1, 1, 0, 0, 0);
                    return TimeZoneInfo.ConvertTimeFromUtc(origin.AddMilliseconds(HostPosixModel.StartPosix), TimeZoneInfo.Local);
                }
            }
            set
            {
                if (value == null) HostPosixModel = null;
                else
                {
                    var origin = new DateTime(1970, 1, 1, 0, 0, 0);
                    ulong startPosix = (ulong)(TimeZoneInfo.ConvertTimeToUtc(value.Value, TimeZoneInfo.Local) - origin).TotalMilliseconds;
                    if (HostPosixModel == null) HostPosixModel = new PosixTimeModel();
                    HostPosixModel.StartPosix = startPosix;
                }
            }
        }

        /// <summary>
        /// 主机Posix时间模型的时间比例
        /// </summary>
        public double TimeRatioToLocal
        {
            get
            {
                if (HostPosixModel == null) return 1.0;
                else return HostPosixModel.TimeRatio;
            }
            set
            {
                if (HostPosixModel != null) HostPosixModel.TimeRatio = value;
            }
        }

        /// <summary>
        /// 按UTC时间表示的卫星Posix时间模型的session开始时间
        /// </summary>
        public DateTime? StartTimeUTC
        {
            get
            {
                if (GNSSPosixModel == null) return null;
                else
                {
                    var origin = new DateTime(1970, 1, 1, 0, 0, 0);
                    return origin.AddMilliseconds(GNSSPosixModel.StartPosix);
                }
            }
            set
            {
                if (value == null) GNSSPosixModel = null;
                else
                {
                    var origin = new DateTime(1970, 1, 1, 0, 0, 0);
                    ulong startPosix = (ulong)(value.Value - origin).TotalMilliseconds;
                    if (GNSSPosixModel == null) GNSSPosixModel = new PosixTimeModel();
                    GNSSPosixModel.StartPosix = startPosix;
                }
            }
        }

        /// <summary>
        /// 卫星Posix时间模型的时间比例
        /// </summary>
        public double TimeRatioToUTC
        {
            get
            {
                if (GNSSPosixModel == null) return 1.0;
                else return GNSSPosixModel.TimeRatio;
            }
            set
            {
                if (GNSSPosixModel != null) GNSSPosixModel.TimeRatio = value;
            }
        }

        private SessionMeta()
        {
        }

        /// <summary>
        /// 创建meta文件对象（仅创建对象，不写入文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="id">Session ID</param>
        /// <param name="guid">Session GUID</param>
        /// <param name="length">Session长度</param>
        /// <param name="startTimeUTC">按UTC时间表示的卫星Posix时间模型的session开始时间</param>
        /// <param name="timeRatioToUTC">卫星Posix时间模型的时间比例</param>
        /// <param name="startTimeLocal">按本地时间表示的主机Posix时间模型的session开始时间</param>
        /// <param name="timeRatioToLocal">主机Posix时间模型的时间比例</param>
        /// <param name="comment">Session的注释说明</param>
        /// <param name="versions">采集Session的软件版本信息</param>
        /// <param name="props">Session的属性</param>
        /// <param name="pick">Session的截取ID，origin表示原始数据</param>
        /// <param name="pickProps">Session的截取属性列表</param>
        /// <returns>返回创建的对象</returns>
        public static SessionMeta Create(String filePath, DateTime id, String guid, double? length, DateTime? startTimeUTC, double timeRatioToUTC, DateTime? startTimeLocal, double timeRatioToLocal, String comment, Dictionary<String, Version> versions, Dictionary<String, String> props, String pick, Dictionary<String, String> pickProps)
        {
            if (filePath == null || filePath.Length == 0) return null;

            var meta = new SessionMeta();
            meta.FilePath = filePath;
            meta.ID = id;
            meta.GUID = guid;
            if (meta.GUID == null) meta.GUID = Guid.NewGuid().ToString();
            meta.Length = length;
            meta.StartTimeUTC = startTimeUTC;
            meta.StartTimeLocal = startTimeLocal;
            meta.TimeRatioToUTC = timeRatioToUTC;
            meta.TimeRatioToLocal = timeRatioToLocal;
            meta.Comment = comment;
            meta.Versions = versions;
            meta.Properties = props;
            if (meta.Properties == null) meta.Properties = new Dictionary<string, string>();
            meta.Pick = pick;
            meta.PickProperties = pickProps;
            if (meta.PickProperties == null) meta.PickProperties = new Dictionary<string, string>();

            return meta;
        }

        /// <summary>
        /// (api:app=2.7.0) 创建meta文件对象（仅创建对象，不写入文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="id">Session ID</param>
        /// <param name="guid">Session GUID</param>
        /// <param name="length">Session长度</param>
        /// <param name="cpuTimeModel">CPU时间模型</param>
        /// <param name="hostPosixModel">主机Posix时间模型</param>
        /// <param name="gnssPosixModel">卫星Posix时间模型</param>
        /// <param name="hostSync">主机是否与授时服务器同步</param>
        /// <param name="comment">Session的注释说明</param>
        /// <param name="versions">采集Session的软件版本信息</param>
        /// <param name="props">Session的属性</param>
        /// <param name="pick">Session的截取ID，origin表示原始数据</param>
        /// <param name="pickProps">Session的截取属性列表</param>
        /// <returns>返回创建的对象</returns>
        public static SessionMeta Create(String filePath, DateTime id, String guid, double? length, CPUTimeModel cpuTimeModel, PosixTimeModel hostPosixModel, PosixTimeModel gnssPosixModel, bool hostSync, String comment, Dictionary<String, Version> versions, Dictionary<String, String> props, String pick, Dictionary<String, String> pickProps)
        {
            if (filePath == null || filePath.Length == 0) return null;

            var meta = new SessionMeta();
            meta.FilePath = filePath;
            meta.ID = id;
            meta.GUID = guid;
            if (meta.GUID == null) meta.GUID = Guid.NewGuid().ToString();
            meta.Length = length;
            meta.CPUTimeModel = cpuTimeModel;
            meta.HostPosixModel = hostPosixModel;
            meta.GNSSPosixModel = gnssPosixModel;
            meta.HostSync = hostSync;
            meta.Comment = comment;
            meta.Versions = versions;
            meta.Properties = props;
            if (meta.Properties == null) meta.Properties = new Dictionary<string, string>();
            meta.Pick = pick;
            meta.PickProperties = pickProps;
            if (meta.PickProperties == null) meta.PickProperties = new Dictionary<string, string>();

            return meta;
        }

        /// <summary>
        /// 从已有meta文件中读取
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回创建的对象</returns>
        public static SessionMeta Load(String filePath)
        {
            if (!File.Exists(filePath)) return null;

            SessionMeta meta = null;
            try
            {
                var xml = new XmlDocument();
                xml.Load(filePath);

                var root = xml.DocumentElement;
                var attribs = root.Attributes;

                meta = new SessionMeta();

                bool found = false;
                try
                {
                    var id = DateTime.ParseExact(attribs["session_id"].Value, "yyyy-MM-dd-HH-mm-ss", null);
                    var length = Convert.ToDouble(attribs["length"].Value);
                    meta.ID = id;
                    meta.Length = length;
                    found = true;
                }
                catch (Exception) { }

                if (!found)
                {
                    try
                    {
                        var begin = DateTime.ParseExact(attribs["begin"].Value, "yyyy-MM-dd-HH-mm-ss", null);
                        var end = DateTime.ParseExact(attribs["end"].Value, "yyyy-MM-dd-HH-mm-ss-fff", null);
                        if (end >= begin)
                        {
                            meta.ID = begin;
                            meta.Length = (end - begin).TotalSeconds;
                        }
                    }
                    catch (Exception) { }
                }

                if (attribs["guid"] != null)
                {
                    meta.GUID = attribs["guid"].Value;
                }

                try
                {
                    var commentNodes = root.GetElementsByTagName("comment");
                    if (commentNodes.Count > 0)
                    {
                        meta.Comment = commentNodes[0].InnerText;
                    }
                    else if (attribs["comment"] != null)
                    {
                        meta.Comment = attribs["comment"].Value;
                    }
                    else meta.Comment = "";
                }
                catch (Exception) { meta.Comment = ""; }

                try
                {
                    if (attribs["start_cpu_tick"] != null && attribs["cpu_ticks_per_second"] != null)
                    {
                        meta.CPUTimeModel = new CPUTimeModel
                        {
                            StartCPUTick = Convert.ToUInt64(attribs["start_cpu_tick"].Value),
                            CPUTicksPerSecond = Convert.ToUInt64(attribs["cpu_ticks_per_second"].Value),
                        };
                    }
                    else meta.CPUTimeModel = null;
                }
                catch (Exception) { meta.CPUTimeModel = null; }

                try
                {
                    if (attribs["start_posix_host"] != null && attribs["time_ratio_host"] != null && attribs["host_sync"] != null)
                    {
                        meta.HostPosixModel = new PosixTimeModel
                        {
                            StartPosix = Convert.ToUInt64(attribs["start_posix_host"].Value),
                            TimeRatio = Convert.ToDouble(attribs["time_ratio_host"].Value),
                        };
                        meta.HostSync = attribs["host_sync"].Value == "yes";
                    }
                    else if (attribs["start_posix_local"] != null && attribs["start_posix_local"].Value != "unknown" && attribs["time_ratio_to_local"] != null)
                    {
                        meta.HostPosixModel = new PosixTimeModel
                        {
                            StartPosix = Convert.ToUInt64(attribs["start_posix_local"].Value),
                            TimeRatio = Convert.ToDouble(attribs["time_ratio_to_local"].Value),
                        };
                        meta.HostSync = false;
                    }
                    else
                    {
                        meta.HostPosixModel = null;
                        meta.HostSync = false;
                    }
                }
                catch (Exception)
                {
                    meta.HostPosixModel = null;
                    meta.HostSync = false;
                }

                try
                {
                    if (attribs["start_posix_gnss"] != null && attribs["time_ratio_gnss"] != null)
                    {
                        meta.GNSSPosixModel = new PosixTimeModel
                        {
                            StartPosix = Convert.ToUInt64(attribs["start_posix_gnss"].Value),
                            TimeRatio = Convert.ToDouble(attribs["time_ratio_gnss"].Value),
                        };
                    }
                    else if (attribs["start_posix_utc"] != null && attribs["start_posix_utc"].Value != "unknown" && attribs["time_ratio_to_utc"] != null)
                    {
                        meta.GNSSPosixModel = new PosixTimeModel
                        {
                            StartPosix = Convert.ToUInt64(attribs["start_posix_utc"].Value),
                            TimeRatio = Convert.ToDouble(attribs["time_ratio_to_utc"].Value),
                        };
                    }
                    else if (attribs["start_time_utc"] != null && attribs["start_time_utc"].Value != "unknown" && attribs["time_ratio_to_utc"] != null)
                    {
                        var startTimeUTC = DateTime.ParseExact(attribs["start_time_utc"].Value, "yyyy-MM-dd-HH-mm-ss-fff", null);
                        var startPosix = (ulong)(startTimeUTC - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
                        meta.GNSSPosixModel = new PosixTimeModel
                        {
                            StartPosix = startPosix,
                            TimeRatio = Convert.ToDouble(attribs["time_ratio_to_utc"].Value),
                        };
                    }
                    else meta.GNSSPosixModel = null;
                }
                catch (Exception) { meta.GNSSPosixModel = null; }

                var pickNodes = root.GetElementsByTagName("pick");
                foreach (XmlElement pickNode in pickNodes)
                {
                    var c = new AttributeParser(pickNode);
                    var pickID = c.ParseString("id", null);
                    if (pickID == null) pickID = c.ParseString("pick_time", null);
                    meta.Pick = pickID;

                    meta.PickProperties = new Dictionary<string, string>();
                    var pickPropertyNodes = pickNode.GetElementsByTagName("property");
                    foreach (XmlElement propertyNode in pickPropertyNodes)
                    {
                        var cp = new AttributeParser(propertyNode);
                        meta.PickProperties[cp.ParseString("key", null)] = cp.ParseString("value", null);
                    }

                    break; // 仅一个pick节点
                }

                meta.Properties = new Dictionary<string, string>();
                var propertyNodes = root.GetElementsByTagName("property");
                foreach (XmlElement propertyNode in propertyNodes)
                {
                    var cp = new AttributeParser(propertyNode);
                    meta.Properties[cp.ParseString("key", null)] = cp.ParseString("value", null);
                }

                meta.Versions = new Dictionary<string, Version>();
                if (attribs["software"] != null && attribs["software_version"] != null)
                {
                    try
                    {
                        meta.Versions[attribs["software"].Value] = Version.Parse(attribs["software_version"].Value);
                    }
                    catch (Exception) { }
                }
                else
                {
                    var versionNodes = xml.GetElementsByTagName("version");
                    foreach (XmlElement versionNode in versionNodes)
                    {
                        try
                        {
                            meta.Versions[versionNode.Attributes["key"].Value] = Version.Parse(versionNode.InnerText);
                        }
                        catch (Exception) { }
                    }
                }

                meta.FilePath = filePath;
            }
            catch (Exception) { }

            return meta;
        }

        /// <summary>
        /// 保存meta文件
        /// </summary>
        public void Save()
        {
            if (FilePath == null) return;

            try
            {
                var root = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            }
            catch (Exception) { return; }

            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));
            var rootNode = xml.AppendChild(xml.CreateElement("root")) as XmlElement;
            var cw = new AttributeWriter(rootNode);

            if (GUID != null) cw.WriteString("guid", GUID);

            cw.WriteString("session_id", ID.ToString("yyyy-MM-dd-HH-mm-ss"));
            cw.WriteString("length", Length == null ? "0" : Length.Value.ToString("F3"));

            if (CPUTimeModel != null)
            {
                cw.WriteLong("start_cpu_tick", (long)CPUTimeModel.StartCPUTick);
                cw.WriteLong("cpu_ticks_per_second", (long)CPUTimeModel.CPUTicksPerSecond);
            }

            if (HostPosixModel != null)
            {
                cw.WriteLong("start_posix_host", (long)HostPosixModel.StartPosix);
                cw.WriteDouble("time_ratio_host", HostPosixModel.TimeRatio);
                cw.WriteBool("host_sync", HostSync, "yes", "no");
                cw.WriteLong("start_posix_local", (long)HostPosixModel.StartPosix); // 兼容老版本
                cw.WriteDouble("time_ratio_to_local", HostPosixModel.TimeRatio); // 兼容老版本
            }

            if (GNSSPosixModel != null)
            {
                cw.WriteLong("start_posix_gnss", (long)GNSSPosixModel.StartPosix);
                cw.WriteDouble("time_ratio_gnss", GNSSPosixModel.TimeRatio);
                cw.WriteLong("start_posix_utc", (long)GNSSPosixModel.StartPosix); // 兼容老版本
                cw.WriteDouble("time_ratio_to_utc", GNSSPosixModel.TimeRatio); // 兼容老版本
                cw.WriteString("start_time_utc", StartTimeUTC.Value.ToString("yyyy-MM-dd-HH-mm-ss-fff")); // 兼容老版本
            }

            var pickNode = rootNode.AppendChild(xml.CreateElement("pick")) as XmlElement;
            cw = new AttributeWriter(xml, pickNode);
            cw.WriteString("id", Pick == null ? "origin" : Pick);

            if (Comment != null)
            {
                rootNode.AppendChild(xml.CreateElement("comment")).InnerText = Comment;
            }

            if (PickProperties != null)
            {
                foreach (var item in PickProperties)
                {
                    cw = new AttributeWriter(xml, pickNode.AppendChild(xml.CreateElement("property")) as XmlElement);
                    cw.WriteString("key", item.Key);
                    cw.WriteString("value", item.Value);
                }
            }

            if (Properties != null)
            {
                foreach (var item in Properties)
                {
                    cw = new AttributeWriter(xml, rootNode.AppendChild(xml.CreateElement("property")) as XmlElement);
                    cw.WriteString("key", item.Key);
                    cw.WriteString("value", item.Value);
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

            xml.Save(FilePath);
        }
    }
}
