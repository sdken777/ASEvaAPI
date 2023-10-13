using System;
using System.Collections.Generic;
using Gtk;

namespace ASEva.UIGtk
{
    /// \~English
    /// <summary>
    /// (api:gtk=2.0.0) Extension methods for flow box
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) Flow框扩展方法
    /// </summary>
    public static class FlowBoxExtension
    {
        public static Widget GetItemAt(this FlowBox flowBox, int index)
        {
            foreach (FlowBoxChild c in flowBox.Children)
            {
                if (c.Index == index) return c.Child;
            }
            return null;
        }

        public static Widget[] GetItems(this FlowBox flowBox)
        {
            var list = new List<Widget>();
            foreach (FlowBoxChild c in flowBox.Children)
            {
                list.Add(c.Child);
            }
            return list.ToArray();
        }

        public static int GetItemCount(this FlowBox flowBox)
        {
            return flowBox.Children.Length;
        }

        public static void RemoveAt(this FlowBox flowBox, int index)
        {
            foreach (FlowBoxChild c in flowBox.Children)
            {
                if (c.Index == index)
                {
                    flowBox.Remove(c);
                    return;
                }
            }
        }

        public static void RemoveItem(this FlowBox flowBox, Widget item)
        {
            if (item == null) return;
            foreach (FlowBoxChild c in flowBox.Children)
            {
                if (c.Child.Equals(item))
                {
                    flowBox.Remove(c);
                    c.Remove(item);
                    return;
                }
            }
        }

        public static void RemoveAll(this FlowBox flowBox)
        {
            foreach (FlowBoxChild c in flowBox.Children)
            {
                flowBox.Remove(c);
            }
        }
    }
}