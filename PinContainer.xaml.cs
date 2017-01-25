using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Pin.MouseOverController;

namespace Pin
{
    /// <summary>
    /// Interaction logic for PinContainer.xaml
    /// </summary>
    public partial class PinContainer : UserControl, INotifyPropertyChanged , IPinState
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
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

        private string _UI_TextBlock_ActionEventType;
        public string UI_TextBlock_ActionEventType
        {
            get
            {
                return _UI_TextBlock_ActionEventType;
            }
            set
            {
                _UI_TextBlock_ActionEventType = value;
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

        private SolidColorBrush _FillColor;
        public SolidColorBrush FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                _FillColor = value;
                NotifyPropertyChanged();
            }

        }

        private bool _UI_ProjectView_IsOpen = false;
        public bool UI_ProjectView_IsOpen
        {
            get
            {
                return _UI_ProjectView_IsOpen;
            }

            set
            {
                _UI_ProjectView_IsOpen = value;
                NotifyPropertyChanged();
            }
        }

        public int PrimaryProjectOrigionalLocation { get; private set; }

        private bool ArrowStatus = true;// true on openarrow.png false on closedarrow.png
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
            FillColor = new SolidColorBrush(Colors.Orange);

            UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
            UI_ExitSource = new BitmapImage(new Uri("images/Exit.png", UriKind.Relative));
            UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            UI_MenuSource = new BitmapImage(new Uri("images/menu.png", UriKind.Relative));

            DataContext = this;
            InitializeComponent();


            ProjectSettings.Instance.ActionEventChanged += new ProjectSettings.ActionEventChangedEventHandler(delegate (ActionEvent e)
            {
                switch (e)
                {
                    case ActionEvent.Copy:
                        UI_TextBlock_ActionEventType = "Copy";
                        break;
                    case ActionEvent.Move:
                        UI_TextBlock_ActionEventType = "Move";
                        break;
                }
            });

            ProjectSettings.Instance.PrimaryProjectChanged += new ProjectSettings.ProjectChangedEventHandler(delegate (Model.Project project)
            {
                UI_StackPanel_PinContainerProjects.Children.Clear();

                var primaryProject = ProjectSettings.Instance.PrimaryProject;

                foreach (var item in ProjectSettings.Instance.Projects)
                {
                    if (primaryProject != null && primaryProject.Equals(item))
                    {
                        UI_StackPanel_PinContainerProjects.Children.Insert(0, new PinContainerProjectItem(item));
                    }
                    else
                    {
                        UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(item));
                    }
                }
                if(UI_StackPanel_PinContainerProjects.Children.Count == 0)
                {
                    UI_TextBlock_FirstProject.Visibility = Visibility.Visible;
                }
                else
                {
                    UI_TextBlock_FirstProject.Visibility = Visibility.Collapsed;
                }

                UI_StackPanel_PinContainerProjects.UpdateLayout();

                FillColor = (project == null)? new SolidColorBrush(Colors.Orange) : FillColor = project.Color;
            });

        }

        private void UI_Btn_Sizing_Click(object sender, RoutedEventArgs e)
        {
            if(ArrowStatus)
            {
                if (OnOpenArrow != null) OnOpenArrow(this, e); // user pressed open arrow
                UI_SizingSource = new BitmapImage(new Uri("images/CloseArrow.png", UriKind.Relative));// change to opposite arrow
            }
            else
            {
                if (OnCloseArrow != null) OnCloseArrow(this, e); // user pressed close arrow
                UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative)); // change to opposite arrow
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
            // passes back to listener presumable an extention of Window that can close itself
            if (OnExit != null) OnExit(this, e); 
        }

        private void UI_Btn_Pin_Click(object sender, RoutedEventArgs e)
        {
            if(PinStatus)
            {
                if (OnUnPinned != null) OnUnPinned(this, e);
                UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative)); // set opposite image
            }
            else
            {
                if (OnPinned != null) OnPinned(this, e);
                UI_PinSource = new BitmapImage(new Uri("images/pinned.png", UriKind.Relative));// set opposite image
            }
            PinStatus = !PinStatus;
        }

        private void UI_UserControl_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Enter");
        }



        private void UI_UserControl_DragLeave(object sender, DragEventArgs e)
        {
            Console.WriteLine("SDFLKDSF");
        }

        public void WindowChangeState(MouseOverController.WindowState? wState = default(MouseOverController.WindowState?))
        {
            switch (wState)
            {
                case MouseOverController.WindowState.Minimized:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;

                    UI_Grid_ProjectView.Visibility = Visibility.Hidden;
                    UI_ProjectView_IsOpen = false;

                    UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
                    ArrowStatus = true;
                    break;
                case MouseOverController.WindowState.Pinned:
                case MouseOverController.WindowState.Normal:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Visible;
                    break;
                case MouseOverController.WindowState.MinimizedOpen:
                    ArrowStatus = true; // after window minimize

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
                case MouseOverController.WindowState.MinimizedDragging:
                    UI_Grid_ProjectView.Visibility = Visibility.Visible;
                    UI_ProjectView_IsOpen = true;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            MouseOverController.isMoveOverWindow = true;
        }

        private void Popup_DragLeave(object sender, DragEventArgs e)
        {
            MouseOverController.isMoveOverWindow = false;
            Console.WriteLine("POPUP");

        }

        private void Popup_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Console.WriteLine("MOUSE LEAVE SDFLSDKJFSD");
        }
    }
}
