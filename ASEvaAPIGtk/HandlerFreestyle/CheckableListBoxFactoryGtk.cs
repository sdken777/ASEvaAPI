using System;
using ASEva.UIEto;
using Eto.Forms;
using Eto.GtkSharp;

namespace ASEva.UIGtk
{
    class CheckableListBoxFactoryGtk : CheckableListBoxFactory
    {
        public void CreateCheckableListBoxBackend(CheckableListBoxCallback callback, out Control etoControl, out CheckableListBoxBackend backend)
        {
            var view = new CheckableListBoxBackendGtk(callback);
            etoControl = view.ToEto();
            backend = view;
        }
    }
}