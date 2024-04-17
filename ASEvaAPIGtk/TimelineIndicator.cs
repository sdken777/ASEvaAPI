using System;
using Gtk;
using ASEva.Utility;
using UI = Gtk.Builder.ObjectAttribute;

namespace ASEva.UIGtk
{
    #pragma warning disable CS0612, CS0649

    /// \~English
    /// <summary>
    /// (api:gtk=3.0.0) Timeline visualization control
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=3.0.0) 时间线显示控件
    /// </summary>
    public class TimelineIndicator : Box
    {
        [UI] DrawingArea draw;

        public TimelineIndicator() : this(new Builder("TimelineIndicator.glade"))
        {
            draw.Drawn += draw_Drawn;
        }

        public double? Lower
        {
            get { return lower; }
            set
            {
                lower = value;
                DrawBeat.CallerBegin(draw);
                draw.QueueDraw();
                DrawBeat.CallerEnd(draw);
            }
        }

        public double? Upper
        {
            get { return upper; }
            set
            {
                upper = value;
                DrawBeat.CallerBegin(draw);
                draw.QueueDraw();
                DrawBeat.CallerEnd(draw);
            }
        }

        public double? Value
        {
            get { return val; }
            set
            {
                val = value;
                DrawBeat.CallerBegin(draw);
                draw.QueueDraw();
                DrawBeat.CallerEnd(draw);
            }
        }

        private TimelineIndicator(Builder builder) : base(builder.GetRawOwnedObject("TimelineIndicator"))
        {
            builder.Autoconnect(this);
        }

        private void draw_Drawn(object o, DrawnArgs args)
        {
            DrawBeat.CallbackBegin(draw, "ASEva.UIGtk.TimelineIndicator");

            var cc = args.Cr;
            cc.LineWidth = 1;

            var drawWidth = draw.AllocatedWidth;
            var drawHeight = draw.AllocatedHeight;

            try
            {
                cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.WhiteSmoke));
                cc.Paint();

                var rangeColor = new ColorRGBA(ColorRGBA.Orange, 192);
                var valueColor = new ColorRGBA(ColorRGBA.Blue, 192);

                if (Lower != null && Upper != null)
                {
                    var lower = Math.Max(0, Math.Min(1, Lower.Value));
                    var upper = Math.Max(0, Math.Min(1, Upper.Value));
                    cc.SetSourceColor(ColorConv.ConvCairo(rangeColor));
                    cc.Rectangle(lower * drawWidth, 0, (upper - lower) * drawWidth, drawHeight);
                    cc.Fill();
                }

                if (Value != null)
                {
                    var value = Math.Max(0, Math.Min(1, Value.Value));
                    cc.SetSourceColor(ColorConv.ConvCairo(valueColor));
                    cc.Rectangle(value * drawWidth - 2, 0, 4, drawHeight);
                    cc.Fill();
                }

                cc.SetSourceColor(ColorConv.ConvCairo(ColorRGBA.DarkGray));
                cc.Rectangle(0, 0, drawWidth, drawHeight);
                cc.Stroke();
            }
            catch (Exception) {}

            DrawBeat.CallbackEnd(draw);
        }

        private double? lower, upper, val;
    }
}
