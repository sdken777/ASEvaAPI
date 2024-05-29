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
        public MainWindow() // For designer
        {
            InitializeComponent();
            this.language = Language.English;
            this.initialize();
        }

        public MainWindow(Language language)
        {
            InitializeComponent();
            this.language = language;
            this.initialize();
        }

        public static TextResource Texts { get; private set; }

        private void initialize()
        {
            Texts = TextResource.Load("test.xml", language);
            this.AddToResources(Texts);

            itemSubA.Header = Texts.Format(itemSubA.Header as String, "A");
            itemSubB.Header = Texts.Format(itemSubB.Header as String, "B");
            itemCheckA.Header = Texts.Format(itemCheckA.Header as String, "A");
            itemCheckB.Header = Texts.Format(itemCheckB.Header as String, "B");
            itemRadioA.Header = Texts.Format(itemRadioA.Header as String, "A");
            itemRadioB.Header = Texts.Format(itemRadioB.Header as String, "B");

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
            var window = new EtoEmbedWindow(language);
            window.Show();
        }

        private void itemShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new EtoWindow(language);
            window.Show();
        }

        private void itemShowEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EtoDialog(language);
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

        private Language language;
    }
}