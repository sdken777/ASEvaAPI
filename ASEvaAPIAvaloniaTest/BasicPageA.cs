using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageA : Panel
    {
        public BasicPageA()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);
        }

        private void itemMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(App.WorkPath, "");
        }
    }
}