using System;
using System.Collections.Generic;
using Gdk;
using Gtk;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 事件框辅助类
    /// </summary>
    public class EventBoxHelper
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 添加事件框
        /// </summary>
        /// <param name="eventBox">事件框</param>
        /// <param name="contextMenu">关联右键菜单</param>
        /// <param name="enableScrolledEvent">是否启用滚轮事件</param>
        public void Add(EventBox eventBox, Menu contextMenu = null, bool enableScrolledEvent = false)
        {
            if (eventBox == null) return;

            if (contextMenu != null) menuMap[eventBox] = contextMenu;

            eventBox.AddEvents((int)Gdk.EventMask.PointerMotionMask);
            if (enableScrolledEvent) eventBox.AddEvents((int)Gdk.EventMask.ScrollMask);

            eventBox.ButtonPressEvent += eventBox_ButtonPress;
            eventBox.ButtonReleaseEvent += eventBox_ButtonRelease;
            eventBox.MotionNotifyEvent += eventBox_MotionNotify;
            eventBox.EnterNotifyEvent += eventBox_EnterNotify;
            eventBox.LeaveNotifyEvent += eventBox_LeaveNotify;
            eventBox.ScrollEvent += eventBox_Scroll;
        }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:gtk=2.5.4) 事件是否允许下层控件接收，默认false
        /// </summary>
        public bool PassEvents { get; set; }

        public delegate void ButtonHandler(EventBox box, EventButton ev);
        public delegate void MovedHandler(EventBox box, EventMotion ev);
        public delegate void ScrolledHandler(EventBox box, EventScroll ev);

        public event ButtonHandler LeftDown;
        public event MovedHandler LeftMoved;
        public event ButtonHandler LeftUp;
        public event ButtonHandler LeftDoubleClicked;
        public event ButtonHandler RightDown;
        public event MovedHandler RightMoved;
        public event ButtonHandler RightUp;
        public event EventHandler Enter;
        public event EventHandler Leave;
        public event MovedHandler Moved;
        public event ScrolledHandler Scrolled;

        private void eventBox_Scroll(object o, ScrollEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;
            if (Scrolled != null) Scrolled(o as EventBox, args.Event);
        }

        private void eventBox_LeaveNotify(object o, LeaveNotifyEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;
            if (args.Event.Detail != Gdk.NotifyType.Inferior)
            {
                if (Leave != null) Leave(o, args);
            }
        }

        private void eventBox_EnterNotify(object o, EnterNotifyEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;
            if (Enter != null) Enter(o, args);
        }

        private void eventBox_MotionNotify(object o, MotionNotifyEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;

            var box = o as EventBox;

            if (args.Event.State.HasFlag(Gdk.ModifierType.Button1Mask))
            {
                if (LeftMoved != null) LeftMoved(box, args.Event);
            }
            else if (args.Event.State.HasFlag(Gdk.ModifierType.Button3Mask))
            {
                if (RightMoved != null) RightMoved(box, args.Event);
            }

            var x = args.Event.X;
            var y = args.Event.Y;
            if (x >= 0 && x < box.AllocatedWidth && y >= 0 && y < box.AllocatedHeight)
            {
                if (Moved != null) Moved(box, args.Event);
            }
        }

        private void eventBox_ButtonRelease(object o, ButtonReleaseEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;

            var box = o as EventBox;

            if (args.Event.Button == 1)
            {
                if (LeftUp != null) LeftUp(box, args.Event);
            }
            else if (args.Event.Button == 3)
            {
                if (RightUp != null) RightUp(box, args.Event);
                if (menuMap.ContainsKey(box)) menuMap[box].PopupAtPointer(args.Event);
            }
        }

        private void eventBox_ButtonPress(object o, ButtonPressEventArgs args)
        {
            if (!PassEvents) args.RetVal = true;

            var box = o as EventBox;

            if (args.Event.Button == 1)
            {
                if (args.Event.Type == Gdk.EventType.ButtonPress)
                {
                    if (LeftDown != null) LeftDown(box, args.Event);
                }
                else if (args.Event.Type == Gdk.EventType.DoubleButtonPress)
                {
                    if (LeftDoubleClicked != null) LeftDoubleClicked(box, args.Event);
                }
            }
            else if (args.Event.Button == 3)
            {
                if (args.Event.Type == Gdk.EventType.ButtonPress)
                {
                    if (RightDown != null) RightDown(box, args.Event);
                }
            }
        }

        private Dictionary<EventBox, Menu> menuMap = new Dictionary<EventBox, Menu>();
    }
}