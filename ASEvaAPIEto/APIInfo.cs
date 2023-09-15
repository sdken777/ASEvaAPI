/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.10.6\n
 * \n
 * Eto.Forms官方仓库: https://github.com/picoe/Eto \n
 * API文档: https://pages.picoe.ca/docs/api
 */

using System;
using System.IO;
using System.Reflection;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.10.6
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 10, 6, 0); // 新增TextBitmap.FastModeDrawOffset
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
    }
}
