using System;

namespace ASEva.Utility
{
	/// \~English
	/// <summary>
	/// (api:app=3.0.0) Filter boolean signals and extract rising edge
	/// </summary>
	/// \~Chinese
	/// <summary>
	/// (api:app=3.0.0) 二值化信号滤波并提取上升沿
	/// </summary>
	public class TriggerFilter
    {
        public double Interval { get; set; } = 1;

        public bool Update(bool state, Sample ts)
        {
	        if (lastState != null && lastStateTS != null)
	        {
		        if (ts.Session == lastStateTS.Session && ts.Offset <= lastStateTS.Offset)
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
		        if (filterTS != null && ts.Session == filterTS.Session && ts.Offset - filterTS.Offset < Interval) filterState = true;
		        else filterState = false;
	        }

	        bool trigger = lastState != null && lastState.Value == false && filterState == true;
	        lastState = filterState;
	        lastStateTS = ts;

	        return trigger;
        }

		private bool? lastState;
		private Sample? lastStateTS;
		private Sample? filterTS;
    }
}
