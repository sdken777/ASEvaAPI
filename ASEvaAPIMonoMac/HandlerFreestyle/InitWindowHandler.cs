using System;
using System.Linq;
using Eto.Forms;
using ASEva.UIEto;

namespace ASEva.UIMonoMac
{
    class InitWindowHandlerMonoMac : InitWindowHandler
    {
        public void InitWindow(Window window)
        {
            if (window.Menu != null) return;

            window.Menu = new MenuBar{ IncludeSystemItems = MenuBarSystemItems.None };

            var edit = new ButtonMenuItem();
            window.Menu.Items.Add(edit);

            var commandTable = window.Menu.SystemCommands.ToLookup(c => c.ID);
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_undo"].First()));
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_redo"].First()));
            edit.Items.Add(new SeparatorMenuItem());
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_cut"].First()));
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_copy"].First()));
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_paste"].First()));
            edit.Items.Add(new SeparatorMenuItem());
            edit.Items.Add(new ButtonMenuItem(commandTable["mac_selectAll"].First()));
        }
    }
}