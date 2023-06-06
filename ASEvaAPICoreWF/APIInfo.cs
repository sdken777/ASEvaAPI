/*! \mainpage
 * 此类库为ASEva-API中基于.net core winform的部分。详见 ASEva.UICoreWF \n
 * 本文档对应API版本：2.4.17
 */

using System;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// version=2.4.17
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:corewf=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 4, 17, 0); // 官方handler添加至仓库
        }
    }
}
