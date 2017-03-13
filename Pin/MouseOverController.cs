using System;
using System.Windows;

namespace Pin
{
    [Obsolete]
    public class MouseOverController
    {
        public delegate bool MouseOverWindowHandler();
        public static event MouseOverWindowHandler MouseOverWindow;

        public delegate void MouseLeaveMenuEventHandler(EventArgs e);
        public static event MouseLeaveMenuEventHandler MouseLeaveMenu;
        private static void OnMouseLeaveMenu(EventArgs e)
        {
            if (MouseLeaveMenu != null)
                MouseLeaveMenu(e);
        }

        
        

        [Obsolete]
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
        [Obsolete]
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
