using System;
using System.IO;
using System.Reflection;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.5.1) For getting the directory of entry module
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.5.1) 用于获取入口模块所在路径
    /// </summary>
    public class EntryFolder
    {
        public static String? Path
        {
            get
            {
                if (specifiedEntryFolder != null) return specifiedEntryFolder;
                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly == null) return null;
                else return System.IO.Path.GetDirectoryName(entryAssembly.Location);
            }
        }

        /// \~English
        /// <summary>
        /// Explicitly specify the path
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 显式指定路径
        /// </summary>
        public static void SpecifyEntryFolder(String folderPath)
        {
            if (specifiedEntryFolder != null) return;
            if (folderPath != null && Directory.Exists(folderPath)) specifiedEntryFolder = folderPath;
        }

        private static String? specifiedEntryFolder = null;
    }
}