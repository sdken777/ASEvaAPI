using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class DrawGL : Panel
    {
        public DrawGL()
        {
            InitializeComponent();

            this.AddToResources(Program.Texts);
        }

        public void OnLoop()
        {
            // glView.QueueRender();
        }
    }
}