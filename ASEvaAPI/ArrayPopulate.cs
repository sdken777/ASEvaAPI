using System;

namespace ASEva
{
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