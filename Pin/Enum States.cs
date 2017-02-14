using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin
{
    public enum WindowState
    {
        Normal,
        Pinned,
        MinimizedOpen,
        Minimized,
        MinimizedDragging
    }

    public enum PinnedState
    {
        open,
        closed
    }

    public enum ActionEvent
    {
        Move = 0,
        Copy = 1
    }
}
