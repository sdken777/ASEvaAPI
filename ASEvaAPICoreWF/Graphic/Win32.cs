using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;

namespace ASEva.UICoreWF
{
	static class Win32
	{
        static Win32()
        {
			try { HandleOpengl32 = Win32.LoadLibrary(OpenGL32); } catch (Exception) {}
			try { HandleGlu32 = Win32.LoadLibrary(Glu32); } catch (Exception) {}
		}

		public static IntPtr HandleOpengl32 { get; private set; }
		public static IntPtr HandleGlu32 { get; private set; }

		private const string Kernel32 = "kernel32.dll";
		private const string User32 = "user32.dll";
        private const string OpenGL32 = "opengl32.dll";
        private const string Gdi32 = "gdi32.dll";
		private const string Glu32 = "glu32.dll";

		[DllImport(Kernel32, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport(Kernel32, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr handle, string name);

		[DllImport(User32, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport(User32, SetLastError = true)]
		public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[DllImport(OpenGL32, SetLastError = true)]
        public static extern int wglMakeCurrent(IntPtr hdc, IntPtr hrc);

        [DllImport(OpenGL32, SetLastError = true)]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        [DllImport(OpenGL32, SetLastError = true)]
        public static extern int wglDeleteContext(IntPtr hrc);

		[DllImport(OpenGL32, SetLastError = true)]
		public static extern bool wglUseFontBitmapsW(IntPtr hDC, uint first, uint count, uint listBase);

		[DllImport(OpenGL32, SetLastError = true)]
		public static extern IntPtr wglGetProcAddress(String name);

		[DllImport(Gdi32, SetLastError = true)]
        public unsafe static extern int ChoosePixelFormat(IntPtr hDC, [In, MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(Gdi32, SetLastError = true)]
        public unsafe static extern int SetPixelFormat(IntPtr hDC, int iPixelFormat, [In, MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern int SwapBuffers(IntPtr hDC);

		[DllImport(Gdi32, SetLastError = true)]
		public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement,
		   int nOrientation, uint fnWeight, uint fdwItalic, uint fdwUnderline, uint
		   fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint
		   fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

		[DllImport(Gdi32, SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport(Gdi32, SetLastError = true)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport(Gdi32, SetLastError = true)]
		public static extern bool GetTextExtentPointW(IntPtr hdc, string lpString, int cbString, ref Size lpSize);

		[StructLayout(LayoutKind.Explicit)]
		public class PIXELFORMATDESCRIPTOR
		{
			[FieldOffset(0)]
			public UInt16 nSize;
			[FieldOffset(2)]
			public UInt16 nVersion;
			[FieldOffset(4)]
			public UInt32 dwFlags;
			[FieldOffset(8)]
			public Byte iPixelType;
			[FieldOffset(9)]
			public Byte cColorBits;
			[FieldOffset(10)]
			public Byte cRedBits;
			[FieldOffset(11)]
			public Byte cRedShift;
			[FieldOffset(12)]
			public Byte cGreenBits;
			[FieldOffset(13)]
			public Byte cGreenShift;
			[FieldOffset(14)]
			public Byte cBlueBits;
			[FieldOffset(15)]
			public Byte cBlueShift;
			[FieldOffset(16)]
			public Byte cAlphaBits;
			[FieldOffset(17)]
			public Byte cAlphaShift;
			[FieldOffset(18)]
			public Byte cAccumBits;
			[FieldOffset(19)]
			public Byte cAccumRedBits;
			[FieldOffset(20)]
			public Byte cAccumGreenBits;
			[FieldOffset(21)]
			public Byte cAccumBlueBits;
			[FieldOffset(22)]
			public Byte cAccumAlphaBits;
			[FieldOffset(23)]
			public Byte cDepthBits;
			[FieldOffset(24)]
			public Byte cStencilBits;
			[FieldOffset(25)]
			public Byte cAuxBuffers;
			[FieldOffset(26)]
			public SByte iLayerType;
			[FieldOffset(27)]
			public Byte bReserved;
			[FieldOffset(28)]
			public UInt32 dwLayerMask;
			[FieldOffset(32)]
			public UInt32 dwVisibleMask;
			[FieldOffset(36)]
			public UInt32 dwDamageMask;

            public void Init()
            {
                nSize = (ushort)Marshal.SizeOf(this);
            }
		}

        public const byte PFD_TYPE_RGBA				= 0;
		public const byte PFD_TYPE_COLORINDEX		= 1;

		public const uint PFD_DOUBLEBUFFER			= 1;
		public const uint PFD_STEREO				= 2;
		public const uint PFD_DRAW_TO_WINDOW		= 4;
		public const uint PFD_DRAW_TO_BITMAP		= 8;
		public const uint PFD_SUPPORT_GDI			= 16;
		public const uint PFD_SUPPORT_OPENGL		= 32;
		public const uint PFD_GENERIC_FORMAT		= 64;
		public const uint PFD_NEED_PALETTE			= 128;
		public const uint PFD_NEED_SYSTEM_PALETTE	= 256;
		public const uint PFD_SWAP_EXCHANGE		    = 512;
		public const uint PFD_SWAP_COPY			    = 1024;
		public const uint PFD_SWAP_LAYER_BUFFERS	= 2048;
		public const uint PFD_GENERIC_ACCELERATED	= 4096;
		public const uint PFD_SUPPORT_DIRECTDRAW	= 8192;

		public const sbyte PFD_MAIN_PLANE			= 0;
		public const sbyte PFD_OVERLAY_PLANE		= 1;
		public const sbyte PFD_UNDERLAY_PLANE		= -1;
	}
}
