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

            buttonOK.Click += delegate
            {
                Close();
            };
        }

        public Language Language
        {
            get { return radioChinese.IsChecked.Value ? Language.Chinese : Language.English; }
        }
    }
}