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
            language = new LanguageSwitch(Resources, Program.Language);
        }

        public void OnLoop()
        {
            drawDefault.OnLoop();
            drawSkia.OnLoop();
            drawGL.OnLoop();
        }

        private LanguageSwitch language;
    }
}