/*! \mainpage
 * \~English This library contains the APIs for Avalonia. See ASEva.UIAvalonia for details. \n
 * \~Chinese 此类库为ASEva-API中基于Avalonia的部分。详见 ASEva.UIAvalonia \n
 * \~English This document corresponds to API version: 1.0.3 \n\n
 * \~Chinese 本文档对应API版本：1.0.3 \n\n
 * 
 * \~English Official documents: https://docs.avaloniaui.net \n
 * \~Chinese 官方文档: https://docs.avaloniaui.net
 */

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using ASEva.Utility;

namespace ASEva.UIAvalonia
{
    /// <summary>
    /// version=1.0.3
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 0, 3, 0); // Update log / 更新记录: 新增TextResourceExtensions.cs
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.0) Get Avalonia library's version
        /// </summary>
        /// <returns>The Avalonia library's version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.0) 获取Avalonia库版本
        /// </summary>
        /// <returns>Avalonia库版本</returns>
        public static Version GetAvaloniaLibVersion()
        {
            var apiAvaloniaPath = Assembly.GetExecutingAssembly().Location;
            var avaloniaLibPath = Path.GetDirectoryName(apiAvaloniaPath) + Path.DirectorySeparatorChar + "Avalonia.dll";

            try
            {
                var avaloniaLib = Assembly.LoadFrom(avaloniaLibPath);
                var avaloniaLibVersion = avaloniaLib.GetName().Version;
                return avaloniaLibVersion == null ? null : new Version(avaloniaLibVersion.Major, avaloniaLibVersion.Minor);
            }
            catch (Exception) { return null;}
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.0) Get 3rd party software license notices
        /// </summary>
        /// <returns>The 3rd party software license notices</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.0) 获取使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["Avalonia"] = ResourceLoader.LoadText("Avalonia.LICENSE");
            table["CustomMessageBox.Avalonia"] = ResourceLoader.LoadText("CustomMessageBox.Avalonia.LICENSE");
            return table;
        }
    }
}
