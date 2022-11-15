using System;
using System.Collections.Generic;
using Eto.Drawing;
using SkiaSharp;

namespace ASEva.UIEto
{
    class FontLibrary
    {
        public static Font GetFont(Font defaultFont, float size)
        {
            FontFamily family = null;
            try { family = defaultFont.Family; }
            catch (Exception) {}

            if (family == null)
            {
                if (!libraryDefault.ContainsKey(size))
                {
                    try
                    {
                        var newFont = new Font(SystemFont.Default, size);
                        libraryDefault[size] = newFont;
                    }
                    catch (Exception)
                    {
                        libraryDefault[size] = null;
                    }
                }
                return libraryDefault[size];
            }
            else return GetFont(family, size, FontStyle.None, FontDecoration.None);
        }

        public static Font GetFont(FontFamily family, float size, FontStyle style, FontDecoration decoration)
        {
            var key = family.Name + ":" + size + ":" + style.ToString() + ":" + decoration.ToString();
            if (!library.ContainsKey(key))
            {
                try
                {
                    var newFont = new Font(family, size, style, decoration);
                    library[key] = newFont;
                }
                catch (Exception)
                {
                    library[key] = null;
                }
            }
            return library[key];
        }

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
                catch (Exception)
                {
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
                catch (Exception)
                {
                    skLibrary[key] = null;
                }
            }
            return skLibrary[key];
        }

        private static Dictionary<String, Font> library = new Dictionary<String, Font>();
        private static Dictionary<float, Font> libraryDefault = new Dictionary<float, Font>();

        private static SKFont skDefault = null;
        private static Dictionary<String, SKFont> skLibrary = new Dictionary<String, SKFont>();
        private static Dictionary<String, SKFont> skLibraryWithDefault = new Dictionary<String, SKFont>();
    }
}