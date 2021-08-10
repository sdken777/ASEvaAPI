using System;
using System.Text;
using System.Reflection;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.0.0) 资源读取器
    /// </summary>
    public class ResourceLoader
    {
        /// <summary>
        /// 按指定名称读取资源
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <returns>资源数据，若找不到资源则返回null</returns>
        public static byte[] Load(String name)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(name);
            if (instream == null) return null;

            var data = new byte[instream.Length];
            instream.Read(data, 0, data.Length);
            return data;
        }

        /// <summary>
        /// (api:app=2.0.8) 按指定名称读取资源，并转为文本
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <returns>文本数据，若找不到资源则返回null</returns>
        public static String LoadText(String name)
        {
            var data = Load(name);
            if (data == null) return null;

            return Encoding.UTF8.GetString(data);
        }
    }
}
