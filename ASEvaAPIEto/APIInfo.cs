/*! \mainpage
 * \~English This library contains the APIs for Eto.Forms. See ASEva.UIEto for details. \n
 * \~Chinese 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * \~English This document corresponds to API version: 3.3.0 \n\n
 * \~Chinese 本文档对应API版本：3.3.0 \n\n
 * 
 * \~English Eto.Forms official repository: https://github.com/picoe/Eto \n
 * \~Chinese Eto.Forms官方仓库: https://github.com/picoe/Eto \n
 * \~English API reference: https://pages.picoe.ca/docs/api \n
 * \~Chinese API文档: https://pages.picoe.ca/docs/api
 */

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using ASEva.Utility;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=3.3.0
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:eto=3.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(3, 3, 0, 1); // Update log / 更新记录: 优化SetContextMenuAsNew。优化CommonImage.Create调用
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.0.0) Get Eto.Forms library's version
        /// </summary>
        /// <returns>The Eto.Forms library's version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.0.0) 获取Eto.Forms库版本
        /// </summary>
        /// <returns>Eto.Forms库版本</returns>
        public static Version? GetEtoLibVersion()
        {
            var entryFolder = EntryFolder.Path;
            if (entryFolder == null) return null;

            var etoLibPath = entryFolder + Path.DirectorySeparatorChar + "Eto.dll";

            try
            {
                var etoLib = Assembly.LoadFrom(etoLibPath);
                var etoLibVersion = etoLib.GetName().Version;
                return etoLibVersion == null ? null : new Version(etoLibVersion.Major, etoLibVersion.Minor);
            }
            catch (Exception ex) { Dump.Exception(ex); return null;}
        }

        /// \~English
        /// <summary>
        /// (api:eto=3.0.0) Get 3rd party software license notices
        /// </summary>
        /// <returns>The 3rd party software license notices</returns>
        /// \~Chinese
        /// <summary>
        /// (api:eto=3.0.0) 获取使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["Eto"] = ResourceLoader.LoadText("Eto.LICENSE") ?? "";
            table["SharpGL"] = ResourceLoader.LoadText("SharpGL.LICENSE") ?? "";
            table["SkiaSharp"] = ResourceLoader.LoadText("SkiaSharp.LICENSE") ?? "";
            table["OxyPlot"] = ResourceLoader.LoadText("OxyPlot.LICENSE") ?? "";
            return table;
        }
    }
}
