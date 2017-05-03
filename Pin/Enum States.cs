using System;

namespace Pin
{
    public enum WindowState
    {
        Normal,
        MinimizedOpen,
        Minimized,
        MinimizedDragging
    }

    public enum PinnedState
    {
        open,
        closed
    }

    public enum ClipboardEvent
    {
        Move = 0,
        Copy = 1
    }
}