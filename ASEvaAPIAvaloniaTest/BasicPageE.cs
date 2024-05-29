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

            var texts = Program.Texts;
            this.AddToResources(texts);
        }
    }
}