/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.0.10
 */

using System;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.0.10
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 0, 10, 1); // 针对无法获取屏幕范围的情况
        }
    }
}
