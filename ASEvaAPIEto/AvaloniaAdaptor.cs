using System;
using Eto.Forms;

namespace ASEva.UIEto
{
    public interface AvaloniaAdaptor
    {
        void RunIteration();
        bool ShouldCreateContainer();
        bool IsControlValid(object control);
        nint CreateContainer(nint parent, object control, out object? context);
        void UseContainer(nint container, object control, out object? context);
        void ReleaseResource(object context);
        void HandleWindowActive(object context, bool active);
        void HandleControlResize(object context, double width, double height);
        bool ShouldOverrideRunDialog();
    }
}