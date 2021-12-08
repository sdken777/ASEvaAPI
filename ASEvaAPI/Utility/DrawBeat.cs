using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.2.2) 绘图时间记录与反馈
    /// </summary>
    public class DrawBeat
    {
        /// <summary>
        /// 在调用绘图前调用
        /// </summary>
        /// <param name="id">绘图对象ID</param>
        /// <returns>是否可调用绘图</returns>
        public static bool CallerBegin(int id)
        {
            if (!ctxs.ContainsKey(id))
            {
                ctxs[id] = new DrawBeatContext();
                ctxs[id].Category = "";
            }
            if (!ctxs[id].Ongoing && !ctxs[id].InCaller)
            {
                if (ctxs[id].IdleTimer != null && (DateTime.Now - ctxs[id].IdleTimer.Value).TotalMilliseconds < ctxs[id].DrawInterval) return false;
                ctxs[id].Ongoing = true;
                ctxs[id].InCaller = true;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 在调用绘图前调用
        /// </summary>
        /// <param name="target">绘图对象</param>
        /// <returns>是否可调用绘图</returns>
        public static bool CallerBegin(object target)
        {
            if (target == null) return false;
            else return CallerBegin(target.GetHashCode());
        }

        /// <summary>
        /// 在调用绘图后调用
        /// </summary>
        /// <param name="id">绘图对象ID</param>
        public static void CallerEnd(int id)
        {
            if (!ctxs.ContainsKey(id)) return;
            ctxs[id].InCaller = false;
        }

        /// <summary>
        /// 在调用绘图后调用
        /// </summary>
        /// <param name="target">绘图对象</param>
        public static void CallerEnd(object target)
        {
            if (target != null) CallerEnd(target.GetHashCode());
        }

        /// <summary>
        /// 在绘图回调函数开始时调用
        /// </summary>
        /// <param name="id">绘图对象ID</param>
        /// <param name="category">类别，设为空表示不归类</param>
        public static void CallbackBegin(int id, String category)
        {
            if (!ctxs.ContainsKey(id))
            {
                ctxs[id] = new DrawBeatContext();
                ctxs[id].Category = category == null ? "" : category;
            }
            if (ctxs[id].CallbackBeginTime == null) ctxs[id].CallbackBeginTime = DateTime.Now;
        }

        /// <summary>
        /// 在绘图回调函数开始时调用
        /// </summary>
        /// <param name="target">绘图对象</param>
        /// <param name="category">类别，设为空表示不归类</param>
        public static void CallbackBegin(object target, String category)
        {
            if (target != null) CallbackBegin(target.GetHashCode(), category);
        }

        /// <summary>
        /// 在绘图毁掉函数结束前调用
        /// </summary>
        /// <param name="id">绘图对象ID</param>
        public static void CallbackEnd(int id)
        {
            if (!ctxs.ContainsKey(id)) return;
            if (ctxs[id].CallbackBeginTime != null)
            {
                DateTimeRange range;
                range.start = 0.0000001 * ctxs[id].CallbackBeginTime.Value.Ticks;
                range.end = 0.0000001 * DateTime.Now.Ticks;

                ctxs[id].RecentCallbackRanges.Add(range);
                while (ctxs[id].RecentCallbackRanges.Last().end - ctxs[id].RecentCallbackRanges[0].start > 3) ctxs[id].RecentCallbackRanges.RemoveAt(0);

                if (!ctxs[id].InCaller)
                {
                    var category = ctxs[id].Category;
                    if (!recentOutCallerCallbackRanges.ContainsKey(category)) recentOutCallerCallbackRanges[category] = new List<DateTimeRange>();

                    var list = recentOutCallerCallbackRanges[category];
                    list.Add(range);
                    while (list.Last().end - list[0].start > 3) list.RemoveAt(0);
                }

                ctxs[id].CallbackBeginTime = null;
                ctxs[id].IdleTimer = DateTime.Now;
                ctxs[id].Ongoing = false;
            }
        }

        /// <summary>
        /// 在绘图毁掉函数结束前调用
        /// </summary>
        /// <param name="target">绘图对象</param>
        public static void CallbackEnd(object target)
        {
            if (target != null) CallbackEnd(target.GetHashCode());
        }

        /// <summary>
        /// 缩减上下文规模
        /// </summary>
        public static void DownSize()
        {
            double latestTime = 0;
            foreach (var pair in ctxs)
            {
                if (pair.Value.RecentCallbackRanges.Count > 0)
                {
                    latestTime = Math.Max(latestTime, pair.Value.RecentCallbackRanges.Last().end);
                }
            }

            var emptyKeys = new List<int>();
            foreach (var pair in ctxs)
            {
                while (true)
                {
                    if (pair.Value.RecentCallbackRanges.Count == 0)
                    {
                        emptyKeys.Add(pair.Key);
                        break;
                    }
                    if (latestTime - pair.Value.RecentCallbackRanges[0].start > 3) pair.Value.RecentCallbackRanges.RemoveAt(0);
                    else break;
                }
            }

            foreach (var key in emptyKeys)
            {
                ctxs.Remove(key);
            }
        }

        /// <summary>
        /// 获取最近3秒回调函数运行平均时间
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, double> GetRecentCallbackAverageTime()
        {
            var table = new Dictionary<int, double>();
            foreach (var pair in ctxs)
            {
                if (pair.Value.RecentCallbackRanges.Count == 0) continue;
                double sum = 0;
                foreach (var range in pair.Value.RecentCallbackRanges)
                {
                    sum += range.end - range.start;
                }
                table[pair.Key] = sum / pair.Value.RecentCallbackRanges.Count;
            }
            return table;
        }

        /// <summary>
        /// 获取在caller外的回调函数运行时间，并清空缓存
        /// </summary>
        public static Dictionary<String, double> PopOutCallerCallbackTime()
        {
            var table = new Dictionary<String, double>();
            foreach (var pair in recentOutCallerCallbackRanges)
            {
                double sum = 0;
                foreach (var range in pair.Value)
                {
                    sum += range.end - range.start;
                }
                pair.Value.Clear();
                table[pair.Key] = sum;
            }
            return table;
        }

        /// <summary>
        /// 设置各绘图对象的绘制时间间隔
        /// </summary>
        public static void SetDrawIntervals(Dictionary<int, int> intervals)
        {
            foreach (var pair in intervals)
            {
                if (ctxs.ContainsKey(pair.Key)) ctxs[pair.Key].DrawInterval = pair.Value;
            }
        }

        private struct DateTimeRange
        {
            public double start; // sec
            public double end; // sec
        }

        private class DrawBeatContext
        {
            public String Category { get; set; }
            public bool Ongoing { get; set; }
            public bool InCaller { get; set; }
            public DateTime? CallbackBeginTime { get; set; }
            public List<DateTimeRange> RecentCallbackRanges { get; set; }
            public DateTime? IdleTimer { get; set; }
            public int DrawInterval { get; set; }

            public DrawBeatContext()
            {
                RecentCallbackRanges = new List<DateTimeRange>();
                DrawInterval = 100;
            }
        }

        private static Dictionary<int, DrawBeatContext> ctxs = new Dictionary<int, DrawBeatContext>();
        private static Dictionary<String, List<DateTimeRange>> recentOutCallerCallbackRanges = new Dictionary<string, List<DateTimeRange>>();
    }
}