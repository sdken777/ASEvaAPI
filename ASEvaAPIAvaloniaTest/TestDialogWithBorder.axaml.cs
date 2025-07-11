using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class TestDialogWithBorder : Window
    {
        public TestDialogWithBorder()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language);

            linkClientSize.Click += linkClientSize_Click;
            linkShowDialogWithBorder.Click += linkShowDialogWithBorder_Click;
            buttonClose.Click += buttonClose_Click;
        }

        private void linkClientSize_Click(object sender, RoutedEventArgs e)
        {
            linkClientSize.Content = ClientSize.Width + "x" + ClientSize.Height;
        }

        private async void linkShowDialogWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialogWithBorder();
            await App.RunDialog(dialog, this);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private LanguageSwitch language;
    }
}