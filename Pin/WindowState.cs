using System;

namespace Pin
{
    public abstract class WindowState
    {
        public abstract void setMinimized();
        public abstract void setMinimizedHover();
        public abstract void setMinimizedDragDrop();
        public abstract void setMinimizedDragDropHover();
        public abstract void setMaximized();

        public enum FormState
        {
            minimized,
            minimizedHover,
            minimizedDragDrop,
            minimizedDragDropHover,
            maximized
        }

        public void setState(FormState state)
        {
            switch (state)
            {
                case FormState.minimized:
                    setMaximized();
                    break;
                case FormState.minimizedHover:
                    setMinimizedHover();
                    break;
                case FormState.minimizedDragDrop:
                    setMinimizedDragDrop();
                    break;
                case FormState.minimizedDragDropHover:
                    setMinimizedDragDropHover();
                    break;
                case FormState.maximized:
                    setMaximized();
                    break;
                default:
                    setMinimized();
                    break;
            }
        }
    }
}
