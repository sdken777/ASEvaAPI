using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.2.0) Default application object for bundle mode
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.2.0) 默认的应用程序对象，用于bundle模式
    /// </summary>
    public partial class AvaloniaApplication : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}