using System;
using System.Threading.Tasks;
using ASEva;

namespace GeneralHostAvalonia
{
    class EtoHostConfigPanel : ASEva.UIEto.ConfigPanel
    {
        public EtoHostConfigPanel(ASEva.UIAvalonia.ConfigPanel configPanel)
        {
            avaloniaConfigPanel = configPanel;
            var avaloniaContainer = new AvaloniaPanelContainer(configPanel);
            var skiaView = ASEva.UIEto.SetContentAsControlExtension.SetContentAsControl(this, new ASEva.UIEto.SkiaView(), 0);
            common = new EtoHostPanelCommon(avaloniaContainer, skiaView);

            avaloniaConfigPanel.CloseRequested += delegate { Close(); };
            
            SizeChanged += delegate
            {
                var containerSize = ASEva.UIEto.SizerExtensions.GetLogicalSize(this);
                avaloniaConfigPanel.Width = containerSize.Width;
                avaloniaConfigPanel.Height = containerSize.Height;
                common.Initialize();
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
            common.StopTimer();
            avaloniaConfigPanel.OnRelease();
            common.CloseContainer();
        }

        public override void OnUpdateUI()
        {
            if (common.IsValid) avaloniaConfigPanel.OnUpdateUI();
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
        private EtoHostPanelCommon common;
    }
}