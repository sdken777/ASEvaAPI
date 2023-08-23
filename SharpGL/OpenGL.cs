using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpGL
{
	/// <summary>
	/// The OpenGL class wraps Suns OpenGL 3D library.
    /// </summary>
	public partial class OpenGL
	{
        /// <summary>
        /// Creates the OpenGL instance using the specified function loader
        /// </summary>
        /// <param name="funcLoader">The function loader</param>
        /// <returns>The OpenGL instance</returns>
        public static OpenGL Create(FuncLoader funcLoader)
        {
            if (funcLoader == null) return null;

            var opengl = new OpenGL();
            opengl.funcLoader = funcLoader;
            return opengl;
        }

        private OpenGL()
        {}

        /// <summary>
        /// Determines whether a named function is supported.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// 	<c>true</c> if the function is supported; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFunctionSupported(string functionName)
        {
            //  Try and get the proc address for the function.
            IntPtr procAddress = funcLoader.GetFunctionAddress(functionName);

            //  As long as the pointer is non-zero, we can invoke the function.
            return procAddress != IntPtr.Zero;
        }

        /// <summary>
        /// Preloads a named function.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// 	<c>true</c> if the function is preloaded successfully; otherwise, <c>false</c>.
        /// </returns>
        public bool PreloadFunction(string functionName)
        {
            IntPtr procAddress = funcLoader.GetFunctionAddress(functionName);
            if (procAddress != IntPtr.Zero)
            {
                preloadFuncTable[functionName] = procAddress;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Gets the error description for a given error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>The error description for the given error code.</returns>
        public string GetErrorDescription(uint errorCode)
        {
            switch (errorCode)
            {
                case GL_NO_ERROR:
                    return "No Error";
                case GL_INVALID_ENUM:
                    return "A GLenum argument was out of range.";
                case GL_INVALID_VALUE:
                    return "A numeric argument was out of range.";
                case GL_INVALID_OPERATION:
                    return "Invalid operation.";
                case GL_STACK_OVERFLOW:
                    return "Command would cause a stack overflow.";
                case GL_STACK_UNDERFLOW:
                    return "Command would cause a stack underflow.";
                case GL_OUT_OF_MEMORY:
                    return "Not enough memory left to execute command.";
                default:
                    return "Unknown Error";
            }
        }

        /// <summary>
        /// Gets the vendor.
        /// </summary>
        /// <value>The vendor.</value>
        public string Vendor
		{
            get { return GetString(GL_VENDOR); }
		}

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>The renderer.</value>
        public string Renderer
		{
            get { return GetString(GL_RENDERER); }
		}

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
		{
            get { return GetString(GL_VERSION); }
		}

        /// <summary>
        /// Gets the extensions by using glGetString(GL_EXTENSIONS)
        /// </summary>
        /// <value>The extensions.</value>
        public string Extensions
		{
            get { return GetString(GL_EXTENSIONS); }
        }

        /// <summary>
        /// Gets the sorted extension list by using glGetStringi(GL_EXTENSIONS, i)
        /// </summary>
        /// <value>The extension list.</value>
        public List<string> ExtensionList
		{
            get
            {
                var nExtension = new int[1];
                GetInteger(OpenGL.GL_NUM_EXTENSIONS, nExtension);

                var extensionTexts = new String[nExtension[0]];
                for (int i = 0; i < extensionTexts.Length; i++)
                {
                    extensionTexts[i] = GetString(OpenGL.GL_EXTENSIONS, (uint)i);
                }

                var list = extensionTexts.ToList();
                list.Sort();
                return list;
            }
        }

        /// <summary>
        /// Gets the function loader.
        /// </summary>
        /// <value>The function loader.</value>
        public FuncLoader FunctionLoader
        {
            get { return funcLoader; }
        }

        private T getDelegateFor<T>(ref Delegate d) where T : class
        {
            if (d == null)
            {
                //  Get the type of the function.
                Type delegateType = typeof(T);

                //  Get the name of the function.
                string name = delegateType.Name;

                // ftlPhysicsGuy - Better way
                IntPtr proc = preloadFuncTable.ContainsKey(name) ? preloadFuncTable[name] : funcLoader.GetFunctionAddress(name);
                if (proc == IntPtr.Zero) throw new Exception("Extension function " + name + " not supported");

                //  Get the delegate for the function pointer.
                d = Marshal.GetDelegateForFunctionPointer(proc, delegateType);
            }
            return d as T;
        }

        private FuncLoader funcLoader = null;

        private Dictionary<String, IntPtr> preloadFuncTable = new Dictionary<string, IntPtr>();
    }
}
