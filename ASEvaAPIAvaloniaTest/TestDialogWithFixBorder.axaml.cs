using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class TestDialogWithFixBorder : Window
    {
        public TestDialogWithFixBorder()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }

        private void linkClientSize_Click(object? sender, RoutedEventArgs e)
        {
            linkClientSize.Content = ClientSize.Width + "x" + ClientSize.Height;
        }

        private void linkShowDialogWithBorder_Click(object? sender, RoutedEventArgs e)
        {
            var dialog = new TestDialogWithBorder();
            dialog.ShowDialog(this);
        }

        private void buttonClose_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}