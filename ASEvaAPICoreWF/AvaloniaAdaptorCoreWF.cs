using System;
using System.Collections.Generic;
using ASEva.UIEto;
using Eto.Forms;

namespace ASEva.UICoreWF
{
    public class AvaloniaAdaptorCoreWF : AvaloniaAdaptor
    {
        public AvaloniaAdaptorCoreWF()
        {
            UsingAvalonia = true;
        }

        public nint CreateContainer(nint parent, Control control, out object context)
        {
            var winformControl = WinFormsHelpers.ToNative(control, true);
            winformControl.Parent = System.Windows.Forms.Control.FromHandle(parent);
            context = winformControl;
            return winformControl.Handle;
        }

        public void HandleControlResize(object context, double width, double height)
        {
        }

        public void HandleWindowActive(object context, bool active)
        {
            if (active) activeFlags[context] = true;
            else if (activeFlags.ContainsKey(context)) activeFlags.Remove(context);
        }

        public void ReleaseResource(object context)
        {
            if (activeFlags.ContainsKey(context)) activeFlags.Remove(context);
        }

        public void RunIteration()
        {
        }

        public bool ShouldCreateContainer()
        {
            return true;
        }

        public void UseContainer(nint container, Control control, out object context)
        {
            context = null;
        }

        public bool ShouldOverrideRunDialog()
        {
            return false;
        }

        public static bool IsActive(System.Windows.Forms.Control control)
        {
            var target = control;
            while (target != null)
            {
                if (activeFlags.ContainsKey(target)) return true;
                target = target.Parent;
            }
            return false;
        }

        public static bool UsingAvalonia { get; private set; }

        private static Dictionary<object, bool> activeFlags = new Dictionary<object, bool>();
    }
}
