/*! \mainpage
 * \~English This library contains the APIs for .NET Core Wpf. See ASEva.UIWpf for details. \n
 * \~Chinese 此类库为ASEva-API中基于.NET Core Wpf的部分。详见 ASEva.UIWpf \n
 * \~English This document corresponds to API version: 1.8.0
 * \~Chinese 本文档对应API版本：1.8.0
 */

using System;

namespace ASEva.UIWpf
{
    /// <summary>
    /// version=1.8.0
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:wpf=1.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:wpf=1.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(1, 8, 0, 0); // Update log / 更新记录: 完善异常处理
        }
    }
}
