using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.0) Default application object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.0) 默认的应用程序对象
    /// </summary>
    public partial class AvaloniaApplication : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}