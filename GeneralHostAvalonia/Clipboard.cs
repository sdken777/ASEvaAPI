using System;
using System.Threading.Tasks;
using Avalonia.Input.Platform;
using AvaloniaDataObject = Avalonia.Input.IDataObject;

namespace GeneralHostAvalonia
{
    class Clipboard : IClipboard
    {
        public Task ClearAsync()
        {
            var etoClipboard = new Eto.Forms.Clipboard();
            etoClipboard.Clear();
            return Task.CompletedTask;
        }

        public Task<object> GetDataAsync(string format)
        {
            var etoClipboard = new Eto.Forms.Clipboard();
            return Task.FromResult(etoClipboard.GetObject(format));
        }

        public Task<string[]> GetFormatsAsync()
        {
            var etoClipboard = new Eto.Forms.Clipboard();
            return Task.FromResult(etoClipboard.Types);
        }

        public Task<string> GetTextAsync()
        {
            var etoClipboard = new Eto.Forms.Clipboard();
            return Task.FromResult(etoClipboard.Text ?? string.Empty);
        }

        public Task SetDataObjectAsync(AvaloniaDataObject data)
        {
            foreach (var format in data.GetDataFormats())
            {
                if (data.Get(format) is string stringData) // 主要处理文本数据
                {
                    var etoClipboard = new Eto.Forms.Clipboard();
                    etoClipboard.Text = stringData;
                    break;
                }
            }
            return Task.CompletedTask;
        }

        public Task SetTextAsync(string text)
        {
            var etoClipboard = new Eto.Forms.Clipboard();
            etoClipboard.Text = text ?? string.Empty;
            return Task.CompletedTask;
        }
    }
}