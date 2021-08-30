/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.Eto \n
 * 本文档对应API版本：1.1.0
 */

using System;

namespace ASEva.Eto
{
    /// <summary>
    /// version=1.1.0
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 1, 0, 0); // 新增ImageResourceLoader
        }
    }
}
