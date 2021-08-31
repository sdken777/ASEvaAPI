/*! \mainpage
 * 此类库为ASEva-API中基于Gtk#的部分。详见 ASEva.Gtk \n
 * 本文档对应API版本：1.1.0
 */

using System;

namespace ASEva.Gtk
{
    /// <summary>
    /// version=1.1.0
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:gtk=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 1, 0, 0); // 实现AppHandler
        }
    }
}
