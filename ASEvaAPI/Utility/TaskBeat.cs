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
        public async void Handle(Func<Task> task)
        {
            if (curAsyncTask != null) return;

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

        private Task curAsyncTask = null;
    }
}