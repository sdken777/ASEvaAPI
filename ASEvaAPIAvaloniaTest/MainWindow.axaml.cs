using System;
using System.ComponentModel;
using System.Threading;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace ASEvaAPIAvaloniaTest
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
            DataContext = new Model();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += delegate
            {
                if (tabControl.SelectedContent == basicPageA) basicPageA.OnLoop();
            };
            timer.Start();
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

        private async void itemSnapshot_Click(object sender, RoutedEventArgs e)
        {
            using (var bitmap = new RenderTargetBitmap(new PixelSize((int)Width, (int)Height), new Vector(96, 96)))
            {
                bitmap.Render(this);
                var dialog = new ImageDialog(bitmap);
                await dialog.ShowDialog(this);
            }
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
            if (Program.DesignerMode) return;
            var window = new EtoEmbedWindow();
            window.Show();
        }

        private void itemShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Program.DesignerMode) return;
            var window = new EtoWindow();
            window.Show();
        }

        private void itemShowEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            if (Program.DesignerMode) return;
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