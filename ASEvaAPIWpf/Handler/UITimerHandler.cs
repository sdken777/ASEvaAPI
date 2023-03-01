using System;
using System.Windows.Threading;
using Eto.Forms;
using Eto;

namespace ASEva.UIWpf
{
	public class UITimerHandler : WidgetHandler<DispatcherTimer, UITimer, UITimer.ICallback>, UITimer.IHandler
	{

		public UITimerHandler()
		{
			Control = new DispatcherTimer(DispatcherPriority.Normal);
			Control.Tick += (sender, e) => Callback.OnElapsed(Widget, EventArgs.Empty);
		}

		public void Start()
		{
			if (Control.IsEnabled)
				Interval = Interval;
			else
				Control.Start();
		}

		public void Stop()
		{
			if (Control.IsEnabled)
				Control.Stop();
		}

		public double Interval
		{
			get
			{
				return Control.Interval.TotalSeconds;
			}
			set
			{
				Control.Interval = TimeSpan.FromSeconds(value);
			}
		}
	}
}

