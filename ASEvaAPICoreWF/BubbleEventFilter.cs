using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using swf = System.Windows.Forms;
using Eto;
using Eto.WinForms;
using Eto.WinForms.Forms;

namespace ASEva.UICoreWF
{
	class BubbleEvent
	{
		public Func<BubbleEventArgs, bool> HandleEvent { get; set; }

		public Win32.WM Message { get; set; }
	}

	class BubbleEventArgs : EventArgs
	{
		public swf.Message Message { get; private set; }

		public Control Control { get; private set; }

		public swf.Control WinControl { get; private set; }

		public IWindowsControl WindowsControl
		{
			get { return Control != null ? Control.Handler as IWindowsControl : null; }
		}

		public IEnumerable<Control> Controls
		{
			get
			{
				var control = Control;
				while (control != null)
				{
					yield return control;
					control = control.VisualParent;
				}
			}
		}

		public IEnumerable<Control> Parents
		{
			get
			{
				if (Control != null)
				{
					var control = Control.VisualParent;
					while (control != null)
					{
						yield return control;
						control = control.VisualParent;
					}
				}
			}
		}

		public BubbleEventArgs(swf.Message message, swf.Control winControl)
		{
			this.Message = message;
			this.Control = ToEto(winControl);
			this.WinControl = winControl;
		}

		static Control ToEto(swf.Control child)
		{
			var handler = child.Tag as Control.IHandler;
			return handler != null ? handler.Widget as Control : null;
		}
	}

	class BubbleEventFilter : swf.IMessageFilter
	{
		readonly Dictionary<int, BubbleEvent> messages = new Dictionary<int, BubbleEvent>();

		public void AddBubbleEvents(Func<BubbleEventArgs, bool> handleEvent, params Win32.WM[] messages)
		{
			foreach (var message in messages)
			{
				AddBubbleEvent(handleEvent, message);
			}
		}

		public void AddBubbleEvent(Func<BubbleEventArgs, bool> handleEvent, Win32.WM message)
		{
			messages.Add((int)message, new BubbleEvent
			{
				Message = message,
				HandleEvent = handleEvent
			});
		}

		public bool PreFilterMessage(ref swf.Message message)
		{
			BubbleEvent bubble;
			if (messages.TryGetValue(message.Msg, out bubble))
			{
				var child = swf.Control.FromHandle(message.HWnd);

				if (child != null)
				{
					var args = new BubbleEventArgs(message, child);
					if (bubble.HandleEvent(args))
						return true;
				}
			}
			return false;
		}

		public void AddBubbleKeyEvent(Action<Control, Control.ICallback, KeyEventArgs> action, Win32.WM message, KeyEventType keyEventType)
		{
			AddBubbleEvent(be => KeyEvent(be, action, keyEventType), message);
		}

		public void AddBubbleKeyCharEvent(Action<Control, Control.ICallback, KeyEventArgs> action, Win32.WM message, KeyEventType keyEventType)
		{
			AddBubbleEvent(be => KeyCharEvent(be, action, keyEventType), message);
		}

		public void AddBubbleMouseEvent(Action<Control, Control.ICallback, MouseEventArgs> action, bool? capture, Win32.WM message, Func<MouseButtons, MouseButtons> modifyButtons = null)
		{
			AddBubbleEvent(be => MouseEvent(be, action, capture, modifyButtons), message);
		}

		public void AddBubbleMouseEvents(Action<Control, Control.ICallback, MouseEventArgs> action, bool? capture, params Win32.WM[] messages)
		{
			foreach (var message in messages)
			{
				AddBubbleEvent(be => MouseEvent(be, action, capture), message);
			}
		}

