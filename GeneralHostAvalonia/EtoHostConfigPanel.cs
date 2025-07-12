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
            common = new EtoHostPanelCommon(avaloniaContainer, this);

            avaloniaConfigPanel.CloseRequested += delegate { Close(); };
            
            SizeChanged += delegate
            {
                common.Initialize(); // 仅首次调用有效
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
            common.CloseAvaloniaWindow();
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