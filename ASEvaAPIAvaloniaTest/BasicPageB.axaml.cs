using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageB : Panel
    {
        public BasicPageB()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);
            DataContext = model;

            var listBoxItems = new List<String>();
            for (int i = 1; i <= 1000; i++) listBoxItems.Add(texts.Format("basic-list-item", i));
            listBox.ItemsSource = listBoxItems;

            var checkListBoxItems = new List<String>();
            for (int i = 1; i <= 1000; i++) checkListBoxItems.Add(texts.Format("basic-list-item-short", i.ToString()));
            checkListBox.ItemsSource = checkListBoxItems;

            for (int i = 1; i <= 99; i++)
            {
                var parentNode = new Node(i.ToString(), texts.Format("basic-tree-parent", i.ToString("D2")));
                parentNode.Foreground = Brushes.Black;
                if (i % 2 == 0) parentNode.Background = Brushes.LightGray;
                if (i > 90) parentNode.IsExpanded = true;
                for (int j = 1; j <= 99; j++)
                {
                    var childNode = new Node(i + "." + j, texts.Format("basic-tree-child", j.ToString("D2")));
                    childNode.Foreground = j % 2 == 0 ? Brushes.Blue : Brushes.Black;
                    parentNode.SubNodes.Add(childNode);
                }
                model.TreeNodes.Add(parentNode);
            }
        }

        private async void treeView_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            var node = treeView.SelectedItem as Node;
            if (node != null) await App.RunDialog(async (window) => await MessageBox.Show(window, node.Key, ""));
        }

        private void linkSelectFirst_Click(object? sender, RoutedEventArgs e)
        {
            treeView.SelectedItem = model.TreeNodes[0];
        }

        private void linkChangeColor_Click(object? sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 97; i += 2)
            {
                model.TreeNodes[i].Foreground = Brushes.White;
                model.TreeNodes[i].Background = Brushes.DimGray;
            }
        }

        private async void linkAddControl_Click(object? sender, RoutedEventArgs e)
        {
            var item = new Item(model);
            model.ControlItems.Add(item);
            await Task.Delay(100);
            flowLayout.ScrollIntoView(item);
        }

        private void linkInsertControl_Click(object? sender, RoutedEventArgs e)
        {
            if (model.ControlItems.Count == 0) model.ControlItems.Add(new Item(model));
            else model.ControlItems.Insert(1, new Item(model));
        }

        private void linkRemoveControl_Click(object? sender, RoutedEventArgs e)
        {
            if (model.ControlItems.Count > 0) model.ControlItems.RemoveAt(model.ControlItems.Count / 2);
        }

        private void linkShowControl_Click(object? sender, RoutedEventArgs e)
        {
            if (model.ControlItems.Count > 0) model.ControlItems[0].IsVisible = true;
        }

        private void linkHideControl_Click(object? sender, RoutedEventArgs e)
        {
            if (model.ControlItems.Count > 0) model.ControlItems[0].IsVisible = false;
        }

        private void linkSelectControl_Click(object? sender, RoutedEventArgs e)
        {
            if (model.ControlItems.Count > 0) model.SelectedControlItem = model.ControlItems[0];
        }

        private async void testControl_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            var item = (sender as TestControl)?.DataContext as Item;
            if (item == null) return;

            model.SelectedControlItem = item;

            var itemIndex = model.ControlItems.IndexOf(item);
            await App.RunDialog(async (window) => await MessageBox.Show(window, Program.Texts.Format("basic-flow-selected", itemIndex), ""));
        }

        private class Node(String key, String titleArg) : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public String Key { get; set; } = key;
            public ObservableCollection<Node> SubNodes { get; set; } = [];

            public String Title
            {
                get => title;
                set { title = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title))); }
            }

            public IBrush? Foreground
            {
                get => foreground;
                set { foreground = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Foreground))); }
            }

            public IBrush? Background
            {
                get => background;
                set { background = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Background))); }
            }

            public bool IsExpanded
            {
                get => isExpanded;
                set { isExpanded = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded))); }
            }

            private String title = titleArg;
            private IBrush? foreground;
            private IBrush? background;
            private bool isExpanded;
        }

        private class Item : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public Item(Model model)
            {
                this.model = model;
            }

            public bool IsVisible
            {
                get => isVisible;
                set { isVisible = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible))); }
            }

            public IBrush BorderBrush
            {
                get => this.Equals(model.SelectedControlItem) ? Brushes.Gray : Brushes.LightGray;
            }

            public void NotifyPropertyChanged(String name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            private Model model;
            private bool isVisible = true;
        }

        private class Model
        {
            public ObservableCollection<Item> ControlItems { get; private set; } = [];
            public ObservableCollection<Node> TreeNodes { get; private set; } = [];

            public Item? SelectedControlItem
            {
                get => selectedControlItem;
                set
                {
                    if (selectedControlItem == value) return;
                    selectedControlItem = value;
                    foreach (var item in ControlItems) item.NotifyPropertyChanged(nameof(Item.BorderBrush));
                }
            }

            private Item? selectedControlItem = null;
        }

        private Model model = new();
    }
}