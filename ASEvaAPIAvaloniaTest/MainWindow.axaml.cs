using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonShowEtoEmbedWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new EtoEmbedWindow(checkChineseLanguage.IsChecked.Value ? Language.Chinese : Language.English, checkOnscreenRendering.IsChecked.Value);
            window.Show();
        }

        private void buttonShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new EtoWindow(checkChineseLanguage.IsChecked.Value ? Language.Chinese : Language.English, checkOnscreenRendering.IsChecked.Value);
            window.Show();
        }

        private void buttonShowEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EtoDialog(checkChineseLanguage.IsChecked.Value ? Language.Chinese : Language.English, checkOnscreenRendering.IsChecked.Value);
            ASEva.UIEto.App.RunDialog(dialog);
        }
    }
}