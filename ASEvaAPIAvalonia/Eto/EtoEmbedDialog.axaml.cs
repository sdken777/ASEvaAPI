using System;
using System.IO;
using System.Linq;
using ASEva.UIEto;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;

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

            var defaultWidth = panel.DefaultSize.Width;

            Title = panel.Title;
            Width = defaultWidth + 1;
            Height = panel.DefaultSize.Height;
            MinWidth = panel.MinSize.Width;
            MinHeight = panel.MinSize.Height;

            if (panel.Mode == DialogPanel.DialogMode.FixMode)
            {
                CanResize = false;
                if (!panel.WithBorder) SystemDecorations = SystemDecorations.None;
            }

            panel.OnDialogClose += delegate { Close(); };
            
            DispatcherTimer.RunOnce(() => Width = defaultWidth, TimeSpan.FromMilliseconds(1)); // 触发一次resize
        }
    }
}