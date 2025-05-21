using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.9.2) Get process by name
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.9.2) 根据进程名获取进程
    /// </summary>
    public class ProcessGetter
    {
        /// \~English
        /// <summary>
        /// Get process by name
        /// </summary>
        /// <param name="processName">Process name, without .exe suffix</param>
        /// <returns>Process object, or null if process does not exist</returns>
        /// \~Chinese
        /// <summary>
        /// 根据进程名获取进程
        /// </summary>
        /// <param name="processName">进程名，不带.exe等后缀</param>
        /// <returns>进程对象，如果进程不存在则返回null</returns>
        public static Process Get(String processName)
        {
            if (OperatingSystem.IsMacOS())
            {
                int maxNameLength = 1;
                var contexts = new List<ProcessContext>();
                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        if (process.HasExited) continue;
                        
                        var name = process.ProcessName;
                        if (String.IsNullOrWhiteSpace(name)) continue;
                        maxNameLength = Math.Max(maxNameLength, name.Length);

                        contexts.Add(new ProcessContext
                        {
                            Name = name,
                            Process = process
                        });
                    }
                    catch (Exception ex) { Dump.Exception(ex); }
                }

                foreach (var ctx in contexts)
                {
                    if (processName.Length > maxNameLength)
                    {
                        if (ctx.Name.Length == maxNameLength && processName.StartsWith(ctx.Name)) return ctx.Process;
                    }
                    else
                    {
                        if (ctx.Name == processName) return ctx.Process;
                    }
                }
                return null;
            }
            else
            {
                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        if (process.HasExited) continue;
                        if (process.ProcessName == processName) return process;
                    }
                    catch (System.ComponentModel.Win32Exception) {} // HasExited failed for Windows system processes
                    catch (Exception ex) { Dump.Exception(ex); }
                }
                return null;
            }
        }

        private class ProcessContext
        {
            public String Name { get; set; }
            public Process Process { get; set; }
        }
    }
}
