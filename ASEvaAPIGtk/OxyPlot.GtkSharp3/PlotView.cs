// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotView.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents a control that displays a <see cref="PlotModel" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.GtkSharp
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Gdk;

    using Gtk;

    #pragma warning disable CS0612

    [Serializable]
    [System.ComponentModel.ToolboxItem(true)]
    partial class PlotView : Layout, IPlotView
    {
        private const string OxyPlotCategory = "OxyPlot";

        private readonly object invalidateLock = new object();

        private readonly object modelLock = new object();

        private readonly object renderingLock = new object();

        private readonly GraphicsRenderContext renderContext;

        [NonSerialized]
        private Gtk.Label trackerLabel = null;

        [NonSerialized]
        private PlotModel currentModel;

        private bool isModelInvalidated;

        private PlotModel model;

        private bool updateDataFlag = true;

        private OxyRect? zoomRectangle;

        private IPlotController defaultController;

        public PlotView() : base(null, null)
        {
            this.renderContext = new GraphicsRenderContext();

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.DoubleBuffered = true;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            this.PanCursor = new Cursor(CursorType.Hand1);
            this.ZoomRectangleCursor = new Cursor(CursorType.Sizing);
            this.ZoomHorizontalCursor = new Cursor(CursorType.SbHDoubleArrow);
            this.ZoomVerticalCursor = new Cursor(CursorType.SbVDoubleArrow);
            this.AddEvents((int)(EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterNotifyMask | EventMask.LeaveNotifyMask | EventMask.ScrollMask | EventMask.KeyPressMask | EventMask.PointerMotionMask));
            this.CanFocus = true;
        }

        [Browsable(false)]
        [DefaultValue(null)]
        [Category(OxyPlotCategory)]
        public PlotModel Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (this.model != value)
                {
                    this.model = value;
                    this.OnModelChanged();
                }
            }
        }

        Model IView.ActualModel
        {
            get
            {
                return this.Model;
            }
        }

        public PlotModel ActualModel
        {
            get
            {
                return this.Model;
            }
        }

        IController IView.ActualController
        {
            get
            {
                return this.ActualController;
            }
        }

        public OxyRect ClientArea
        {
            get
            {
                return new OxyRect(0, 0, Allocation.Width, Allocation.Height);
            }
        }

        public IPlotController ActualController
        {
            get
            {
                return this.Controller ?? (this.defaultController ?? (this.defaultController = new PlotController()));
            }
        }

        public IPlotController Controller { get; set; }

        [Category(OxyPlotCategory)]
        public Cursor PanCursor { get; set; }

        [Category(OxyPlotCategory)]
        public Cursor ZoomHorizontalCursor { get; set; }

        [Category(OxyPlotCategory)]
        public Cursor ZoomRectangleCursor { get; set; }

        [Category(OxyPlotCategory)]
        public Cursor ZoomVerticalCursor { get; set; }

        public void HideTracker()
        {
            if (this.trackerLabel != null)
                this.trackerLabel.Parent.Visible = false;
        }

        public void HideZoomRectangle()
        {
            this.zoomRectangle = null;
            this.QueueDraw();
        }

        public void InvalidatePlot(bool updateData)
        {
            lock (this.invalidateLock)
            {
                this.isModelInvalidated = true;
                this.updateDataFlag = this.updateDataFlag || updateData;
            }

            this.QueueDraw();
        }

        public void OnModelChanged()
        {
            lock (this.modelLock)
            {
                if (this.currentModel != null)
                {
                    ((IPlotModel)this.currentModel).AttachPlotView(null);
                    this.currentModel = null;
                }

                if (this.Model != null)
                {
                    ((IPlotModel)this.Model).AttachPlotView(this);
                    this.currentModel = this.Model;
                }
            }

            this.InvalidatePlot(true);
        }

        public void ShowTracker(TrackerHitResult data)
        {
            if (this.trackerLabel == null)
            {
                // Holding the tracker label inside an EventBox allows
                // us to set the background color
                Gtk.EventBox labelHolder = new Gtk.EventBox();
                this.trackerLabel = new Gtk.Label();
                this.trackerLabel.SetPadding(3, 3);
                OxyColor bgColor = OxyColors.LightSkyBlue;
                labelHolder.ModifyBg(StateType.Normal, new Gdk.Color(bgColor.R, bgColor.G, bgColor.B));
                labelHolder.Add(this.trackerLabel);
                this.Add(labelHolder);
                labelHolder.ShowAll();
            }
            this.trackerLabel.Parent.Visible = true;
            this.trackerLabel.Text = data.ToString();
            Gtk.Requisition req = this.trackerLabel.Parent.SizeRequest();
            int xPos = (int)data.Position.X - req.Width / 2;
            int yPos = (int)data.Position.Y - req.Height;
            xPos = Math.Max(0, Math.Min(xPos, this.Allocation.Width - req.Width));
            yPos = Math.Max(0, Math.Min(yPos, this.Allocation.Height - req.Height));
            this.Move(trackerLabel.Parent, xPos, yPos);
        }

        public void ShowZoomRectangle(OxyRect rectangle)
        {
            this.zoomRectangle = rectangle;
            this.QueueDraw();
        }

        public void SetClipboardText(string text)
        {
            try
            {
                // todo: can't get the following solution to work
                // http://stackoverflow.com/questions/5707990/requested-clipboard-operation-did-not-succeed
                this.GetClipboard(Gdk.Selection.Clipboard).Text = text;
            }
            catch (ExternalException)
            {
                // Requested Clipboard operation did not succeed.
                // MessageBox.Show(this, ee.Message, "OxyPlot");
            }
        }

        protected override bool OnButtonPressEvent(EventButton e)
        {
            this.GrabFocus();

            return this.ActualController.HandleMouseDown(this, e.ToMouseDownEventArgs());
        }

        protected override bool OnMotionNotifyEvent(EventMotion e)
        {
            return this.ActualController.HandleMouseMove(this, e.ToMouseEventArgs());
        }

        protected override bool OnButtonReleaseEvent(EventButton e)
        {
            return this.ActualController.HandleMouseUp(this, e.ToMouseUpEventArgs());
        }

        protected override bool OnScrollEvent(EventScroll e)
        {
            return this.ActualController.HandleMouseWheel(this, GetMouseWheelEventArgs(e));
        }

        protected override bool OnEnterNotifyEvent(EventCrossing e)
        {
            // If mouse has entered from an inferior window (ie the tracker label),
            // further propagation of the event could be dangerous; e.g. if it results in
            // the label being moved, it will cause further LeaveNotify and MotionNotify
            // events being fired under X11.
            if (e.Detail == NotifyType.Inferior)
                return base.OnEnterNotifyEvent(e);
            return this.ActualController.HandleMouseEnter(this, e.ToMouseEventArgs());
        }

        protected override bool OnLeaveNotifyEvent(EventCrossing e)
        {
            // If mouse has left via an inferior window (ie the tracker label),
            // further propagation of the event could be dangerous; e.g. if it results in
            // the label being moved, it will cause further LeaveNotify and MotionNotify
            // events being fired under X11.
            if (e.Detail == NotifyType.Inferior)
                return base.OnLeaveNotifyEvent(e);
            return this.ActualController.HandleMouseLeave(this, e.ToMouseEventArgs());
        }

        protected override bool OnKeyPressEvent(EventKey e)
        {
            return this.ActualController.HandleKeyDown(this, e.ToKeyEventArgs());
        }

        void DrawPlot (Cairo.Context cr)
        {
            try
            {
                lock (this.invalidateLock)
                {
                    if (this.isModelInvalidated)
                    {
                        if (this.model != null)
                        {
                            ((IPlotModel)this.model).Update(this.updateDataFlag);
                            this.updateDataFlag = false;
                        }

                        this.isModelInvalidated = false;
                    }
                }

                lock (this.renderingLock)
                {
                    EdgeRenderingMode edgeRenderingMode = EdgeRenderingMode.Automatic.GetActual(EdgeRenderingMode.Adaptive);
                    this.renderContext.SetGraphicsTarget(cr);
                    if (this.model != null)
                    {
                        OxyRect rect = new OxyRect(0, 0, Allocation.Width, Allocation.Height);
                        if (!this.model.Background.IsUndefined())
                        {
                            this.renderContext.DrawRectangle(rect, this.model.Background, OxyColors.Undefined, 0, edgeRenderingMode);
                        }

                        ((IPlotModel)this.model).Render(this.renderContext, rect);
                    }

                    if (this.zoomRectangle.HasValue)
                    {
                        this.renderContext.DrawRectangle(this.zoomRectangle.Value, OxyColor.FromArgb(0x40, 0xFF, 0xFF, 0x00), OxyColors.Transparent, 1.0, edgeRenderingMode);
                    }
                }
            }
            catch (Exception ex)
            {
                ASEva.Utility.Dump.Exception(ex);
            }
        }
    }
}
