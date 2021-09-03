using System;
using System.Collections.Generic;
using System.Reflection;
using GLib;
using ASEva;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.0) 计时器
    /// </summary>
    public class Timer
    {
        public Timer(uint interval, TimeoutHandler handler)
        {
            if (handler == null) return;

            interval = Math.Max(1, interval);
            timerID = GLib.Timeout.Add(interval, handler);
            handlerMethod = handler.Target.GetType().ToString() + "." + handler.Method.Name;

            int timerCount = 0;
            lock (timers)
            {
                timers.Add(this);
                timerCount = timers.Count;
            }

            if (timerCount >= 200)
            {
                Agency.Print("[Timer] Too many timers:\n- " + String.Join("\n- ", Timer.Handlers));
            }
        }

        public void Release()
        {
            if (timerID == null) return;

            GLib.Timeout.Remove(timerID.Value);
            timerID = null;

            lock (timers)
            {
                timers.Remove(this);
            }
        }

        public static int Count
        {
            get
            {
                lock (timers)
                {
                    return timers.Count;
                }
            }
        }

        public static String[] Handlers
        {
            get
            {
                lock (timers)
                {
                    var handlers = new List<String>();
                    foreach (var timer in timers)
                    {
                        handlers.Add(timer.handlerMethod);
                    }
                    return handlers.ToArray();
                }
            }
        }

        private uint? timerID = null;
        private String handlerMethod;

        private static List<Timer> timers = new List<Timer>();
    }
}