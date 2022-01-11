/*! \mainpage
 * ASEva-API分为三类：回调类API，主动调用类API，以及实用功能。\n
 * \n
 * 回调类API：继承Plugin类作为插件入口，实现插件功能供ASEva系统调用。详见 ASEva.Plugin \n
 * 主动调用类API：在插件功能的实现中可直接调用的API。详见 ASEva.Agency \n
 * 实用功能：在插件开发中时常用到的功能类和控件等。详见 ASEva.Utility \n
 * \n
 * 另外，插件中使用的样本相关定义参考 ASEva.Samples ；图表报告相关定义参考 ASEva.Graph \n
 * \n
 * 本文档对应API版本：2.3.0
 */

using System;
using System.Diagnostics;

namespace ASEva
{
    /// <summary>
    /// version=2.3.0
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:app=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 3, 0, 2); // 调整MainWorkflow接口
        }

        /// <summary>
        /// (api:app=2.0.0) 返回当前运行的OS代号
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
