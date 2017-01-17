using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pin
{
    /// <summary>
    /// Interaction logic for PinContainer.xaml
    /// </summary>
    public partial class PinContainer : UserControl, INotifyPropertyChanged , IPinState
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ImageSource _UI_SizingSource;
        public ImageSource UI_SizingSource
        {
            get
            {
                return _UI_SizingSource;
            }
            set
            {
                _UI_SizingSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_MenuSource;

        public ImageSource UI_MenuSource
        {
            get
            {
                return _UI_MenuSource;
            }
            set
            {
                _UI_MenuSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_ExitSource;
        public ImageSource UI_ExitSource
        {
            get
            {
                return _UI_ExitSource;
            }
            set
            {
                _UI_ExitSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_PinSource;
        public ImageSource UI_PinSource
        {
            get
            {
                return _UI_PinSource;
            }
            set
            {
                _UI_PinSource = value;
                NotifyPropertyChanged();
            }
        }

        private bool ArrowStatus = false;
        private bool PinStatus = false;
        private bool MenuStatus = false;

        #endregion

        public event EventHandler OnPinned;
        public event EventHandler OnUnPinned;

        public event EventHandler OnOpenMenu;
        public event EventHandler OnCloseMenu;

        public event EventHandler OnExit;

        public event EventHandler OnOpenArrow;
        public event EventHandler OnCloseArrow;


        public PinContainer()
        {
            UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
            UI_ExitSource = new BitmapImage(new Uri("images/Exit.png", UriKind.Relative));
            UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            UI_MenuSource = new BitmapImage(new Uri("images/menu.png", UriKind.Relative));

            DataContext = this;
            InitializeComponent();

            if (Properties.Settings.Default.Projects != null)
            {
                foreach (var item in Properties.Settings.Default.Projects)
                {
                    UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(Model.Project.Deserialize(item)));
                }
            }
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Projects"))
            {
                UI_StackPanel_PinContainerProjects.Children.Clear();
                if (Properties.Settings.Default.Projects != null)
                {
                    foreach (var item in Properties.Settings.Default.Projects)
                    {
                        UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(Model.Project.Deserialize(item)));
                    }
                }
            }
        }

        private void UI_Btn_Sizing_Click(object sender, RoutedEventArgs e)
        {
            if(ArrowStatus)
            {
                UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
                if (OnCloseArrow != null) OnCloseArrow(this, e);
            }
            else
            {
                UI_SizingSource = new BitmapImage(new Uri("images/CloseArrow.png", UriKind.Relative));
                if (OnOpenArrow != null) OnOpenArrow(this, e);
            }
            ArrowStatus = !ArrowStatus;
        }

        private void UI_Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            if(MenuStatus)
            {
                if (OnOpenMenu != null) OnOpenMenu(this, e);
            }
            else
            {
                if (OnCloseMenu != null) OnCloseMenu(this, e);
            }
            MenuStatus = !MenuStatus;
        }

        private void UI_Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (OnExit != null) OnExit(this, e);
        }

        private void UI_Btn_Pin_Click(object sender, RoutedEventArgs e)
        {
            if(PinStatus)
            {
                UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
                if (OnUnPinned != null) OnUnPinned(this, e);
            }
            else
            {
                UI_PinSource = new BitmapImage(new Uri("images/pinned.png", UriKind.Relative));
                if (OnPinned != null) OnPinned(this, e);
            }
            PinStatus = !PinStatus;
        }

        
        private void UI_Btn_Polygon_Drop(object sender, DragEventArgs e)
        {

        }

        private void UI_UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!MouseOverController.isMoveOverWindow)
            {
                UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

                UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                UI_Grid_Maximized.Visibility = Visibility.Hidden;
            }
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
                        //Console.WriteLine(String.Join(", ", item) + "\t=>\t" + project.getCurrentProjectPath());
                        // TODO:setting selector for cut,copy,move
                        //File.Copy(item, savePath);
                    }
                }
            }
        }

        private void sizing_btn_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        public void WindowChangeState(MouseOverController.WindowState? wState = default(MouseOverController.WindowState?))
        {
            switch (wState)
            {
                case MouseOverController.WindowState.Minimized:

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;


                    UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
                    ArrowStatus = true;

                    break;
                case MouseOverController.WindowState.Normal:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Visible;
                    break;
                case MouseOverController.WindowState.pinDrop:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
                case MouseOverController.WindowState.MinimizedDragging:
                    UI_Grid_ProjectView.Visibility = Visibility.Visible;
                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
                case MouseOverController.WindowState.pinned:

                    break;
                default:

                    break;
            }
        }
    }
}
