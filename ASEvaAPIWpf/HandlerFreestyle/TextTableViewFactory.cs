using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UIWpf
{
    class TextTableViewFactoryWpf : TextTableViewFactory
    {
        public void CreateTextTableViewBackend(TextTableViewCallback callback, out Control etoControl, out TextTableViewBackend backend)
        {
            var panel = new TextTableViewBackendWpf(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }
}
