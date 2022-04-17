using SWF = System.Windows.Forms;
using Eto.Forms;
using Eto.WinForms;
using Eto.WinForms.Forms;
using ASEva.UIEto;

namespace ASEva.UICoreWF
{
	public class ProgressBarHandler : WindowsControl<SWF.ProgressBar, ProgressBar, ProgressBar.ICallback>, ProgressBar.IHandler
	{
		public ProgressBarHandler ()
		{
			this.Control = new SWF.ProgressBar {
				Maximum = 100,
				Style = SWF.ProgressBarStyle.Blocks,
			};
			this.Control.Size = new System.Drawing.Size((int)(this.Control.Width * SizerExtensions.PixelScale),
				(int)(this.Control.Height * SizerExtensions.PixelScale));
		}

		static SWF.ProgressBarStyle IndeterminateStyle
		{
			get { return (SWF.Application.RenderWithVisualStyles) ? SWF.ProgressBarStyle.Marquee : SWF.ProgressBarStyle.Continuous; }
		}

		public bool Indeterminate {
			get { return Control.Style == SWF.ProgressBarStyle.Continuous || Control.Style == SWF.ProgressBarStyle.Marquee; }
			set { 
				Control.Style = value ? IndeterminateStyle : SWF.ProgressBarStyle.Blocks;
			}
		}

		public int MaxValue {
			get { return Control.Maximum; }
			set { Control.Maximum = value; }
		}

		public int MinValue {
			get { return Control.Minimum; }
			set { Control.Minimum = value; }
		}

		public int Value {
			get { return Control.Value; }
			set { Control.Value = value; }
		}
    }
}

