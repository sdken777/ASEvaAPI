using System;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.7.4) Extension to initialize object array
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.4) 用于初始化对象数组的扩展
    /// </summary>
    public static class ArrayPopulateExtensions
    {
        /// \~English
        /// <summary>
        /// For object array initialization, avoiding null reference
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 用于初始化对象数组，避免空引用
        /// </summary>
        public static T[] Populate<T>(this T[] arr, Func<int, T> constructor)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = constructor.Invoke(i);
            }
            return arr;
        }
    }
}