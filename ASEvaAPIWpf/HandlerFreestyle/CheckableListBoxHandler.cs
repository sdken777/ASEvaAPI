using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ASEva.UIWpf
{
    class CheckableListBoxBackendWpf : ListBox, ASEva.UIEto.CheckableListBoxBackend
    {
        public CheckableListBoxBackendWpf(ASEva.UIEto.CheckableListBoxCallback callback)
        {
            this.callback = callback;
            MouseUp += CheckableListBoxBackendWpf_MouseUp;
        }

        public void AddItems(string[] itemsText, bool[] itemsChecked, bool[] itemsEnabled)
        {
            for (int i = 0; i < itemsText.Length; i++)
            {
                var checkBox = new CheckBox();
                checkBox.Content = itemsText[i];
                checkBox.IsEnabled = itemsEnabled == null ? true : itemsEnabled[i];
                checkBox.IsChecked = itemsChecked == null ? false : itemsChecked[i];
                checkBox.Margin = new Thickness(1);
                checkBox.VerticalContentAlignment = VerticalAlignment.Center;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Checked;
                Items.Add(new ListBoxItem { Content = checkBox });
            }
        }

        public void RemoveItems(int[] indices)
        {
            foreach (var index in indices)
            {
                Items.RemoveAt(index);
            }
        }

        public void RemoveAllItems()
        {
            Items.Clear();
        }

        public bool GetChecked(int index)
        {
            return ((Items[index] as ListBoxItem).Content as CheckBox).IsChecked.Value;
        }

        public void SetChecked(int[] indices, bool isChecked)
        {
            foreach (var index in indices)
            {
                var checkBox = (Items[index] as ListBoxItem).Content as CheckBox;
                checkBox.Checked -= CheckBox_Checked;
                checkBox.Unchecked -= CheckBox_Checked;
                checkBox.IsChecked = isChecked;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Checked;
            }
        }

        public void SetText(int index, string text)
        {
            ((Items[index] as ListBoxItem).Content as CheckBox).Content = text;
        }

        public void SetEnabled(int index, bool isEnabled)
        {
            ((Items[index] as ListBoxItem).Content as CheckBox).IsEnabled = isEnabled;
        }

        public int[] GetCheckedIndices()
        {
            var list = new List<int>();
            int index = 0;
            foreach (ListBoxItem item in Items)
            {
                var checkBox = item.Content as CheckBox;
                if (checkBox.IsChecked.Value) list.Add(index);
                index++;
            }
            return list.ToArray();
        }

        public int GetSelectedRowIndex()
        {
            return SelectedIndex;
        }

        private void CheckableListBoxBackendWpf_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedIndex < 0) return;
            
            var checkBox = (Items[SelectedIndex] as ListBoxItem).Content as CheckBox;
            if (!checkBox.IsEnabled) return;

            checkBox.Checked -= CheckBox_Checked;
            checkBox.Unchecked -= CheckBox_Checked;
            checkBox.IsChecked = !checkBox.IsChecked;
            checkBox.Checked += CheckBox_Checked;
            checkBox.Unchecked += CheckBox_Checked;
            callback.OnItemClicked();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (ListBoxItem item in Items)
            {
                if (item.Content.Equals(sender))
                {
                    SelectedIndex = index;
                    callback.OnItemClicked();
                    break;
                }
                index++;
            }
        }

        private ASEva.UIEto.CheckableListBoxCallback callback;
    }
}
