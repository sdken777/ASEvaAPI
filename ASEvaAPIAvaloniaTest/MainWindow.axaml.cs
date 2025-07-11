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
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language);
            DataContext = new Model();

            Closing += MainWindow_Closing;
            itemFullScreen.Click += itemFullScreen_Click;
            itemExit.Click += itemExit_Click;
            itemSnapshot.Click += itemSnapshot_Click;
            itemExceptionMain.Click += itemExceptionMain_Click;
            itemExceptionSub.Click += itemExceptionSub_Click;
            itemShowEtoWindow.Click += itemShowEtoWindow_Click;
            itemShowEtoDialog.Click += itemShowEtoDialog_Click;

            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += delegate
            {
                if (tabControl.SelectedContent == basicPageA) basicPageA.OnLoop();
                drawGroup.OnLoop();
            };
            timer.Start();
        }

        private async void MainWindow_Closing(object sender, WindowClosingEventArgs e)
        {
            if (exitConfirmed || e.CloseReason != WindowCloseReason.WindowClosing)
            {
                timer.Stop();
                return;
            }

            e.Cancel = true;

            if (exitConfirming) return;
            exitConfirming = true;

            await App.RunDialog(async (window) => 
            {
                exitConfirmed = await MessageBox.Show(window, language["exit-confirm"], "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == MessageBoxResult.Yes;
            });
            if (exitConfirmed) Close();

            exitConfirming = false;
        }
        private bool exitConfirmed = false;
        private bool exitConfirming = false;

        private void itemFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            WindowState = WindowState.FullScreen;
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void itemSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapshot = this.Snapshot();
            if (snapshot != null)
            {
                var dialog = new ImageDialog(snapshot);
                await App.RunDialog(dialog);
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

        private void itemShowEtoWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Program.DesignerMode) return;
            var window = new EtoEmbedWindow();
            window.Show();
        }

        private async void itemShowEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            if (Program.DesignerMode) return;
            var dialog = new EtoDialog();
            await ASEva.UIEto.App.RunDialog(dialog);
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

        private DispatcherTimer timer = new DispatcherTimer();
        private LanguageSwitch language;
    }
}