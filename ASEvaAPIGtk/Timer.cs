using System;
using System.Collections.Generic;
using System.Reflection;
using GLib;
using ASEva;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=2.8.2) Timer
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.8.2) 计时器
    /// </summary>
    public class Timer
    {
        public Timer(uint interval, TimeoutHandler handler)
        {
            if (handler == null) return;

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

        public static int Count
        {
            get
            {
                return 0;
            }
        }

        public static String[] Handlers
        {
            get
            {
                return new String[0];
            }
        }

        private long timerID = -1;
    }
}