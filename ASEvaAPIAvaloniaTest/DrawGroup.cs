using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawGroup : Panel
    {
        public DrawGroup()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);
        }
    }
}