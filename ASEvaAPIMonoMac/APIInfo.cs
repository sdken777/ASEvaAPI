/*! \mainpage
 * 此类库为ASEva-API中基于MonoMac的部分。详见 ASEva.UIMonoMac \n
 * 本文档对应API版本：1.3.5\n
 * \n
 * MonoMac仓库(.NET Core分支): https://github.com/cwensley/monomac \n
 * API文档(Swift/ObjC): https://developer.apple.com/documentation/appkit
 */

using System;

namespace ASEva.UIGtk
{
    /// <summary>
    /// version=1.3.5
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:monomac=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 3, 5, 0); // GLViewFactory支持输出supportOverlay
        }
    }
}
