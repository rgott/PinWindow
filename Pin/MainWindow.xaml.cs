﻿using Pin.MenuContainer;
using Pin.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace Pin
{
    public delegate void WindowStateEventHandler(Pin.WindowState? requestState);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged , IMainWindow
    {
        public delegate void MinimizedWindowEventHandler(EventArgs e);

        public static event MinimizedWindowEventHandler MinimizedWindow;

        WindowState Win_prev_State;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MenuItemViewModel _MenuContainerBind;
        public MenuItemViewModel MenuContainerBind
        {
            get
            {
                return _MenuContainerBind;
            }
            set
            {
                _MenuContainerBind = value;
                NotifyPropertyChanged();
            }
        }


        public ProjectViewModelList ProjectVML { get; set; }
        public MainWindow()
        {
            //Properties.Settings.Default.Reset();
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Upgrade();

            Width = 280;
            Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;

            ProjectVML = new ProjectViewModelList(this, Properties.Settings.Default);


            MenuContainerBind = new MenuItemViewModel(this, ProjectVML);


            MouseOverController.Init();

            DataContext = this;
            InitializeComponent();
            WindowChangeState(Pin.WindowState.Minimized);


            switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
            {
                case ActionEvent.Move:
                    UI_RadioButton_Move.IsChecked = true;
                    break;
                case ActionEvent.Copy:
                    UI_RadioButton_Move.IsChecked = false;
                    break;
            }


            MouseOverController.MouseLeaveMenu += MouseOverController_MouseLeaveMenu;
        }


        #region Window Controller

        public void WindowChangeState(WindowState? wState = null)
        {
            // if state is locked do not change state
            if (WindowStateLocks.Count != 0) return;

            if (wState == null)
            { // setoppositestate
                if (MouseOverController.Win_State == Pin.WindowState.Normal && MouseOverController.isPinned)
                {
                    wState = Pin.WindowState.Pinned;
                }
                else if (MouseOverController.Win_State == Pin.WindowState.Minimized || MouseOverController.Win_State == Pin.WindowState.Pinned)
                {
                    wState = Pin.WindowState.Normal;
                }
                else
                {
                    wState = Pin.WindowState.Minimized;
                }
            }

            Win_prev_State = MouseOverController.Win_State;
            MouseOverController.Win_State = (Pin.WindowState)wState;

            MenuContainerBind.WindowChangeState(MouseOverController.Win_State);
            switch (wState)
            {
                // remove width and height changes
                case Pin.WindowState.Pinned:
                    MouseOverController.isPinned = true;
                    border.Visibility = Visibility.Visible;
                    break;
                case Pin.WindowState.Normal:
                    MouseOverController.isPinned = false;
                    //Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    //Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;
                    border.Visibility = Visibility.Visible;
                    break;
                case Pin.WindowState.Minimized:
                    border.Visibility = Visibility.Hidden;
                    //Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_WIDTH;
                    //Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_HEIGHT;
                    break;
                case Pin.WindowState.MinimizedOpen:
                    border.Visibility = Visibility.Hidden;
                    //Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZEDPINNED_WIDTH;
                    //Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZEDPINNED_HEIGHT;
                    break;
                case Pin.WindowState.MinimizedDragging:
                    border.Visibility = Visibility.Hidden;
                    //Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    //Height = 1000;
                    break;
            }
        }


        Task task;
        CancellationTokenSource TokenSource;

        private void minimizeWindowDelay(int millisecondDelay = 750)
        {
            // should prepare to minimize window?
            if (
                MouseOverController.Win_State != Pin.WindowState.Minimized
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
                    if (MouseOverController.Win_State != Pin.WindowState.Minimized
                        && !IsMouseOver
                        && !MouseOverController.isPinned)
                    {
                        Dispatcher.Invoke(() => { WindowChangeState(Pin.WindowState.Minimized); });
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
                    ProjectVML.setActionEvent(ActionEvent.Copy);
                }
                else if (RButton.Name.Equals("UI_RadioButton_Move"))
                {
                    ProjectVML.setActionEvent(ActionEvent.Move);
                }
                Properties.Settings.Default.Save();
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
            if (MouseOverController.Win_State == Pin.WindowState.Minimized)
            {
                WindowChangeState(Pin.WindowState.MinimizedOpen);
            }
        }

        private void pinWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MouseOverController.Win_State == Pin.WindowState.MinimizedDragging)
            {
                WindowChangeState(Pin.WindowState.Minimized);
            }
            else
            {
                minimizeWindowDelay();
            }
        }

        public void onExit()
        {
            Properties.Settings.Default.Save();

            Close();
        }

        List<object> WindowStateLocks = new List<object>();
        public void PauseState(object lockingObject)
        {
            WindowStateLocks.Add(lockingObject);
        }

        public void ResumeState(object lockingObject)
        {
            WindowStateLocks.Remove(lockingObject);
        }

        #endregion
    }
}