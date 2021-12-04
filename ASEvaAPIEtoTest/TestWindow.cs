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
            Size = MinimumSize = this.Sizer(1200, 700);
            Title = t["title"] + " (OS:" + ASEva.APIInfo.GetRunningOS() + ")";

            var contextMenu = this.SetContextMenuAsNew();
            InitContextMenu(contextMenu);

            var layout = this.SetContentAsTableLayout();
            var rowFirst = layout.AddRow(true);
            var rowSecond = layout.AddRow(true);

            var groupBasic = rowFirst.AddGroupBox(t["basic-group-title"], true, true);
            InitBasicGroupBox(groupBasic);

            var groupWeb = rowFirst.AddGroupBox(t["web-group-title"], true, true);
            InitWebGroupBox(groupWeb);

            var groupDraw = rowSecond.AddGroupBox(t["draw-group-title"], true, true);
            InitDrawGroupBox(groupDraw);

            rowSecond.AddGroupBox(t["reserved"], true, true, 200, 100);
        }

        private void InitContextMenu(ContextMenu menu)
        {
            menu.AddButtonItem(t["menu-button"], Icon.FromResource("menu-button.png")).Click += delegate { MessageBox.Show(App.WorkPath, ""); };
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

            layout.AddLabel(t["basic-menu-notice"]);

            var layoutRow1 = layout.AddRowLayout();
            layoutRow1.AddLabel(t.Format("basic-label-row", 1));
            var checkBox = layoutRow1.AddCheckBox(t["basic-checkbox"]);
            var radioButtonList = layoutRow1.AddRadioButtonList(new string[] { t["basic-radiobutton-file"], t["basic-radiobutton-dir"] });
            layoutRow1.AddSpace();
            var linkButton = layoutRow1.AddLinkButton(t["basic-linkbutton"]);
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
            layoutRow2.AddControl(new DateTimePicker(), true);

            var layoutRow3 = layout.AddRowLayout();
            layoutRow3.AddLabel(t.Format("basic-label-row", 3));
            layoutRow3.AddControl(new TextBox(), true);
            layoutRow3.AddControl(new SearchBox());
            layoutRow3.AddControl(new NumericStepper { MinValue = 0, MaxValue = 100 } );

            var layoutRow4 = layout.AddRowLayout();
            layoutRow4.AddLabel(t.Format("basic-label-row", 4));
            var slider = layoutRow4.AddControl(new Slider { MinValue = 0, MaxValue = 100, TickFrequency = 10 }, true, 0, 40) as Slider;
            var progressBar = layoutRow4.AddControl(new ProgressBar()) as ProgressBar;
            slider.ValueChanged += delegate { progressBar.Value = slider.Value; };

            var layoutRow5 = layout.AddRowLayout();
            layoutRow5.AddLabel(t.Format("basic-label-row", 5));
            layoutRow5.AddButton(t["basic-button"]);
            layoutRow5.AddButton(Icon.FromResource("button.png"));
            layoutRow5.AddControl(new ColorPicker { Value = Colors.Red } );
        }

        private void InitBasicTagPage2(TabPage tabPage)
        {
            var splitter = tabPage.SetContentAsColumnLayout().AddControl(new Splitter { Position = this.Sizer(200) }, true) as Splitter;

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
            tableView.AddColumn(t["basic-grid-value-title"], 200);

            linkButtonAdd.Click += delegate { tableView.AddRow(); };
            linkButtonRemove.Click += delegate { tableView.RemoveRow(tableView.SelectedRow); };
        }

        private void InitWebGroupBox(GroupBox groupBox)
        {
            var layout = groupBox.SetContentAsColumnLayout();
            var layoutRow = layout.AddRowLayout();
            var webView = layout.AddControl(new WebView(), true, 500, 200) as WebView;

            layoutRow.AddButton(t["web-go-back"]).Click += delegate { webView.GoBack(); };
            layoutRow.AddButton(t["web-go-forward"]).Click += delegate { webView.GoForward(); };
            var textBox = layoutRow.AddControl(new TextBox(), true) as TextBox;
            layoutRow.AddButton(t["web-go-url"]).Click += delegate
            {
                if (!String.IsNullOrEmpty(textBox.Text))
                {
                    webView.Url = new Uri(textBox.Text.StartsWith("http") ? textBox.Text : ("http://" + textBox.Text));
                }
            };
            layoutRow.AddButton(t["web-call-script"]).Click += delegate
            {
                webView.ExecuteScriptAsync("callScript()");
            };

            webView.LoadHtml(ResourceLoader.LoadText("index.html"));
        }

        private void InitDrawGroupBox(GroupBox groupBox)
        {
            var layoutRow = groupBox.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);

            var layoutOverlay = layoutRow.AddControl(new OverlayLayout(), false, 200, 200) as OverlayLayout;
            var drawable = layoutOverlay.AddControl(new Drawable(), 0, 0, 0, 0) as Drawable;
            layoutOverlay.AddControl(new Button { Text = "A"}, 10, null, 10, null);
            layoutOverlay.AddControl(new Button { Text = "B"}, 10, null, null, 10);
            layoutOverlay.AddControl(new Button { Text = "C"}, null, 10, 10, null);
            layoutOverlay.AddControl(new Button { Text = "D"}, null, 10, null, 10);

            drawable.Paint += drawable_Paint;
        }

        private void drawable_Paint(object o, PaintEventArgs a)
        {
            var g = a.Graphics;
            g.SetScaleForLogical();

            g.Clear(Colors.White);
            g.DrawLine(Colors.Black, 10, 100, 190, 100);
            g.DrawLine(Colors.Black, 10, 120, 190, 120);
            g.DrawLine(Colors.Black, 100, 10, 100, 190);
            g.DrawLine(new Pen(Colors.Red, 20), 20, 110, 90, 110);
            g.DrawText(g.ScaledDefaultFont(), Colors.Black, 100, 100, t["draw-text"]);
            g.DrawImage(Icon.FromResource("camera.png"), 80, 80);
            g.FillPie(Color.FromArgb(0, 128, 0, 128), 10, 10, 180, 180, -90, 270);
        }

        private TextResource t;
    }
}