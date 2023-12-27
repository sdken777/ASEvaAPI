using System;

namespace SharpGL
{
    /// <summary>
    /// Function loader
    /// </summary>
    public interface FuncLoader
    {
        /// <summary>
        /// Returns the function pointer of specified name
        /// </summary>
        /// <param name="name">The function name</param>
        /// <returns>The function pointer</returns>
        IntPtr GetFunctionAddress(String name);
    }
}