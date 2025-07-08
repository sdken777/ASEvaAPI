using System;
using System.Threading.Tasks;
using Avalonia.Headless;
using SkiaSharp;
using ASEva;
using ASEva.UIEto;

namespace GeneralHostAvalonia
{
    class EtoHostConfigPanel : ASEva.UIEto.ConfigPanel
    {
        public EtoHostConfigPanel(ASEva.UIAvalonia.ConfigPanel configPanel)
        {
            avaloniaConfigPanel = configPanel;
            avaloniaContainer = new AvaloniaPanelContainer(configPanel);
            skiaView = ASEva.UIEto.SetContentAsControlExtension.SetContentAsControl(this, new ASEva.UIEto.SkiaView(), 0);

            avaloniaConfigPanel.CloseRequested += delegate { Close(); };
            
            SizeChanged += delegate
            {
                var containerSize = this.GetLogicalSize();
                avaloniaConfigPanel.Width = containerSize.Width;
                avaloniaConfigPanel.Height = containerSize.Height;

                if (!containerShown)
                {
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
                        if (skiaImage != null) e.Canvas.DrawImage(skiaImage, 8, 8);
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
            };
        }

        public override void OnInitSize(string config)
        {
            avaloniaConfigPanel.OnInitSize(config);
        }

        public override IntSize OnGetSize()
        {
            return new IntSize((int)avaloniaConfigPanel.Width, (int)avaloniaConfigPanel.Height);
        }

        public override void OnInit(string config)
        {
            avaloniaConfigPanel.OnInit(config);
        }

        public override void OnRelease()
        {
            timer.Stop();
            avaloniaConfigPanel.OnRelease();
            if (containerShown) avaloniaContainer.Close();
        }

        public override void OnUpdateUI()
        {
            if (containerShown) avaloniaConfigPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            avaloniaConfigPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return avaloniaConfigPanel.OnHandleAsync();
        }

        private ASEva.UIAvalonia.ConfigPanel avaloniaConfigPanel;
        private AvaloniaPanelContainer avaloniaContainer;
        private bool containerShown = false;
        private ASEva.UIEto.SkiaView skiaView;
        private SKImage skiaImage;
        private Eto.Forms.UITimer timer = new Eto.Forms.UITimer { Interval = 0.015 };
    }
}