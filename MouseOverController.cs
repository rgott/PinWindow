using System;
using System.Windows;

namespace Pin
{
    public class MouseOverController
    {
        public delegate bool MouseOverWindowHandler();
        public static event MouseOverWindowHandler MouseOverWindow;


        internal static bool isMoveOverWindow;

        public delegate void MouseLeaveMenuEventHandler(EventArgs e);
        public static event MouseLeaveMenuEventHandler MouseLeaveMenu;
        private static void OnMouseLeaveMenu(EventArgs e)
        {
            if (MouseLeaveMenu != null)
                MouseLeaveMenu(e);
        }

        public static void Init()
        {
            Win_State = WindowState.Minimized;
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
        private static bool _isProjectOpen = false;
        public static bool isProjectOpen
        {
            get
            {
                return _isProjectOpen;
            }
            set
            {
                _isProjectOpen = value;
            }
        }


        public static bool isMouseOverMenu
        {
            get
            {
                return (bool)Application.Current.Properties["isMouseOverMenu"] && !DoAllEqualsDesired<bool>(MouseOverWindow, false);
            }
            set
            {
                Application.Current.Properties["isMouseOverMenu"] = value;
                if (value == false)
                {
                    OnMouseLeaveMenu(EventArgs.Empty);
                }
            }
        }

        public static bool CancellationRequested { get; set; }

        public static bool DoAllEqualsDesired<T>(Delegate del, T desired)
        {

            foreach (var item in del.GetInvocationList())
            {
                if(desired.Equals(item.DynamicInvoke()))
                    return false;
            }
            return true;
        }
    }

}
