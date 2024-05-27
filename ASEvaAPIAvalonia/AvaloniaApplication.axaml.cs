using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.0) For opening designer in project
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.0) 用于在项目中打开设计器
    /// </summary>
    public partial class AvaloniaApplication : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}