using System;
using System.Collections.Generic;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.4) For registering and running any functions
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.4) 用于注册和运行任意函数
    /// </summary>
    public class FuncManager
    {
        public static bool Register(String name, Func<object?, object?> func)
        {
            if (String.IsNullOrEmpty(name) || table.ContainsKey(name)) return false;
            table[name] = func;
            return true;
        }

        public static object? Run(String name, object? input = null)
        {
            object? output = null;
            if (Run(name, input, out output)) return output;
            else return null;
        }

        public static bool Run(String name, object? input, out object? output)
        {
            output = null;
            if (String.IsNullOrEmpty(name) || !table.ContainsKey(name)) return false;
            output = table[name](input);
            return true;
        }

        private static Dictionary<String, Func<object?, object?>> table = [];
    }
}