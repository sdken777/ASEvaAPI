using System;
using System.Collections.Generic;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.6.0) For buffering dumped info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.6.0) 用于缓存被丢弃的信息
    /// </summary>
    public class Dump
    {
        /// \~English
        /// <summary>
        /// Dump exception
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 丢弃异常
        /// </summary>
        public static void Exception(Exception ex)
        {
            if (ex == null) return;

            var msg = ex.Message + "\n" + ex.StackTrace;
            if (msg == lastMsg) return;
            else lastMsg = msg;

            lock (exceptions)
            {
                exceptions.Add(msg);
                if (exceptions.Count > ExceptionSizeLimit) exceptions.RemoveRange(0, ExceptionSizeLimit / 10);
            }
        }

        /// \~English
        /// <summary>
        /// Clear dumped exceptions
        /// </summary>
        /// <returns>Info of dumped exceptions</returns>
        /// \~Chinese
        /// <summary>
        /// 清空被丢弃的异常
        /// </summary>
        /// <returns>被丢弃的异常信息</returns>
        public static String[] ClearExceptions()
        {
            lock (exceptions)
            {
                var result = exceptions.ToArray();
                exceptions.Clear();
                return result;
            }
        }

        private static List<String> exceptions = new List<String>();
        private static String lastMsg = "";
        private const int ExceptionSizeLimit = 10000;
    }
}