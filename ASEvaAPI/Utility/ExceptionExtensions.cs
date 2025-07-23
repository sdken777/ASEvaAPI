using System;
using System.Collections.Generic;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.9.7) Exception object related extensions
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.9.7) 异常对象相关的扩展
    /// </summary>
    public static class ExceptionExtensions
    {
        /// \~English
        /// <summary>
        /// For object array initialization, avoiding null reference
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 用于初始化对象数组，避免空引用
        /// </summary>
        public static String GetAllStackTrace(this Exception ex)
        {
            var list = new List<String>();
            if (!String.IsNullOrEmpty(ex.StackTrace)) list.Add(ex.StackTrace);
            for (int i = 0; i < 8; i++)
            {
                ex = ex.InnerException;
                if (ex == null) break;
                list.Add(ex.Message + "\n" + ex.StackTrace);
            }
            return String.Join("\nv v v v v v v v v v v v v v v v v v v v v v v v v v v v v v v v\n", list);
        }
    }
}