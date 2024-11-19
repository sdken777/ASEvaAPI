using System;
using System.Collections.Generic;
using Eto.Drawing;
using ASEva.Utility;

namespace ASEva.UIEto
{
    class FontLibraryEto
    {
        public static Font? GetFont(Font defaultFont, float size)
        {
            FontFamily? family = null;
            if (!FontLibraryOption.EtoSkipGetFamily)
            {
                try { family = defaultFont.Family; }
                catch (Exception ex) { Dump.Exception(ex); }
            }

            if (family == null)
            {
                if (!libraryDefault.ContainsKey(size))
                {
                    try
                    {
                        var newFont = new Font(SystemFont.Default, size);
                        libraryDefault[size] = newFont;
                    }
                    catch (Exception ex)
                    {
                        Dump.Exception(ex);
                        libraryDefault[size] = null;
                    }
                }
                return libraryDefault[size];
            }
            else return GetFont(family, size, FontStyle.None, FontDecoration.None);
        }

        public static Font? GetFont(FontFamily family, float size, FontStyle style, FontDecoration decoration)
        {
            var key = family.Name + ":" + size + ":" + style.ToString() + ":" + decoration.ToString();
            if (!library.ContainsKey(key))
            {
                try
                {
                    var newFont = new Font(family, size, style, decoration);
                    library[key] = newFont;
                }
                catch (Exception ex)
                {
                    Dump.Exception(ex);
                    library[key] = null;
                }
            }
            return library[key];
        }

        private static Dictionary<String, Font?> library = [];
        private static Dictionary<float, Font?> libraryDefault = [];
    }

    public class FontLibraryOption
    {
        public static bool EtoSkipGetFamily { get; set; }
    }
}