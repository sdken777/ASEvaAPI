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
 * \~English This document corresponds to API version: 3.1.0 \n
 * \~Chinese 本文档对应API版本：3.1.0
 */

using System;
using System.Diagnostics;

namespace ASEva
{
    /// <summary>
    /// version=3.1.0
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
            return new Version(3, 1, 0, 0); // Update log / 更新记录: Agency API精简。Agency底层使用AgencyLocal和AgencyAsync。除MainWorkflow.OnInit/OnRun外的模态函数改为异步。Agency.AddBusProtocolFile输出BusProtocolFileID数组。新增CurrentThreadSyncContext。合并MainWorkflowTaskCallback和MainWorkflowTaskIODetailsCallback。Plugin.OnApplicationPrepareStopping支持取消退出
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
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        osCode = "windows";
                        break;
                    case PlatformID.Unix:
                        {
                            var pi = new ProcessStartInfo();
                            pi.FileName = "uname";
                            pi.Arguments = "-sm";
                            pi.RedirectStandardOutput = true;
                            var proc = Process.Start(pi);
                            var s = proc.StandardOutput.ReadToEnd();
                            proc.WaitForExit();

                            var comps = s.Split(' ');
                            if (comps.Length != 2) break;

                            var osName = comps[0].ToLower();
                            var archName = comps[1].ToLower().TrimEnd('\r', '\n');
                            if (osName == "linux")
                            {
                                if (archName == "x86_64") osCode = "linux";
                                else if (archName == "aarch64") osCode = "linuxarm";
                            }
                            else if (osName == "darwin")
                            {
                                if (archName == "x86_64") osCode = "macos";
                                else if (archName == "arm64") osCode = "macosarm";
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return osCode;
        }

        private static String osCode = null;
    }
}
