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
            new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");
        }

        public void OnLoop()
        {
            drawDefault.OnLoop();
            drawSkia.OnLoop();
            drawGL.OnLoop();
        }
    }
}