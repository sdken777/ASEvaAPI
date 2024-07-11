using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.1.4) Application object using simple style
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.1.4) 使用简易样式的应用程序对象
    /// </summary>
    public partial class AvaloniaApplicationSimpleTheme : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}