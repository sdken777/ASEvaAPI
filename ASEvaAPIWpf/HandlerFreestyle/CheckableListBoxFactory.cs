using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIWpf
{
    class CheckableListBoxFactoryWpf : CheckableListBoxFactory
    {
        public void CreateCheckableListBoxBackend(CheckableListBoxCallback callback, out Eto.Forms.Control etoControl, out CheckableListBoxBackend backend)
        {
            var panel = new CheckableListBoxBackendWpf(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }
}
