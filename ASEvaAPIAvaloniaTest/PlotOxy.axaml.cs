using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ExampleLibrary;

namespace ASEvaAPIAvaloniaTest
{
    partial class PlotOxy : Panel
    {
        public PlotOxy()
        {
            InitializeComponent();

            DataContext = model;

            RenderingCapabilities.PixelScale = 1;

            var exampleTable = new Dictionary<String, List<ExampleInfo>>();
            foreach (var example in Examples.GetList())
            {
                if (!exampleTable.ContainsKey(example.Category)) exampleTable[example.Category] = [];
                exampleTable[example.Category].Add(example);
            }

            var categories = exampleTable.Keys.ToList();
            categories.Sort();

            var sortedTable = new Dictionary<String, List<ExampleInfo>>();
            foreach (var category in categories)
            {
                sortedTable[category] = exampleTable[category];
                sortedTable[category].Sort((e1, e2) => e1.Title.CompareTo(e2.Title));
            }
            exampleTable = sortedTable;

            Node? targetCategoryNode = null, targetExampleNode = null;
            foreach (var pair in exampleTable)
            {
                var categoryNode = new Node(pair.Key);
                foreach (var example in pair.Value)
                {
                    var exampleNode = new Node(example.Title);
                    exampleNode.Example = example;
                    categoryNode.SubNodes.Add(exampleNode);
                    if (example.Title == "Peaks")
                    {
                        targetCategoryNode = categoryNode;
                        targetExampleNode = exampleNode;
                    }
                }
                model.TreeNodes.Add(categoryNode);
            }
            if (targetCategoryNode != null) targetCategoryNode.IsExpanded = true;
            
            Loaded += delegate
            {
                if (targetExampleNode != null) treeView.SelectedItem = targetExampleNode;
            };
        }

        private void treeView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                var exampleNode = treeView.SelectedItem as Node;
                if (exampleNode?.Example != null) plotView.Model = exampleNode.Example.PlotModel;
            }
        }

        private class Node(String titleArg) : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public ExampleInfo? Example { get; set; }
            public ObservableCollection<Node> SubNodes { get; set; } = [];

            public String Title
            {
                get => title;
                set { title = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title))); }
            }

            public bool IsExpanded
            {
                get => isExpanded;
                set { isExpanded = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded))); }
            }

            private String title = titleArg;
            private bool isExpanded;
        }

        private class Model
        {
            public ObservableCollection<Node> TreeNodes { get; private set; } = [];
        }

        private Model model = new();
    }
}