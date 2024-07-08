using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Threading;

namespace ASEva.UIAvalonia
{
    partial class EtoInitWindow : Window
    {
        public EtoInitWindow()
        {
            InitializeComponent();

            timer.Tick += delegate
            {
                if (App.EtoInitializeResult != null)
                {
                    timer.Stop();
                    Close();
                }
            };
            timer.Start();
        }

        private DispatcherTimer timer = new DispatcherTimer{ Interval = TimeSpan.FromMilliseconds(1) };
    }
}