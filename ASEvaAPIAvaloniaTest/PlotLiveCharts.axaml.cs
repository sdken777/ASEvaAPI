using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class PlotLiveCharts : Panel
    {
        public PlotLiveCharts()
        {
            InitializeComponent();

            DataContext = model;

            ViewModelsSamples.Index.AlwaysUseDefaultFont = ASEva.APIInfo.GetRunningOS() == "linux";

            var exampleTable = new Dictionary<String, List<String>>();
            foreach (var path in ViewModelsSamples.Index.Samples)
            {
                var pathComps = path.Split('/');
                var category = pathComps[0];
                var title = pathComps[1];
                if (!exampleTable.ContainsKey(category)) exampleTable[category] = [];
                exampleTable[category].Add(title);
            }

            var categories = exampleTable.Keys.ToList();
            categories.Sort();

            var sortedTable = new Dictionary<String, List<String>>();
            foreach (var category in categories)
            {
                sortedTable[category] = exampleTable[category];
                sortedTable[category].Sort((s1, s2) => s1.CompareTo(s2));
            }
            exampleTable = sortedTable;

            Node? targetCategoryNode = null, targetExampleNode = null;
            foreach (var pair in exampleTable)
            {
                var category = pair.Key;
                var categoryNode = new Node(category);
                foreach (var title in pair.Value)
                {
                    var exampleNode = new Node(title);
                    exampleNode.ClassName = "AvaloniaSample." + category + "." + title + ".View";
                    categoryNode.SubNodes.Add(exampleNode);
                    if (title == "Race")
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
                if (exampleNode?.ClassName == null) return;

                var view = typeof(ViewModelsSamples.Index).Assembly.CreateInstance(exampleNode.ClassName) as ContentControl;
                if (view == null) return;

                if (container.Children.Count > 0) container.Children.Clear();
                container.Children.Add(view);
            }
        }

        private class Node(String titleArg) : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public String? ClassName { get; set; }
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
            public ObservableCollection<Node> TreeNodes { get; private set; }

            public Model()
            {
                TreeNodes = [];
            }
        }

        private Model model = new();
    }
}