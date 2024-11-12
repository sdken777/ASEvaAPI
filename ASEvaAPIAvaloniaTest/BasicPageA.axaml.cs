using System;
using System.ComponentModel;
using System.Linq;
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
            
            this.AddToResources(Program.Texts);
            DataContext = new Model();

            searchBox.ItemsSource = new string[]{ "Cat", "Camel", "Cow", "Chameleon", "Mouse", "Lion", "Zebra", "大象" }.OrderBy(x => x);
        }

        public void OnLoop()
        {
            labelActive.Content = this.GetParentWindow().IsActive ? "O" : "X";
        }

        private void itemMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(App.WorkPath, "");
        }

        private async void linkBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (radioFile.IsChecked.Value)
            {
                if (checkSaveFile.IsChecked.Value)
                {
                    var options = new FilePickerSaveOptions
                    {
                        DefaultExtension = ".txt",
                        FileTypeChoices = [ new FilePickerFileType(Program.Texts["basic-save-file-filter"]) { Patterns = ["*.txt"] } ]
                    };
                    var file = await this.GetParentWindow().StorageProvider.SaveFilePickerAsync(options);
                    if (file != null) await MessageBox.Show(file.Path, "");
                }
                else
                {
                    var files = await this.GetParentWindow().StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
                    if (files.Count > 0) await MessageBox.Show(files[0].Path, "");
                }
            }
            else // radioDir
            {
                var folders = await this.GetParentWindow().StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
                if (folders.Count > 0) await MessageBox.Show(folders[0].Path, "");
            }
        }

        private void checkShowPassword_IsCheckedChanged(object sender, RoutedEventArgs e)
        {
            passwordBox.PasswordChar = checkShowPassword.IsChecked.Value ? '\0' : '●';
        }

        private void buttonShowWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.CanResize = false;
            window.Content = new Panel{ MinWidth = 300, MinHeight = 300};
            window.Show();
        }

        private async void buttonShowDialog_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window();
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.CanResize = false;
            window.Content = new Panel{ MinWidth = 300, MinHeight = 300};
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            await window.ShowDialog(await this.GetActiveWindow());
        }

        private async void linkShowDialogNoBorder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialogNoBorder();
            await dialog.ShowDialog(await this.GetActiveWindow());
        }
        
        private async void linkShowDialogWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialogWithBorder();
            await dialog.ShowDialog(await this.GetActiveWindow());
        }

        private async void linkShowDialogWithFixBorder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TestDialogWithFixBorder();
            await dialog.ShowDialog(await this.GetActiveWindow());
        }

        private void linkClientSize_Click(object sender, RoutedEventArgs e)
        {
            var size = this.GetParentWindow().ClientSize;
            linkClientSize.Content = size.Width + "x" + size.Height;
        }

        private class Model : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public double Progress
            {
                get => progress;
                set { progress = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress))); }
            }

            private double progress = 0;
        }
    }
}