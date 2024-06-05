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

            this.AddToResources(Program.Texts);
        }

        public void OnLoop()
        {
            drawDefault.OnLoop();
        }
    }
}