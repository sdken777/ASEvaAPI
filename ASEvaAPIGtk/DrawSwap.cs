using System;
using Gtk;
using Cairo;
using ASEva.Utility;

namespace ASEva.UIGtk
{
    /// <summary>
    /// (api:gtk=2.0.8) 预绘制对象
    /// </summary>
    public class DrawSwap
    {
        /// <summary>
        /// 创建预绘制对象
        /// </summary>
        /// <param name="widget">绘图对象</param>
        /// <param name="category">类别，设为空表示不归类</param>
        public DrawSwap(Widget widget, String category)
        {
            this.widget = widget;
            this.category = category;
            widget.Drawn += widget_Drawn;
        }

        /// <summary>
        /// 刷新，其中会调用Paint
        /// </summary>
        public void Refresh()
        {
            if (widget == null || Paint == null) return;

            if (drawnRefreshed)
            {
                if (surface != null) surface.Dispose();
                surface = new Cairo.ImageSurface(Cairo.Format.Argb32, widget.AllocatedWidth, widget.AllocatedHeight);

                var cc = new Context(surface);
                Paint(this, cc);
                cc.Dispose();

                drawnRefreshed = false;
            }

            if (DrawBeat.CallerBegin(widget))
            {
                widget.QueueDraw();
                DrawBeat.CallerEnd(widget);
            }
        }

        /// <summary>
        /// 释放相关资源
        /// </summary>
        public void Close()
        {
            if (surface != null)
            {
                surface.Dispose();
                surface = null;
            }
            widget = null;
        }

        public delegate void PaintHandler(DrawSwap swap, Context cc);

        public event PaintHandler Paint;

        private void widget_Drawn(object o, DrawnArgs args)
        {
            if (widget == null || surface == null) return;

            DrawBeat.CallbackBegin(widget, category);

            args.Cr.SetSourceSurface(surface, 0, 0);
            args.Cr.Paint();
            drawnRefreshed = true;

            DrawBeat.CallbackEnd(widget);
        }

        private Widget widget;
        private String category;
        private ImageSurface surface;
        private bool drawnRefreshed = true;
    }
} 