using System;
using System.IO;
using System.Linq;
using ASEva.UIEto;
using ASEva.Utility;
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
                var frame = panel.Icon.Frames.First().Bitmap;
                if (frame != null)
                {
                    try
                    {
                        var commonImage = frame.ToCommonImage();
                        if (commonImage != null)
                        {
                            var avaloniaBitmap = commonImage.ToAvaloniaBitmap();
                            if (avaloniaBitmap != null)
                            {
                                Icon = new WindowIcon(avaloniaBitmap);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Dump.Exception(ex);
                    }
                }
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
            Closing += delegate { panel.OnClosing(); };
            
            DispatcherTimer.RunOnce(() => Width = defaultWidth, TimeSpan.FromMilliseconds(1)); // 触发一次resize
        }
    }
}