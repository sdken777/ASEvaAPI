using System;
using System.Threading.Tasks;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;

namespace GeneralHostAvalonia
{
    class DialogHandler : AvaloniaDialogHandler
    {
        public bool IsActive(Visual visual)
        {
            if (TopLevel.GetTopLevel(visual) is Window avaloniaWindow)
            {
                var etoPanel = EtoHostPanelCommon.GetEtoPanel(avaloniaWindow);
                if (etoPanel == null) return false;
                else return ASEva.UIEto.TopMostExtensions.IsTopMost(etoPanel);
            }
            return false;
        }

        public async Task RunDialog(Window window, object owner)
        {
            await Task.Delay(10);

            var dialogPanel = new DialogPanelHost(window);
            var common = new EtoHostPanelCommon(window, dialogPanel);
            common.Initialize();

            await ASEva.UIEto.App.RunDialog(dialogPanel);
            common.StopTimer();

            dialogPanel.SystemClose = true;
            common.CloseAvaloniaWindow();
        }

        public async Task ShowMessageBox(string message, string caption, bool isError)
        {
            await Task.Delay(10);
            await ASEva.UIEto.App.ShowMessageBox(message, caption, isError ? ASEva.LogLevel.Error : ASEva.LogLevel.Info);
        }

        private class DialogPanelHost : ASEva.UIEto.DialogPanel
        {
            public DialogPanelHost(Window avaloniaWindow)
            {
                Title = avaloniaWindow.Title;
                if (avaloniaWindow.CanResize) SetResizableMode((int)avaloniaWindow.MinWidth, (int)avaloniaWindow.MinHeight, (int)avaloniaWindow.Width, (int)avaloniaWindow.Height);
                else SetFixMode((int)avaloniaWindow.Width, (int)avaloniaWindow.Height, avaloniaWindow.SystemDecorations != SystemDecorations.None);

                avaloniaWindow.Closing += delegate
                {
                    if (!SystemClose) Close();
                };
            }

            public bool SystemClose { private get; set; }
        }
    }
}