/*! \mainpage
 * 此类库为ASEva-API中基于Gtk#的部分。详见 ASEva.UIGtk \n
 * 本文档对应API版本：2.0.6
 */

using System;

namespace ASEva.UIGtk
{
    /// <summary>
    /// version=2.0.6
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:gtk=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 0, 6, 3); // 增加Redirection.RedirectMarshaller，解决arm下启动文件对话框问题
        }
    }
}
