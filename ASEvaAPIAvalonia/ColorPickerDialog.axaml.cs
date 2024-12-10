using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.2.8) Color picker dialog
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.2.8) 颜色选择对话框
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        public ColorPickerDialog()
        {
            InitializeComponent();
#if !ASEVA_API_BUNDLE_MODE
            if (App.MainWindow != null) Icon = App.MainWindow.Icon;
#endif
        }

        public Color Color
        {
            get => color;
            set
            {
                view.Color = value;
                color = value;
            }
        }
        private Color color;

        public bool SupportAlpha
        {
            get => supportAlpha;
            set
            {
                view.IsAlphaEnabled = view.IsAlphaVisible = value;
                supportAlpha = value;
            }
        }
        private bool supportAlpha = true;

        private void buttonOK_Click(object sender, RoutedEventArgs args)
        {
            color = view.Color;
            this.Close();
        }
    }
}