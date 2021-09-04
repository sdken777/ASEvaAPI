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

            Size = MinimumSize = this.Sizer(1000, 600);
            Title = t["title"] + " (OS:" + ASEva.APIInfo.GetRunningOS() + ")";

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow();

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            InitBasicGroupBox(groupBasic);

            rowFirst.AddGroupBox(t["reserved"], false, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], false, true, 200, 100);
            rowSecond.AddGroupBox(t["reserved"], false, true, 200, 100);
        }

        private void InitBasicGroupBox(GroupBox groupBox)
        {
            var layout = groupBox.SetContentAsRowStackLayout();
            var tabControl = layout.AddControl(new TabControl(), true) as TabControl;

            var tabPage1 = new TabPage { Text = t.Format("basic-tabpage-title", 1) };
            tabControl.Pages.Add(tabPage1);
            InitBasicTagPage1(tabPage1);

            var tabPage2 = new TabPage { Text = t.Format("basic-tabpage-title", 2) };
            tabControl.Pages.Add(tabPage2);
            InitBasicTagPage2(tabPage2);
        }

        private void InitBasicTagPage1(TabPage tabPage)
        {
            var layoutMain = tabPage.SetContentAsRowStackLayout();

            var layoutStack1 = layoutMain.AddColumnStackLayout();
            layoutStack1.AddLabel(t.Format("basic-label-stack", 1));
            layoutStack1.AddCheckBox(t["basic-checkbox"]);
            layoutStack1.AddRadioButtonList(new string[] { t.Format("basic-radiobutton", 1), t.Format("basic-radiobutton", 2) });
            layoutStack1.AddLinkButton(t["basic-linkbutton"]); // TODO: Gtk-Ubuntu2004-X86(高亮异常)

            var layoutStack2 = layoutMain.AddColumnStackLayout();
            layoutStack2.AddLabel(t.Format("basic-label-stack", 2));
            layoutStack2.AddComboBox(new string[] { t.Format("basic-combobox", 1), t.Format("basic-combobox", 2) }, true);
            layoutStack2.AddButton(t["basic-button"]);
            layoutStack2.AddControl(new ColorPicker { Value = Colors.Red } ); // TODO: CoreWF(高DPI尺寸问题) Gtk(无法退出)

            var layoutStack3 = layoutMain.AddColumnStackLayout();
            layoutStack3.AddLabel(t.Format("basic-label-stack", 3));
            layoutStack3.AddControl(new TextBox(), true);
            layoutStack3.AddControl(new SearchBox()); // TODO: CoreWF(图标/清除)
            layoutStack3.AddControl(new NumericStepper { MinValue = 0, MaxValue = 100 } );

            var layoutStack4 = layoutMain.AddColumnStackLayout();
            layoutStack4.AddLabel(t.Format("basic-label-stack", 4));
            var slider = layoutStack4.AddControl(new Slider { MinValue = 0, MaxValue = 100, TickFrequency = 10 }, true, 0, 40) as Slider;
            var progressBar = layoutStack4.AddControl(new ProgressBar()) as ProgressBar;
            slider.ValueChanged += delegate { progressBar.Value = slider.Value; };
        }

        private void InitBasicTagPage2(TabPage tabPage)
        {
            var rowMain = tabPage.SetContentAsTableLayout().AddRow(true);

            rowMain.AddControl(new TextArea { Text = t["empty"] }, false, true, 200); // TODO: CoreWF(宽度设置无效)
            var splitter = rowMain.AddControl(new Splitter { Position = 100 }, true, true) as Splitter;
 
            var listBox = new ListBox();
            listBox.Items.Add(t.Format("basic-list-item", 1));
            listBox.Items.Add(t.Format("basic-list-item", 2));
            listBox.Items.Add(t.Format("basic-list-item", 3));
            splitter.Panel1 = listBox;

            var scrollBox = new Scrollable();
            scrollBox.SetContentAsRowStackLayout().AddControl(new ImageView { Image = ImageResourceLoader.Load("picture.png") });
            splitter.Panel2 = scrollBox;
        }

        private TextResource t;
    }
}