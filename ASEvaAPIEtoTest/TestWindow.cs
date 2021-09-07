using System;
using ASEva.Utility;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    class TestWindow : Form
    {
        public TestWindow(String languageCode)
        {
            t = TextResource.Load("test.xml", languageCode);

            Icon = Icon.FromResource("icon.png");
            Size = MinimumSize = this.Sizer(1000, 600);
            Title = t["title"] + " (OS:" + ASEva.APIInfo.GetRunningOS() + ")";

            ContextMenu = new ContextMenu();
            InitContextMenu(ContextMenu);

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow(true);

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            InitBasicGroupBox(groupBasic);

            rowFirst.AddGroupBox(t["reserved"], true, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);
        }

        private void InitContextMenu(ContextMenu menu)
        {
            menu.AddButtonItem(t["menu-button"], Bitmap.FromResource("menu-button.png")).Click += delegate { MessageBox.Show(t["title"], ""); };
            menu.AddSeparator();
            menu.AddCheckItem(t.Format("menu-check", "A"));
            menu.AddCheckItem(t.Format("menu-check", "B"));
            menu.AddSeparator();
            menu.AddRadioItems(new String[] { t.Format("menu-radio", "A"), t.Format("menu-radio", "B") });
        }

        private void InitBasicGroupBox(GroupBox groupBox)
        {
            var tabControl = groupBox.SetContentAsColumnLayout().AddControl(new TabControl(), true) as TabControl;

            var tabPage1 = tabControl.AddPage(t.Format("basic-tabpage-title", "A"));
            InitBasicTagPage1(tabPage1);

            var tabPage2 = tabControl.AddPage(t.Format("basic-tabpage-title", "B"));
            InitBasicTagPage2(tabPage2);

            var tabPage3 = tabControl.AddPage(t.Format("basic-tabpage-title", "C"));
            InitBasicTagPage3(tabPage3);
        }

        private void InitBasicTagPage1(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();

            var layoutRow1 = layout.AddRowLayout();
            layoutRow1.AddLabel(t.Format("basic-label-row", 1));
            var checkBox = layoutRow1.AddCheckBox(t["basic-checkbox"]);
            var radioButtonList = layoutRow1.AddRadioButtonList(new string[] { t["basic-radiobutton-file"], t["basic-radiobutton-dir"] });
            layoutRow1.AddSpace();
            var linkButton = layoutRow1.AddLinkButton(t["basic-linkbutton"]); // TODO: Gtk-Ubuntu2004-X86(高亮异常)
            linkButton.Click += delegate
            {
                if (radioButtonList.SelectedIndex == 0)
                {
                    if (checkBox.Checked.Value) new SaveFileDialog().ShowDialog(null);
                    else new OpenFileDialog().ShowDialog(null);
                }
                else new SelectFolderDialog().ShowDialog(null);
            };

            var layoutRow2 = layout.AddRowLayout();
            layoutRow2.AddLabel(t.Format("basic-label-row", 2));
            layoutRow2.AddComboBox(new string[] { t.Format("basic-combobox", "A"), t.Format("basic-combobox", "B") }, true);
            layoutRow2.AddButton(t["basic-button"]);
            layoutRow2.AddButton(Bitmap.FromResource("button.png"));
            layoutRow2.AddControl(new ColorPicker { Value = Colors.Red } ); // TODO: CoreWF(高DPI尺寸问题) Gtk(无法退出)

            var layoutRow3 = layout.AddRowLayout();
            layoutRow3.AddLabel(t.Format("basic-label-row", 3));
            layoutRow3.AddControl(new TextBox(), true);
            layoutRow3.AddControl(new SearchBox()); // TODO: CoreWF(图标/清除)
            layoutRow3.AddControl(new NumericStepper { MinValue = 0, MaxValue = 100 } );

            var layoutRow4 = layout.AddRowLayout();
            layoutRow4.AddLabel(t.Format("basic-label-row", 4));
            var slider = layoutRow4.AddControl(new Slider { MinValue = 0, MaxValue = 100, TickFrequency = 10 }, true, 0, 40) as Slider;
            var progressBar = layoutRow4.AddControl(new ProgressBar()) as ProgressBar;
            slider.ValueChanged += delegate { progressBar.Value = slider.Value; };

            var layoutRow5 = layout.AddRowLayout();
            layoutRow5.AddLabel(t.Format("basic-label-row", 5));
            layoutRow5.AddControl(new DateTimePicker(), true);
        }

        private void InitBasicTagPage2(TabPage tabPage)
        {
            var splitter = tabPage.SetContentAsColumnLayout().AddControl(new Splitter { Position = this.Sizer(100) }, true) as Splitter;

            var layout = splitter.SetPanel1().SetContentAsColumnLayout(0);
            
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            listBox.Items.Add(t.Format("basic-list-item", "A"));
            listBox.Items.Add(t.Format("basic-list-item", "B"));
            listBox.Items.Add(t.Format("basic-list-item", "C"));

            var checkListBox = layout.AddControl(new CheckBoxList { Orientation = Orientation.Vertical} , true) as CheckBoxList;
            checkListBox.Items.Add(t.Format("basic-list-item", "A"));
            checkListBox.Items.Add(t.Format("basic-list-item", "B"));
            checkListBox.Items.Add(t.Format("basic-list-item", "C"));

            var scrollBox = splitter.SetPanel2().SetContentAsColumnLayout(0).AddControl(new Scrollable(), true) as Scrollable;
            scrollBox.SetContentAsColumnLayout().AddControl(new ImageView { Image = Bitmap.FromResource("picture.png") }, true);
        }

        private void InitBasicTagPage3(TabPage tabPage)
        {
            var layoutRow = tabPage.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            layoutRow.AddControl(new TextArea { Text = t["empty"] }, false, 150);

            var layoutGridView = layoutRow.AddColumnLayout(true, 2);

            var layoutGridViewRow = layoutGridView.AddRowLayout();
            var linkButtonAdd = layoutGridViewRow.AddLinkButton(t["basic-grid-add-row"]);
            layoutGridViewRow.AddSpace();
            var linkButtonRemove = layoutGridViewRow.AddLinkButton(t["basic-grid-remove-row"]);

            var tableView = layoutGridView.AddControl(new TextTableView(), true) as TextTableView;
            tableView.AddColumn(t["basic-grid-key-title"], 100);
            tableView.AddColumn(t["basic-grid-value-title"], 150);

            linkButtonAdd.Click += delegate { tableView.AddRow(); };
            linkButtonRemove.Click += delegate { tableView.RemoveRow(tableView.SelectedRow); };
        }

        private TextResource t;
    }
}