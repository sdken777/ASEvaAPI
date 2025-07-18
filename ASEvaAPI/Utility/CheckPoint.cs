using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.10.2) Checkpoint time cost statistics
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.10.2) 检查点耗时统计信息
    /// </summary>
    public class CheckPointStat
    {
        /// \~English
        /// <summary>
        /// Time cost of each checkpoint, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 各检查点耗时，单位秒
        /// </summary>
        public Dictionary<string, double> TimeCosts { get; set; } = [];
        
        /// \~English
        /// <summary>
        /// Total time cost, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 总耗时，单位秒
        /// </summary>
        public double TotalTime { get; set; }

        /// \~English
        /// <summary>
        /// Convert to string, which is convenient for recording
        /// </summary>
        /// <returns>Formatted statistics string</returns>
        /// \~Chinese
        /// <summary>
        /// 转换为方便记录的字符串
        /// </summary>
        /// <returns>格式化的统计信息字符串</returns>
        public override string ToString()
        {
            if (TotalTime == 0) return string.Empty;

            var sortedCosts = TimeCosts.OrderBy(kvp => kvp.Value).ToList();
            sortedCosts.Reverse();
            
            var rows = new List<string>();
            foreach (var kvp in sortedCosts)
            {
                double percentage = kvp.Value / TotalTime * 100;
                string timeInfo;
                
                if (kvp.Value >= 1)
                    timeInfo = $"{kvp.Value:F3}s";
                else if (kvp.Value >= 0.001)
                    timeInfo = $"{(kvp.Value * 1000):F3}ms";
                else
                    timeInfo = $"{(kvp.Value * 1000000):F3}us";
                
                rows.Add($"{percentage:F1}% : {kvp.Key} ({timeInfo})");
            }
            
            return string.Join("\n", rows);
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.10.2) Checkpoint time cost statistics utility class
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.10.2) 检查点耗时统计工具类
    /// </summary>
    public class CheckPoint
    {
        /// \~English
        /// <summary>
        /// Add a checkpoint
        /// </summary>
        /// <param name="statName">Statistics name</param>
        /// <param name="checkpointName">Checkpoint name</param>
        /// \~Chinese
        /// <summary>
        /// 添加一个检查点
        /// </summary>
        /// <param name="statName">统计名称</param>
        /// <param name="checkpointName">检查点名称</param>
        public static void AddCheckpoint(string statName, string checkpointName)
        {
            long enterCpuTick = Stopwatch.GetTimestamp();

            if (string.IsNullOrEmpty(checkpointName))
            {
                ClearCheckpoint(statName);
                return;
            }

            lock (statTable)
            {
                if (!statTable.ContainsKey(statName))
                {
                    statTable[statName] = new CheckpointStatContext();
                }
                var context = statTable[statName];

                if (context.LastCpuTick != 0)
                {
                    long cpuTickCost = enterCpuTick - context.LastCpuTick;
                    if (context.CpuTickCosts.ContainsKey(context.LastCheckpointName)) context.CpuTickCosts[context.LastCheckpointName] += cpuTickCost;
                    else context.CpuTickCosts[context.LastCheckpointName] = cpuTickCost;
                }

                context.LastCheckpointName = checkpointName;

                long leaveCpuTick = Stopwatch.GetTimestamp();
                context.LastCpuTick = leaveCpuTick;
                if (context.FirstCpuTick == 0) context.FirstCpuTick = leaveCpuTick;
            }
        }

        /// \~English
        /// <summary>
        /// Clear current checkpoint. If reset is true, output statistics and reset
        /// </summary>
        /// <param name="statName">Statistics name</param>
        /// <param name="reset">Whether to reset statistics</param>
        /// <returns>Checkpoint statistics</returns>
        /// \~Chinese
        /// <summary>
        /// 清除当前检查点，若reset为true则输出统计信息并重置
        /// </summary>
        /// <param name="statName">统计名称</param>
        /// <param name="reset">是否重置统计</param>
        /// <returns>检查点统计信息</returns>
        public static CheckPointStat ClearCheckpoint(string statName, bool reset = false)
        {
            long enterCpuTick = Stopwatch.GetTimestamp();

            lock (statTable)
            {
                if (!statTable.ContainsKey(statName))
                {
                    return new CheckPointStat();
                }
                var context = statTable[statName];

                if (context.LastCpuTick != 0)
                {
                    long cpuTickCost = enterCpuTick - context.LastCpuTick;
                    if (context.CpuTickCosts.ContainsKey(context.LastCheckpointName))
                        context.CpuTickCosts[context.LastCheckpointName] += cpuTickCost;
                    else
                        context.CpuTickCosts[context.LastCheckpointName] = cpuTickCost;
                }

                context.LastCheckpointName = string.Empty;
                context.LastCpuTick = 0;

                if (reset)
                {
                    var stat = new CheckPointStat();
                    if (context.FirstCpuTick != 0)
                    {
                        double cpuTicksPerSecond = Stopwatch.Frequency;
                        foreach (var kvp in context.CpuTickCosts)
                        {
                            stat.TimeCosts[kvp.Key] = (double)kvp.Value / cpuTicksPerSecond;
                        }
                        stat.TotalTime = (double)(enterCpuTick - context.FirstCpuTick) / cpuTicksPerSecond;

                        context.CpuTickCosts.Clear();
                        context.FirstCpuTick = 0;
                    }
                    return stat;
                }
                else return null;
            }
        }

        private class CheckpointStatContext
        {
            public long FirstCpuTick { get; set; }
            public long LastCpuTick { get; set; }
            public string LastCheckpointName { get; set; } = string.Empty;
            public Dictionary<string, long> CpuTickCosts { get; set; } = [];
        }

        private static readonly Dictionary<string, CheckpointStatContext> statTable = new Dictionary<string, CheckpointStatContext>();
    }
}