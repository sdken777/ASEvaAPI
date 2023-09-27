/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.11.3\n
 * \n
 * Eto.Forms官方仓库: https://github.com/picoe/Eto \n
 * API文档: https://pages.picoe.ca/docs/api
 */

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using ASEva.Utility;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.11.3
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 11, 3, 2); // 修正OverlayLayout
        }

        /// <summary>
        /// (api:eto=2.10.5) 获取Eto库版本
        /// </summary>
        /// <returns>Eto库版本</returns>
        public static Version GetEtoLibVersion()
        {
            var apiEtoPath = Assembly.GetExecutingAssembly().Location;
            var etoLibPath = Path.GetDirectoryName(apiEtoPath) + Path.DirectorySeparatorChar + "Eto.dll";

            try
            {
                var etoLib = Assembly.LoadFrom(etoLibPath);
                var etoLibVersion = etoLib.GetName().Version;
                return etoLibVersion == null ? null : new Version(etoLibVersion.Major, etoLibVersion.Minor);
            }
            catch (Exception) { return null;}
        }

        /// <summary>
        /// (api:eto=2.11.0) 获取使用的第三方软件版权声明
        /// </summary>
        /// <returns>键为标题，值为版权声明</returns>
        public static Dictionary<String, String> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["Eto"] = ResourceLoader.LoadText("Eto.LICENSE");
            table["SharpGL"] = ResourceLoader.LoadText("SharpGL.LICENSE");
            table["SkiaSharp"] = ResourceLoader.LoadText("SkiaSharp.LICENSE");
            return table;
        }
    }
}
