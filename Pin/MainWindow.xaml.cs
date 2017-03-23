using Pin.MenuContainer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using LibraryImports;
using Pin;

namespace Pin
{
    public partial class MainWindow : Window, IApplicationWindow
    {
        public ISettings Settings { get; set; }
        public MainWindow()
        {
            Settings = Properties.Settings.Default;
//#if DEBUG
//            Properties.Settings.Default.Reset();
//            Properties.Settings.Default.Save();
//            Properties.Settings.Default.Upgrade();
//#endif

            Width = 280;
            Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            MainContextBind = ViewModelFactory.MainVM(Settings, this);
            DataContext = MainContextBind;

            WindowChangeState(Pin.WindowState.Minimized);
            InitializeComponent();
        }



        public void onExit()
        {
            Settings.Save();
            base.Close();
        }

        readonly List<object> WindowStateLocks = new List<object>();

        public void PauseState(object lockingObject)
        {
            WindowStateLocks.Add(lockingObject);
        }

        public void ResumeState(object lockingObject)
        {
            WindowStateLocks.Remove(lockingObject);
            if (WindowStateLocks.Count == 0 && !base.IsMouseOver)
            {
                MinimizeWindowDelay();
            }
        }
        public WindowState State { get; set; } = Pin.WindowState.Minimized;
        public MainViewModel MainContextBind { get; set; }


        public void WindowChangeState(WindowState wState)
        {
            // if state is locked do not change state
            if (WindowStateLocks.Count != 0) return;

            State = (Pin.WindowState)wState;

            MainContextBind.WindowChangeState(State);
        }


        Task task;
        CancellationTokenSource TokenSource;
        private void MinimizeWindowDelay(int millisecondDelay = 750)
        {
            // should prepare to minimize window?
            if (State != Pin.WindowState.Minimized && WindowStateLocks.Count == 0)
            {
                // prevent multiple threads from running at one time
                if (task != null)
                {
                    TokenSource.Cancel();
                    task.Wait(); // wait for task to finish being cancelled

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
                    if (State != Pin.WindowState.Minimized
                        && !IsMouseOver
                        && WindowStateLocks.Count == 0)
                    {
                        Dispatcher.Invoke(() => { WindowChangeState(Pin.WindowState.Minimized); });
                    }
                }, TokenSource.Token);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var wndHelper = new WindowInteropHelper(this);

            // get window style
            var exStyle = (int)Win32.GetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE);

            // add to style that it is a tool window so it does not show in the task view (alt + tab)
            exStyle |= (int)Win32.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            Win32.SetWindowLong(wndHelper.Handle, (int)Win32.GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle); // set style
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (State == Pin.WindowState.Minimized)
                WindowChangeState(Pin.WindowState.MinimizedOpen);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            switch (State)
            {
                case Pin.WindowState.MinimizedOpen:
                case Pin.WindowState.MinimizedDragging:
                    WindowChangeState(Pin.WindowState.Minimized);
                    break;
                default:
                    MinimizeWindowDelay();
                    break;
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            WindowChangeState(Pin.WindowState.MinimizedDragging);
        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {
            WindowChangeState(Pin.WindowState.Minimized);
        }
    }
}