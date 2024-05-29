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

            var texts = Program.Texts;
            this.AddToResources(texts);
        }
    }
}