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
            language = new LanguageSwitch(Resources, Program.Language == Language.Chinese ? "zh" : "en");
            DataContext = new Model();

            searchBox.ItemsSource = new string[]{ "Cat", "Camel", "Cow", "Chameleon", "Mouse", "Lion", "Zebra", "大象" }.OrderBy(x => x);

            linkBrowse.Click += linkBrowse_Click;
            linkSwitch.Click += linkSwitch_Click;
            buttonShowWindow.Click += buttonShowWindow_Click;
            buttonShowDialog.Click += buttonShowDialog_Click;
            linkShowDialogNoBorder.Click += linkShowDialogNoBorder_Click;
            linkShowDialogWithBorder.Click += linkShowDialogWithBorder_Click;
            linkShowDialogWithFixBorder.Click += linkShowDialogWithFixBorder_Click;
            linkClientSize.Click += linkClientSize_Click;
            itemMenu.Click += itemMenu_Click;
            itemMenuAvaloniaWindow.Click += itemMenuAvaloniaWindow_Click;
            itemMenuAvaloniaDialog.Click += itemMenuAvaloniaDialog_Click;
            itemMenuEtoDialog.Click += itemMenuEtoDialog_Click;
            checkShowPassword.IsCheckedChanged += checkShowPassword_IsCheckedChanged;
        }

        public void OnLoop()
        {
            labelActive.Content = this.GetParentWindow().IsActive ? "O" : "X";
        }

        private async void itemMenu_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(async (window) => await MessageBox.Show(window, App.WorkPath, ""));
        }

        private void itemMenuAvaloniaWindow_Click(object sender, RoutedEventArgs e)
        {
            var avaloniaWindow = new CrossAvaloniaWindow();
            avaloniaWindow.Title = language["menu-avalonia-window"];
            avaloniaWindow.Show();
        }

        private async void itemMenuAvaloniaDialog_Click(object sender, RoutedEventArgs e)
        {
            var avaloniaDialog = new CrossAvaloniaWindow();
            avaloniaDialog.Title = language["menu-avalonia-dialog"];
            await App.RunDialog(avaloniaDialog);
        }

        private async void itemMenuEtoDialog_Click(object sender, RoutedEventArgs e)
        {
            await ASEva.UIEto.App.RunDialog(new CrossEtoPanel());
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
                        FileTypeChoices = [ new FilePickerFileType(language["basic-save-file-filter"]) { Patterns = ["*.txt"] } ]
                    };
                    var file = await this.GetParentWindow().StorageProvider.SaveFilePickerAsync(options);
                    if (file != null) await App.RunDialog(async (window) => await MessageBox.Show(window, file.Path.LocalPath, ""));
                }
                else
                {
                    var files = await this.GetParentWindow().StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
                    if (files.Count > 0) await App.RunDialog(async (window) => await MessageBox.Show(window, files[0].Path.LocalPath, ""));
                }
            }
            else // radioDir
            {
                var folders = await this.GetParentWindow().StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions());
                if (folders.Count > 0) await App.RunDialog(async (window) => await MessageBox.Show(window, folders[0].Path.LocalPath, ""));
            }
        }

        private void linkSwitch_Click(object sender, RoutedEventArgs e)
        {
            linkEnableDisable.IsEnabled = !linkEnableDisable.IsEnabled;
            linkEnableDisable.Content = language[linkEnableDisable.IsEnabled ? "basic-linkbutton-enabled" : "basic-linkbutton-disabled"];
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
            await App.RunDialog(window);
        }

        private async void linkShowDialogNoBorder_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(new TestDialogNoBorder());
        }
        
        private async void linkShowDialogWithBorder_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(new TestDialogWithBorder());
        }

        private async void linkShowDialogWithFixBorder_Click(object sender, RoutedEventArgs e)
        {
            await App.RunDialog(new TestDialogWithFixBorder());
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

        private LanguageSwitch language;
    }
}