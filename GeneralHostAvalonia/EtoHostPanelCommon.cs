using System;
using Avalonia.Headless;
using SkiaSharp;

namespace GeneralHostAvalonia
{
    class EtoHostPanelCommon(AvaloniaPanelContainer avaloniaContainer, ASEva.UIEto.SkiaView skiaView)
    {
        public void Initialize()
        {
            if (containerShown) return;

            avaloniaContainer.Show();
            containerShown = true;

            skiaView.MouseDown += (o, e) =>
            {
                avaloniaContainer.MouseDown(new Avalonia.Point(e.Location.X, e.Location.Y), Avalonia.Input.MouseButton.Left, Avalonia.Input.RawInputModifiers.None);
            };

            skiaView.MouseUp += (o, e) =>
            {
                avaloniaContainer.MouseUp(new Avalonia.Point(e.Location.X, e.Location.Y), Avalonia.Input.MouseButton.Left, Avalonia.Input.RawInputModifiers.None);
            };

            skiaView.Render += (o, e) =>
            {
                if (skiaImage != null) e.Canvas.DrawImage(skiaImage, 0, 0);
            };

            timer.Elapsed += delegate
            {
                avaloniaContainer.InvalidateVisual();
                var bitmap = avaloniaContainer.CaptureRenderedFrame();
                if (bitmap != null)
                {
                    var commonImage = ASEva.UIAvalonia.CommonImageAvaloniaExtensions.ToCommonImage(bitmap);
                    if (commonImage != null) skiaImage = ASEva.UIEto.CommonImageSkiaExtensions.ToSKImage(commonImage);
                }
                skiaView.QueueRender();
            };
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void CloseContainer()
        {
            if (containerShown && !containerClosed)
            {
                skiaView.Close();
                avaloniaContainer.Close();
                containerClosed = true;
            }
        }

        public bool IsValid => containerShown && !containerClosed;

        private bool containerShown = false;
        private bool containerClosed = false;
        private SKImage skiaImage;
        private Eto.Forms.UITimer timer = new Eto.Forms.UITimer { Interval = 0.015 };
    }
}