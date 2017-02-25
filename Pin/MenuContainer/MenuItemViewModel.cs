using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Pin.MenuContainer
{
    public class MenuItemViewModel : ViewModelBase
    {
        #region Properties
        //depends on fill color
        private ProjectViewModel _PrimaryProject;
        public ProjectViewModel PrimaryProject
        {
            get
            {
                return _PrimaryProject;
            }
            set
            {
                _PrimaryProject = value;
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }
        private bool _SizingStatus;

        public bool SizingStatus
        {
            get
            {
                return _SizingStatus;
            }
            set
            {
                _SizingStatus = value;
                RaisePropertyChanged();
            }
        }

        private bool _UI_Popup_Menu_IsOpen = false;
        public bool UI_Popup_Menu_IsOpen
        {
            get
            {
                return _UI_Popup_Menu_IsOpen;
            }
            set
            {
                _UI_Popup_Menu_IsOpen = value;
                RaisePropertyChanged();
            }
        }

        private ProjectViewModelList _Projectitems;
        public ProjectViewModelList Projectitems
        {
            get
            {
                return _Projectitems;
            }
            set
            {
                _Projectitems = value;
                RaisePropertyChanged();
            }
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
                RaisePropertyChanged();
            }
        }

        #region Item Visibility
        private bool _Vis_Minimized = false;
        public bool Vis_Minimized
        {
            get
            {
                return _Vis_Minimized;
            }
            set
            {
                _Vis_Minimized = value;
                RaisePropertyChanged();
            }
        }

        private bool _Vis_ProjectView = false;
        public bool Vis_ProjectView
        {
            get
            {
                return _Vis_ProjectView;
            }
            set
            {
                _Vis_ProjectView = value;
                RaisePropertyChanged();
            }
        }

        private bool _Vis_MinimizedOpen = false;
        public bool Vis_MinimizedOpen
        {
            get
            {
                return _Vis_MinimizedOpen;
            }
            set
            {
                _Vis_MinimizedOpen = value;
                RaisePropertyChanged();
            }
        }

        private bool _Vis_Maximized = false;
        public bool Vis_Maximized
        {
            get
            {
                return _Vis_Maximized;
            }
            set
            {
                _Vis_Maximized = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        private bool PinStatus = false;
        public event EventHandler OnPinned;
        public event EventHandler OnUnPinned;

        public event EventHandler OnExit;

        public RelayCommand SizingBtnCmd { get; set; }
        public RelayCommand ExitBtnCmd { get; set; }
        public RelayCommand DragOutCmd { get; set; }
        public RelayCommand MenuBtnCmd { get; set; }
        public RelayCommand CheckedBtnCmd { get; set; }
        public RelayCommand UncheckedBtnCmd { get; set; }

        public MenuItemViewModel(ProjectViewModelList pList)
        {
            UncheckedBtnCmd = new RelayCommand(() => UncheckedBtn());
            CheckedBtnCmd = new RelayCommand(() => CheckedBtn());
            SizingBtnCmd = new RelayCommand(() => SizingBtn());
            MenuBtnCmd = new RelayCommand(() => MenuBtn());
            ExitBtnCmd = new RelayCommand(() => ExitBtn());
            DragOutCmd = new RelayCommand(() => DragOut());


            //PinContainer
            UC_DragEnterCmd = new RelayCommand(() => UC_DragEnter());
            UC_DragLeaveCmd = new RelayCommand(() => UC_DragLeave());
            UC_DropCmd = new RelayCommand(() => UC_Drop());
            UC_MouseEnterCmd = new RelayCommand(() => UC_MouseEnter());


            UI_DragOut_Color = new SolidColorBrush(Colors.Orange);

            ProjectList = pList.Projects;

            ProjectViewModelList.Instance.ActionEventChanged += new ProjectViewModelList.ActionEventChangedEventHandler(delegate (ActionEvent e)
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

            ProjectViewModelList.Instance.OnUpdate += Instance_OnUpdate;

            ProjectViewModelList.Instance.PrimaryProjectChanged += Instance_PrimaryProjectChanged;

        }

        private void Instance_OnUpdate(object sender, ProjectViewModel project)
        {
            RaisePropertyChanged("ProjectList");
        }

        private void Instance_PrimaryProjectChanged(ProjectViewModel project)
        {
            PrimaryProject = project;
        }

        
        private void UncheckedBtn()
        {
            if (OnUnPinned != null) OnUnPinned(this, EventArgs.Empty);
            PinStatus = !PinStatus;
        }

        private void CheckedBtn()
        {
            if (OnPinned != null) OnPinned(this, EventArgs.Empty);
            PinStatus = !PinStatus;
        }

        private void MenuBtn()
        {
            throw new NotImplementedException();
        }

        private void SizingBtn()
        {
            SizingStatus = !SizingStatus;
            if (SizingStatus)
            { // show expanded
                RaiseWindowChangeState(WindowState.Normal);
            }
            else
            { // show minimized open
                RaiseWindowChangeState(WindowState.MinimizedOpen);
            }
        }

        string[] lastDraggedIn;
        private void DragOut()
        {
            // TODO: change null
            DropDataHandler.dragDataOut(null, lastDraggedIn);
        }

        private void ExitBtn()
        {
            if (OnExit != null) OnExit(this, EventArgs.Empty);
        }

        private ObservableCollection<ProjectViewModel> _ProjectList;
        public ObservableCollection<ProjectViewModel> ProjectList
        {
            get
            {
                return _ProjectList;
            }
            set
            {
                _ProjectList = value;
                RaisePropertyChanged();
            }
        }

        #region PinContainerProjectItem


        private bool _popup_isOpen;
        public bool popup_isOpen
        {
            get
            {
                return _popup_isOpen;
            }
            set
            {
                _popup_isOpen = value;
                RaisePropertyChanged();
            }
        }



        public RelayCommand UC_DragEnterCmd { get; set; }
        public RelayCommand UC_DropCmd { get; set; }
        public RelayCommand UC_DragLeaveCmd { get; set; }
        public RelayCommand UC_MouseEnterCmd { get; set; }


        private void UC_DragLeave()
        {
            throw new NotImplementedException();
        }

        private void UC_Drop()
        {
            throw new NotImplementedException();
        }

        private void UC_MouseEnter()
        {
            throw new NotImplementedException();
        }

        //TODO: change to a behavior
        private void UC_DragEnter()
        {
            //e.Effects = DragDropEffects.Copy;
            //isPopupOpen = true;
            //e.Handled = true;
        }

        #endregion

        

        public event WindowStateEventHandler ChangedWindowState;

        /// <summary>
        /// Executes events as well as windowchangestate method
        /// </summary>
        /// <param name="wState"></param>
        public void RaiseWindowChangeState(WindowState? wState = default(WindowState?))
        {
            if (ChangedWindowState != null) ChangedWindowState(wState);
            WindowChangeState(wState);
        }

        public void WindowChangeState(WindowState? wState = default(WindowState?))
        {
            Vis_Minimized = false;
            Vis_MinimizedOpen = false;
            Vis_Maximized = false;
            Vis_ProjectView = false;

            switch (wState)
            {
                case WindowState.Minimized:
                    Vis_Minimized = true;
                    break;
                case WindowState.Pinned:
                case WindowState.Normal:
                    Vis_Maximized = true;
                    break;
                case WindowState.MinimizedOpen:
                    SizingStatus = false;
                    Vis_MinimizedOpen = true;
                    break;
                case WindowState.MinimizedDragging:
                    Vis_ProjectView = true;
                    Vis_Minimized = true;
                    break;
            }
        }

    }
}
