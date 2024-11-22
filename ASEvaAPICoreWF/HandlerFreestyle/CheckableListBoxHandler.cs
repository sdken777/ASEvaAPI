using System;
using System.Linq;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    class CheckableListBoxFactoryCoreWF : CheckableListBoxFactory
    {
        public void CreateCheckableListBoxBackend(CheckableListBoxCallback callback, out Control etoControl, out CheckableListBoxBackend backend)
        {
            var panel = new CheckableListBoxBackendCoreWF(callback);
            etoControl = panel.ToEto();
            backend = panel;
        }
    }

    class CheckableListBoxBackendCoreWF : System.Windows.Forms.FlowLayoutPanel, CheckableListBoxBackend
    {
        public CheckableListBoxBackendCoreWF(CheckableListBoxCallback callback)
        {
            FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AutoScroll = true;

            this.callback = callback;
            this.timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;
            timer.Tick += delegate { updateToPanel(); };
            timer.Enabled = true;
        }

        public void AddItems(string[] itemsText, bool[] itemsChecked, bool[] itemsEnabled)
        {
            for (int i = 0; i < itemsText.Length; i++)
            {
                var checkBox = new System.Windows.Forms.CheckBox();
                checkBox.Height = (int)(Pixel.Scale * 20);
                checkBox.Text = itemsText[i];
                checkBox.Checked = itemsChecked == null ? false : itemsChecked[i];
                checkBox.Enabled = itemsEnabled == null ? true : itemsEnabled[i];
                checkBox.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
                checkBox.CheckedChanged += checkBox_CheckedChanged;
                checkBoxes.Add(checkBox);
            }
        }

        public void RemoveItems(int[] indices)
        {
            foreach (int index in indices)
            {
                Controls.Remove(checkBoxes[index]);
                checkBoxes.RemoveAt(index);
                if (selectedIndex == index) selectedIndex = -1;
            }
            removedFromPanel = true;
        }

        public void RemoveAllItems()
        {
            Controls.Clear();
            checkBoxes.Clear();
            removedFromPanel = true;
            selectedIndex = -1;
        }

        public bool GetChecked(int index)
        {
            return checkBoxes[index].Checked;
        }

        public void SetChecked(int[] indices, bool isChecked)
        {
            foreach (var index in indices)
            {
                checkBoxes[index].CheckedChanged -= checkBox_CheckedChanged;
                checkBoxes[index].Checked = isChecked;
                checkBoxes[index].CheckedChanged += checkBox_CheckedChanged;
            }
        }

        public void SetText(int index, string text)
        {
            checkBoxes[index].Text = text;
        }

        public void SetEnabled(int index, bool isEnabled)
        {
            checkBoxes[index].Enabled = isEnabled;
        }

        public int[] GetCheckedIndices()
        {
            var list = new List<int>();
            int index = 0;
            foreach (var checkBox in checkBoxes)
            {
                if (checkBox.Checked) list.Add(index);
                index++;
            }
            return list.ToArray();
        }

        public int GetSelectedRowIndex()
        {
            return selectedIndex;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var target = sender as System.Windows.Forms.CheckBox;
            target.BackColor = System.Drawing.Color.LightGray;
            var newSelectedIndex = checkBoxes.IndexOf(target);
            if (selectedIndex >= 0 && selectedIndex != newSelectedIndex) checkBoxes[selectedIndex].BackColor = System.Drawing.Color.Transparent;
            selectedIndex = newSelectedIndex;
            callback.OnItemClicked();
        }

        private void updateToPanel()
        {
            int added = 0;
            if (Visible)
            {
                int index = 0;
                foreach (var checkBox in checkBoxes)
                {
                    if (!Controls.Contains(checkBox))
                    {
                        Controls.Add(checkBox);
                        Controls.SetChildIndex(checkBox, index);
                        if (++added == 10) break;
                    }
                    index++;
                }
            }

            bool resized = false;
            if (Width > 0 && Height > 0 && (Width != lastSize.Width || Height != lastSize.Height))
            {
                lastSize = Size;
                resized = true;
            }

            if (added > 0 || resized || removedFromPanel)
            {
                removedFromPanel = false;

                if (lastSize.Width < 50) return;
                foreach (var checkBox in checkBoxes)
                {
                    checkBox.Width = lastSize.Width - (int)(6 * Pixel.Scale) - (VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0);
                }
            }
        }

        private CheckableListBoxCallback callback;
        private System.Windows.Forms.Timer timer;
        private List<System.Windows.Forms.CheckBox> checkBoxes = new List<System.Windows.Forms.CheckBox>();
        private bool removedFromPanel = false;
        private System.Drawing.Size lastSize;
        private int selectedIndex = -1;
    }
}
