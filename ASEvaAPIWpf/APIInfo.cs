/*! \mainpage
 * 此类库为ASEva-API中基于.net core wpf的部分。详见 ASEva.UIWpf \n
 * 本文档对应API版本：1.2.1
 */

using System;

namespace ASEva.UIWpf
{
    /// <summary>
    /// version=1.2.1
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:wpf=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 2, 1, 1); // 修正OpenGLControlWpf找不到资源问题
        }
    }
}
