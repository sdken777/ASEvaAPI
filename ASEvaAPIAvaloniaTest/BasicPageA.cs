using System;
using ASEva;
using ASEva.UIAvalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CustomMessageBox.Avalonia;

namespace ASEvaAPIAvaloniaTest
{
    partial class BasicPageA : Panel
    {
        public BasicPageA()
        {
            InitializeComponent();

            var texts = Program.Texts;
            this.AddToResources(texts);

            labelRow1.Content = texts.Format(labelRow1.Content as String, 1);
        }

        private void itemMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(App.WorkPath, "");
        }

        private async void linkBrowse_Click(object sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (radioFile.IsChecked.Value)
            {
                if (checkSaveFile.IsChecked.Value)
                {
                    var options = new FilePickerSaveOptions
                    {
                        DefaultExtension = ".txt",
                        FileTypeChoices = [ new FilePickerFileType(Program.Texts["basic-save-file-filter"]) { Patterns = ["*.txt"] } ]
                    };
                    var file = await topLevel.StorageProvider.SaveFilePickerAsync(options);
                    if (file != null) await MessageBox.Show(file.Path, "");
                }
                else
                {
                    var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
                    if (files.Count > 0) await MessageBox.Show(files[0].Path, "");
                }
            }
            else // radioDir
            {
                var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
                if (folders.Count > 0) await MessageBox.Show(folders[0].Path, "");
            }
        }
    }
}