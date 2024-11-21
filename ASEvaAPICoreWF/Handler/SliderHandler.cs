using System;
using System.Linq;
using swf = System.Windows.Forms;
using sd = System.Drawing;
using Eto.Forms;
using Eto.WinForms.Forms;

namespace ASEva.UICoreWF
{
	class SliderHandler : WindowsControl<swf.TrackBar, Slider, Slider.ICallback>, Slider.IHandler
	{
		int? lastValue;

		class EtoTrackBar : swf.TrackBar
		{
			public EtoTrackBar()
            {
				SetStyle(swf.ControlStyles.UserPaint, true);
            }

			protected override void OnCreateControl()
			{
				SetStyle(swf.ControlStyles.SupportsTransparentBackColor, true);
				if (Parent != null)
					BackColor = Parent.BackColor;

				base.OnCreateControl();
			}

			// CHECK: 修正窗口最大化/恢复时控件整体闪烁问题，Win10可重现
            protected override void OnPaint(swf.PaintEventArgs e)
            {
				var bitmap = new sd.Bitmap(Width, Height);
				SetStyle(swf.ControlStyles.UserPaint, false);
				DrawToBitmap(bitmap, new sd.Rectangle(0, 0, Width, Height));
				SetStyle(swf.ControlStyles.UserPaint, true);
				e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }

		public SliderHandler()
		{
			this.Control = new EtoTrackBar
			{
				TickStyle = System.Windows.Forms.TickStyle.BottomRight,
				Maximum = 100,
				AutoSize = true
			};
		}

		public override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Control.ValueChanged += HandleScaleValueChanged;
		}

		void HandleScaleValueChanged(object? sender, EventArgs e)
		{
			var value = Control.Value;
			var tick = Control.TickFrequency;
			var offset = value % tick;
			if (SnapToTick && offset != 0)
			{
				if (offset > tick / 2)
					Control.Value = value - offset + tick;
				else
					Control.Value -= offset;
			}
			else if (lastValue == null || lastValue.Value != value)
			{
				Callback.OnValueChanged(Widget, EventArgs.Empty);
				lastValue = value;
			}
		}

		public int MaxValue
		{
			get { return Control.Maximum; }
			set { Control.Maximum = value; }
		}

		public int MinValue
		{
			get { return Control.Minimum; }
			set { Control.Minimum = value; }
		}

		public int Value
		{
			get { return Control.Value; }
			set { Control.Value = value; }
		}

		public bool SnapToTick { get; set; }

		public int TickFrequency
		{
			get { return Control.TickFrequency; }
			set
			{
				Control.TickFrequency = value;
			}
		}

		public Orientation Orientation
		{
			get { return Control.Orientation == swf.Orientation.Horizontal ? Orientation.Horizontal : Orientation.Vertical; }
			set { Control.Orientation = value == Orientation.Horizontal ? swf.Orientation.Horizontal : swf.Orientation.Vertical; }
		}

        static readonly int[] intrinsicEvents = [0x0201/* Win32.WM.LBUTTONDOWN */, 0x0202/* Win32.WM.LBUTTONUP */, 0x0203/* Win32.WM.LBUTTONDBLCLK */];
        public override bool ShouldBubbleEvent(swf.Message msg)
        {
            return !intrinsicEvents.Contains(msg.Msg) && base.ShouldBubbleEvent(msg);
        }
    }
}

