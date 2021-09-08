/*! \mainpage
 * 此类库为ASEva-API中基于Gtk#的部分。详见 ASEva.UIGtk \n
 * 本文档对应API版本：2.0.3
 */

using System;

namespace ASEva.UIGtk
{
    /// <summary>
    /// version=2.0.3
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:gtk=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 0, 3, 0); // 实现ColorPickerHandler，解决默认handler无法退出问题
        }
    }
}
