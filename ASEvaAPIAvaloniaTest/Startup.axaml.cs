using System;
using ASEva;
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

        public Language Language
        {
            get { return radioChinese.IsChecked.Value ? Language.Chinese : Language.English; }
        }

        private void Button_Click(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}