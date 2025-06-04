using System;

namespace ASEva.Utility
{
	/// \~English
	/// <summary>
	/// (api:app=3.9.5) Asynchronous debouncing for the GUI (pause refreshing the GUI for a period after operations)
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:app=3.9.5) 实现界面的异步防抖（在操作后一段时间暂停刷新界面）
	/// </summary>
    public class UpdateKeeper
    {
        /// \~English
        /// <summary>
        /// The time to pause refreshing after an operation, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 操作后暂停刷新的时间，单位毫秒
        /// </summary>
        public int KeepTime { get; set; } = 1000;

        /// \~English
        /// <summary>
        /// Check if the GUI can currently be refreshed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取当前是否可以刷新界面
        /// </summary>
        public bool CanUpdate
        {
            get
            {
                if (lastSetTime == null) return true;

                var now = DateTime.Now;
                if (now < lastSetTime) lastSetTime = now;
                return (now - lastSetTime.Value).TotalMilliseconds > KeepTime;
            }
        }

        /// \~English
        /// <summary>
        /// Notify that an operation has been performed
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通知进行了操作
        /// </summary>
        public void Set()
        {
            lastSetTime = DateTime.Now;
        }

        private DateTime? lastSetTime = null;
    }
}