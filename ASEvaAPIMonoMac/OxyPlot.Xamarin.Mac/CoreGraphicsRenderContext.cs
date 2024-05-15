// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreGraphicsRenderContext.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Implements a <see cref="IRenderContext"/> for CoreGraphics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Xamarin.Mac
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MonoMac.AppKit;
    using MonoMac.CoreGraphics;
    using MonoMac.CoreText;
    using MonoMac.Foundation;
    using nfloat = System.Double;

    class CoreGraphicsRenderContext : ClippingRenderContext, IDisposable
    {
        private readonly HashSet<OxyImage> imagesInUse = new HashSet<OxyImage> ();

        private readonly Dictionary<string, CTFont> fonts = new Dictionary<string, CTFont> ();

        private readonly Dictionary<OxyImage, NSImage> imageCache = new Dictionary<OxyImage, NSImage> ();

        private readonly CGContext gctx;

        public CoreGraphicsRenderContext (CGContext context)
        {
            this.gctx = context;

            // Set rendering quality
            this.gctx.SetAllowsFontSmoothing (true);
            this.gctx.SetAllowsFontSubpixelQuantization (true);
            this.gctx.SetAllowsAntialiasing (true);
            this.gctx.SetShouldSmoothFonts (true);
            this.gctx.SetShouldAntialias (true);
            this.gctx.InterpolationQuality = CGInterpolationQuality.High;
            this.gctx.SetTextDrawingMode (CGTextDrawingMode.Fill);
            this.gctx.TextMatrix = CGAffineTransform.MakeScale (1, 1);
        }

        public override void DrawEllipse (OxyRect rect, OxyColor fill, OxyColor stroke, double thickness, EdgeRenderingMode erm)
        {
            bool aliased = !this.ShouldUseAntiAliasingForEllipse(erm);
            this.SetAlias (aliased);
            var convertedRectangle = aliased ? rect.ConvertAliased() : rect.Convert ();
            if (fill.IsVisible ()) {
                this.SetFill (fill);
                using (var path = new CGPath ()) {
                    path.AddEllipseInRect (convertedRectangle);
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Fill);
            }

            if (stroke.IsVisible () && thickness > 0) {
                this.SetStroke (stroke, thickness);

                using (var path = new CGPath ()) {
                    path.AddEllipseInRect (convertedRectangle);
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Stroke);
            }
        }

        public override void DrawImage (OxyImage source, double srcX, double srcY, double srcWidth, double srcHeight, double destX, double destY, double destWidth, double destHeight, double opacity, bool interpolate)
        {
            var image = this.GetImage (source);
            if (image == null) {
                return;
            }

            this.gctx.SaveState ();

            double x = destX - (srcX / srcWidth * destWidth);
            double y = destY - (srcY / srcHeight * destHeight);
            this.gctx.ScaleCTM (1, -1);
            this.gctx.TranslateCTM ((float)x, -(float)(y + destHeight));
            this.gctx.SetAlpha ((float)opacity);
            this.gctx.InterpolationQuality = interpolate ? CGInterpolationQuality.High : CGInterpolationQuality.None;
            var destRect = new CGRect (0f, 0f, (float)destWidth, (float)destHeight);
            this.gctx.DrawImage (destRect, image.CGImage);
            this.gctx.RestoreState ();
        }

        public override void CleanUp ()
        {
            var imagesToRelease = this.imageCache.Keys.Where (i => !this.imagesInUse.Contains (i)).ToList ();
            foreach (var i in imagesToRelease) {
                var image = this.GetImage (i);
                image.Dispose ();
                this.imageCache.Remove (i);
            }

            this.imagesInUse.Clear ();
        }

        protected override void SetClip (OxyRect rect)
        {
            this.gctx.SaveState ();
            this.gctx.ClipToRect (rect.Convert ());
        }

        protected override void ResetClip ()
        {
            this.gctx.RestoreState ();
        }

        public override void DrawLine (IList<ScreenPoint> points, OxyColor stroke, double thickness, EdgeRenderingMode erm, double[] dashArray, LineJoin lineJoin)
        {
            if (stroke.IsVisible () && thickness > 0) {
                bool aliased = !this.ShouldUseAntiAliasingForLine(erm, points);
                this.SetAlias (aliased);
                this.SetStroke (stroke, thickness, dashArray, lineJoin);

                using (var path = new CGPath ()) {
                    var convertedPoints = (aliased ? points.Select (p => p.ConvertAliased ()) : points.Select (p => p.Convert ())).ToArray ();
                    path.AddLines (convertedPoints);
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Stroke);
            }
        }

        public override void DrawPolygon (IList<ScreenPoint> points, OxyColor fill, OxyColor stroke, double thickness, EdgeRenderingMode erm, double[] dashArray, LineJoin lineJoin)
        {
            bool aliased = !this.ShouldUseAntiAliasingForLine(erm, points);
            this.SetAlias (aliased);
            var convertedPoints = (aliased ? points.Select (p => p.ConvertAliased ()) : points.Select (p => p.Convert ())).ToArray ();
            if (fill.IsVisible ()) {
                this.SetFill (fill);
                using (var path = new CGPath ()) {
                    path.AddLines (convertedPoints);
                    path.CloseSubpath ();
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Fill);
            }

            if (stroke.IsVisible () && thickness > 0) {
                this.SetStroke (stroke, thickness, dashArray, lineJoin);

                using (var path = new CGPath ()) {
                    path.AddLines (convertedPoints);
                    path.CloseSubpath ();
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Stroke);
            }
        }

        public override void DrawRectangle (OxyRect rect, OxyColor fill, OxyColor stroke, double thickness, EdgeRenderingMode erm)
        {
            bool aliased = !this.ShouldUseAntiAliasingForRect(erm);
            this.SetAlias (aliased);
            var convertedRect = aliased ? rect.ConvertAliased () : rect.Convert();

            if (fill.IsVisible ()) {
                this.SetFill (fill);
                using (var path = new CGPath ()) {
                    path.AddRect (convertedRect);
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Fill);
            }

            if (stroke.IsVisible () && thickness > 0) {
                this.SetStroke (stroke, thickness);

                using (var path = new CGPath ()) {
                    path.AddRect (convertedRect);
                    this.gctx.AddPath (path);
                }

                this.gctx.DrawPath (CGPathDrawingMode.Stroke);
            }
        }

        public override void DrawText (ScreenPoint p, string text, OxyColor fill, string fontFamily, double fontSize, double fontWeight, double rotate, HorizontalAlignment halign, VerticalAlignment valign, OxySize? maxSize)
        {
            if (string.IsNullOrEmpty (text)) {
                return;
            }

            var fontName = GetActualFontName (fontFamily, fontWeight);
            var font = this.GetCachedFont (fontName, fontSize);
            this.GetFontMetrics(font, out nfloat lineHeight, out nfloat delta);

            var rows = text.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = rows[i].TrimEnd('\r');
            }

            for (int row = 0; row < rows.Length; row++)
            {
                using (var attributedString = new NSAttributedString (rows[row], new CTStringAttributes {
                    ForegroundColorFromContext = true,
                    Font = font
                })) {
                    using (var textLine = new CTLine (attributedString)) {
                        nfloat width;
                        nfloat height;

                        this.gctx.TextPosition = new CGPoint (0, 0);

                        var bounds = textLine.GetImageBounds (this.gctx);

                        var x0 = 0;
                        var y0 = delta;

                        if (maxSize.HasValue || halign != HorizontalAlignment.Left || valign != VerticalAlignment.Bottom) {
                            width = bounds.Left + bounds.Width;
                            height = lineHeight * rows.Length;
                        } else {
                            width = height = 0f;
                        }

                        if (maxSize.HasValue) {
                            if (width > maxSize.Value.Width) {
                                width = (float)maxSize.Value.Width;
                            }

                            if (height > maxSize.Value.Height) {
                                height = (float)maxSize.Value.Height;
                            }
                        }

                        var dx = halign == HorizontalAlignment.Left ? 0d : (halign == HorizontalAlignment.Center ? -width * 0.5 : -width);
                        var dy = valign == VerticalAlignment.Bottom ? 0d : (valign == VerticalAlignment.Middle ? height * 0.5 : height);
                        dy -= (rows.Length - row - 1) * height / rows.Length;

                        this.SetFill (fill);
                        this.SetAlias (false);

                        this.gctx.SaveState ();
                        this.gctx.TranslateCTM ((float)p.X, (float)p.Y);
                        if (!rotate.Equals (0)) {
                            this.gctx.RotateCTM ((float)(rotate / 180 * Math.PI));
                        }

                        this.gctx.TranslateCTM ((float)dx + x0, (float)dy + y0);
                        this.gctx.ScaleCTM (1f, -1f);

                        if (maxSize.HasValue) {
                            var clipRect = new CGRect (-x0, y0, (float)Math.Ceiling (width), (float)Math.Ceiling (height / rows.Length));
                            this.gctx.ClipToRect (clipRect);
                        }

                        textLine.Draw (this.gctx);

                        this.gctx.RestoreState ();
                    }
                }
            }
        }

        public override OxySize MeasureText (string text, string fontFamily, double fontSize, double fontWeight)
        {
            if (string.IsNullOrEmpty (text) || fontFamily == null) {
                return OxySize.Empty;
            }

            var fontName = GetActualFontName (fontFamily, fontWeight);
            var font = this.GetCachedFont (fontName, (float)fontSize);
            this.GetFontMetrics(font, out nfloat lineHeight, out nfloat delta);

            var rows = text.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = rows[i].TrimEnd('\r');
            }

            nfloat maxWidth = 0;
            for (int row = 0; row < rows.Length; row++)
            {
                using (var attributedString = new NSAttributedString (rows[row], new CTStringAttributes {
                    ForegroundColorFromContext = true,
                    Font = font
                })) {
                    using (var textLine = new CTLine (attributedString)) {
                        // the text position must be set to get the correct bounds
                        this.gctx.TextPosition = new CGPoint (0, 0);

                        var bounds = textLine.GetImageBounds (this.gctx);
                        var width = bounds.Left + bounds.Width;
                        maxWidth = Math.Max(maxWidth, width);
                    }
                }
            }
            return new OxySize (maxWidth, lineHeight * rows.Length);
        }

        public void Dispose ()
        {
            foreach (var image in this.imageCache.Values) {
                image.Dispose ();
            }

            foreach (var font in this.fonts.Values) {
                font.Dispose ();
            }
        }

        private static string GetActualFontName (string fontFamily, double fontWeight)
        {
            string fontName;
            switch (fontFamily) {
            case null:
            case "Segoe UI":
                fontName = "HelveticaNeue";
                break;
            case "Arial":
                fontName = "ArialMT";
                break;
            case "Times":
            case "Times New Roman":
                fontName = "TimesNewRomanPSMT";
                break;
            case "Courier New":
                fontName = "CourierNewPSMT";
                break;
            default:
                fontName = fontFamily;
                break;
            }

            if (fontWeight >= 700) {
                fontName += "-Bold";
            }

            return fontName;
        }

        private void GetFontMetrics (CTFont font, out nfloat defaultLineHeight, out nfloat delta)
        {
            var ascent = font.AscentMetric;
            var descent = font.DescentMetric;
            var leading = font.LeadingMetric;

            //// http://stackoverflow.com/questions/5511830/how-does-line-spacing-work-in-core-text-and-why-is-it-different-from-nslayoutm

            leading = leading < 0 ? 0 : (float)Math.Floor (leading + 0.5f);
            var lineHeight = (nfloat)Math.Floor (ascent + 0.5f) + (nfloat)Math.Floor (descent + 0.5) + leading;
            var ascenderDelta = leading >= 0 ? 0 : (nfloat)Math.Floor ((0.2 * lineHeight) + 0.5);
            defaultLineHeight = lineHeight + ascenderDelta;
            delta = ascenderDelta - descent;
        }

        private CTFont GetCachedFont (string fontName, double fontSize)
        {
            var key = fontName + fontSize.ToString ("0.###");
            if (this.fonts.TryGetValue(key, out CTFont font))
            {
                return font;
            }

            return this.fonts [key] = new CTFont (fontName, (nfloat)fontSize);
        }

        private void SetAlias (bool alias)
        {
            this.gctx.SetShouldAntialias (!alias);
        }

        private void SetFill (OxyColor c)
        {
            this.gctx.SetFillColor (c.ToCGColor ());
        }

        private void SetStroke (OxyColor c, double thickness, double[] dashArray = null, LineJoin lineJoin = LineJoin.Miter)
        {
            this.gctx.SetStrokeColor (c.ToCGColor ());
            this.gctx.SetLineWidth ((float)thickness);
            this.gctx.SetLineJoin (lineJoin.Convert ());
            if (dashArray != null) {
                var lengths = dashArray.Select (d => (nfloat)d).ToArray ();
                this.gctx.SetLineDash (0f, lengths);
            } else {
                this.gctx.SetLineDash (0, new nfloat[0]);
            }
        }

        private NSImage GetImage (OxyImage source)
        {
            if (source == null) {
                return null;
            }

            if (!this.imagesInUse.Contains (source)) {
                this.imagesInUse.Add (source);
            }

            if (!this.imageCache.TryGetValue(source, out NSImage src))
            {
                using (var ms = new System.IO.MemoryStream(source.GetData()))
                {
                    src = NSImage.FromStream(ms);
                }

                if (src != null)
                {
                    this.imageCache.Add(source, src);
                }
            }

            return src;
        }
    }
}