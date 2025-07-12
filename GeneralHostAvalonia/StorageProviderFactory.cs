using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Platform.Storage;

namespace GeneralHostAvalonia
{
    class StorageProviderFactory : IStorageProviderFactory
    {
        public IStorageProvider CreateProvider(TopLevel topLevel)
        {
            return new StorageProvider(topLevel);
        }
    }

    class StorageProvider : IStorageProvider
    {
        public StorageProvider(TopLevel topLevel)
        {
            if (topLevel is Window window)
            {
                var etoPanel = EtoHostPanelCommon.GetEtoPanel(window);
                if (etoPanel != null)
                {
                    parent = ASEva.UIEto.App.PassParent(etoPanel.ParentWindow);
                }
            }
        }

        public bool CanOpen => true;

        public bool CanSave => true;

        public bool CanPickFolder => true;

        public Task<IStorageBookmarkFile> OpenFileBookmarkAsync(string bookmark)
        {
            return Task.FromResult<IStorageBookmarkFile>(null);
        }

        public async Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(FilePickerOpenOptions options)
        {
            var title = options?.Title ?? "";
            var multiSelect = options?.AllowMultiple ?? false;
            var startFolder = options?.SuggestedStartLocation?.Path?.LocalPath;

            Dictionary<string, string[]> filters = null;
            if (options?.FileTypeFilter != null && options.FileTypeFilter.Count > 0)
            {
                filters = new Dictionary<string, string[]>();
                foreach (var filter in options.FileTypeFilter)
                {
                    if (filter.Patterns != null && filter.Patterns.Count > 0)
                    {
                        var patterns = new string[filter.Patterns.Count];
                        for (int i = 0; i < filter.Patterns.Count; i++)
                        {
                            patterns[i] = filter.Patterns[i].Substring(1);
                        }
                        filters[filter.Name] = patterns;
                    }
                }
            }

            var selectedFiles = await ASEva.UIEto.App.ShowOpenFileDialog(parent, title, multiSelect, startFolder, filters);
            
            if (selectedFiles == null || selectedFiles.Length == 0)
                return new List<IStorageFile>();

            var result = new List<IStorageFile>();
            foreach (var filePath in selectedFiles)
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    result.Add(new StorageFile(filePath));
                }
            }
            
