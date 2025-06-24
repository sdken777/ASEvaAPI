using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.4.0) Application object using round corner simple style
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.4.0) 使用圆角简易样式的应用程序对象
    /// </summary>
    public partial class AvaloniaApplicationSimpleRoundTheme : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}