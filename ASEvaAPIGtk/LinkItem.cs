using System;
using Gtk;
using ASEva;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// <summary>
    /// (api:gtk=2.0.0) 带下划线的蓝色文字
    /// </summary>
    public class LinkItem : Box
    {
        [UI] EventBox eventBox;
        [UI] Label label;

        public LinkItem() : this(new Builder("LinkItem.glade"))
        {
            linkColor = new ColorRGBA(0, 0, 255, 255); // blue
            updateColor();

            StateFlagsChanged += this_StateFlagsChanged;
            eventBox.ButtonPressEvent += eventBox_ButtonPress;
            eventBox.EnterNotifyEvent += eventBox_EnterNotify;
            eventBox.LeaveNotifyEvent += eventBox_LeaveNotify;
        }

        private LinkItem(Builder builder) : base(builder.GetObject("LinkItem").Handle)
        {
            builder.Autoconnect(this);
        }

        public event EventHandler Clicked;

        public String Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public ColorRGBA ForeColor
        {
            get { return linkColor; }
            set
            {
                linkColor = value;
                updateColor();
            }
        }

        public object Tag { get; set; }

        private void eventBox_ButtonPress(object o, ButtonPressEventArgs args)
        {
            if (args.Event.Button == 1 && args.Event.Type == Gdk.EventType.ButtonPress)
            {
                args.RetVal = true;
                if (Clicked != null) Clicked(this, null); 
            }
        }

        private void eventBox_LeaveNotify(object o, LeaveNotifyEventArgs args)
        {
            updateColor(false);
        }

        private void eventBox_EnterNotify(object o, EnterNotifyEventArgs args)
        {
            if (timer != null) return;
            timer = new Timer(20, () =>
            {
                updateColor();
                timer.Release();
                timer = null;
                return false;
            });
        }

        private void this_StateFlagsChanged(object o, StateFlagsChangedArgs args)
        {
            updateColor();
        }

        private void updateColor(bool? forceInside = null)
        {
            if (StateFlags.HasFlag(StateFlags.Insensitive))
            {
                label.ModifyFg(StateType.Normal);
            }
            else
            {
                int x, y;
                eventBox.GetPointer(out x, out y);
                bool inside = x >= 0 && x < eventBox.AllocatedWidth && y >= 0 && y < eventBox.AllocatedHeight;

                if (forceInside != null) inside = forceInside.Value;

                if (inside)
                {
                    var r = (byte)(((int)linkColor.R + 255) / 2);
                    var g = (byte)(((int)linkColor.G + 255) / 2);
                    var b = (byte)(((int)linkColor.B + 255) / 2);
                    label.OverrideColor(StateFlags.Normal, ColorConv.Conv(new ColorRGBA(r, g, b)));
                }
                else label.OverrideColor(StateFlags.Normal, ColorConv.Conv(linkColor));
            }
        }

        private ColorRGBA linkColor;
        private Timer timer = null;
    }
}
