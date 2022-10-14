using System;
using System.Collections.Generic;
using Eto.Drawing;

namespace ASEva.UIEto
{
    class FontLibrary
    {
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

        private static Dictionary<String, Font> library = new Dictionary<String, Font>();
    }
}