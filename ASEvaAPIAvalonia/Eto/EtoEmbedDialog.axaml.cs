using System;
using System.IO;
using System.Linq;
using ASEva.UIEto;
using Avalonia;
using Avalonia.Controls;

namespace ASEva.UIAvalonia
{
    partial class EtoEmbedDialog : Window
    {
        public EtoEmbedDialog()
        {
            InitializeComponent();
        }

        public EtoEmbedDialog(DialogPanel panel)
        {
            InitializeComponent();
            embedder.EtoControl = panel;

            if (panel.Icon != null && panel.Icon.Frames.Count() > 0)
            {
                var stream = new MemoryStream();
                panel.Icon.Frames.First().Bitmap.Save(stream, Eto.Drawing.ImageFormat.Png);
                stream.Position = 0;
                Icon = new WindowIcon(stream);
                stream.Close();
            }

            Title = panel.Title;
            Width = panel.DefaultSize.Width;
            Height = panel.DefaultSize.Height;
            MinWidth = panel.MinSize.Width;
            MinHeight = panel.MinSize.Height;

            if (panel.Mode == DialogPanel.DialogMode.FixMode)
            {
                CanResize = false;
                if (!panel.WithBorder) SystemDecorations = SystemDecorations.None;
            }
        }
    }
}