using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageC : Panel
    {
        public BasicPageC()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);
        }
    }
}