namespace ASEva.UIWpf {
    using System;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ASEva.UIWpf.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static byte[] Extended_WPF_Toolkit__3_6_ {
            get {
                object obj = ResourceManager.GetObject("Extended_WPF_Toolkit__3_6_", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] SharpDX {
            get {
                object obj = ResourceManager.GetObject("SharpDX", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] WebView2 {
            get {
                object obj = ResourceManager.GetObject("WebView2", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
