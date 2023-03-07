/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.9.12\n
 * \n
 * Eto.Forms官方仓库: https://github.com/picoe/Eto \n
 * API文档: https://pages.picoe.ca/docs/api
 */

using System;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.9.12
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 9, 12, 0); // 增加DefaultFlowLayout2DBackend.FixedScrollBarSize以规避VisibleRect调用
        }
    }
}
