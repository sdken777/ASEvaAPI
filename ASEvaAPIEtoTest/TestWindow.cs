using System;
using ASEva;
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

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow(true);

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            InitBasicGroupBox(groupBasic);

            rowFirst.AddGroupBox(t["reserved"], true, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);
        }

        private void InitBasicGroupBox(GroupBox groupBox)
        {
            var tabControl = groupBox.SetContentAsColumnLayout().AddControl(new TabControl(), true) as TabControl;

            var tabPage1 = tabControl.AddPage(t.Format("basic-tabpage-title", 1));
            InitBasicTagPage1(tabPage1);

            var tabPage2 = tabControl.AddPage(t.Format("basic-tabpage-title", 2));
            InitBasicTagPage2(tabPage2);

            var tabPage3 = tabControl.AddPage(t.Format("basic-tabpage-title", 3));
            InitBasicTagPage3(tabPage3);
        }

        private void InitBasicTagPage1(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();

            var layoutRow1 = layout.AddRowLayout();
            layoutRow1.AddLabel(t.Format("basic-label-stack", 1));
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
            layoutRow2.AddLabel(t.Format("basic-label-stack", 2));
            layoutRow2.AddComboBox(new string[] { t.Format("basic-combobox", 1), t.Format("basic-combobox", 2) }, true);
            layoutRow2.AddButton(t["basic-button"]);
            layoutRow2.AddButton(Bitmap.FromResource("button.png"));
            layoutRow2.AddControl(new ColorPicker { Value = Colors.Red } ); // TODO: CoreWF(高DPI尺寸问题) Gtk(无法退出)

            var layoutRow3 = layout.AddRowLayout();
            layoutRow3.AddLabel(t.Format("basic-label-stack", 3));
            layoutRow3.AddControl(new TextBox(), true);
            layoutRow3.AddControl(new SearchBox()); // TODO: CoreWF(图标/清除)
            layoutRow3.AddControl(new NumericStepper { MinValue = 0, MaxValue = 100 } );

            var layoutRow4 = layout.AddRowLayout();
            layoutRow4.AddLabel(t.Format("basic-label-stack", 4));
            var slider = layoutRow4.AddControl(new Slider { MinValue = 0, MaxValue = 100, TickFrequency = 10 }, true, 0, 40) as Slider;
            var progressBar = layoutRow4.AddControl(new ProgressBar()) as ProgressBar;
            slider.ValueChanged += delegate { progressBar.Value = slider.Value; };

            var layoutRow5 = layout.AddRowLayout();
            layoutRow5.AddLabel(t.Format("basic-label-stack", 5));
            layoutRow5.AddControl(new DateTimePicker(), true);
        }

        private void InitBasicTagPage2(TabPage tabPage)
        {
            var splitter = tabPage.SetContentAsColumnLayout().AddControl(new Splitter { Position = 100 }, true) as Splitter;

            var layout = splitter.SetPanel1().SetContentAsColumnLayout(0);
            
            var listBox = layout.AddControl(new ListBox(), true) as ListBox;
            listBox.Items.Add(t.Format("basic-list-item", 1));
            listBox.Items.Add(t.Format("basic-list-item", 2));
            listBox.Items.Add(t.Format("basic-list-item", 3));

            var checkListBox = layout.AddControl(new CheckBoxList { Orientation = Orientation.Vertical} , true) as CheckBoxList;
            checkListBox.Items.Add(t.Format("basic-list-item", 1));
            checkListBox.Items.Add(t.Format("basic-list-item", 2));
            checkListBox.Items.Add(t.Format("basic-list-item", 3));

            var scrollBox = splitter.SetPanel2().SetContentAsColumnLayout(0).AddControl(new Scrollable(), true) as Scrollable;
            scrollBox.SetContentAsColumnLayout().AddControl(new ImageView { Image = Bitmap.FromResource("picture.png") }, true);
        }

        private void InitBasicTagPage3(TabPage tabPage)
        {
            var layoutRow = tabPage.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            layoutRow.AddControl(new TextArea { Text = t["empty"] }, false, 150);

            // var gridView = layoutRow.AddControl(new GridView(), true) as GridView;
            // gridView.Columns.Add(new GridColumn { HeaderText = "Key"});
        }

        private TextResource t;
    }
}