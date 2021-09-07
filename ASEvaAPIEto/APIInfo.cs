/*! \mainpage
 * 此类库为ASEva-API中基于Eto.Forms的部分。详见 ASEva.UIEto \n
 * 本文档对应API版本：2.0.2
 */

using System;

namespace ASEva.UIEto
{
    /// <summary>
    /// version=2.0.2
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:eto=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 0, 2, 0); // RowStackLayout和ColumnStackLayout意义反转，且略写Stack。StackLayout初始化增加alignment参数。AddControlExtensions细分为StackLayoutExtensions和TableLayoutExtensions。新增TabControlExtensions和SplitterExtensions。移除ImageResourceLoader（可通过Bitmap.FromResource加载）
        }
    }
}
