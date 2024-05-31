using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageE : Panel
    {
        public BasicPageE()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }
    }
}