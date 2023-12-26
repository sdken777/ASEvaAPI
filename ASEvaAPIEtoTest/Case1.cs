using System;
using Eto.Forms;
using ASEva.UIEto;

namespace ASEvaAPIEtoTest
{
    class Case1 : DialogPanel // ASEvaAPIEto 2.12.3.1解决
    {
        public Case1()
        {
            this.SetFixMode(500, 500, true);

            var listLayout = this.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);
            var functionList = listLayout.AddControl(new CheckableListBox(), true) as CheckableListBox;
            var pluginList = listLayout.AddControl(new CheckableListBox(), true) as CheckableListBox;

            functionList.AddItem("Click to trigger Gtk bug");
        }
    }
}