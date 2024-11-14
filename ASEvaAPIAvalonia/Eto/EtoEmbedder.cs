using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace ASEva.UIAvalonia
{
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.0) For embedding Eto control in Avalonia GUI
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.0) 用于在Avalonia界面中嵌入Eto控件
    /// </summary>
    public class EtoEmbedder : NativeControlHost
    {
        /// \~English
        /// <summary>
        /// The Eto control to be embedded. Either this or NativeControl should be set just after calling constructor's InitializeComponent
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 待嵌入的Eto控件，需要在构造函数调用InitializeComponent后立即设置 (与NativeControl二选一)
        /// </summary>
        public Eto.Forms.Control EtoControl { protected get; set; }

        /// \~English
        /// <summary>
        /// The native control to be embedded. Either this or EtoControl should be set just after calling constructor's InitializeComponent
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 待嵌入的原生控件，需要在构造函数调用InitializeComponent后立即设置 (与EtoControl二选一)
        /// </summary>
        public object NativeControl { protected get; set; }

        /// \~English
        /// <summary>
        /// Not for user
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无需直接调用
        /// </summary>
        public static void RunIteration()
        {
            foreach (var embedder in embedders)
            {
                var curActive = embedder.isParentWindowActive();
                if (curActive != embedder.active)
                {
                    embedder.active = curActive;
                    EtoInitializer.Adaptor.HandleWindowActive(embedder.context, curActive);
                }

                var curSize = embedder.Bounds.Size;
                if (curSize.Width > 0 && curSize.Height > 0 && curSize != embedder.size)
                {
                    embedder.size = curSize;
                    EtoInitializer.Adaptor.HandleControlResize(embedder.context, curSize.Width, curSize.Height);
                }
            }
        }

        public static Eto.Forms.Control[] ExtractControls(Window window)
        {
            var list = new List<Eto.Forms.Control>();
            extractFrom(window, list);
            return list.ToArray();
        }

        private static void extractFrom(Control control, List<Eto.Forms.Control> list)
        {
            if (control is Panel)
            {
                var panel = control as Panel;
                if (panel.Children == null) return;
                foreach (var child in panel.Children)
                {
                    if (child is EtoEmbedder)
                    {
                        var etoControl = (child as EtoEmbedder).EtoControl;
                        if (etoControl != null) list.Add(etoControl);
                    }
                    else if (child is Panel || child is ContentControl || child is Decorator) extractFrom(child as Control, list);
                }
            }
            else if (control is ContentControl)
            {
                var cc = control as ContentControl;
                if (cc.Content != null)
                {
                    if (cc.Content is EtoEmbedder)
                    {
                        var etoControl = (cc.Content as EtoEmbedder).EtoControl;
                        if (etoControl != null) list.Add(etoControl);
                    }
                    else if (cc.Content is Panel || cc.Content is ContentControl || cc.Content is Decorator) extractFrom(cc.Content as Control, list);
                }
            }
            else if (control is Decorator)
            {
                var decorator = control as Decorator;
                if (decorator.Child != null)
                {
                    if (decorator.Child is EtoEmbedder)
                    {
                        var etoControl = (decorator.Child as EtoEmbedder).EtoControl;
                        if (etoControl != null) list.Add(etoControl);
                    }
                    else if (decorator.Child is Panel || decorator.Child is ContentControl || decorator.Child is Decorator) extractFrom(decorator.Child as Control, list);
                }
            }
        }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {
            if (!canCreateNativeControlCore()) return base.CreateNativeControlCore(parent);

            PlatformHandle ret = null;
            if (EtoInitializer.Adaptor.ShouldCreateContainer())
            {
                object context = null;
                var container = EtoInitializer.Adaptor.CreateContainer(parent.Handle, EtoControl ?? NativeControl, out context);
                ret = new PlatformHandle
                {
                    Handle = container,
                    HandleDescriptor = parent.HandleDescriptor,
                    Context = context,
                };
            }
            else
            {
                object context = null;
                var container = base.CreateNativeControlCore(parent);
                EtoInitializer.Adaptor.UseContainer(container.Handle, EtoControl ?? NativeControl, out context);
                ret = new PlatformHandle
                {
                    Handle = container.Handle,
                    HandleDescriptor = container.HandleDescriptor,
                    Context = context,
                };
            }

            context = ret.Context;
            embedders.Add(this);
            return ret;
        }

        protected override void DestroyNativeControlCore(IPlatformHandle control)
        {
            embedders.Remove(this);
            if (control is PlatformHandle)
            {
                var context = (control as PlatformHandle).Context;
                EtoInitializer.Adaptor.ReleaseResource(context);
            }
            else base.DestroyNativeControlCore(control);
        }

        private bool isParentWindowActive()
        {
            StyledElement parentWindow = Parent;
            while (parentWindow.Parent != null) parentWindow = parentWindow.Parent;
            if (parentWindow == null || parentWindow is not Window) return false;
            return (parentWindow as Window).IsActive;
        }

        private bool canCreateNativeControlCore()
        {
            if (EtoInitializer.Adaptor == null || !EtoInitializer.Initialized) return false;
            if (EtoControl != null) return true;
            if (NativeControl == null) return false;
            return EtoInitializer.Adaptor.IsControlValid(NativeControl);
        }

        private class PlatformHandle : IPlatformHandle
        {
            public IntPtr Handle { get; set; }
            public string HandleDescriptor { get; set; }
            public object Context { get; set; }
        }

        private object context = null;
        private bool active = false;
        private Size size;

        private static List<EtoEmbedder> embedders = new List<EtoEmbedder>();
    }
}