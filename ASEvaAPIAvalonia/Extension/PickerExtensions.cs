using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.1) Extensions for select file and folder
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.1) 方便选择文件和文件夹
    /// </summary>
    public static class PickerExtensions
    {
        public static async Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(this Control control, FilePickerOpenOptions options)
        {
            var parentWindow = control.GetParentWindow();
            if (parentWindow == null) return [];
            return await parentWindow.StorageProvider.OpenFilePickerAsync(options);
        }

        public static async Task<IStorageFile?> SaveFilePickerAsync(this Control control, FilePickerSaveOptions options)
        {
            var parentWindow = control.GetParentWindow();
            if (parentWindow == null) return null;
            return await parentWindow.StorageProvider.SaveFilePickerAsync(options);
        }

        public static async Task<IReadOnlyList<IStorageFolder>> OpenFolderPickerAsync(this Control control, FolderPickerOpenOptions options)
        {
            var parentWindow = control.GetParentWindow();
            if (parentWindow == null) return [];
            return await parentWindow.StorageProvider.OpenFolderPickerAsync(options);
        }
    }
}