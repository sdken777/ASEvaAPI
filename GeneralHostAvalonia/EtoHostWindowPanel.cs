using System;
using System.Threading.Tasks;
using ASEva;

namespace GeneralHostAvalonia
{
    class EtoHostWindowPanel : ASEva.UIEto.WindowPanel
    {
        public EtoHostWindowPanel(ASEva.UIAvalonia.WindowPanel windowPanel)
        {
            avaloniaWindowPanel = windowPanel;
            var avaloniaContainer = new AvaloniaPanelContainer(windowPanel);
            common = new EtoHostPanelCommon(avaloniaContainer, this);
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
            if (common.IsValid) avaloniaWindowPanel.OnUpdateUI();
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
                common.Initialize();
            }
        }

        public override void OnRelease()
        {
            common.StopTimer();
            avaloniaWindowPanel.OnRelease();
            common.CloseContainer();
        }

        private ASEva.UIAvalonia.WindowPanel avaloniaWindowPanel;
        private EtoHostPanelCommon common;
    }
}