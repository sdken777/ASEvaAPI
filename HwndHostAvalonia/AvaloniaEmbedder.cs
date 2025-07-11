using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using Avalonia.Controls.Embedding;

namespace HwndHostAvalonia
{
    [ContentProperty("Content")]
    class AvaloniaEmbedder : HwndHost
    {
        private EmbeddableControlRoot _root;
        private Avalonia.Controls.Control _content;

        public AvaloniaEmbedder()
        {
            DataContextChanged += AvaloniaHwndHost_DataContextChanged;

            SizeChanged += delegate
            {
                if (Content == null) return;
                Content.Width = Width;
                Content.Height = Height;
            };
        }

        private void AvaloniaHwndHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Content != null) Content.DataContext = e.NewValue;
        }

        public Avalonia.Controls.Control Content
        {
            get => _content;
            set
            {
                if (_content == value) return;
                _content = value;
                if (_root is not null) _root.Content = value;
                if (value != null) value.DataContext = DataContext;
            }
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            _root = new EmbeddableControlRoot();
            _root.Content = _content;
            _root.Prepare();
            _root.StartRendering();

            var handle = _root.TryGetPlatformHandle()?.Handle ?? throw new InvalidOperationException("Unable to create EmbeddableControlRoot.");
            if (PresentationSource.FromVisual(this) is HwndSource source) _ = SetParent(handle, source.Handle);

            controlMap[_root] = this;

            return new HandleRef(_root, handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            if (controlMap.ContainsKey(_root)) controlMap.Remove(_root);
            _root?.Dispose();
        }

        public static HwndHost GetHwndHost(EmbeddableControlRoot root)
        {
            if (controlMap.ContainsKey(root)) return controlMap[root];
            else return null;
        }

        [DllImport("user32.dll")]
        private static extern bool SetParent(IntPtr hWnd, IntPtr hWndNewParent);

        private static Dictionary<EmbeddableControlRoot, HwndHost> controlMap = new();
    }
}