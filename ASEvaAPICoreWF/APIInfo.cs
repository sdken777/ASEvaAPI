/*! \mainpage
 * 此类库为ASEva-API中基于.net core winform的部分。详见 ASEva.UICoreWF \n
 * 本文档对应API版本：2.3.3
 */

using System;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// version=2.3.3
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:corewf=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 3, 3, 0); // GLViewFactoryCoreWF支持在屏渲染。离屏渲染初始化时检查GL_EXT_framebuffer_object扩展
        }
    }
}
