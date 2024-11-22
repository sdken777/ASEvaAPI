/*! \mainpage
 * \~English This library contains the APIs for MonoMac. See ASEva.UIMonoMac for details. \n
 * \~Chinese 此类库为ASEva-API中基于MonoMac的部分。详见 ASEva.UIMonoMac \n
 * \~English This document corresponds to API version: 2.2.2 \n\n
 * \~Chinese 本文档对应API版本：2.2.2 \n\n
 * 
 * \~English MonoMac repository (.NET Core branch): https://github.com/cwensley/monomac \n
 * \~Chinese MonoMac仓库(.NET Core分支): https://github.com/cwensley/monomac \n
 * \~English API reference (Swift/ObjC): https://developer.apple.com/documentation/appkit
 * \~Chinese API文档(Swift/ObjC): https://developer.apple.com/documentation/appkit
 */

using System;

namespace ASEva.UIMonoMac
{
    /// <summary>
    /// version=2.2.2
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:monomac=2.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:monomac=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 2, 2, 0); // Update log / 更新记录: 使用新class
        }
    }
}
