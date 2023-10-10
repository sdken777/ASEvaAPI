using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class SimpleTreeViewFactoryGtk : SimpleTreeViewFactory
    {
        public void CreateSimpleTreeViewBackend(SimpleTreeViewCallback callback, out Control etoControl, out SimpleTreeViewBackend backend)
        {
            var view = new SimpleTreeViewBackendGtk(callback);
            etoControl = view.ToEto();
            backend = view;
        }
    }
}