using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using ASEva.Utility;

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
                var exprCa = typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, [resourceLocater]) as PackagePart;
                if (exprCa == null) return;
                var stream = exprCa.GetStream();
                var uriBase = typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null, null) as Uri;
                if (uriBase == null) return;
                var uri = new Uri(uriBase, resourceLocater);
                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };
                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, [stream, parserContext, self, true]);
            }
            catch (Exception ex)
            { Dump.Exception(ex); }
        }
    }
}
