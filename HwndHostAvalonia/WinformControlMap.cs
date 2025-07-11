using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HwndHostAvalonia
{
    class WinformControlMap
    {
        public static void Add(AvaloniaEmbedder embedder, UserControl control)
        {
            map.Add(embedder, control);
        }

        public static void Remove(AvaloniaEmbedder embedder)
        {
            if (map.ContainsKey(embedder)) map.Remove(embedder);
        }

        public static UserControl Get(AvaloniaEmbedder embedder)
        {
            if (map.ContainsKey(embedder)) return map[embedder];
            return null;
        }

        private static Dictionary<AvaloniaEmbedder, UserControl> map = new();
    }
}