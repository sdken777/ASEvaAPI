using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageD : Panel
    {
        public BasicPageD()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }
    }
}