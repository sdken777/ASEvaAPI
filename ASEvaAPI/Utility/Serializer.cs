using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) XML/Json object reader and writer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) XML/Json的对象读写与文件读写
    /// </summary>
    public class Serializer
    {
        /// \~English
        /// <summary>
        /// Convert object to XML string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对象转XML字符串
        /// </summary>
        public static string ObjectToXml(object obj)
        {
            try
            {
                using (var memory = new MemoryStream())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(memory, obj);
                    memory.Position = 0;
                    using (var reader = new StreamReader(memory))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception) { return null; }
        }

        /// \~English
        /// <summary>
        /// Convert XML string to object
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// XML字符串转对象
        /// </summary>
        public static object XmlToObject(string xml, Type objectType)
        {
            try
            {
                using (var memory = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
                {
                    var serialize = new XmlSerializer(objectType);
                    return serialize.Deserialize(memory);
                }
            }
            catch (Exception) { return null; }
        }

        /// \~English
        /// <summary>
        /// Write object to XML file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对象写入XML文件
        /// </summary>
        public static bool ObjectToXmlFile(object obj, string file)
        {
            try
            {
                var fs = new FileStream(file, FileMode.Create);
                var xml = new XmlSerializer(obj.GetType());
                xml.Serialize(fs, obj);
                fs.Close();
                return true;
            }
            catch (Exception) { return false; }
        }

        /// \~English
        /// <summary>
        /// Load object from XML file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 从XML文件读取对象
        /// </summary>
        public static object XmlFileToObject(string file, Type objectType)
        {
            try
            {
                var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var serializer = new XmlSerializer(objectType);
                var data = serializer.Deserialize(stream);
                stream.Close();
                return data;
            }
            catch (Exception) { return null; }
        }

        /// \~English
        /// <summary>
        /// Convert object to Json String
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对象转JSON字符串
        /// </summary>
        public static string ObjectToJson(object obj)
        {
            try
            {
                using (var memory = new MemoryStream())
                {
                    var serializer = new DataContractJsonSerializer(obj.GetType());
                    serializer.WriteObject(memory, obj);
                    memory.Position = 0;
                    using (var reader = new StreamReader(memory))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception) { return null; }
        }

        /// \~English
        /// <summary>
        /// Convert Json string to object
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// JSON字符串转对象
        /// </summary>
        public static object JsonToObject(string json, Type objectType)
        {
            try
            {
                using (var memory = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    var serialize = new DataContractJsonSerializer(objectType);
                    return serialize.ReadObject(memory);
                }
            }
            catch (Exception) { return null; }
        }

        /// \~English
        /// <summary>
        /// Write object to Json file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对象写入JSON文件
        /// </summary>
        public static bool ObjectToJsonFile(object obj, string file)
        {
            try
            {
                var fs = new FileStream(file, FileMode.Create);
                var json = new DataContractJsonSerializer(obj.GetType());
                json.WriteObject(fs, obj);
                fs.Close();
                return true;
            }
            catch (Exception) { return false; }
        }

        /// \~English
        /// <summary>
        /// Load object from Json file
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 从JSON文件读取对象
        /// </summary>
        public static object JsonFileToObject(string file, Type objectType)
        {
            try
            {
                var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var serializer = new DataContractJsonSerializer(objectType);
                var data = serializer.ReadObject(stream);
                stream.Close();
                return data;
            }
            catch (Exception) { return null; }
        }
    }
}