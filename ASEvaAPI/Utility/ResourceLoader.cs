using System;
using System.Text;
using System.Reflection;

namespace ASEva.Utility
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 资源读取器
    /// </summary>
    public class ResourceLoader
    {
        /// \~English
        /// 
        /// \~Chinese
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
            if (instream.Length > 0) instream.Read(data, 0, data.Length);
            instream.Close();

            return data;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.8) 按指定名称读取资源（UTF-8文本），并转为文本
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <returns>文本数据，若找不到资源则返回null</returns>
        public static String LoadText(String name)
        {
            var instream = Assembly.GetCallingAssembly().GetManifestResourceStream(name);
            if (instream == null) return null;

            var data = new byte[instream.Length];
            if (instream.Length > 0) instream.Read(data, 0, data.Length);
            instream.Close();

            if (data.Length >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
            {
                var buf = new byte[data.Length - 3];
                if (buf.Length > 0) Array.Copy(data, 3, buf, 0, buf.Length);
                data = buf;
            }

            return Encoding.UTF8.GetString(data);
        }
    }
}
