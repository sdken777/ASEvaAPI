using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace ASEva.UIGtk
{
	static class Linux
	{
        static Linux()
        {
			try { HandleGL = Linux.dlopen(libGL, 0x0a); } catch (Exception) {}
			try { HandleGLU = Linux.dlopen(libGLU, 0x0a); } catch (Exception) {}
		}

		public static IntPtr HandleGL { get; private set; }
		public static IntPtr HandleGLU { get; private set; }
		public static IntPtr HandleGLEW { get; private set; }

		private const string libDL = "libdl.so.2";
        private const string libGL = "libGL.so.1";
		private const string libGLU = "libGLU.so.1";
		private const string libGLEWGLX = "libGLEW.so.2.1.glx";
		private const string libGLEWEGL = "libGLEW.so.2.1.egl";
		private const string libGLX = "libGLX.so.0";
		private const string libX11 = "libX11.so.6";
		private const string libEGL = "libEGL.so.1";
		private const string libGDK = "libgdk-3.so.0";
		private const string libWLEGL = "libwayland-egl.so.1";

		[DllImport(libDL, SetLastError = true)]
        public static extern IntPtr dlopen(string fileName, int mode);

		[DllImport(libDL, SetLastError = true)]
		public static extern IntPtr dlsym(IntPtr handle, string name);

		private static class GlewGlx
		{
			[DllImport(libGLEWGLX, SetLastError = true)]
			public static extern uint glewInit();
		}

		private static class GlewEgl
		{
			[DllImport(libGLEWEGL, SetLastError = true)]
			public static extern uint glewInit();
		}

		public static unsafe uint glewInit(Gdk.Window window)
		{
			var backendType = BackendChecker.Check(window);
			if (backendType == BackendType.X11)
			{
				if (HandleGLEW == IntPtr.Zero)
				{
					var glewFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + libGLEWGLX;
					try { HandleGLEW = Linux.dlopen(glewFilePath, 0x0a); } catch (Exception) {}
				}

				try { return GlewGlx.glewInit(); }
				catch {}
			}
			else
			{
				if (HandleGLEW == IntPtr.Zero)
				{
					var glewFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + libGLEWEGL;
					try { HandleGLEW = Linux.dlopen(glewFilePath, 0x0a); } catch (Exception) {}
				}

				try { return GlewEgl.glewInit(); }
				catch {}
			}
			return 1;
		}

		private static bool glxUseBackup = false;

		private static class Normal
		{

			[DllImport(libGLX, SetLastError = true)]
			public static extern unsafe IntPtr glXCreateContext(IntPtr x11Display, IntPtr x11VisualInfo, IntPtr shareList, bool direct);

			[DllImport(libGLX, SetLastError = true)]
			public static extern void glXDestroyContext(IntPtr x11Display, IntPtr context);

			[DllImport(libGLX, SetLastError = true)]
			public static extern bool glXMakeCurrent(IntPtr x11Display, uint x11WindowID, IntPtr context);

			[DllImport(libGLX, SetLastError = true)]
			public static extern void glXSwapBuffers(IntPtr x11Display, uint x11WindowID);
		}

		private static class Backup
		{

			[DllImport(libGL, SetLastError = true)]
			public static extern unsafe IntPtr glXCreateContext(IntPtr x11Display, IntPtr x11VisualInfo, IntPtr shareList, bool direct);

			[DllImport(libGL, SetLastError = true)]
			public static extern void glXDestroyContext(IntPtr x11Display, IntPtr context);

			[DllImport(libGL, SetLastError = true)]
			public static extern bool glXMakeCurrent(IntPtr x11Display, uint x11WindowID, IntPtr context);

			[DllImport(libGL, SetLastError = true)]
			public static extern void glXSwapBuffers(IntPtr x11Display, uint x11WindowID);
		}

		public static unsafe IntPtr glXCreateContext(IntPtr x11Display, IntPtr x11VisualInfo, IntPtr shareList, bool direct)
		{
			if (!glxUseBackup)
			{
				try { return Normal.glXCreateContext(x11Display, x11VisualInfo, shareList, direct); }
				catch { glxUseBackup = true; }
			}
			if (glxUseBackup)
			{
				return Backup.glXCreateContext(x11Display, x11VisualInfo, shareList, direct);
			}
			return IntPtr.Zero;
		}

		public static void glXDestroyContext(IntPtr x11Display, IntPtr context)
		{
			if (!glxUseBackup)
			{
				try { Normal.glXDestroyContext(x11Display, context); }
				catch { glxUseBackup = true; }
			}
			if (glxUseBackup)
			{
				Backup.glXDestroyContext(x11Display, context);
			}
		}

		public static bool glXMakeCurrent(IntPtr x11Display, uint x11WindowID, IntPtr context)
		{
			if (!glxUseBackup)
			{
				try { return Normal.glXMakeCurrent(x11Display, x11WindowID, context); }
				catch { glxUseBackup = true; }
			}
			if (glxUseBackup)
			{
				return Backup.glXMakeCurrent(x11Display, x11WindowID, context);
			}
			return false;
		}

		public static void glXSwapBuffers(IntPtr x11Display, uint x11WindowID)
		{
			if (!glxUseBackup)
			{
				try { Normal.glXSwapBuffers(x11Display, x11WindowID); }
				catch { glxUseBackup = true; }
			}
			if (glxUseBackup)
			{
				Backup.glXSwapBuffers(x11Display, x11WindowID);
			}
		}

		[DllImport(libGLEWGLX, SetLastError = true)]
		public static extern void glXSwapIntervalEXT(IntPtr x11Display, uint x11WindowID, int interval);

		[DllImport(libX11, SetLastError = true)]
		public static extern uint XVisualIDFromVisual(IntPtr x11Visual);

		[DllImport(libX11, SetLastError = true)]
		public static unsafe extern IntPtr XGetVisualInfo(IntPtr x11Display, int mask, XVisualInfo* vinfoTemplate, int* count);

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_x11_display_get_xdisplay(IntPtr gdkDisplay);

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_x11_visual_get_xvisual(IntPtr gdkVisual);

		[DllImport(libGDK, SetLastError = true)]
		public static extern uint gdk_x11_window_get_xid(IntPtr gdkWindow);

		[DllImport(libGDK, SetLastError = true)]
		public static extern int gdk_x11_screen_get_screen_number(IntPtr gdkScreen);

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_x11_window_get_type();

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_wayland_window_get_type();

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_mir_window_get_type();

		public const int GLX_USE_GL = 1;
		public const int GLX_BUFFER_SIZE = 2;
		public const int GLX_LEVEL = 3;
		public const int GLX_RGBA = 4;
		public const int GLX_DOUBLEBUFFER = 5;
		public const int GLX_STEREO = 6;
		public const int GLX_AUX_BUFFERS = 7;
		public const int GLX_RED_SIZE = 8;
		public const int GLX_GREEN_SIZE = 9;
		public const int GLX_BLUE_SIZE = 10;
		public const int GLX_ALPHA_SIZE = 11;
		public const int GLX_DEPTH_SIZE = 12;
		public const int GLX_STENCIL_SIZE = 13;
		public const int GLX_ACCUM_RED_SIZE = 14;
		public const int GLX_ACCUM_GREEN_SIZE = 15;
		public const int GLX_ACCUM_BLUE_SIZE = 16;
		public const int GLX_ACCUM_ALPHA_SIZE = 17;

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_wayland_display_get_wl_display(IntPtr gdkDisplay);

		[DllImport(libGDK, SetLastError = true)]
		public static extern IntPtr gdk_wayland_window_get_wl_surface(IntPtr gdkWindow);

		[DllImport(libWLEGL, SetLastError = true)]
		public static extern IntPtr wl_egl_window_create(IntPtr wlSurface, int width, int height);
		
		[DllImport(libWLEGL, SetLastError = true)]
		public static extern void wl_egl_window_destroy(IntPtr wlEglWindow);

		[DllImport(libEGL, SetLastError = true)]
		public static extern IntPtr eglGetDisplay(IntPtr wlDisplay);

		[DllImport(libEGL, SetLastError = true)]
		public static extern unsafe bool eglInitialize(IntPtr eglDisplay, int* major, int* minor);

		[DllImport(libEGL, SetLastError = true)]
		public static extern bool eglBindAPI(int api);

		[DllImport(libEGL, SetLastError = true)]
		public static extern unsafe bool eglChooseConfig(IntPtr eglDisplay, int* attribs, IntPtr* configs, int configSize, int* numConfigs);

		[DllImport(libEGL, SetLastError = true)]
		public static extern IntPtr eglCreateContext(IntPtr eglDisplay, IntPtr config, IntPtr shareContext, IntPtr attribs);

		[DllImport(libEGL, SetLastError = true)]
		public static extern bool eglDestroyContext(IntPtr eglDisplay, IntPtr eglContext);

		[DllImport(libEGL, SetLastError = true)]
		public static extern IntPtr eglCreateWindowSurface(IntPtr eglDisplay, IntPtr config, IntPtr nativeEglWindow, IntPtr attribs);

		[DllImport(libEGL, SetLastError = true)]
		public static extern bool eglDestroySurface(IntPtr eglDisplay, IntPtr eglSurface);

		[DllImport(libEGL, SetLastError = true)]
		public static extern bool eglMakeCurrent(IntPtr eglDisplay, IntPtr drawSurface, IntPtr readSurface, IntPtr ctx);
	}

	struct XVisualInfo
	{
		public IntPtr visual;
		public uint visualid;
		public int screen;
		public int depth;
		public int c_class;
		public uint red_mask;
		public uint green_mask;
		public uint blue_mask;
		public int colormap_size;
		public int bits_per_rgb;
	}
}
