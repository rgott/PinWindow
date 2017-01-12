using System.Windows;

namespace Pin
{
    static class MouseOverController
    {
        public static void init()
        {
            Win_State = MouseOverController.WindowState.Minimized;
            isPinned = false;
            isMouseOverMenu = false;
        }
        public static WindowState Win_State
        {
            get
            {
                return (WindowState)Application.Current.Properties["WindowState"];
            }
            set
            {
                Application.Current.Properties["WindowState"] = value;
            }

        }
        public static bool isPinned
        {
            get
            {
                return (bool)Application.Current.Properties["isPinned"];
            }
            set
            {
                Application.Current.Properties["isPinned"] = value;
            }

        }
        public static bool isMouseOverMenu
        {
            get
            {
                return (bool)Application.Current.Properties["isMouseOverMenu"];
            }
            set
            {
                Application.Current.Properties["isMouseOverMenu"] = value;
            }
        }
        public enum WindowState
        {
            Normal,
            pinned,
            pinDrop,
            Minimized
        }
        public enum PinnedWindowState
        {
            open,
            closed
        }
    }
}
