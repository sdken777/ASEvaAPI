using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ASEva;

namespace ASEvaAPIAvaloniaTest
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            foreach (var window in windows) window.Close();
            foreach (var form in forms) form.Close();
        }

        private void buttonShowAvaloniaWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new SubWindow(checkChineseLanguage.IsChecked.Value ? Language.Chinese : Language.English, checkOnscreenRendering.IsChecked.Value);
            window.Show();
            windows.Add(window);
        }

        private void buttonShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            var form = new EtoForm(checkChineseLanguage.IsChecked.Value ? Language.Chinese : Language.English, checkOnscreenRendering.IsChecked.Value);
            form.Show();
            forms.Add(form);
        }

        private void buttonShowTestDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialog();
            dialog.ShowDialog(this);
        }

        List<SubWindow> windows = new List<SubWindow>();
        List<EtoForm> forms = new List<EtoForm>();
    }
}