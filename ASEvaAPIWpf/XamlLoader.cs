using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace ASEva.UIWpf
{
    /// \~English
    /// <summary>
    /// (api:wpf=2.0.0) Load xaml dynamically
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:wpf=2.0.0) 动态加载xaml
    /// </summary>
    public class XamlLoader
    {
        public static void Load(object self, string baseUri)
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.Relative);
                var exprCa = (PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { resourceLocater });
                var stream = exprCa.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), resourceLocater);
                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };
                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { stream, parserContext, self, true });
            }
            catch (Exception)
            { }
        }
    }
}
