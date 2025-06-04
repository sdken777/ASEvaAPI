using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageC : Panel
    {
        public BasicPageC()
        {
            InitializeComponent();
            new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");
            DataContext = model;

            for (int i = 0; i < 1000; i++)
            {
                model.CheckItems.Add(new CheckItem
                {
                    Content = Program.Texts.Format("basic-list-item-short", i.ToString()),
                    IsChecked = i % 2 == 0,
                    IsEnabled = (i / 2) % 2 == 0,
                });
            }

            for (int i = 0; i < 10; i++)
            {
                subTableView.Columns.Add(new DataGridTextColumn
                {
                    Header = Program.Texts.Format("basic-grid-column", i + 1),
                    Width = new DataGridLength(75),
                });
            }

            linkCheckAll.Click += linkCheckAll_Click;
            linkUncheckAll.Click += linkUncheckAll_Click;
            linkRemoveCheckRow.Click += linkRemoveCheckRow_Click;
            linkSetCheckText.Click += linkSetCheckText_Click;
            linkGetCheck.Click += linkGetCheck_Click;
            linkSetCheck.Click += linkSetCheck_Click;
            linkGetEnable.Click += linkGetEnable_Click;
            linkSetEnable.Click += linkSetEnable_Click;
            linkAddTableRow.Click += linkAddTableRow_Click;
            linkRemoveTableRow.Click += linkRemoveTableRow_Click;
            linkChangeTableGridColor.Click += linkChangeTableGridColor_Click;
            checkListBox.PointerReleased += checkListBox_PointerReleased;
            mainTableView.SelectionChanged += mainTableView_SelectionChanged;
        }

        private async void checkListBox_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            var selectedIndex = checkListBox.SelectedIndex;
            if (selectedIndex >= 0) await App.RunDialog(async (window) => await MessageBox.Show(window, selectedIndex + ": " + model.CheckItems[selectedIndex].IsChecked, ""));
        }

        private void linkCheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in model.CheckItems)
            {
                if (item.IsEnabled) item.IsChecked = true;
            }
        }

        private void linkUncheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in model.CheckItems)
            {
                if (item.IsEnabled) item.IsChecked = false;
            }
        }

        private void linkRemoveCheckRow_Click(object sender, RoutedEventArgs e)
        {
            if (model.CheckItems.Count > 100) model.CheckItems.RemoveAt(model.CheckItems.Count - 1);
        }

        private void linkSetCheckText_Click(object sender, RoutedEventArgs e)
        {
            model.CheckItems[checkListTarget].Content = checkListTarget.ToString();
            if (checkListTarget < 99) checkListTarget++;
        }

        private async void linkGetCheck_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(async (window) => await MessageBox.Show(window, checkListTarget + ": " + model.CheckItems[checkListTarget].IsChecked, ""));
        }

        private void linkSetCheck_Click(object sender, RoutedEventArgs e)
        {
            model.CheckItems[checkListTarget].IsChecked = !model.CheckItems[checkListTarget].IsChecked;
            if (checkListTarget < 99) checkListTarget++;
        }

        private async void linkGetEnable_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(async (window) => await MessageBox.Show(window, checkListTarget + ": " + model.CheckItems[checkListTarget].IsEnabled, ""));
        }

        private void linkSetEnable_Click(object sender, RoutedEventArgs e)
        {
            model.CheckItems[checkListTarget].IsEnabled = !model.CheckItems[checkListTarget].IsEnabled;
            if (checkListTarget < 99) checkListTarget++;
        }

        private void linkAddTableRow_Click(object sender, RoutedEventArgs e)
        {
            model.MainTableItems.Add(new MainTableItem());
        }

        private void linkRemoveTableRow_Click(object sender, RoutedEventArgs e)
        {
            if (mainTableView.SelectedIndex >= 0) model.MainTableItems.RemoveAt(mainTableView.SelectedIndex);
        }

        private void linkChangeTableGridColor_Click(object sender, RoutedEventArgs e)
        {
            if (model.MainTableItems.Count == 0) return;
            model.MainTableItems[0].KeyForeground = new SolidColorBrush(Colors.Red);
            model.MainTableItems[0].ValueBackground = new SolidColorBrush(Colors.Green);
        }

        private void mainTableView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            labelChangedRow.Content = mainTableView.SelectedIndex.ToString();
        }

        private void onTableCellLostFocus(object sender, int column)
        {
            if (mainTableView.SelectedIndex > 0)
            {
                model.MainTableItems[0].Key = Program.Texts.Format("basic-grid-edited", mainTableView.SelectedIndex, column);
                model.MainTableItems[0].Value = (sender as TextBox).Text;
            }
        }

        private void tableKeyCell_LostFocus(object sender, RoutedEventArgs e)
        {
            onTableCellLostFocus(sender, 1);
        }

        private void tableValueCell_LostFocus(object sender, RoutedEventArgs e)
        {
            onTableCellLostFocus(sender, 2);
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

        private class MainTableItem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public MainTableItem()
            {
                Index = (tableItemIndex++).ToString();
                KeyForeground = new SolidColorBrush(Colors.Black);
            }

            public String Index { get; set; }

            public String Key
            {
                get => key;
                set { key = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key))); }
            }

            public String Value
            {
                get => val;
                set { val = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value))); }
            }

            public IBrush KeyForeground
            {
                get => keyForeground;
                set { keyForeground = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KeyForeground))); }
            }

            public IBrush ValueBackground
            {
                get => valBackground;
                set { valBackground = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueBackground))); }
            }

            private String key;
            private String val;
            private IBrush keyForeground;
            private IBrush valBackground;

            private static int tableItemIndex = 0;
        }

        private class Model
        {
            public ObservableCollection<CheckItem> CheckItems { get; set; }
            public ObservableCollection<MainTableItem> MainTableItems { get; set; }

            public Model()
            {
                CheckItems = new ObservableCollection<CheckItem>();
                MainTableItems = new ObservableCollection<MainTableItem>();
            }
        }

        private Model model = new Model();
        private int checkListTarget = 0;
    }
}