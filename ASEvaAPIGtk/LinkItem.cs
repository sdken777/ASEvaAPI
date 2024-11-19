using System;
using Gtk;
using ASEva;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Compact link item
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 带下划线的蓝色文字
    /// </summary>
    public class LinkItem : Box
    {
        [UI] EventBox? eventBox;
        [UI] Label? label;

        public LinkItem() : this(new Builder("LinkItem.glade"))
        {}

        private LinkItem(Builder builder) : base(builder.GetRawOwnedObject("LinkItem"))
        {
            builder.Autoconnect(this);

            linkColor = new ColorRGBA(0, 0, 255, 255); // blue
            updateColor();

            StateFlagsChanged += this_StateFlagsChanged;
            
            if (eventBox != null)
            {
                eventBox.ButtonPressEvent += eventBox_ButtonPress;
                eventBox.EnterNotifyEvent += eventBox_EnterNotify;
                eventBox.LeaveNotifyEvent += eventBox_LeaveNotify;
            }
        }

        public event EventHandler? Clicked;

        public String Text
        {
            get { return label?.Text ?? ""; }
            set { if (label != null) label.Text = value; }
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

        public void SetFont(Pango.FontDescription fd)
        {
            label?.OverrideFont(fd);
        }

        public object? Tag { get; set; }

        private void eventBox_ButtonPress(object? o, ButtonPressEventArgs args)
        {
            if (args.Event.Button == 1 && args.Event.Type == Gdk.EventType.ButtonPress)
            {
                args.RetVal = true;
                Clicked?.Invoke(this, EventArgs.Empty); 
            }
        }

        private void eventBox_LeaveNotify(object? o, LeaveNotifyEventArgs args)
        {
            updateColor(false);
        }

        private void eventBox_EnterNotify(object? o, EnterNotifyEventArgs args)
        {
            if (timer != null) return;
            timer = new Timer(20, () =>
            {
                updateColor();
                timer?.Release();
                timer = null;
                return false;
            });
        }

        private void this_StateFlagsChanged(object? o, StateFlagsChangedArgs args)
        {
            updateColor();
        }

        private void updateColor(bool? forceInside = null)
        {
            if (StateFlags.HasFlag(StateFlags.Insensitive))
            {
                label?.ModifyFg(StateType.Normal);
            }
            else
            {
                if (eventBox == null) return;

                int x, y;
                eventBox.GetPointer(out x, out y);
                bool inside = x >= 0 && x < eventBox.AllocatedWidth && y >= 0 && y < eventBox.AllocatedHeight;

                if (forceInside != null) inside = forceInside.Value;

                if (inside)
                {
                    var r = (byte)(((int)linkColor.R + 255) / 2);
                    var g = (byte)(((int)linkColor.G + 255) / 2);
                    var b = (byte)(((int)linkColor.B + 255) / 2);
                    label?.OverrideColor(StateFlags.Normal, ColorConv.Conv(new ColorRGBA(r, g, b)));
                }
                else label?.OverrideColor(StateFlags.Normal, ColorConv.Conv(linkColor));
            }
        }

        private ColorRGBA linkColor;
        private Timer? timer = null;
    }
}
