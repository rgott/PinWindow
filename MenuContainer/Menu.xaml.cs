using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pin.MenuContainer
{
    /// <summary>
    /// Interaction logic for PinContainer.xaml
    /// </summary>
    public partial class Menu : UserControl, INotifyPropertyChanged
    {

        private MenuViewModel _Context;
        public MenuViewModel Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
                NotifyPropertyChanged();
            }
        }
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        #endregion

        public event EventHandler OnPinned;
        public event EventHandler OnUnPinned;

        public event EventHandler OnExit;

        public event EventHandler OnOpenArrow;
        public event EventHandler OnCloseArrow;

        public Menu()
        {
            DataContext = this;
            Context = new MenuViewModel();
            Context.OnExit += Menu_OnExit;

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
        }

        private void Menu_OnExit(object sender, EventArgs e)
        {
            if (OnExit != null) OnExit(this, e);
        }



        //    ProjectSettings.Instance.PrimaryProjectChanged += new ProjectSettings.ProjectChangedEventHandler(delegate (ProjectViewModel ViewModel)
        //    {
        //        UI_StackPanel_PinContainerProjects.Children.Clear();

        //        var primaryProject = ProjectSettings.Instance.PrimaryProject;

        //        foreach (var item in ProjectSettings.Instance.Projects)
        //        {
        //            var pinProjectItem = new PinContainerProjectItem(item);
        //            pinProjectItem.ProjectItemDropped += PinProjectItem_ProjectItemDropped;

        //            if (primaryProject != null && primaryProject.Equals(item))
        //            {
        //                UI_StackPanel_PinContainerProjects.Children.Insert(0, pinProjectItem);
        //            }
        //            else
        //            {
        //                UI_StackPanel_PinContainerProjects.Children.Add(pinProjectItem);
        //            }
        //        }
        //        if (UI_StackPanel_PinContainerProjects.Children.Count == 0)
        //        {
        //            UI_TextBlock_FirstProject.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            UI_TextBlock_FirstProject.Visibility = Visibility.Collapsed;
        //        }

        //        UI_StackPanel_PinContainerProjects.UpdateLayout();

        //        FillColor = (ViewModel == null) ? new SolidColorBrush(Colors.Orange) : ViewModel.Project.Color;
        //    });

        //}


        private void UI_Btn_Pin_Click(object sender, RoutedEventArgs e)
        {
            if (PinStatus)
            {
                if (OnUnPinned != null) OnUnPinned(this, e);
            }
            else
            {
                if (OnPinned != null) OnPinned(this, e);
            }
            PinStatus = !PinStatus;
        }
        
        public void WindowChangeState(WindowState? wState = default(WindowState?))
        {
            switch (wState)
            {
                case WindowState.Minimized:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Collapsed;
                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;
                    UI_Grid_Maximized.Visibility = Visibility.Collapsed;

                    UI_Grid_ProjectView.Visibility = Visibility.Collapsed;
                    UI_ProjectView_IsOpen = false;

                    ArrowStatus = true;
                    break;
                case WindowState.Pinned:
                case WindowState.Normal:
                    UI_Grid_MinimizedOpen.Visibility = Visibility.Collapsed;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Collapsed;
                    UI_Grid_Maximized.Visibility = Visibility.Visible;
                    break;
                case WindowState.MinimizedOpen:
                    ArrowStatus = true; // after window minimize

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Collapsed;
                    UI_Grid_Maximized.Visibility = Visibility.Collapsed;
                    break;
                case WindowState.MinimizedDragging:
                    UI_Grid_ProjectView.Visibility = Visibility.Visible;
                    UI_ProjectView_IsOpen = true;

                    UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;

                    UI_Grid_MinimizedOpen.Visibility = Visibility.Collapsed;
                    UI_Grid_Maximized.Visibility = Visibility.Collapsed;
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
        }
    }
}