		static bool MouseEvent(BubbleEventArgs be, Action<Control, Control.ICallback, MouseEventArgs> action, bool? capture, Func<MouseButtons, MouseButtons> modifyButtons = null)
		{
			var mainControl = be.Control;
			if (mainControl == null)
				return false;

			var modifiers = swf.Control.ModifierKeys.ToEto();
			var delta = new SizeF(0, Win32.GetWheelDeltaWParam(be.Message.WParam) / WinConversions.WheelDelta);
			var buttons = Win32.GetMouseButtonWParam(be.Message.WParam).ToEto();
			if (modifyButtons != null)
				buttons = modifyButtons(buttons);
			var handler = be.WindowsControl;
			var mousePosition = Eto.WinForms.WinConversions.ToEto(swf.Control.MousePosition);

			var msg = be.Message;
			var me = new MouseEventArgs(buttons, modifiers, mainControl.PointFromScreen(mousePosition), delta);
			action(mainControl, handler.Callback, me);

			if (!me.Handled && handler != null && handler.ShouldBubbleEvent(msg))
			{
				foreach (var control in be.Parents)
				{
					me = new MouseEventArgs(buttons, modifiers, control.PointFromScreen(mousePosition), delta);
					action(control, handler.Callback, me);
					if (me.Handled)
						return true;
				}
			}
			return me.Handled;
		}

		static bool KeyEvent(BubbleEventArgs be, Action<Control, Control.ICallback, KeyEventArgs> action, KeyEventType keyEventType)
		{
			Keys keyData = ((swf.Keys)(long)be.Message.WParam | swf.Control.ModifierKeys).ToEto();
			
			char? keyChar = null;
			var kevt = new KeyEventArgs(keyData, keyEventType, keyChar);
			if (be.Control != null)
				action(be.Control, (Control.ICallback)((ICallbackSource)be.Control).Callback, kevt);
			if (!kevt.Handled && (keyEventType != KeyEventType.KeyDown || !IsInputKey(be.Message.HWnd, keyData)))
			{
				foreach (var control in be.Parents)
				{
					var callback = (Control.ICallback)((ICallbackSource)control).Callback;
					action(control, callback, kevt);
					if (kevt.Handled)
						break;
				}
			}
			return kevt.Handled;
		}

		static bool IsInputChar(IntPtr hwnd, char charCode)
		{
			int num = charCode == '\t' ? 134 : 132;
			return ((int)((long)Win32.SendMessage(hwnd, Win32.WM.GETDLGCODE, IntPtr.Zero, IntPtr.Zero)) & num) != 0;
		}

		static bool IsInputKey(IntPtr hwnd, Keys keyData)
		{
			if (keyData.HasFlag(Keys.Alt))
			{
				return false;
			}
			int num = 4;
			switch (keyData & Keys.KeyMask)
			{
				case Keys.Tab:
					num = 6;
					break;
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					num = 5;
					break;
			}
			if (!EtoEnvironment.Platform.IsWindows)
				return false;
			return ((int)((long)Win32.SendMessage(hwnd, Win32.WM.GETDLGCODE, IntPtr.Zero, IntPtr.Zero)) & num) != 0;
		}

		static bool KeyCharEvent(BubbleEventArgs be, Action<Control, Control.ICallback, KeyEventArgs> action, KeyEventType keyEventType)
		{
			char keyChar = (char)((long)be.Message.WParam);
			var kevt = new KeyEventArgs(Keys.None, keyEventType, keyChar);
			if (be.Control != null)
				action(be.Control, (Control.ICallback)((ICallbackSource)be.Control).Callback, kevt);
			if (!kevt.Handled && !IsInputChar(be.Message.HWnd, keyChar))
			{
				foreach (var control in be.Parents)
				{
					var callback = (Control.ICallback)((ICallbackSource)control).Callback;
					action(control, callback, kevt);
					if (kevt.Handled)
						break;
				}
			}
			return kevt.Handled;
		}
	}

	static partial class Win32
    {
		public enum WM
		{
			SETREDRAW = 0xB,

			GETDLGCODE = 0x0087,

