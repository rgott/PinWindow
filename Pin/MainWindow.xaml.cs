using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Pin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MouseOverController.WindowState Win_prev_State;
        bool dragDrop = false;
        public MainWindow()
        {
            MouseOverController.init();
            InitializeComponent();
            WindowChangeState(MouseOverController.WindowState.Minimized);
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
        private void sizing_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowChangeState();
        }

        private void pinWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseOverController.Win_State == MouseOverController.WindowState.Minimized && !dragDrop)
            {
                WindowChangeState(MouseOverController.WindowState.pinned);
            }
        }

        private void sizing_btn_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            dragDrop = true;
        }

        private void sizing_btn_Drop(object sender, DragEventArgs e)
        {
            //Console.WriteLine(e.Source);
            //Console.WriteLine(e.RoutedEvent.Name);
            //Console.WriteLine(e.KeyStates);
            //Console.WriteLine(e.OriginalSource);
            //Console.WriteLine(e.GetType());
            //Console.WriteLine(string.Join(", ",e.Data.GetFormats()));
            //Console.WriteLine(string.Join(", ",e.Data.GetData("FileName")));
            //Console.WriteLine(string.Join(", ", e.Data.GetData("FileDrop")));
            //Console.WriteLine(e.GetType());
            //Console.WriteLine(getName(e));
            saveFile(e, "");
        }

        private void saveFile(DragEventArgs e, string savePath)
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

        private void pinWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            dragDrop = false;
            if (
                MouseOverController.Win_State != MouseOverController.WindowState.Minimized 
                && !MouseOverController.isPinned 
                && !MouseOverController.isMouseOverMenu
            ){
                if(MouseOverController.Win_State == MouseOverController.WindowState.pinned)
                {
                    new Task(() =>
                    {
                        Thread.Sleep(100);
                        // recheck after x time
                        if (MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                            && !MouseOverController.isPinned
                            && !MouseOverController.isMouseOverMenu
                            )
                            Dispatcher.Invoke(() => { WindowChangeState(MouseOverController.WindowState.Minimized); });
                    }).Start();

                }
                else
                {
                    new Task(() =>
                    {
                        Thread.Sleep(750);
                        // recheck after x time
                        if (MouseOverController.Win_State != MouseOverController.WindowState.Minimized
                            && !IsMouseOver
                            && !MouseOverController.isPinned
                            && !MouseOverController.isMouseOverMenu
                            )
                        Dispatcher.Invoke(() => { WindowChangeState(MouseOverController.WindowState.Minimized); });
                    }).Start();

                }

            }
        }
        
        private void pin_btn_Click(object sender, RoutedEventArgs e)
        {
            if(MouseOverController.isPinned)
            {
                MouseOverController.isPinned = false;
                pin_btn_image.Source = new BitmapImage(new Uri("pin.png", UriKind.Relative));
            }
            else
            {
                MouseOverController.isPinned = true;
                pin_btn_image.Source = new BitmapImage(new Uri("pinned.png", UriKind.Relative));
            }
        }

        private void borderMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }
        private void Popup_MouseLeave(object sender, MouseEventArgs e)
        {
            //popup.IsOpen = false;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //new Options().Show();
        }
        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }
        
        private void pinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }
        #region Window styles
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion
    }
}