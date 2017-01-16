using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MinimizedWindowEventHandler(EventArgs e);

        public static event MinimizedWindowEventHandler MinimizedWindow;

        private void OnMinimizedWindow(EventArgs e)
        {
            if (MinimizedWindow != null)
                MinimizedWindow(e);
        }

        MouseOverController.WindowState Win_prev_State;
        bool dragDrop = false;
        public MainWindow()
        {
            Properties.Settings.Default.Upgrade();
            MouseOverController.init();
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

            switch (wState)
            {
                case MouseOverController.WindowState.Normal:
                    pin_btn.Visibility = Visibility.Visible;
                    border.Visibility = Visibility.Visible;
                    setPinnedWindowState(MouseOverController.PinnedWindowState.open);
                    borderMain.Margin = new Thickness(80, 0, 0, 0);
                    Width = Properties.Settings.Default.WINDOW_STATE_NORMAL_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_NORMAL_HEIGHT;
                    break;
                case MouseOverController.WindowState.Minimized:
                    pin_btn.Visibility = Visibility.Hidden;
                    setPinnedWindowState(MouseOverController.PinnedWindowState.closed);
                    borderMain.Margin = new Thickness(80, 0, 0, 0);
                    border.Visibility = Visibility.Hidden;
                    Width = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_WIDTH;
                    Height = Properties.Settings.Default.WINDOW_STATE_MINIMIZED_HEIGHT;
                    break;
                case MouseOverController.WindowState.pinned:
                    setPinnedWindowState(MouseOverController.PinnedWindowState.closed);
                    border.Visibility = Visibility.Hidden;
                    borderMain.Margin = new Thickness(80, 0, 0, 0);
                    Width = 40;
                    Height = 40;
                    break;
            }
        }
        
        private void setPinnedWindowState(MouseOverController.PinnedWindowState pws)
        {
            switch (pws)
            {
                case MouseOverController.PinnedWindowState.open:
                    menu_btn.Margin = new Thickness(40, 0, 0, 0);
                    sizing_btn.Margin = new Thickness(0);
                    exit_btn.Margin = new Thickness(20, 0, 0, 0);
                    pin_btn.Margin = new Thickness(60, 0, 0, 0);
                    project.Visibility = Visibility.Visible;
                    sizing_btn_image.Source = new BitmapImage(new Uri("images/CloseArrow.png", UriKind.Relative));

                    PinnedHolder.VerticalAlignment = VerticalAlignment.Top;
                    PinnedHolder.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case MouseOverController.PinnedWindowState.closed:
                    menu_btn.Margin = new Thickness(60, 0, 0, 0);
                    sizing_btn.Margin = new Thickness(60, -20, 0, 0);
                    exit_btn.Margin = new Thickness(80, -20, 0, 0);
                    pin_btn.Margin = new Thickness(60, 0, 0, 0);
                    project.Visibility = Visibility.Hidden;
                    sizing_btn_image.Source = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));

                    PinnedHolder.VerticalAlignment = VerticalAlignment.Bottom;
                    PinnedHolder.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
        }
        #endregion

        #region Sizing btn
        private void sizing_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowChangeState();
        }
        private void sizing_btn_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            dragDrop = true;
        }

        private void sizing_btn_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetFormats().contains(DataFormats.Html))
            {// if html then from web retrieve and save image
                Console.WriteLine(String.Join(", ", e.Data.GetData(DataFormats.Html)));
            }
            else
            {// file drop
                Array data = ((IDataObject)e.Data).GetData(DataFormats.FileDrop) as Array;
                if (data != null)
                {
                    foreach (string item in data)
                    {
                        Console.WriteLine(String.Join(", ", item) + "\t=>\t" + project.getCurrentProjectPath());
                        // TODO:setting selector for cut,copy,move
                        //File.Copy(item, savePath);
                    }
                }
            }
        }
        #endregion

        private void pin_btn_Click(object sender, RoutedEventArgs e)
        {
            if(MouseOverController.isPinned)
            {
                MouseOverController.isPinned = false;
                pin_btn_image.Source = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            }
            else
            {
                MouseOverController.isPinned = true;
                pin_btn_image.Source = new BitmapImage(new Uri("images/pinned.png", UriKind.Relative));
            }
        }

        private void borderMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }


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
            if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized && !dragDrop)
            {
                WindowChangeState(MouseOverController.WindowState.pinned);
            }
        }

        private void pinWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            dragDrop = false;

            minimizeWindowDelay();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        #endregion
    }
}