using System;
using Gtk;
using ASEva;
using ASEva.Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System.Collections.Generic;

namespace ASEvaAPIGtkTest
{
    #pragma warning disable CS0612, CS0649
    class ASEvaAPIGtkTest : Window
    {
        [UI] Box boxTimelineIndicator, boxLinkItem;
        [UI] Label info, info2;
        [UI] EventBox eventBox;
        [UI] Menu menu;
        [UI] Label labelColor;
        [UI] ComboBoxText comboBox;
        [UI] Switch switchButton;
        [UI] Scale scale1, scale3;
        [UI] FlowBox flowBox;
        [UI] ListBox listBox;
        [UI] TreeView tableView;
        [UI] ListStore tableStore;
        [UI] Button buttonLabelColorReset, buttonComboBoxQuery, buttonComboBoxModify, buttonSwitch, buttonFlowBoxAdd, buttonFlowBoxRemove, buttonFlowBoxQuery, buttonListBoxAdd, buttonListBoxRemove, buttonListBoxQuery, buttonTableViewModify, buttonTableViewQuery, buttonTimelineIndicatorDraw, buttonDisableLink, buttonEnableLink;

        LinkItem linkItem = new LinkItem() { Text = "Link item", ForeColor = ColorRGBA.Green };
        EventBoxHelper eventBoxHelper = new EventBoxHelper();
        SwitchHelper switchHelper = new SwitchHelper();
        TimelineIndicator timelineIndicator = new TimelineIndicator();

        public ASEvaAPIGtkTest() : this(new Builder("ASEvaAPIGtkTest.glade"))
        {
            DialogHelper.MainWindow = this;

            eventBox.SetBackColor(ColorRGBA.LightSteelBlue);
            eventBoxHelper.Add(eventBox, menu, true);

            labelColor.SetForeColor(ColorRGBA.LimeGreen);
            labelColor.SetBackColor(ColorRGBA.LightYellow);

            comboBox.Active = 0;

            switchHelper.Add(switchButton);

            ScrollDisabler.Disable(scale1);
            ScrollDisabler.Disable(scale3);

            var editableRenderer = new CellRendererText() { Editable = true };
            tableView.SetColumnRenderer(0, new CellRendererText(), "text", 0, "foreground", 1);
            tableView.SetColumnRenderer(1, editableRenderer, "text", 2, "foreground", 3);
            tableStore.AppendValues("Weather", "black", "Sunny", "green");
            tableStore.AppendValues("Road type", "black", "Highway", "blue");

            boxTimelineIndicator.PackStart(timelineIndicator, true, true, 0);

            boxLinkItem.PackStart(linkItem, false, false, 0);

            DeleteEvent += window_DeleteEvent;

            eventBoxHelper.LeftDown += delegate { info.Text = "Left down"; };
            eventBoxHelper.LeftMoved += delegate { info.Text = "Left moved"; };
            eventBoxHelper.LeftUp += delegate { info.Text = "Left up"; };
            eventBoxHelper.LeftDoubleClicked += delegate { info2.Text = "Left double"; };
            eventBoxHelper.RightDown += delegate { info.Text = "Right down"; };
            eventBoxHelper.RightMoved += delegate { info.Text = "Right moved"; };
            eventBoxHelper.RightUp += delegate { info.Text = "Right up"; };
            eventBoxHelper.Moved += delegate { info2.Text = "Moved"; };
            eventBoxHelper.Enter += delegate { info.Text = "Enter"; };
            eventBoxHelper.Leave += delegate { info.Text = "Leave"; };
            eventBoxHelper.Scrolled += delegate(EventBox o, Gdk.EventScroll e) { info.Text = e.Direction.ToString(); };

            buttonLabelColorReset.Clicked += delegate { labelColor.SetForeColor(ColorRGBA.Black); };

            buttonComboBoxQuery.Clicked += buttonComboBoxQuery_Clicked;
            buttonComboBoxModify.Clicked += buttonComboBoxModify_Clicked;

            switchHelper.Toggled += delegate(Switch s, bool toggled) { info.Text = toggled.ToString(); };
            buttonSwitch.Clicked += delegate { switchButton.State = !switchButton.State; };

            buttonFlowBoxAdd.Clicked += buttonFlowBoxAdd_Clicked;
            buttonFlowBoxRemove.Clicked += buttonFlowBoxRemove_Clicked;
            buttonFlowBoxQuery.Clicked += buttonFlowBoxQuery_Clicked;

            buttonListBoxAdd.Clicked += buttonListBoxAdd_Clicked;
            buttonListBoxRemove.Clicked += buttonListBoxRemove_Clicked;
            buttonListBoxQuery.Clicked += buttonListBoxQuery_Clicked;

            buttonTableViewModify.Clicked += buttonTableViewModify_Clicked;
            buttonTableViewQuery.Clicked += buttonTableViewQuery_Clicked;

            editableRenderer.Edited += editableRenderer_Edited;

            buttonTimelineIndicatorDraw.Clicked += buttonTimelineIndicatorDraw_Clicked;

            linkItem.Clicked += delegate { DialogHelper.Notice("Clicked !"); };
            buttonDisableLink.Clicked += delegate { boxLinkItem.Sensitive = false; };
            buttonEnableLink.Clicked += delegate { boxLinkItem.Sensitive = true; };
        }

