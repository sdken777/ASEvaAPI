using System;

namespace ASEva.Utility
{
	/// \~English
	/// <summary>
	/// (api:app=2.0.0) Filter boolean signals and extract rising edge
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:app=2.0.0) 二值化信号滤波并提取上升沿
	/// </summary>
	public class TriggerFilter
    {
        public TriggerFilter()
        {
            Interval = 1;
        }

        public double Interval { get; set; }

        public bool Update(bool state, Sample ts)
        {
	        if (lastState != null)
	        {
		        if (ts.Base == lastStateTS.Base && ts.Offset <= lastStateTS.Offset)
		        {
			        return false;
		        }
	        }

	        bool filterState = false;
	        if (state == true)
	        {
		        filterState = true;
		        filterTS = ts;
	        }
	        else
	        {
		        if (filterTS != null && ts.Base == filterTS.Base && ts.Offset - filterTS.Offset < Interval) filterState = true;
		        else filterState = false;
	        }

	        bool trigger = lastState != null && lastState.Value == false && filterState == true;
	        lastState = filterState;
	        lastStateTS = ts;

	        return trigger;
        }

		private bool? lastState;
		private Sample lastStateTS;
		private Sample filterTS;
    }
}
