using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Pin.MenuContainer
{
    public class MenuViewModel : ViewModelBase, INotifyPropertyChanged
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
        private bool _SizingStatus;

        private bool SizingStatus
        {
            get
            {
                return _SizingStatus;
            }
            set
            {
                if(value)
                { // show expanded
                    
                }
                else
                { // show minimized open

                }
                _SizingStatus = value;
            }
        }

        public int PrimaryProjectOrigionalLocation { get; private set; }

        private bool PinStatus = false;

        #endregion

        public event EventHandler OnPinned;
        public event EventHandler OnUnPinned;

        public event EventHandler OnOpenMenu;
        public event EventHandler OnCloseMenu;

        public event EventHandler OnExit;

        public event EventHandler OnOpenArrow;
        public event EventHandler OnCloseArrow;

        public RelayCommand SizingBtnCmd { get; set; }
        public RelayCommand ExitBtnCmd { get; set; }
        public RelayCommand DragOutCmd { get; set; }
        public RelayCommand MenuBtnCmd { get; set; }
        public RelayCommand CleanProjectsBtnCmd { get; set; }
        public RelayCommand CheckedBtnCmd { get; set; }
        public RelayCommand UncheckedBtnCmd { get; set; }
        public MenuViewModel()
        {
            UncheckedBtnCmd = new RelayCommand(() => UncheckedBtn());
            CheckedBtnCmd = new RelayCommand(() => CheckedBtn());
            SizingBtnCmd = new RelayCommand(() => SizingBtn());
            CleanProjectsBtnCmd = new RelayCommand(() => CleanProjectsBtn());
            MenuBtnCmd = new RelayCommand(() => MenuBtn());
            ExitBtnCmd = new RelayCommand(() => ExitBtn());
            DragOutCmd = new RelayCommand(() => DragOut());

            UI_DragOut_Color = new SolidColorBrush(Colors.Orange);
            FillColor = new SolidColorBrush(Colors.Orange);

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

            //ProjectSettings.Instance.PrimaryProjectChanged += new ProjectSettings.ProjectChangedEventHandler(delegate (Model.Project project)
            //{
            //    //UI_StackPanel_PinContainerProjects.Children.Clear();

            //    //var primaryProject = ProjectSettings.Instance.PrimaryProject;

            //    //foreach (var item in ProjectSettings.Instance.Projects)
            //    //{
            //    //    if (primaryProject != null && primaryProject.Equals(item))
            //    //    {
            //    //        UI_StackPanel_PinContainerProjects.Children.Insert(0, new PinContainerProjectItem(item));
            //    //    }
            //    //    else
            //    //    {
            //    //        UI_StackPanel_PinContainerProjects.Children.Add(new PinContainerProjectItem(item));
            //    //    }
            //    //}
            //    //if (UI_StackPanel_PinContainerProjects.Children.Count == 0)
            //    //{
            //    //    UI_TextBlock_FirstProject.Visibility = Visibility.Visible;
            //    //}
            //    //else
            //    //{
            //    //    UI_TextBlock_FirstProject.Visibility = Visibility.Collapsed;
            //    //}

            //    //UI_StackPanel_PinContainerProjects.UpdateLayout();

            //    //FillColor = (project == null) ? new SolidColorBrush(Colors.Orange) : FillColor = project.Color;
            //});

        }
        public SolidColorBrush _UI_DragOut_Color { get; set; }
        public SolidColorBrush UI_DragOut_Color
        {
            get
            {
                return _UI_DragOut_Color;
            }
            set
            {
                _UI_DragOut_Color = value;
                NotifyPropertyChanged();
            }
        }
        private void UncheckedBtn()
        {
            PinStatus = !PinStatus;
        }

        private void CheckedBtn()
        {
            PinStatus = !PinStatus;
        }

        private void CleanProjectsBtn()
        {
            throw new NotImplementedException();
        }

        private void MenuBtn()
        {
            throw new NotImplementedException();
        }

        private void SizingBtn()
        {
            SizingStatus = !SizingStatus;
        }

        private void DragOut()
        {
            throw new NotImplementedException();
        }

        private void ExitBtn()
        {
            if (OnExit != null) OnExit(this, EventArgs.Empty);
        }

        //public void WindowChangeState(MouseOverController.WindowState? wState = default(MouseOverController.WindowState?))
        //{
        //    switch (wState)
        //    {
        //        case MouseOverController.WindowState.Minimized:
        //            UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
        //            UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;
        //            UI_Grid_Maximized.Visibility = Visibility.Hidden;

        //            UI_Grid_ProjectView.Visibility = Visibility.Hidden;
        //            UI_ProjectView_IsOpen = false;

        //            UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
        //            ArrowStatus = true;
        //            break;
        //        case MouseOverController.WindowState.Pinned:
        //        case MouseOverController.WindowState.Normal:
        //            UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;

        //            UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
        //            UI_Grid_Maximized.Visibility = Visibility.Visible;
        //            break;
        //        case MouseOverController.WindowState.MinimizedOpen:
        //            ArrowStatus = true; // after window minimize

        //            UI_Grid_MinimizedOpen.Visibility = Visibility.Visible;

        //            UI_Grid_MinimizedClosed.Visibility = Visibility.Hidden;
        //            UI_Grid_Maximized.Visibility = Visibility.Hidden;
        //            break;
        //        case MouseOverController.WindowState.MinimizedDragging:
        //            UI_Grid_ProjectView.Visibility = Visibility.Visible;
        //            UI_ProjectView_IsOpen = true;

        //            UI_Grid_MinimizedClosed.Visibility = Visibility.Visible;

        //            UI_Grid_MinimizedOpen.Visibility = Visibility.Hidden;
        //            UI_Grid_Maximized.Visibility = Visibility.Hidden;
        //            break;
        //    }
        //}

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            MouseOverController.isMoveOverWindow = true;
        }

    }
}
