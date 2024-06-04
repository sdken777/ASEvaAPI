using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageC : Panel
    {
        public BasicPageC()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);

            var checkItems = new ObservableCollection<CheckItem>();
            for (int i = 0; i < 1000; i++)
            {
                checkItems.Add(new CheckItem{ Content = Program.Texts.Format("basic-list-item-short", i.ToString()), IsChecked = i % 2 == 0, IsEnabled = (i / 2) % 2 == 0 });
            }
            checkListBox.ItemsSource = checkItems;
        }

        private void checkListBox_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            var selectedIndex = checkListBox.SelectedIndex;
            if (selectedIndex >= 0) MessageBox.Show(selectedIndex + ": " + (checkListBox.ItemsSource as ObservableCollection<CheckItem>)[selectedIndex].IsChecked, "");
        }

        private void linkCheckAll_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            foreach (var item in items)
            {
                if (item.IsEnabled) item.IsChecked = true;
            }
        }

        private void linkUncheckAll_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            foreach (var item in items)
            {
                if (item.IsEnabled) item.IsChecked = false;
            }
        }

        private void linkRemoveCheckRow_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            if (items.Count > 100) items.RemoveAt(items.Count - 1);
        }

        private void linkSetCheckText_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            items[checkListTarget].Content = checkListTarget.ToString();
            if (checkListTarget < 99) checkListTarget++;
        }

        private void linkGetCheck_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            MessageBox.Show(checkListTarget + ": " + items[checkListTarget].IsChecked, "");
        }

        private void linkSetCheck_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            items[checkListTarget].IsChecked = !items[checkListTarget].IsChecked;
            if (checkListTarget < 99) checkListTarget++;
        }

        private void linkGetEnable_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            MessageBox.Show(checkListTarget + ": " + items[checkListTarget].IsEnabled, "");
        }

        private void linkSetEnable_Click(object sender, RoutedEventArgs e)
        {
            var items = checkListBox.ItemsSource as ObservableCollection<CheckItem>;
            items[checkListTarget].IsEnabled = !items[checkListTarget].IsEnabled;
            if (checkListTarget < 99) checkListTarget++;
        }

        private class CheckItem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public String Content
            {
                get => content;
                set { content = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content))); }
            }

            public bool IsEnabled
            {
                get => isEnabled;
                set { isEnabled = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled))); }
            }
            
            public bool IsChecked
            {
                get => isChecked;
                set { isChecked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked))); }
            }

            private String content;
            private bool isEnabled;
            private bool isChecked;
        }

        private int checkListTarget = 0;
    }
}