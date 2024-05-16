// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotView.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides a view that can show a <see cref="PlotModel" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Xamarin.Mac
{
    using MonoMac.AppKit;
    using MonoMac.Foundation;

    using OxyPlot;

    [Register("PlotView")]
    class PlotView : NSView, IPlotView
    {
        private PlotModel model;

        private IPlotController defaultController;

        public PlotView()
        {
            this.Initialize ();
        }

        public PlotView(MonoMac.CoreGraphics.CGRect frame) : base(frame)
        {
            this.Initialize ();
        }

        [Export ("initWithCoder:")]
        public PlotView(NSCoder coder) : base (coder)
        {
            this.Initialize ();
        }

        [Export ("requiresConstraintBasedLayout")]
        bool UseNewLayout ()
        {
            return true;
        }

        private void Initialize() {
            this.WantsLayer = true;
        }

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
                    if (this.model != null)
                    {
                        ((IPlotModel)this.model).AttachPlotView(null);
                        this.model = null;
                    }

                    if (value != null)
                    {
                        ((IPlotModel)value).AttachPlotView(this);
                        this.model = value;
                    }
					
                    this.InvalidatePlot();
                }
            }
        }

        public IPlotController Controller { get; set; }

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
                // TODO
                return new OxyRect(0, 0, 100, 100);
            }
        }

        public IPlotController ActualController
        {
            get
            {
                return this.Controller ?? (this.defaultController ?? (this.defaultController = CreateDefaultController()));
            }
        }

        private PlotController CreateDefaultController(){
            var c = new PlotController ();
            c.UnbindMouseDown (OxyMouseButton.Left);
            c.BindMouseDown (OxyMouseButton.Left, PlotCommands.PanAt);
            return c;
        }

        public void HideTracker()
        {
        }

        public void HideZoomRectangle()
        {
        }

        public void InvalidatePlot(bool updateData = true)
        {
            var actualModel = this.model;
            if (actualModel != null)
            {
                // TODO: update the model on a background thread
                ((IPlotModel)actualModel).Update(updateData);
            }

            if (actualModel != null && !actualModel.Background.IsUndefined())
            {
                this.WantsLayer = true;
                this.Layer.BackgroundColor = actualModel.Background.ToCGColor();
            }
            else
            {
                // Use white as default background color
                this.Layer.BackgroundColor = OxyColors.White.ToCGColor();
            }

            this.NeedsDisplay = true;
        }

        public void SetCursorType(CursorType cursorType)
        {
            this.ResetCursorRects ();
            var cursor = Convert (cursorType);
            if (cursor!=null)
            this.AddCursorRect (this.Bounds, cursor);
        }

        public static NSCursor Convert(CursorType cursorType){
            switch (cursorType) {
            case CursorType.Default:
                return null;
            case CursorType.Pan:
                return NSCursor.PointingHandCursor;
            case CursorType.ZoomHorizontal:
                return NSCursor.ResizeUpDownCursor;
            case CursorType.ZoomVertical:
                return NSCursor.ResizeLeftRightCursor;
            case CursorType.ZoomRectangle:
                return NSCursor.CrosshairCursor;
            default:
                return null;
            }
        }

        public void ShowTracker(TrackerHitResult trackerHitResult)
        {
            // TODO
        }

        public void ShowZoomRectangle(OxyRect rectangle)
        {
            // TODO
        }

        public void SetClipboardText(string text)
        {
            // TODO
            // UIPasteboard.General.SetValue(new NSString(text), "public.utf8-plain-text");
        }

        public override void DrawRect(MonoMac.CoreGraphics.CGRect dirtyRect)
        {
            if (this.model != null)
            {
                var context = NSGraphicsContext.CurrentContext.GraphicsPort;
                context.TranslateCTM(0f, dirtyRect.Height);
                context.ScaleCTM(1f, -1f);
                // TODO: scale font matrix??
                using (var renderer = new CoreGraphicsRenderContext(context))
                {
                    OxyRect orect = new OxyRect(0, 0, dirtyRect.Width, dirtyRect.Height);
                    ((IPlotModel)this.model).Render(renderer, orect);
                }
            }
        }

        public override void MouseDown (NSEvent theEvent)
        {
            base.MouseDown (theEvent);
            this.ActualController.HandleMouseDown (this, this.ToMouseDownEventArgs (theEvent));
        }

        public override void MouseDragged (NSEvent theEvent)
        {
            base.MouseDragged (theEvent);
            this.ActualController.HandleMouseMove (this, this.ToMouseEventArgs (theEvent));
        }

        public override void MouseMoved (NSEvent theEvent)
        {
            base.MouseMoved (theEvent);
            this.ActualController.HandleMouseMove (this, this.ToMouseEventArgs (theEvent));
        }

        public override void MouseUp (NSEvent theEvent)
        {
            base.MouseUp (theEvent);
            this.ActualController.HandleMouseUp (this, this.ToMouseEventArgs (theEvent));
        }

        public override void MouseEntered (NSEvent theEvent)
        {
            base.MouseEntered (theEvent);
            this.ActualController.HandleMouseEnter (this, this.ToMouseEventArgs (theEvent));
        }

        public override void MouseExited (NSEvent theEvent)
        {
            base.MouseExited (theEvent);
            this.ActualController.HandleMouseLeave (this, this.ToMouseEventArgs (theEvent));
        }

        public override void ScrollWheel (NSEvent theEvent)
        {
            // TODO: use scroll events to pan?
            base.ScrollWheel (theEvent);
            this.ActualController.HandleMouseWheel (this, this.ToMouseWheelEventArgs (theEvent));
        }

        public override void OtherMouseDown (NSEvent theEvent)
        {
            base.OtherMouseDown (theEvent);
        }

        public override void RightMouseDown (NSEvent theEvent)
        {
            base.RightMouseDown (theEvent);
        }

        public override void KeyDown (NSEvent theEvent)
        {
            base.KeyDown (theEvent);
            this.ActualController.HandleKeyDown (this, this.ToKeyEventArgs(theEvent));
        }

        public override void TouchesBeganWithEvent (NSEvent theEvent)
        {
            base.TouchesBeganWithEvent (theEvent);
        }

        public override void MagnifyWithEvent (NSEvent theEvent)
        {
            base.MagnifyWithEvent (theEvent);
            // TODO: handle pinch event
            // https://developer.apple.com/library/mac/documentation/cocoa/conceptual/eventoverview/HandlingTouchEvents/HandlingTouchEvents.html
        }

        public override void SmartMagnify (NSEvent withEvent)
        {
            base.SmartMagnify (withEvent);
        }

        public override void SwipeWithEvent (NSEvent theEvent)
        {
            base.SwipeWithEvent (theEvent);
        }

        private OxyMouseDownEventArgs ToMouseDownEventArgs (NSEvent theEvent)
        {
            // https://developer.apple.com/library/mac/documentation/Cocoa/Reference/ApplicationKit/Classes/NSEvent_Class/Reference/Reference.html
            return new OxyMouseDownEventArgs {
                Position = this.GetRelativePosition(theEvent),
                ChangedButton = theEvent.Type.ToButton (),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys (),
                ClickCount = (int)theEvent.ClickCount
            };
        }

        private OxyMouseEventArgs ToMouseEventArgs (NSEvent theEvent)
        {
            return new OxyMouseEventArgs {
                Position = this.GetRelativePosition(theEvent),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        private OxyMouseWheelEventArgs ToMouseWheelEventArgs (NSEvent theEvent)
        {
            return new OxyMouseWheelEventArgs {
                Delta = (int)theEvent.ScrollingDeltaY,
                Position = this.GetRelativePosition(theEvent),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        private OxyKeyEventArgs ToKeyEventArgs (NSEvent theEvent)
        {
            return new OxyKeyEventArgs {
                Key = theEvent.KeyCode.ToKey (),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        private ScreenPoint GetRelativePosition (NSEvent p)
        {
            // OSX has the origin in the lower left corner
            var relativePoint = this.ConvertPointFromView (p.LocationInWindow, null);
            var y = this.Bounds.Height - relativePoint.Y;
            return new ScreenPoint (relativePoint.X, y);
        }
    }
}