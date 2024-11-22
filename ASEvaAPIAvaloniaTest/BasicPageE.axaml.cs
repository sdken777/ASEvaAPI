using System;
using System.Linq;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageE : Panel
    {
        public BasicPageE()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }

        private void linkAdd_Click(object sender, RoutedEventArgs e)
        {
            var control = new ControlWithBorder();
            control.PointerReleased += control_PointerReleased;
            flowLayout.Children.Add(control);
        }

        private void linkRemove_Click(object sender, RoutedEventArgs e)
        {
            if (flowLayout.Children.Count > 0) flowLayout.Children.RemoveAt(flowLayout.Children.Count / 2);
        }

        private void linkInsert_Click(object sender, RoutedEventArgs e)
        {
            var control = new ControlWithBorder();
            control.PointerReleased += control_PointerReleased;
            flowLayout.Children.Insert(1, control);
        }

        private void linkSelect_Click(object sender, RoutedEventArgs e)
        {
            selectControl(0);
        }

        private void linkShow_Click(object sender, RoutedEventArgs e)
        {
            if (flowLayout.Children.Count > 0) flowLayout.Children[0].IsVisible = true;
        }

        private void linkHide_Click(object sender, RoutedEventArgs e)
        {
            if (flowLayout.Children.Count > 0) flowLayout.Children[0].IsVisible = false;
        }

        private void linkBigger_Click(object sender, RoutedEventArgs e)
        {
            foreach (ControlWithBorder border in flowLayout.Children)
            {
                var control = border.Child;
                control.Width = 350;
                control.Height = 120;
            }
        }

        private void linkSmaller_Click(object sender, RoutedEventArgs e)
        {
            foreach (ControlWithBorder border in flowLayout.Children)
            {
                var control = border.Child;
                control.Width = 250;
                control.Height = 80;
            }
        }

        private async void control_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            var target = sender as ControlWithBorder;
            var targetIndex = flowLayout.Children.IndexOf(target);
            selectControl(targetIndex);
            await App.RunDialog(async (window) => await MessageBox.Show(window, Program.Texts.Format("basic-flow-selected", targetIndex), ""));
        }

        private void selectControl(int index)
        {
            var children = flowLayout.Children.ToArray();
            for (int i = 0; i < children.Length; i++) (children[i] as ControlWithBorder).IsSelected = i == index;
        }

        private class ControlWithBorder : Border
        {
            public ControlWithBorder()
            {
                Margin = new Thickness(4);
                Background = new SolidColorBrush(Colors.LightYellow);
                BorderThickness = new Thickness(2);
                BorderBrush = new SolidColorBrush(Colors.LightGray);
                CornerRadius = new CornerRadius(3);
                Child = new TestControl{ Width = 250, Height = 80 };
            }

            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    if (value == isSelected) return;
                    isSelected = value;
                    BorderBrush = new SolidColorBrush(value ? Colors.Gray : Colors.LightGray);
                }
            }

            private bool isSelected = false;
        }
    }
}