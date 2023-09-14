using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initBasicTabPageA(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();

            layout.AddLabel(t["basic-menu-notice"]);

            var layoutRow1 = layout.AddRowLayout();
            initBasicTabPageARow1(layoutRow1);

            var layoutRow2 = layout.AddRowLayout();
            initBasicTabPageARow2(layoutRow2);

            var layoutRow3 = layout.AddRowLayout();
            initBasicTabPageARow3(layoutRow3);

            var layoutRow4 = layout.AddRowLayout();
            initBasicTabPageARow4(layoutRow4);

            var layoutRow5 = layout.AddRowLayout();
            initBasicTabPageARow5(layoutRow5);

            var layoutRow6 = layout.AddRowLayout();
            initBasicTabPageARow6(layoutRow6);
        }

        private void initBasicTabPageARow1(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 1));
            var checkBox = layout.AddCheckBox(t["basic-checkbox"]);
            var radioButtonList = layout.AddRadioButtonList(new string[] { t["basic-radiobutton-file"], t["basic-radiobutton-dir"] });
            layout.AddSpace();
            var linkButton = layout.AddLinkButton(t["basic-linkbutton"]);
            linkButton.TextColor = Colors.ForestGreen;
            linkButton.Click += delegate
            {
                if (radioButtonList.SelectedIndex == 0)
                {
                    if (checkBox.Checked.Value) new SaveFileDialog().ShowDialog(null);
                    else new OpenFileDialog().ShowDialog(null);
                }
                else new SelectFolderDialog().ShowDialog(null);
            };
        }

        private void initBasicTabPageARow2(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 2));
            layout.AddComboBox(new string[] { t.Format("basic-combobox", "A"), t.Format("basic-combobox", "B") }, true);
            layout.AddControl(new DateTimePicker(), true);
        }

        private void initBasicTabPageARow3(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 3));
            layout.AddControl(new TextBox(), true);
            layout.AddControl(new SearchBox());
            layout.AddControl(new NumericStepper { MinValue = 0, MaxValue = 100 } );
        }

        private void initBasicTabPageARow4(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 4));
            var slider = layout.AddControl(new Slider { MinValue = 0, MaxValue = 100, TickFrequency = 10 }, true, 0, 40) as Slider;
            var progressBar = layout.AddControl(new ProgressBar()) as ProgressBar;
            slider.ValueChanged += delegate { progressBar.Value = slider.Value; };
        }

        private void initBasicTabPageARow5(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 5));
            var textButton = layout.AddButton(t["basic-button"]);
            textButton.BackgroundColor = Colors.Green;
            textButton.TextColor = Colors.Gold;
            var imageButton = layout.AddButton(CommonImage.LoadResource("button.png").ToEtoBitmap());
            imageButton.BackgroundColor = Colors.PaleGreen;
            layout.AddControl(new ColorPicker { Value = Colors.Red } );
        }

        private void initBasicTabPageARow6(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-label-row", 6));
            layout.AddLinkButton(t["basic-dialog-no-border"]).Click += delegate
            {
                var dialog = new TestDialog(true, false, t);
                App.RunDialog(dialog);
            };
            layout.AddLinkButton(t["basic-dialog-with-border"]).Click += delegate
            {
                var dialog = new TestDialog(false, true, t);
                App.RunDialog(dialog);
            };
            layout.AddLinkButton(t["basic-dialog-with-border-fix"]).Click += delegate
            {
                var dialog = new TestDialog(true, true, t);
                App.RunDialog(dialog);
            };
            layout.AddLinkButton(t["basic-client-size"], true).Click += (sender, args) =>
            {
                (sender as LinkButton).Text = ClientSize.Width + "x" + ClientSize.Height;
            };
        }
    }
}