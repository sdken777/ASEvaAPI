using System;

namespace ASEva.Utility
{
    /// <summary>
    /// (api:app=2.11.2) 执行操作的间隔控制
    /// </summary>
    public class IntervalControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval">执行操作的间隔时间，单位秒，至少0.1秒</param>
        /// <param name="manual">是否手动控制计时，手动模式下需要调用Next方法开启下一轮计时</param>
        /// <param name="immediate">是否可立即执行操作，否则需要等待interval时间后才可进行操作</param>
        public IntervalControl(double interval, bool manual = false, bool immediate = true)
        {
            this.interval = Math.Max(0.1, interval);
            this.manual = manual;
            lastOperated = immediate ? new DateTime(0) : DateTime.Now;
        }

        /// <summary>
        /// 检查是否应执行操作
        /// </summary>
        public bool Test()
        {
            if (lastOperated == null) return false;

            var curTime = DateTime.Now;
            if (curTime < lastOperated.Value) lastOperated = curTime;

            if ((curTime - lastOperated.Value).TotalSeconds < interval) return false;

            if (manual) lastOperated = null;
            else lastOperated = curTime;

            return true;
        }

        /// <summary>
        /// 手动模式下开启下一轮计时
        /// </summary>
        public void Next()
        {
            if (lastOperated == null) lastOperated = DateTime.Now;
        }

        /// <summary>
        /// 重置间隔控制
        /// </summary>
        /// <param name="immediate">是否可立即执行操作，否则需要等待interval时间后才可进行操作</param>
        public void Reset(bool immediate = true)
        {
            lastOperated = immediate ? new DateTime(0) : DateTime.Now;
        }

        private double interval;
        private bool manual;
        private DateTime? lastOperated;
    }
}