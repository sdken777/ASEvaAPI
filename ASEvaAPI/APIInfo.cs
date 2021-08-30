/*! \mainpage
 * ASEva-API分为三类：回调类API，主动调用类API，以及实用功能。\n
 * \n
 * 回调类API：继承Plugin类作为插件入口，实现插件功能供ASEva系统调用。详见 ASEva.Plugin \n
 * 主动调用类API：在插件功能的实现中可直接调用的API。详见 ASEva.Agency \n
 * 实用功能：在插件开发中时常用到的功能类和控件等。详见 ASEva.Utility \n
 * \n
 * 另外，插件中使用的样本相关定义参考 ASEva.Samples ；图表报告相关定义参考 ASEva.Graph \n
 * \n
 * 本文档对应API版本：2.1.0
 */

using System;
using System.Reflection;

namespace ASEva
{
    /// <summary>
    /// version=2.1.0
    /// </summary>
    public class APIInfo
    {
        /// <summary>
        /// (api:app=2.0.0) 获取API版本
        /// </summary>
        /// <returns>API版本</returns>
        public static Version GetAPIVersion()
        {
            return new Version(2, 1, 0, 0); // 新增TextResource
        }

        /// <summary>
        /// (api:app=2.0.0) 返回当前运行的OS
        /// </summary>
        /// <returns>"windows"或"linux"，若无法识别返回null</returns>
        public static String GetRunningOS()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return "windows";
                case PlatformID.Unix:
                    return "linux";
                default:
                    return null;
            }
        }
    }
}
