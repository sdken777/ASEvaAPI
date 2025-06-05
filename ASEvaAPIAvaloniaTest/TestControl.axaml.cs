using System;
using Avalonia;
using Avalonia.Controls;
using ASEva;
using ASEva.UIAvalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class TestControl : Panel
    {
        public TestControl()
        {
            InitializeComponent();
            language = new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");

            labelRow1.Content = language.Format("basic-label-row", ++flowItemCount);
            labelRow2.Content = language.Format("basic-label-row", ++flowItemCount);
        }

        private static int flowItemCount = 0;
        private LanguageSwitch language;
    }
}