            return result;
        }

        public Task<IStorageBookmarkFolder> OpenFolderBookmarkAsync(string bookmark)
        {
            return Task.FromResult<IStorageBookmarkFolder>(null);
        }

        public async Task<IReadOnlyList<IStorageFolder>> OpenFolderPickerAsync(FolderPickerOpenOptions options)
        {
            var title = options?.Title ?? "";
            var startFolder = options?.SuggestedStartLocation?.Path?.LocalPath;

            var selectedFolder = await ASEva.UIEto.App.ShowSelectFolderDialog(parent, title, startFolder);
            
            if (string.IsNullOrEmpty(selectedFolder))
                return new List<IStorageFolder>();

            return new List<IStorageFolder> { new StorageFolder(selectedFolder) };
        }

        public async Task<IStorageFile> SaveFilePickerAsync(FilePickerSaveOptions options)
        {
            var title = options?.Title ?? "";
            var startFolder = options?.SuggestedStartLocation?.Path?.LocalPath;
            var initialFileName = options?.SuggestedFileName;
            
            string filterTitle = null;
            string filterSuffix = null;
            if (options?.FileTypeChoices != null && options.FileTypeChoices.Count > 0)
            {
                var firstChoice = options.FileTypeChoices[0];
                filterTitle = firstChoice.Name;
                if (firstChoice.Patterns != null && firstChoice.Patterns.Count > 0)
                {
                    filterSuffix = firstChoice.Patterns[0].Substring(1);
                }
            }

            var selectedFile = await ASEva.UIEto.App.ShowSaveFileDialog(parent, title, startFolder, initialFileName, filterTitle, filterSuffix);
            
            if (string.IsNullOrEmpty(selectedFile))
                return null;

            return new StorageFile(selectedFile);
        }

        public Task<IStorageFile> TryGetFileFromPathAsync(Uri filePath)
        {
            if (filePath == null || !filePath.IsFile)
                return Task.FromResult<IStorageFile>(null);

            var path = filePath.LocalPath;
            if (File.Exists(path))
            {
                return Task.FromResult<IStorageFile>(new StorageFile(path));
            }

            return Task.FromResult<IStorageFile>(null);
        }

        public Task<IStorageFolder> TryGetFolderFromPathAsync(Uri folderPath)
        {
            if (folderPath == null)
                return Task.FromResult<IStorageFolder>(null);

            var path = folderPath.LocalPath;
            if (Directory.Exists(path))
            {
                return Task.FromResult<IStorageFolder>(new StorageFolder(path));
            }

            return Task.FromResult<IStorageFolder>(null);
        }

        public Task<IStorageFolder> TryGetWellKnownFolderAsync(WellKnownFolder wellKnownFolder)
        {
            string path = null;
            
            switch (wellKnownFolder)
            {
                case WellKnownFolder.Desktop:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    break;
                case WellKnownFolder.Documents:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case WellKnownFolder.Downloads:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    if (!string.IsNullOrEmpty(path)) path = Path.Combine(path, "Downloads");
                    break;
                case WellKnownFolder.Music:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                    break;
                case WellKnownFolder.Pictures:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    break;
                case WellKnownFolder.Videos:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    break;
                default:
                    return Task.FromResult<IStorageFolder>(null);
            }

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                return Task.FromResult<IStorageFolder>(new StorageFolder(path));
            }

            return Task.FromResult<IStorageFolder>(null);
        }

        class StorageFile : IStorageFile
        {
            private readonly string _path;

            public StorageFile(string path)
            {
                _path = path;
            }

            public string Name => System.IO.Path.GetFileName(_path);
            public Uri Path => new Uri(_path);
            public bool CanBookmark => false;
            public Task<string> SaveBookmarkAsync() => Task.FromResult<string>(null);
            public Task<Stream> OpenReadAsync() => Task.FromResult<Stream>(File.OpenRead(_path));
            public Task<Stream> OpenWriteAsync() => Task.FromResult<Stream>(File.OpenWrite(_path));
            public Task<Stream> OpenReadWriteAsync() => Task.FromResult<Stream>(File.Open(_path, FileMode.Open, FileAccess.ReadWrite));
            public Task DeleteAsync()
            {
                File.Delete(_path);
                return Task.CompletedTask;
            }
            public Task<Avalonia.Platform.Storage.StorageItemProperties> GetBasicPropertiesAsync() => Task.FromResult<Avalonia.Platform.Storage.StorageItemProperties>(null);
            public Task<IStorageFolder> GetParentAsync()
            {
                var parentPath = System.IO.Path.GetDirectoryName(_path);
                if (!string.IsNullOrEmpty(parentPath) && Directory.Exists(parentPath))
                {
                    return Task.FromResult<IStorageFolder>(new StorageFolder(parentPath));
                }
                return Task.FromResult<IStorageFolder>(null);
            }
            public Task<IStorageItem> MoveAsync(IStorageFolder destination)
            {
                if (destination?.Path != null)
                {
                    var destPath = System.IO.Path.Combine(destination.Path.LocalPath, Name);
                    File.Move(_path, destPath);
                    return Task.FromResult<IStorageItem>(new StorageFile(destPath));
                }
                return Task.FromResult<IStorageItem>(null);
            }
            public void Dispose() { }
        }

        #pragma warning disable CS1998

        class StorageFolder : IStorageFolder
        {
            private readonly string _path;

            public StorageFolder(string path)
            {
                _path = path;
            }

            public string Name => System.IO.Path.GetDirectoryName(_path);
            public Uri Path => new Uri(_path);
            public bool CanBookmark => false;
            public Task<string> SaveBookmarkAsync() => Task.FromResult<string>(null);
            public Task<Avalonia.Platform.Storage.StorageItemProperties> GetBasicPropertiesAsync() => Task.FromResult<Avalonia.Platform.Storage.StorageItemProperties>(null);
            public Task<IReadOnlyList<IStorageFile>> GetFilesAsync()
            {
                var files = Directory.GetFiles(_path);
                var result = new List<IStorageFile>();
                foreach (var file in files)
                {
                    result.Add(new StorageFile(file));
                }
                return Task.FromResult<IReadOnlyList<IStorageFile>>(result);
            }
            public Task<IReadOnlyList<IStorageFolder>> GetFoldersAsync()
            {
                var folders = Directory.GetDirectories(_path);
                var result = new List<IStorageFolder>();
                foreach (var folder in folders)
                {
                    result.Add(new StorageFolder(folder));
                }
                return Task.FromResult<IReadOnlyList<IStorageFolder>>(result);
            }
            public async IAsyncEnumerable<IStorageItem> GetItemsAsync()
            {
                var files = Directory.GetFiles(_path);
                foreach (var file in files)
                {
                    yield return new StorageFile(file);
                }
                
                var folders = Directory.GetDirectories(_path);
                foreach (var folder in folders)
                {
                    yield return new StorageFolder(folder);
                }
            }
            public Task<IStorageFile> CreateFileAsync(string name)
            {
                var filePath = System.IO.Path.Combine(_path, name);
                File.Create(filePath).Dispose();
                return Task.FromResult<IStorageFile>(new StorageFile(filePath));
            }
            public Task<IStorageFolder> CreateFolderAsync(string name)
            {
                var folderPath = System.IO.Path.Combine(_path, name);
                Directory.CreateDirectory(folderPath);
                return Task.FromResult<IStorageFolder>(new StorageFolder(folderPath));
            }
            public Task<IStorageFolder> GetParentAsync()
            {
                var parentPath = System.IO.Path.GetDirectoryName(_path);
                if (!string.IsNullOrEmpty(parentPath) && Directory.Exists(parentPath))
                {
                    return Task.FromResult<IStorageFolder>(new StorageFolder(parentPath));
                }
                return Task.FromResult<IStorageFolder>(null);
            }
            public Task<IStorageItem> MoveAsync(IStorageFolder destination)
            {
                if (destination?.Path != null)
                {
                    var destPath = System.IO.Path.Combine(destination.Path.LocalPath, Name);
                    Directory.Move(_path, destPath);
                    return Task.FromResult<IStorageItem>(new StorageFolder(destPath));
                }
                return Task.FromResult<IStorageItem>(null);
            }
            public Task DeleteAsync()
            {
                Directory.Delete(_path, true);
                return Task.CompletedTask;
            }
            public void Dispose() { }
        }

        private object parent = null;
    }
}