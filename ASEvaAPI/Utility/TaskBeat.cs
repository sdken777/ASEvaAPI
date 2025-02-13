using System;
using System.Threading.Tasks;

namespace ASEva.Utility
{
	/// \~English
	/// <summary>
	/// (api:app=3.6.1) Make sure that tasks of the same target are executed in sequence
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:app=3.6.1) 确保相同目标的任务依次执行
	/// </summary>
    public class TaskBeat
    {
        /// \~English
        /// <summary>
        /// Loop call this method
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 循环调用此方法
        /// </summary>
        public async void Handle(Func<Task> task)
        {
            if (curAsyncTask != null) return;
            if (intervalControl != null && !intervalControl.Test()) return;

            try
            {
                curAsyncTask = task();
            }
            catch (Exception ex) { Dump.Exception(ex); }
            if (curAsyncTask == null) return;

            try
            {
                await curAsyncTask;
            }
            catch (Exception ex) { Dump.Exception(ex); }
            curAsyncTask = null;
        }
        
        /// \~English
        /// <summary>
        /// (api:app=3.7.10) The minimum interval between task executions, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.7.10) 任务执行的最小间隔，单位秒
        /// </summary>
        public double MinInterval
        {
            get => minInterval;
            set
            {
                if (value < 0 || value == minInterval) return;
                minInterval = value;
                if (minInterval == 0) intervalControl = null;
                else intervalControl = new IntervalControl(minInterval, false, false);
            }
        }

        private Task curAsyncTask = null;
        private double minInterval = 0;
        private IntervalControl intervalControl = null;
    }
}