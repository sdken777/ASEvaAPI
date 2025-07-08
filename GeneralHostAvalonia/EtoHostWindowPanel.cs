using System;
using System.Threading.Tasks;
using Avalonia.Headless;
using SkiaSharp;
using ASEva;

namespace GeneralHostAvalonia
{
    class EtoHostWindowPanel : ASEva.UIEto.WindowPanel
    {
        public EtoHostWindowPanel(ASEva.UIAvalonia.WindowPanel windowPanel)
        {
            avaloniaWindowPanel = windowPanel;
            avaloniaContainer = new AvaloniaPanelContainer(windowPanel);
            skiaView = ASEva.UIEto.SetContentAsControlExtension.SetContentAsControl(this, new ASEva.UIEto.SkiaView(), 0);
        }

        public override void OnInitSize(string config)
        {
            avaloniaWindowPanel.OnInitSize(config);
        }

        public override IntSize OnGetMinimumSize()
        {
            return new IntSize((int)Math.Ceiling(avaloniaWindowPanel.MinWidth), (int)Math.Ceiling(avaloniaWindowPanel.MinHeight));
        }

        public override IntSize OnGetDefaultSize()
        {
            return new IntSize((int)avaloniaWindowPanel.Width, (int)avaloniaWindowPanel.Height);
        }

        public override void OnInit(string config)
        {
            avaloniaWindowPanel.OnInit(config);
        }

        public override string OnGetConfig()
        {
            return avaloniaWindowPanel.OnGetConfig();
        }

        public override void OnInputData(object data)
        {
            avaloniaWindowPanel.OnInputData(data);
        }

        public override void OnResetData()
        {
            avaloniaWindowPanel.OnResetData();
        }

        public override void OnStartSession()
        {
            avaloniaWindowPanel.OnStartSession();
        }

        public override void OnStopSession()
        {
            avaloniaWindowPanel.OnStopSession();
        }

        public override void OnUpdateUI()
        {
            if (containerShown) avaloniaWindowPanel.OnUpdateUI();
        }

        public override void OnHandleModal()
        {
            avaloniaWindowPanel.OnHandleModal();
        }

        public override Task OnHandleAsync()
        {
            return avaloniaWindowPanel.OnHandleAsync();
        }

        public override void OnUpdateContainerSize(IntSize containerSize)
        {
            if (containerSize.Width > 0 && containerSize.Height > 0)
            {
                avaloniaWindowPanel.Width = containerSize.Width;
                avaloniaWindowPanel.Height = containerSize.Height;

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
            }
        }

        public override void OnRelease()
        {
            timer.Stop();
            avaloniaWindowPanel.OnRelease();
            if (containerShown) avaloniaContainer.Close();
        }

        private ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel;
        private AvaloniaPanelContainer avaloniaContainer;
        private bool containerShown = false;
        private ASEva.UIEto.SkiaView skiaView;
        private SKImage skiaImage;
        private Eto.Forms.UITimer timer = new Eto.Forms.UITimer { Interval = 0.015 };
    }
}