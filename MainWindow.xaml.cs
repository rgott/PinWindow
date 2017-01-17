using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Pin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MinimizedWindowEventHandler(EventArgs e);

        public static event MinimizedWindowEventHandler MinimizedWindow;

        MouseOverController.WindowState Win_prev_State;

        bool dragDrop = false; // TODO:fix

        private void OnMinimizedWindow(EventArgs e)
        {
            if (MinimizedWindow != null)
                MinimizedWindow(e);
        }

        public MainWindow()
        {
            Properties.Settings.Default.Upgrade();
            MouseOverController.Init();
            DataContext = this;
            InitializeComponent();
            WindowChangeState(MouseOverController.WindowState.Minimized);
            MouseOverController.MouseLeaveMenu += MouseOverController_MouseLeaveMenu;
        }

        private void MouseOverController_MouseLeaveMenu(EventArgs e)
        {
            minimizeWindowDelay(250);
        }

        #region Window Controller
        private void WindowChangeState(MouseOverController.WindowState? wState = null)
        {
            if (wState == null)
            { // setoppositestate
                if (MouseOverController.Win_State == MouseOverController.WindowState.Normal && MouseOverController.isPinned)
                {
                    wState = MouseOverController.WindowState.pinned;
                }
                else if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized)
                {
                    wState = MouseOverController.WindowState.Normal;
                }
                else if (MouseOverController.Win_State == MouseOverController.WindowState.pinned)
                {
                    wState = MouseOverController.WindowState.Normal;
                }
                else
                {
                    wState = MouseOverController.WindowState.Minimized;
                }
            }
            Win_prev_State = MouseOverController.Win_State;
            MouseOverController.Win_State = (MouseOverController.WindowState)wState;
            UI_PinContainer.WindowChangeState(wState);
            switch (wState)
            {
                case MouseOverController.WindowState.Normal:
                    Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;
                    border.Visibility = Visibility.Visible;
                    break;
                case MouseOverController.WindowState.Minimized:
                    border.Visibility = Visibility.Hidden;
                    Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_HEIGHT;
                    break;
                case MouseOverController.WindowState.pinned:
                    border.Visibility = Visibility.Hidden;
                    Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZEDPINNED_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZEDPINNED_HEIGHT;
                    break;
                case MouseOverController.WindowState.MinimizedDragging:
                    border.Visibility = Visibility.Hidden;
                    Width = 1000;
                    Height = 1000;
                    break;
            }
        }
        #endregion

        private void minimizeWindowDelay(int millisecondDelay = 750)
        {
            // should prepare to minimize window?
            if (
                MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                && !MouseOverController.isPinned
            )
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(millisecondDelay);
                    // recheck after x time
                    if (MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                        && !IsMouseOver
                        && !MouseOverController.isPinned
                        && !MouseOverController.isMouseOverMenu
                        )
                    {
                        Dispatcher.Invoke(() => { WindowChangeState(MouseOverController.WindowState.Minimized); });
                        OnMinimizedWindow(EventArgs.Empty);
                    }
                });
            }
        }

        #region Window Events

        private void pinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            // get window style
            int exStyle = (int)Win32.GetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE);

            // add to style that it is a tool window so it does not show in the task view (alt + tab)
            exStyle |= (int)Win32.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            Win32.SetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle); // set style
        }

        private void pinWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverController.isMoveOverWindow = true;
            if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized && !dragDrop)
            {
                WindowChangeState(MouseOverController.WindowState.pinned);
            }
        }

        private void pinWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverController.isMoveOverWindow = false;
            dragDrop = false;
            if(MouseOverController.Win_State == MouseOverController.WindowState.MinimizedDragging)
            {
                WindowChangeState(MouseOverController.WindowState.Minimized);
            }
            minimizeWindowDelay();
        }

        #endregion

        #region PinContainer
        private void UI_PinContainer_OnCloseArrow(object sender, EventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.Minimized);
        }

        private void UI_PinContainer_OnOpenArrow(object sender, EventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.Normal);
        }

        private void UI_PinContainer_OnCloseMenu(object sender, EventArgs e)
        {
            // TODO: create menu
        }

        private void UI_PinContainer_OnOpenMenu(object sender, EventArgs e)
        {
            // TODO: create menu
        }

        private void UI_PinContainer_OnUnPinned(object sender, EventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.Normal);
        }

        private void UI_PinContainer_OnPinned(object sender, EventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.pinned);
        }

        private void UI_PinContainer_OnExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }
        #endregion

        private void UI_pinWindow_DragEnter(object sender, DragEventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.MinimizedDragging);
            e.Effects = DragDropEffects.Move;
        }
    }
}