using System;
using System.ComponentModel;
using System.Threading;
using ASEva;
using ASEva.UIAvalonia;
using ASEva.Utility;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;

namespace ASEvaAPIAvaloniaTest
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);

            tabA.Header = texts.Format(tabA.Header as String, "A");
            tabB.Header = texts.Format(tabB.Header as String, "B");
            tabC.Header = texts.Format(tabC.Header as String, "C");
            tabD.Header = texts.Format(tabD.Header as String, "D");
            tabE.Header = texts.Format(tabE.Header as String, "E");
            itemSubA.Header = texts.Format(itemSubA.Header as String, "A");
            itemSubB.Header = texts.Format(itemSubB.Header as String, "B");
            itemCheckA.Header = texts.Format(itemCheckA.Header as String, "A");
            itemCheckB.Header = texts.Format(itemCheckB.Header as String, "B");
            itemRadioA.Header = texts.Format(itemRadioA.Header as String, "A");
            itemRadioB.Header = texts.Format(itemRadioB.Header as String, "B");

            DataContext = new Model();
        }

        private void itemFullScreen_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.FullScreen;
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void itemCheckA_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Model;
            model.CheckedA = !model.CheckedA;
        }

        private void itemCheckB_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Model;
            model.CheckedB = !model.CheckedB;
        }

        private void itemRadioA_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Model;
            model.SelectedB = false;
        }

        private void itemRadioB_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Model;
            model.SelectedB = true;
        }

        private void itemSnapshot_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void itemExceptionMain_Click(object sender, RoutedEventArgs e)
        {
            String a = null;
            Console.WriteLine(a.Length.ToString());
        }

        private void itemExceptionSub_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                String a = null;
                Console.WriteLine(a.Length.ToString());
            });
            thread.Start();
        }

        private void itemShowEtoEmbedWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new EtoEmbedWindow();
            window.Show();
        }

        private void itemShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new EtoWindow();
            window.Show();
        }

        private void itemShowEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EtoDialog();
            ASEva.UIEto.App.RunDialog(dialog);
        }

        private class Model : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public bool CheckedA
            {
                get => checkedA;
                set { checkedA = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckedA))); }
            }

            public bool CheckedB
            {
                get => checkedB;
                set { checkedB = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckedB))); }
            }

            public bool SelectedB
            {
                get => selectedB;
                set { selectedB = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedB))); }
            }
            
            private bool checkedA;
            private bool checkedB;
            private bool selectedB;
        }
    }
}