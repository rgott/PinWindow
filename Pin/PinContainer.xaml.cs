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
        

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _PrimaryProjectId;
        public int PrimaryProjectId
        {
            get
            {
                return _PrimaryProjectId;
            }
            set
            {
                _PrimaryProjectId = value;
                PrimaryProject = Model.Project.Deserialize(Properties.Settings.Default.Projects[value]);
            }
        }

        private Model.Project _PrimaryProject;
        public Model.Project PrimaryProject
        {
            get
            {
                return _PrimaryProject;
            }
            set
            {
                _PrimaryProject = value;
                PrimaryProjectColor = value.Color;
            }
        }

        private SolidColorBrush _PrimaryProjectColor;
        public SolidColorBrush PrimaryProjectColor
        {
            get
            {
                return _PrimaryProjectColor;
            }
            set
            {
                _PrimaryProjectColor = value;
                NotifyPropertyChanged();
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
            UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
            UI_ExitSource = new BitmapImage(new Uri("images/Exit.png", UriKind.Relative));
            UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            UI_MenuSource = new BitmapImage(new Uri("images/menu.png", UriKind.Relative));

            DataContext = this;
            InitializeComponent();
            if(Properties.Settings.Default.PrimaryProjectId == -1)
            {
                PrimaryProjectColor = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                PrimaryProjectId = Properties.Settings.Default.PrimaryProjectId;
            }
            if (Properties.Settings.Default.Projects != null)
            {
                foreach (var item in Properties.Settings.Default.Projects)
                {
                    UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(Model.Project.Deserialize(item)));
                }
                if(PrimaryProjectId != -1 && UI_StackPanel_PinContainerProjects.Children.Count >= PrimaryProjectId + 1)
                {
                    var project = UI_StackPanel_PinContainerProjects.Children[PrimaryProjectId];
                    UI_StackPanel_PinContainerProjects.Children.RemoveAt(PrimaryProjectId);
                    UI_StackPanel_PinContainerProjects.Children.Insert(0, project);
                }
            }
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

            switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
            {
                case ActionEvent.Copy:
                    UI_TextBlock_ActionEventType = "Copy";
                    break;
                case ActionEvent.Move:
                    UI_TextBlock_ActionEventType = "Move";
                    break;
            }

        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("PrimaryProjectId"))
            {
                if (Properties.Settings.Default.PrimaryProjectId != -1)
                {
                    PrimaryProjectId = Properties.Settings.Default.PrimaryProjectId;
                }
            }
            else if (e.PropertyName.Equals("ActionEvent"))
            {
                switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                {
                    case ActionEvent.Copy:
                        UI_TextBlock_ActionEventType = "Copy";
                        break;
                    case ActionEvent.Move:
                        UI_TextBlock_ActionEventType = "Move";
                        break;
                }
            }
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
            MouseOverController.isMoveOverWindow = true;
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
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
                case MouseOverController.WindowState.MinimizedDragging:
                    UI_StackPanel_PinContainerProjects.Children.Clear();
                    if (Properties.Settings.Default.Projects != null)
                    {
                        foreach (var item in Properties.Settings.Default.Projects)
                        {
                            UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(Model.Project.Deserialize(item)));
                        }
                        if (PrimaryProjectId != -1)
                        {
                            var project = UI_StackPanel_PinContainerProjects.Children[PrimaryProjectId];
                            UI_StackPanel_PinContainerProjects.Children.RemoveAt(PrimaryProjectId);
                            UI_StackPanel_PinContainerProjects.Children.Insert(0, project);
                        }
                    }

                    UI_Grid_ProjectView.Visibility = Visibility.Visible;
                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
                    UI_Grid_Maximized.Visibility = Visibility.Hidden;
                    break;
                default:

                    break;
            }
        }
    }
}
