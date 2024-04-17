/*! \mainpage
 * \~English This library contains the APIs for .NET Core Winform. See ASEva.UICoreWF for details. \n
 * \~Chinese 此类库为ASEva-API中基于.NET Core Winform的部分。详见 ASEva.UICoreWF \n
 * \~English This document corresponds to API version: 0.0.0
 * \~Chinese 本文档对应API版本：0.0.0
 */

using System;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// version=0.0.0
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:corewf=3.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:corewf=3.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(0, 0, 0, 0);
        }
    }
}
