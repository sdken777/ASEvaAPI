using System;
using System.Collections.Generic;
using System.Reflection;
using GLib;
using ASEva;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Timer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 计时器
    /// </summary>
    public class Timer
    {
        public Timer(uint interval, TimeoutHandler handler)
        {
            interval = Math.Max(1, interval);
            timerID = (long)GLib.Timeout.Add(interval, handler);
        }

        public void Release()
        {
            if (timerID >= 0)
            {
                GLib.Timeout.Remove((uint)timerID);
                timerID = -1;
            }
        }

        ~Timer()
        {
            Release();
        }

        private long timerID = -1;
    }
}