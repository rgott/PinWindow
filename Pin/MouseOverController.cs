using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Pin
{
    public class MouseOverController
    {
        internal static bool isMoveOverWindow;

        public static event MouseLeaveMenuEventHandler MouseLeaveMenu;
        private static void OnMouseLeaveMenu(EventArgs e)
        {
            if (MouseLeaveMenu != null)
                MouseLeaveMenu(e);
        }

        public static void Init()
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
                if(value == false)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(350);
                        OnMouseLeaveMenu(EventArgs.Empty);
                    });
                }
            }
        }

        public enum WindowState
        {
            Normal,
            Pinned,
            MinimizedOpen,
            Minimized,
            MinimizedDragging
        }
        public enum PinnedWindowState
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
    public delegate void MouseLeaveMenuEventHandler(EventArgs e);


}
