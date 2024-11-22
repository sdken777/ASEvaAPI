using System;
using System.Collections.Generic;
using SkiaSharp;
using ASEva.Utility;

namespace ASEva.UIEto
{
    class FontLibrarySkia
    {
        public static SKFont GetSKFont()
        {
            if (skDefault == null) skDefault = new SKFont();
            return skDefault;
        }

        public static SKFont GetSKFont(String fontName)
        {
            var key = fontName;
            if (!skLibraryWithDefault.ContainsKey(key))
            {
                try
                {
                    var newFont = new SKFont(SKTypeface.FromFamilyName(fontName));
                    skLibraryWithDefault[key] = newFont;
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    skLibraryWithDefault[key] = null;
                }
            }
            return skLibraryWithDefault[key];
        }

        public static SKFont GetSKFont(String fontName, float size, SKFontStyleWeight weight, SKFontStyleWidth width, SKFontStyleSlant slant)
        {
            var key = fontName + ":" + size + ":" + weight.ToString() + ":" + width.ToString() + ":" + slant.ToString();
            if (!skLibrary.ContainsKey(key))
            {
                try
                {
                    var newFont = new SKFont(SKTypeface.FromFamilyName(fontName, weight, width, slant), size);
                    skLibrary[key] = newFont;
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    skLibrary[key] = null;
                }
            }
            return skLibrary[key];
        }

        private static SKFont skDefault = null;
        private static Dictionary<String, SKFont> skLibrary = new Dictionary<String, SKFont>();
        private static Dictionary<String, SKFont> skLibraryWithDefault = new Dictionary<String, SKFont>();
    }
}