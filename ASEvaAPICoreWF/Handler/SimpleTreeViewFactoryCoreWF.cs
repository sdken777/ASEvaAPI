using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.WinForms;

namespace ASEva.UICoreWF
{
    class SimpleTreeViewFactoryCoreWF : SimpleTreeViewFactory
    {
        public void CreateSimpleTreeViewBackend(SimpleTreeViewCallback callback, out Control etoControl, out SimpleTreeViewBackend backend)
        {
            var view = new SimpleTreeViewBackendCoreWF(callback);
            etoControl = view.ToEto();
            backend = view;
        }
    }
}