        private ASEvaAPIGtkTest(Builder builder) : base(builder.GetObject("ASEvaAPIGtkTest").Handle)
        {
            builder.Autoconnect(this);
        }

        private void window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void buttonComboBoxQuery_Clicked(object sender, EventArgs e)
        {
            DialogHelper.Notice(comboBox.GetItemCount() + ", " + comboBox.GetItemAt(1) + "\n" + String.Join(',', comboBox.GetItems()));
        }

        private void buttonComboBoxModify_Clicked(object sender, EventArgs e)
        {
            comboBox.SetItemAt(1, "Hello world");
        }

        private void buttonFlowBoxAdd_Clicked(object sender, EventArgs e)
        {
            flowBox.RemoveAll();
            for (int i = 0; i < 10; i++)
            {
                flowBox.Add(new Button()
                {
                    Label = i.ToString(),
                    Visible = true,
                });
            }
        }

        private void buttonFlowBoxRemove_Clicked(object sender, EventArgs e)
        {
            flowBox.RemoveAt(0);
            var item = flowBox.GetItemAt(1);
            if (item != null) flowBox.RemoveItem(item);
        }

        private void buttonFlowBoxQuery_Clicked(object sender, EventArgs e)
        {
            info.Text = flowBox.GetItemCount().ToString();
            var texts = new List<String>();
            foreach (Button item in flowBox.GetItems())
            {
                texts.Add(item.Label);
            }
            DialogHelper.Notice(String.Join("\n", texts));
        }

        private void buttonListBoxAdd_Clicked(object sender, EventArgs e)
        {
            listBox.RemoveAll();
            listBox.AddLabel("This is label 1");
            listBox.AddLabel("This is label 2");
            listBox.AddLabel("This is label 3");
            listBox.AddCheckButton("This is checked button", true);
            listBox.AddCheckButton("This is unchecked button", false);
        }

        private void buttonListBoxRemove_Clicked(object sender, EventArgs e)
        {
            listBox.RemoveItem(listBox.GetActiveItem());
            listBox.RemoveAt(0);
        }

        private String listBoxItemToString(Widget item)
        {
            if (item == null) return "null";
            else if (item is Label) return (item as Label).Text;
            else if (item is CheckButton) return (item as CheckButton).Label;
            else return "wrong type";
        }

        private String listBoxItemsToString(Widget[] items)
        {
            var list = new List<String>();
            foreach (var item in items) list.Add(listBoxItemToString(item));
            return String.Join('\n', list);
        }

        private void buttonListBoxQuery_Clicked(object sender, EventArgs e)
        {
            info.Text = listBox.GetActiveIndex() + " / " + listBox.GetItemCount();
            info2.Text = String.Join(',', listBox.GetActiveIndices());
            DialogHelper.Notice(listBoxItemToString(listBox.GetItemAt(0)));
            DialogHelper.Notice(listBoxItemsToString(listBox.GetItems()));
            DialogHelper.Notice(listBoxItemsToString(listBox.GetActiveItems()));
        }

        private void buttonTableViewModify_Clicked(object sender, EventArgs e)
        {
            tableStore.SetAttribute(1, 2, "City road");
            tableStore.SetAttribute(1, 3, "orange");
        }

        private void buttonTableViewQuery_Clicked(object sender, EventArgs e)
        {
            DialogHelper.Notice(tableStore.GetAttribute(1, 0) + ": " + tableStore.GetAttribute(1, 2));
        }

        private void editableRenderer_Edited(object o, EditedArgs args)
        {
            tableStore.SetAttribute(args.Path, 2, args.NewText);
        }

        private void buttonTimelineIndicatorDraw_Clicked(object sender, EventArgs e)
        {
            timelineIndicator.Lower = 0;
            timelineIndicator.Upper = 1;
            timelineIndicator.Value = 0;
        }
    }
}
