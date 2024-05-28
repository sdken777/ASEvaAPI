using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ASEvaAPIAvaloniaTest
{
    partial class Startup : Window
    {
        public Startup()
        {
            InitializeComponent();
        }

        public bool Chinese
        {
            get { return radioChinese.IsChecked.Value; }
        }

        private void Button_Click(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}