using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample synchronization
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 样本同步
    /// </summary>
    public class SampleSync
    {
        /// \~English
        /// <summary>
        /// Synchronize samples according to the target timeline point, output interpolated or nearest samples
        /// </summary>
        /// <param name="timeline">Timeline point, in seconds</param>
        /// <param name="session">The session that sample belongs to</param>
        /// <param name="sampleLists">Input sample buffers (can be multiple)</param>
        /// <param name="outputs">Output synchronized samples</param>
        /// <returns>Whether the target time can be passed, false means you should try to synchronize again later</returns>
        /// \~Chinese
        /// <summary>
        /// 样本同步函数，根据目标时间点搜索样本缓存，输出插值或最近样本
        /// </summary>
        /// <param name="timeline">希望同步的时间线，单位秒</param>
        /// <param name="session">时间线所在session</param>
        /// <param name="sampleLists">输入的样本缓存，可以是多个缓存</param>
        /// <param name="outputs">各个缓存按指定时间点输出插值或最近样本</param>
        /// <returns>需要保留返回false，否则返回true</returns>
        static public bool Sync(double timeline, DateTime session, List<List<Sample>> sampleLists, out List<Sample> outputs)
        {
            bool pass = false;
            bool disc = true;
            outputs = new List<Sample>();

            foreach (var list in sampleLists)
            {
                disc &= (list.Count == 0 || Sample.IsOutRangeLower(list, timeline));  //所有list均为disc 返回true
            }
            if (disc)
            {
                foreach (var dummy in sampleLists)
                {
                    outputs.Add(null);
                }
                return true;
            }

            foreach (var list in sampleLists)
            {
                pass |= (Sample.IsOutRangeUpper(list, timeline));  //只要有一个list为pass 返回false
            }
            if (pass) return false;

            foreach (var list in sampleLists)
            {
                if (Sample.IsInRange(list, timeline) && list.Count != 0 && list.First().SupportInterpolation())
                {
                    var s = Sample.SearchAndInterpolate(list, timeline);

                    if (s == null || s.Base != session) outputs.Add(null);
                    else outputs.Add(s);
                }
                else if (Sample.IsInRange(list, timeline) && list.Count != 0 && !list.First().SupportInterpolation())
                {
                    var s = Sample.SearchAndGetNearest(list, timeline, session);
                    outputs.Add(s);
                }
                else outputs.Add(null);
            }

            return true;
        }
    }
}
