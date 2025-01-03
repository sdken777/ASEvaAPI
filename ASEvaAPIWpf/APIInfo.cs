/*! \mainpage
 * \~English This library contains the APIs for .NET Core Wpf. See ASEva.UIWpf for details. \n
 * \~Chinese 此类库为ASEva-API中基于.NET Core Wpf的部分。详见 ASEva.UIWpf \n
 * \~English This document corresponds to API version: 2.2.0
 * \~Chinese 本文档对应API版本：2.2.0
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ASEva.UIWpf
{
    /// <summary>
    /// version=2.2.0
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:wpf=2.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:wpf=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 2, 0, 1); // Update log / 更新记录: 修正WebView关闭后不析构问题
        }

        /// \~English
        /// <summary>
        /// (api:wpf=2.1.1) Get third party license notices of software used by this library
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:wpf=2.1.1) 获取此类库使用的第三方软件版权声明
        /// </summary>
        public static Dictionary<string, string> GetThirdPartyNotices()
        {
            var table = new Dictionary<string, string>();
            table["WebView2"] = Encoding.UTF8.GetString(Resource.WebView2);
            table["SharpDX"] = Encoding.UTF8.GetString(Resource.SharpDX);
            table["Extended WPF Toolkit version 3.6.0"] = Encoding.UTF8.GetString(Resource.Extended_WPF_Toolkit__3_6_);
            return table;
        }
    }
}
