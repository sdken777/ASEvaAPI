using System;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto.Forms;
using ExampleLibrary;

namespace ASEvaAPIEtoTest
{
    partial class TestWindow
    {
        private void initPlotGroupBox(GroupBox groupBox)
        {
            var mainLayout = groupBox.SetContentAsRowLayout(8, 8, VerticalAlignment.Stretch);
            var treeView = mainLayout.AddControl(new SimpleTreeView(), false, 200) as SimpleTreeView;
            var plotView = mainLayout.AddControl(new OxyPlotView(), true) as OxyPlotView;

            ExampleLibrary.RenderingCapabilities.PixelScale = Pixel.Scale;

            var exampleTable = new Dictionary<String, List<ExampleInfo>>();
            foreach (var example in Examples.GetList())
            {
                if (!exampleTable.ContainsKey(example.Category)) exampleTable[example.Category] = new List<ExampleInfo>();
                exampleTable[example.Category].Add(example);
            }

            var categoryNodes = new List<SimpleTreeNode>();
            object targetKey = null;
            foreach (var pair in exampleTable)
            {
                var categoryNode = new SimpleTreeNode();
                categoryNode.Key = categoryNode.Text = pair.Key;
                foreach (var example in pair.Value)
                {
                    var exampleNode = new SimpleTreeNode();
                    exampleNode.Key = example;
                    exampleNode.Text = example.Title;
                    categoryNode.ChildNodes.Add(exampleNode);
                    if (example.Title == "Peaks") targetKey = example;
                }
                categoryNodes.Add(categoryNode);
            }
            treeView.SetModel(categoryNodes.ToArray(), true);

            treeView.SelectedItemChanged += delegate
            {
                var selected = treeView.GetSelectedKey();
                if (selected is ExampleInfo) plotView.SetModel((selected as ExampleInfo).PlotModel);
            };

            treeView.SelectItem(targetKey);
        }
    }
}