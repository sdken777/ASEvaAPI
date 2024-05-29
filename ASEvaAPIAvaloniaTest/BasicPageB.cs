using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageB : Panel
    {
        public BasicPageB()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);
        }
    }
}