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
        private void initBasicTagPageD(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();

            var layoutTextControl = layout.AddRowLayout(false, 8, VerticalAlignment.Stretch);
            initBasicTagPageDTextControl(layoutTextControl);
        }

        private void initBasicTagPageDTextControl(StackLayout layout)
        {
            layout.AddSeparator();
            var layoutRows = layout.AddColumnLayout(true);
            layout.AddSeparator();

            var layoutLabels = layoutRows.AddRowLayout();
            layoutLabels.AddLabel(t["basic-text"], true).TextColor = Colors.Red;
            layoutLabels.AddSeparator();
            layoutLabels.AddLabel(t["basic-text"], TextAlignment.Center, true).Font = App.DefaultFont(0.8f);
            layoutLabels.AddSeparator();
            layoutLabels.AddLabel(t["basic-text"], TextAlignment.Right, true).Font = App.DefaultFont(1.2f);

            var layoutLinks = layoutRows.AddRowLayout();
            layoutLinks.AddLinkButton(t["basic-text"], true).TextColor = Colors.Red;
            layoutLinks.AddSeparator();
            layoutLinks.AddLinkButton(t["basic-text"], true).Font = App.DefaultFont(0.8f);
            layoutLinks.AddSeparator();
            layoutLinks.AddLinkButton(t["basic-text"], true).Font = App.DefaultFont(1.2f);

            var layoutChecks = layoutRows.AddRowLayout();
            layoutChecks.AddCheckBox(t["basic-text"], true).TextColor = Colors.Red;
            layoutChecks.AddSeparator();
            layoutChecks.AddCheckBox(t["basic-text"], true).Font = App.DefaultFont(0.8f);
            layoutChecks.AddSeparator();
            layoutChecks.AddCheckBox(t["basic-text"], true).Font = App.DefaultFont(1.2f);

            var layoutRadios = layoutRows.AddRowLayout();
            layoutRadios.AddControl(new RadioButton{ Text = t["basic-text"], TextColor = Colors.Red }, true);
            layoutRadios.AddSeparator();
            layoutRadios.AddControl(new RadioButton{ Text = t["basic-text"], Font = App.DefaultFont(0.8f) }, true);
            layoutRadios.AddSeparator();
            layoutRadios.AddControl(new RadioButton{ Text = t["basic-text"], Font = App.DefaultFont(1.2f) }, true);

            var layoutTextBoxes = layoutRows.AddRowLayout();
            layoutTextBoxes.AddControl(new TextBox{ Text = t["basic-text"], TextColor = Colors.Red }, true);
            layoutTextBoxes.AddSeparator();
            layoutTextBoxes.AddControl(new TextBox{ Text = t["basic-text"], Font = App.DefaultFont(0.8f) }, true);
            layoutTextBoxes.AddSeparator();
            layoutTextBoxes.AddControl(new TextBox{ Text = t["basic-text"], Font = App.DefaultFont(1.2f) }, true);

            var layoutButtons = layoutRows.AddRowLayout();
            layoutButtons.AddButton(t["basic-text"], true).TextColor = Colors.Red;
            layoutButtons.AddSeparator();
            layoutButtons.AddButton(t["basic-text"], true).Font = App.DefaultFont(0.8f);
            layoutButtons.AddSeparator();
            layoutButtons.AddButton(t["basic-text"], true).Font = App.DefaultFont(1.2f);
        }
    }
}