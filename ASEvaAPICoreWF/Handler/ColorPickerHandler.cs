using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swf = System.Windows.Forms;
using sd = System.Drawing;
using Eto;
using Eto.WinForms;
using Eto.WinForms.Forms;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
	class ColorPickerHandler : WindowsControl<swf.Button, ColorPicker, ColorPicker.ICallback>, ColorPicker.IHandler
	{
		public ColorPickerHandler()
		{
			Control = new swf.Button { Width = 40 };
			Control.Click += HandleClick;
			Control.BackColor = sd.Color.Black;
			this.Control.Size = new System.Drawing.Size((int)(this.Control.Width * SizerExtensions.PixelScale),
				(int)(this.Control.Height * SizerExtensions.PixelScale));
		}

		public override Color BackgroundColor
		{
			get { return Colors.Transparent; }
			set
			{
				// cannot set background color, we use that as the currently selected color
			}
		}

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case ColorPicker.ColorChangedEvent:
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		void HandleClick(object sender, EventArgs e)
		{
			using (Widget.Platform.Context)
			{
				var picker = new ColorDialog { Color = Color, AllowAlpha = AllowAlpha };
				var result = picker.ShowDialog(Widget);
				if (result == DialogResult.Ok)
				{
					Color = picker.Color;
				}
			}
		}

		public Eto.Drawing.Color Color
		{
			get { return Control.BackColor.ToEto(); }
			set
			{
				var color = value.ToSD();
				if (Control.BackColor != color)
				{
					Control.BackColor = color;
					Callback.OnColorChanged(Widget, EventArgs.Empty);
				}
			}
		}

		public bool AllowAlpha { get; set; }

		public bool SupportsAllowAlpha => false;

		static readonly WM[] intrinsicEvents = { WM.LBUTTONDOWN, WM.LBUTTONUP, WM.LBUTTONDBLCLK };
		public override bool ShouldBubbleEvent(swf.Message msg)
		{
			return !intrinsicEvents.Contains((WM)msg.Msg) && base.ShouldBubbleEvent(msg);
		}
	}

	enum WM
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

		DPICHANGED = 0x02E0,
		NCCREATE = 0x0081,
		NCLBUTTONDOWN = 0x00A1
	}
}
