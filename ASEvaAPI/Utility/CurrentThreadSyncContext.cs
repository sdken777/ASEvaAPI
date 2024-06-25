using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.1.0) Used to ensure thread consistency of asynchronous call in non-GUI programs
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.1.0) 用于在非GUI程序中确保异步调用线程一致性
    /// </summary>
    public class CurrentThreadSyncContext : SynchronizationContext
    {
        private CurrentThreadSyncContext()
        {
            threadID = Thread.CurrentThread.ManagedThreadId;
        }

        /// \~English
        /// <summary>
        /// Enable for current thread
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 为当前线程启用
        /// </summary>
        public static CurrentThreadSyncContext InstallIfNeeded()
        {
            if (Current is CurrentThreadSyncContext) return Current as CurrentThreadSyncContext;

            var target = new CurrentThreadSyncContext();
            SetSynchronizationContext(target);
            return target;
        }

        /// \~English
        /// <summary>
        /// Disable for current thread
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 为当前线程禁用
        /// </summary>
        public static void Uninstall()
        {
            if (Current is CurrentThreadSyncContext)
            {
                var target = Current as CurrentThreadSyncContext;
                target.contexts.Clear();
                SetSynchronizationContext(null);
            }
        }

        /// \~English
        /// <summary>
        /// You should invoke the method in the thread continuously to handle asynchronous callbacks
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 需要在线程中持续调用此方法处理异步回调
        /// </summary>
        public void Dispatch(ref bool shouldEnd)
        {
            if (Thread.CurrentThread.ManagedThreadId != threadID) return;

            while (!shouldEnd)
            {
                CallbackContext ctx = null;
                if (!contexts.TryDequeue(out ctx)) break;
                ctx.Callback(ctx.State);
                ctx.Finished = true;
            }
        }

        /// \~English
        /// <summary>
        /// Override method, don't call directly
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 重载方法，请勿直接调用
        /// </summary>
        public override void Post(SendOrPostCallback d, object state)
        {
            var ctx = new CallbackContext
            {
                Callback = d,
                State = state,
            };
            contexts.Enqueue(ctx);
        }

        /// \~English
        /// <summary>
        /// Override method, don't call directly
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 重载方法，请勿直接调用
        /// </summary>
        public override void Send(SendOrPostCallback d, object state)
        {
            if (Thread.CurrentThread.ManagedThreadId == threadID) d(state);
            else
            {
                var ctx = new CallbackContext
                {
                    Callback = d,
                    State = state,
                };
                contexts.Enqueue(ctx);

                while (true)
                {
                    if (ctx.Finished) break;
                    Thread.Sleep(1);
                }
            }
        }

        private class CallbackContext
        {
            public SendOrPostCallback Callback { get; set; }
            public object State { get; set; }
            public bool Finished { get; set; }
        }

        private int threadID;
        private ConcurrentQueue<CallbackContext> contexts = new ConcurrentQueue<CallbackContext>();
    }
}
