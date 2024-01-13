using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Mac;

namespace ASEva.UIMonoMac
{
    class SimpleTreeViewFactoryMonoMac : SimpleTreeViewFactory
    {
        public void CreateSimpleTreeViewBackend(SimpleTreeViewCallback callback, out Control etoControl, out SimpleTreeViewBackend backend)
        {
            var view = new SimpleTreeViewBackendMonoMac(callback);
            etoControl = view.ToEto();
            backend = view;
        }
    }
}