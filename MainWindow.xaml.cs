using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static Pin.MouseOverController;

namespace Pin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPinState
    {
        public delegate void MinimizedWindowEventHandler(EventArgs e);

        public static event MinimizedWindowEventHandler MinimizedWindow;

        MouseOverController.WindowState Win_prev_State;

        private void OnMinimizedWindow(EventArgs e)
        {
            if (MinimizedWindow != null)
                MinimizedWindow(e);
        }

        public MainWindow()
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            //Properties.Settings.Default.Upgrade();
            
            MouseOverController.Init();
            DataContext = this;
            InitializeComponent();
            WindowChangeState(MouseOverController.WindowState.Minimized);


            switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
            {
                case ActionEvent.Move:
                    UI_RadioButton_Move.IsChecked = true;
                    break;
                case ActionEvent.Copy:
                    UI_RadioButton_Move.IsChecked = true;
                    break;
            }

            MouseOverController.MouseLeaveMenu += MouseOverController_MouseLeaveMenu;
        }

        #region Window Controller
        public void WindowChangeState(MouseOverController.WindowState? wState = null)
        {
            if (wState == null)
            { // setoppositestate
                if (MouseOverController.Win_State == MouseOverController.WindowState.Normal && MouseOverController.isPinned)
                {
                    wState = MouseOverController.WindowState.Pinned;
                }
                else if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized)
                {
                    wState = MouseOverController.WindowState.Normal;
                }
                else if (MouseOverController.Win_State == MouseOverController.WindowState.Pinned)
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
                case MouseOverController.WindowState.Pinned:
                    MouseOverController.isPinned = true;
                    Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;
                    border.Visibility = Visibility.Visible;
                    break;
                case MouseOverController.WindowState.Normal:
                    MouseOverController.isPinned = false;
                    Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;
                    border.Visibility = Visibility.Visible;
                    break;
                case MouseOverController.WindowState.Minimized:
                    border.Visibility = Visibility.Hidden;
                    Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_HEIGHT;
                    break;
                case MouseOverController.WindowState.MinimizedOpen:
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


        Task task;
        CancellationTokenSource TokenSource;

        private void minimizeWindowDelay(int millisecondDelay = 750)
        {
            // should prepare to minimize window?
            if (
                MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                && !MouseOverController.isPinned
            )
            {
                if(task != null)
                {
                    TokenSource.Cancel();
                    task.Wait(); // wait for task to finish

                    // dispose both
                    TokenSource.Dispose();
                    task.Dispose();
                }

                TokenSource = new CancellationTokenSource();
                task = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(millisecondDelay);

                    if (TokenSource?.IsCancellationRequested == true) return;

                    // recheck after x time
                    if (MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                        && !IsMouseOver
                        && !MouseOverController.isPinned
                        && !MouseOverController.isMouseOverMenu
                        && !MouseOverController.isProjectOpen
                        )
                    {
                        Dispatcher.Invoke(() => { WindowChangeState(MouseOverController.WindowState.Minimized); });
                        OnMinimizedWindow(EventArgs.Empty);
                    }
                },TokenSource.Token);
            }
        }
        #endregion

        private void MouseOverController_MouseLeaveMenu(EventArgs e)
        {
            minimizeWindowDelay(250);
        }

        private void UI_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
            {
                var RButton = sender as RadioButton;
                if (RButton.Name.Equals("UI_RadioButton_Copy"))
                {
                    ProjectSettings.Instance.setActionEvent(ActionEvent.Copy);
                }
                else if (RButton.Name.Equals("UI_RadioButton_Move"))
                {
                    ProjectSettings.Instance.setActionEvent(ActionEvent.Move);
                }
                Properties.Settings.Default.Save();
            }
        }

        #region PinContainer Events
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
            WindowChangeState(MouseOverController.WindowState.Pinned);
        }

        private void UI_PinContainer_OnExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }
        #endregion

        #region Window Events
        private void pinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            // get window style
            int exStyle = (int)Win32.GetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE);

            // add to style that it is a tool window so it does not show in the task view (alt + tab)
            exStyle |= (int)Win32.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            Win32.SetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle); // set style

            ProjectSettings.Instance.Load();
        }

        private void pinWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverController.isMoveOverWindow = true;
            if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized)
            {
                WindowChangeState(MouseOverController.WindowState.MinimizedOpen);
            }
        }

        private void pinWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverController.isMoveOverWindow = false;
            if (MouseOverController.Win_State == MouseOverController.WindowState.MinimizedDragging && !MouseOverController.isMoveOverWindow)
            {
                WindowChangeState(MouseOverController.WindowState.Minimized);
            }
            else
            {
                minimizeWindowDelay();
            }
        }

        private void UI_pinWindow_DragEnter(object sender, DragEventArgs e)
        {
            WindowChangeState(MouseOverController.WindowState.MinimizedDragging);
            DropDataHandler.setEffects(e);
        }

        private void pinWindow_DragLeave(object sender, DragEventArgs e)
        {
            if(MouseOverController.Win_State == MouseOverController.WindowState.MinimizedDragging && !MouseOverController.isMoveOverWindow)
            {
                WindowChangeState(MouseOverController.WindowState.Minimized);
            }
        }

        #endregion

    }
}