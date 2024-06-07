using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class TestControl : Panel
    {
        public TestControl()
        {
            InitializeComponent();

            labelRow1.Content = Program.Texts.Format("basic-label-row", ++flowItemCount);
            labelRow2.Content = Program.Texts.Format("basic-label-row", ++flowItemCount);
        }

        private static int flowItemCount = 0;
    }
}