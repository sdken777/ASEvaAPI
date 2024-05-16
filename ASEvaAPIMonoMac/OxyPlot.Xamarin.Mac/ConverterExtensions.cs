// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterExtensions.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides extension methods that converts between MonoTouch and OxyPlot types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Xamarin.Mac
{
    using System;

    using MonoMac.AppKit;
    using MonoMac.CoreGraphics;
 
    using OxyPlot;

    static class ConverterExtensions
    {
        public static ScreenPoint ToScreenPoint (this CGPoint p)
        {
            return new ScreenPoint (p.X, p.Y);
        }

        public static CGColor ToCGColor (this OxyColor c)
        {
            return new CGColor (c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
        }

        public static CGLineJoin Convert (this LineJoin lineJoin)
        {
            switch (lineJoin) {
            case LineJoin.Bevel:
                return CGLineJoin.Bevel;
            case LineJoin.Miter:
                return CGLineJoin.Miter;
            case LineJoin.Round:
                return CGLineJoin.Round;
            default:
                throw new InvalidOperationException ("Invalid join type.");
            }
        }

        public static CGPoint Convert (this ScreenPoint p)
        {
            return new CGPoint ((float)p.X, (float)p.Y);
        }

        public static CGPoint ConvertAliased (this ScreenPoint p)
        {
            return new CGPoint (0.5f + (float)Math.Round (p.X), 0.5f + (float)Math.Round (p.Y));
        }

        public static CGRect ConvertAliased (this OxyRect rect)
        {
            float x = 0.5f + (float)Math.Round (rect.Left);
            float y = 0.5f + (float)Math.Round (rect.Top);
            float w = 0.5f + (float)Math.Round (rect.Right) - x;
            float h = 0.5f + (float)Math.Round (rect.Bottom) - y;
            return new CGRect (x, y, w, h);
        }

        public static CGRect Convert (this OxyRect rect)
        {
            return new CGRect ((float)rect.Left, (float)rect.Top, (float)(rect.Right - rect.Left), (float)(rect.Bottom - rect.Top));
        }

        public static OxyMouseButton ToButton (this NSEventType theType)
        {
            switch (theType) {
            case NSEventType.LeftMouseDown:
                return OxyMouseButton.Left;
            case NSEventType.RightMouseDown:
                return OxyMouseButton.Right;
            case NSEventType.OtherMouseDown:
                return OxyMouseButton.Middle;
            default:
                return OxyMouseButton.None;
            }
        }

        public static OxyModifierKeys ToModifierKeys (this NSEventModifierMask theMask)
        {
            var keys = OxyModifierKeys.None;
            if ((theMask & NSEventModifierMask.ShiftKeyMask) == NSEventModifierMask.ShiftKeyMask)
                keys |= OxyModifierKeys.Shift;
            if ((theMask & NSEventModifierMask.ControlKeyMask) == NSEventModifierMask.ControlKeyMask)
                keys |= OxyModifierKeys.Control;
            if ((theMask & NSEventModifierMask.AlternateKeyMask) == NSEventModifierMask.AlternateKeyMask)
                keys |= OxyModifierKeys.Alt;

            // TODO
            if ((theMask & NSEventModifierMask.CommandKeyMask) == NSEventModifierMask.CommandKeyMask)
                keys |= OxyModifierKeys.Control;

            return keys;
        }

        public static OxyKey ToKey (this ushort keycode)
        {
            // TODO
            return OxyKey.A;
        }
    }
}