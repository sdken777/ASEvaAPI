using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ASEva.UIMonoMac
{
	public static class MacOS
	{
        static MacOS()
        {
			try { HandleGL = MacOS.dlopen(libGL, 0x0a); } catch (Exception) {}
			try { HandleGLU = MacOS.dlopen(libGLU, 0x0a); } catch (Exception) {}

			var entryFolder = EntryFolder.Path;
			if (entryFolder != null)
			{
				var glewFilePath = entryFolder + Path.DirectorySeparatorChar + libGLEW;
				try { HandleGLEW = MacOS.dlopen(glewFilePath, 0x0a); } catch (Exception) {}
			}
		}

		public static IntPtr HandleGL { get; private set; }
		public static IntPtr HandleGLU { get; private set; }
		public static IntPtr HandleGLEW { get; private set; }

		private const string libSystem = "libSystem.dylib";
        private const string libGL = "/System/Library/Frameworks/OpenGL.framework/Libraries/libGL.dylib";
		private const string libGLU = "/System/Library/Frameworks/OpenGL.framework/Libraries/libGLU.dylib";
		private const string libGLEW = "libGLEW.2.1.0.dylib";

		[DllImport(libSystem, SetLastError = true)]
        public static extern IntPtr dlopen(string fileName, int mode);

		[DllImport(libSystem, SetLastError = true)]
		public static extern IntPtr dlsym(IntPtr handle, string name);

		[DllImport(libGLEW, SetLastError = true)]
		public static extern uint glewInit();
	}
}
