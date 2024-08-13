/*! \mainpage
 * \~English ASEva-API consists of three parts: Callback API, Agency API, and utility functions. \n\n
 * \~Chinese ASEva-API分为三类：回调类API，主动调用类API，以及实用功能。 \n\n
 * 
 * \~English Callback API: Implement Plugin class, to achieve plugin functions for framework calls. See ASEva.Plugin for details \n
 * \~Chinese 回调类API：继承Plugin类作为插件入口，实现插件功能供框架软件调用。详见 ASEva.Plugin \n
 * \~English Agency API: API that can be directly called in plugin functions. See ASEva.Agency for details \n
 * \~Chinese 主动调用类API：在插件功能的实现中可直接调用的API。详见 ASEva.Agency \n
 * \~English Utility functions: Utility classes that are often used in plugin development. See ASEva.Utility for details \n\n
 * \~Chinese 实用功能：在插件开发中时常用到的功能类等。详见 ASEva.Utility \n\n
 * 
 * \~English In addition, refer to ASEva.Samples for the definition of samples used in plugin, and ASEva.Graph for graph report definitions. \n\n
 * \~Chinese 另外，插件中使用的样本相关定义参考 ASEva.Samples ；图表报告相关定义参考 ASEva.Graph 。 \n\n
 *
 * \~English This document corresponds to API version: 3.2.13 \n
 * \~Chinese 本文档对应API版本：3.2.13
 */

using System;
using System.Runtime.InteropServices;

namespace ASEva
{
    /// <summary>
    /// version=3.2.13
    /// </summary>
    public class APIInfo
    {
        /// \~English
        /// <summary>
        /// (api:app=3.0.0) Get API version
        /// </summary>
        /// <returns>The API version</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(3, 2, 13, 0); // Update log / 更新记录: 新增Agency/Async.GetGPUDecoderTestResults
        }

        /// \~English
        /// <summary>
        /// (api:app=3.0.0) Get the currently running OS code
        /// </summary>
        /// <returns>OS code, null if not recognized</returns>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.0.0) 返回当前运行的OS代号
        /// </summary>
        /// <returns>OS代号，若无法识别返回null</returns>
        public static String GetRunningOS()
        {
            if (osCode == null)
            {
                osCode = "unknown";
                if (OperatingSystem.IsWindows())
                {
                    osCode = "windows";
                }
                else if (OperatingSystem.IsLinux())
                {
                    if (RuntimeInformation.OSArchitecture == Architecture.X64) osCode = "linux";
                    else if (RuntimeInformation.OSArchitecture == Architecture.Arm64) osCode = "linuxarm";
                }
                else if (OperatingSystem.IsMacOS())
                {
                    if (RuntimeInformation.OSArchitecture == Architecture.X64) osCode = "macos";
                    else if (RuntimeInformation.OSArchitecture == Architecture.Arm64) osCode = "macosarm";
                }
            }
            return osCode;
        }

        private static String osCode = null;
    }
}
