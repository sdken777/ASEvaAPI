/*! \mainpage
 * \~English This library contains the APIs for .NET Core Winform. See ASEva.UICoreWF for details. \n
 * \~Chinese 此类库为ASEva-API中基于.NET Core Winform的部分。详见 ASEva.UICoreWF \n
 * \~English This document corresponds to API version: 3.4.0
 * \~Chinese 本文档对应API版本：3.4.0
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ASEva.UICoreWF
{
    /// <summary>
    /// version=3.4.0
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
            return new Version(3, 4, 0, 2); // Update log / 更新记录: 修正部分情况下按钮背景色不透明问题
        }

        /// \~English
        /// <summary>
        /// (api:corewf=3.2.1) Get third party license notices of software used by this library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:corewf=3.2.1) 获取此类库使用的第三方软件版权声明
        /// </summary>
        public static Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<String, String>();
            table["WebView2"] = Encoding.UTF8.GetString(Resource.WebView2);
            return table;
        }
    }
}
