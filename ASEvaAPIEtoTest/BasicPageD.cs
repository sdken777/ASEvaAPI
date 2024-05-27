using System;
using ASEva.Utility;
using ASEva.Samples;
using ASEva.UIEto;
using Eto.Forms;
using Eto.Drawing;

namespace ASEvaAPIEtoTest
{
    partial class EtoTestPanel
    {
        private void initBasicTabPageD(TabPage tabPage)
        {
            var layout = tabPage.SetContentAsColumnLayout();

            var layoutTextControl = layout.AddRowLayout(false, 8, VerticalAlignment.Stretch);
            initBasicTabPageDTextControl(layoutTextControl);

            var layoutTooltip = layout.AddRowLayout();
            initBasicTabPageDTooltip(layoutTooltip);
        }

        private void initBasicTabPageDTextControl(StackLayout layout)
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

        private void initBasicTabPageDTooltip(StackLayout layout)
        {
            layout.AddLabel(t.Format("basic-tooltip", 1)).SetToolTip(t.Format("basic-tooltip", 1));
            layout.AddLinkButton(t.Format("basic-tooltip", 2)).SetToolTip(t.Format("basic-tooltip", 2));
            layout.AddCheckBox(t.Format("basic-tooltip", 3)).SetToolTip(t.Format("basic-tooltip", 3));
            layout.AddButton(t.Format("basic-tooltip", 4)).SetToolTip(t.Format("basic-tooltip", 4));
            var textButton = layout.AddButtonPanel(t.Format("basic-tooltip", 5), false, 0, 0, 2);
            textButton.Font = App.DefaultFont(1.2f);
            textButton.SetToolTip(t.Format("basic-tooltip", 5));
            layout.AddButtonPanel(Bitmap.FromResource("button.png")).SetToolTip(t.Format("basic-tooltip", 6));
        }
    }
}