			KEYDOWN = 0x0100,
			KEYUP = 0x0101,
			CHAR = 0x0102,
			SYSKEYDOWN = 0x0104,
			SYSKEYUP = 0x0105,
			SYSCHAR = 0x0106,
			IME_CHAR = 0x0286,

			MOUSEMOVE = 0x0200,
			LBUTTONDOWN = 0x0201,
			LBUTTONUP = 0x0202,
			LBUTTONDBLCLK = 0x0203,
			RBUTTONDOWN = 0x0204,
			RBUTTONUP = 0x0205,
			RBUTTONDBLCLK = 0x0206,
			MBUTTONDOWN = 0x0207,
			MBUTTONUP = 0x0208,
			MBUTTONDBLCLK = 0x0209,
			MOUSEWHEEL = 0x20A,

			CUT = 0x0300,
			COPY = 0x0301,
			PASTE = 0x0302,
			CLEAR = 0x0303,

			ERASEBKGND = 0x14,

			TV_FIRST = 0x1100,
			TVM_SETBKCOLOR = TV_FIRST + 29,
			TVM_SETEXTENDEDSTYLE = TV_FIRST + 44,

			ECM_FIRST = 0x1500,
			EM_SETCUEBANNER = ECM_FIRST + 1,
			EM_SETMARGINS = 0xd3,

			DPICHANGED = 0x02E0,
			NCCREATE = 0x0081,
			NCLBUTTONDOWN = 0x00A1,
			PRINT = 0x0317,
			SHOWWINDOW = 0x00000018
		}

		public static ushort LOWORD(IntPtr word)
		{
			return (ushort)(((long)word) & 0xffff);
		}

		public static ushort LOWORD(int word)
		{
			return (ushort)(word & 0xFFFF);
		}

		public static ushort HIWORD(IntPtr dwValue)
		{
			return (ushort)((((long)dwValue) >> 0x10) & 0xffff);
		}

		public static ushort HIWORD(uint dwValue)
		{
			return (ushort)(dwValue >> 0x10);
		}

		public static int SignedHIWORD(IntPtr n)
		{
			return SignedHIWORD((int)((long)n));
		}

		public static int SignedLOWORD(IntPtr n)
		{
			return SignedLOWORD((int)((long)n));
		}

		public static int SignedHIWORD(int n)
		{
			return (short)((n >> 16) & 0xFFFF);
		}

		public static int SignedLOWORD(int n)
		{
			return (short)(n & 0xFFFF);
		}

		public static int GetWheelDeltaWParam(IntPtr wParam)
		{
			return SignedHIWORD(wParam);
		}

		[Flags]
		public enum MK
		{
			NONE = 0x0000,
			LBUTTON = 0x0001,
			RBUTTON = 0x0002,
			SHIFT = 0x0004,
			CONTROL = 0x0008,
			MBUTTON = 0x0010,
			XBUTTON1 = 0x0020,
			XBUTTON2 = 0x0040
		}

		public static System.Windows.Forms.MouseButtons GetMouseButtonWParam(IntPtr wParam)
		{
			var mask = (MK)LOWORD(wParam);
			var buttons = System.Windows.Forms.MouseButtons.None;

			if (mask.HasFlag(MK.LBUTTON))
				buttons |= System.Windows.Forms.MouseButtons.Left;
			if (mask.HasFlag(MK.RBUTTON))
				buttons |= System.Windows.Forms.MouseButtons.Right;
			if (mask.HasFlag(MK.MBUTTON))
				buttons |= System.Windows.Forms.MouseButtons.Middle;
			if (mask.HasFlag(MK.XBUTTON1))
				buttons |= System.Windows.Forms.MouseButtons.XButton1;
			if (mask.HasFlag(MK.XBUTTON2))
				buttons |= System.Windows.Forms.MouseButtons.XButton2;
			return buttons;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, WM wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, WM msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
	}
}
