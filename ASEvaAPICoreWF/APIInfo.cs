/*! \mainpage
 * 此类库为ASEva-API中基于.net core winform的部分。详见 ASEva.UICoreWF \n
 * 本文档对应API版本：2.3.5
 */

using System;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// version=2.3.5
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:corewf=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 3, 5, 2); // 修正关闭窗口和对话框后WebView遗留或闪现的问题
        }
    }
}
