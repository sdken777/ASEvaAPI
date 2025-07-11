using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using ASEva.UIAvalonia;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Embedding;

namespace HwndHostAvalonia
{
    class DialogHandler : AvaloniaDialogHandler
    {
        public async Task RunDialog(Window window, object owner)
        {
            await Task.Delay(10);

            var windowHandle = window.TryGetPlatformHandle();
            if (windowHandle == null || windowHandle.Handle == 0) return;

            nint parentHwnd = 0;
            if (owner is Visual visual)
            {
                if (TopLevel.GetTopLevel(visual) is Window parentWindow)
                {
                    await window.ShowDialog(parentWindow);
                    return;
                }

                parentHwnd = getVisualParentHwnd(visual);
            }
            
            if (parentHwnd == 0)
            {
                var activeForm = Form.ActiveForm;
                if (activeForm != null)
                {
                    parentHwnd = activeForm.Handle;
                }
                else
                {
                    var activeWindow = System.Windows.Application.Current?.MainWindow;
                    if (activeWindow != null && activeWindow.IsActive)
                    {
                        parentHwnd = new WindowInteropHelper(activeWindow).Handle;
                    }
                    else
                    {
                        var windows = System.Windows.Application.Current?.Windows;
                        if (windows != null)
                        {
                            foreach (System.Windows.Window wpfWindow in windows)
                            {
                                if (wpfWindow.IsActive)
                                {
                                    parentHwnd = new WindowInteropHelper(wpfWindow).Handle;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (parentHwnd != 0)
            {
                EnableWindow(parentHwnd, false);

                window.Show();
                SetWindowLongPtrImp(windowHandle.Handle, GetWindowLongIndexes.GWL_HWNDPARENT, parentHwnd);

                window.Closing += (o, e) =>
                {
                    if (e.Cancel) return;
                    SetWindowLongPtrImp(windowHandle.Handle, GetWindowLongIndexes.GWL_HWNDPARENT, 0);
                    EnableWindow(parentHwnd, true);
                };
            }
        }

        public async Task ShowMessageBox(string message, string caption, bool isError)
        {
            await Task.Delay(10);
            MessageBox.Show(message, caption, MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        public bool IsActive(Visual visual)
        {
            if (TopLevel.GetTopLevel(visual) is not EmbeddableControlRoot root) return false;

            var hwndHost = AvaloniaEmbedder.GetHwndHost(root);
            if (hwndHost == null) return false;

            var rootWindow = System.Windows.Window.GetWindow(hwndHost);
            if (rootWindow != null)
            {
                return rootWindow.IsActive;
            }
            else
            {
                if (hwndHost is not AvaloniaEmbedder embedder) return false;

                var control = WinformControlMap.Get(embedder);
                if (control == null) return false;

                var parentForm = getParentForm(control);
                if (parentForm == null) return false;

                var activeForm = Form.ActiveForm;
                if (activeForm == null) return false;

                return parentForm.Equals(activeForm);
            }
        }

        private static Form getParentForm(System.Windows.Forms.Control control)
        {
            var parent = control.Parent;
            if (parent is Form form) return form;
            else if (parent is System.Windows.Forms.Control parentControl) return getParentForm(parentControl);
            else return null;
        }

        private static nint getVisualParentHwnd(Visual visual)
        {
            if (TopLevel.GetTopLevel(visual) is not EmbeddableControlRoot root) return 0;

            var hwndHost = AvaloniaEmbedder.GetHwndHost(root);
            if (hwndHost == null) return 0;

            var rootWindow = System.Windows.Window.GetWindow(hwndHost);
            if (rootWindow != null)
            {
                return new WindowInteropHelper(rootWindow).Handle;
            }
            else
            {
                if (hwndHost is not AvaloniaEmbedder embedder) return 0;

                var control = WinformControlMap.Get(embedder);
                if (control == null) return 0;

                var parentForm = getParentForm(control);
                if (parentForm == null) return 0;

                return parentForm.Handle;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetWindowLongPtrW", ExactSpelling = true, SetLastError = true)]
        private static extern nint SetWindowLongPtrImp(nint hWnd, GetWindowLongIndexes nIndex, nint dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnableWindow", ExactSpelling = true, SetLastError = true)]
        private static extern bool EnableWindow(nint hWnd, bool bEnable);

        private enum GetWindowLongIndexes
        {
            GWL_HWNDPARENT = -8,
        }
    }
}