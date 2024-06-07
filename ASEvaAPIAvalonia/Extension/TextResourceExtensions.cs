using System;
using ASEva.Utility;
using Avalonia;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.3) Extensions for adding text resource
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.3) 方便添加文本资源的扩展
    /// </summary>
    public static class TextResourceExtensions
    {
        public static void AddToResources(this StyledElement elem, TextResource texts)
        {
            if (texts != null)
            {
                foreach (var id in texts.IDs)
                {
                    elem.Resources.Add(id, texts[id]);
                }
            }
        }
    }
}