using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class TextTableViewFactoryGtk : TextTableViewFactory
    {
        public void CreateTextTableViewBackend(TextTableViewCallback callback, out Control etoControl, out TextTableViewBackend backend)
        {
            var view = new TextTableViewBackendGtk(callback);
            etoControl = view.ToEto();
            backend = view;
        }
    }
}