/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.0.4
 */

using System;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.0.4
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 0, 4, 0); // 新增OverlayLayout。新增GraphicsExtensions。新增App.DefaultFont。输入图像改为Image（支持Icon.FromResource）
        }
    }
}
