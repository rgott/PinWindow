using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.ColorPicker;
using Pin.Model;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace Pin.MenuContainer
{
    public class MenuItemViewModel : ViewModelBase
    {
        public RelayCommand SizingBtnCmd { get; set; }
        public RelayCommand ExitBtnCmd { get; set; }
        public RelayCommand CheckedBtnCmd { get; set; }
        public RelayCommand UncheckedBtnCmd { get; set; }

        public RelayCommand AddProject_ClickCmd { get; set; }

        public RelayCommand ResumeWindow { get; set; }
        public RelayCommand PauseWindow { get; set; }

        public RelayCommand ProjectAddClick { get; set; }
        public RelayCommand UI_Popup_Menu { get; set; }

        public RelayCommand UI_Popup_Menu_ClickCmd { get; set; }
        private bool UI_Popup_Menu_IsOpen = false;
        public IMainWindow Window { get; set; }

        public ICommand OpenDragOverView { get; set; }
        public ICommand CloseDragOverView { get; set; }

        public Model.Project AddProject { get; set; } = new Project("", "", new SolidColorBrush(Colors.Black));
        public MenuItemViewModel(IMainWindow window, ProjectViewModelList pList)
        {
            ResumeWindow = new RelayCommand(() => Window.ResumeState(this));
            PauseWindow = new RelayCommand(() => Window.ResumeState(this));

            OpenDragOverView = new RelayCommand(() => Window.WindowChangeState(WindowState.MinimizedDragging));
            CloseDragOverView = new RelayCommand(() => Window.WindowChangeState(WindowState.Minimized));

            SizingBtnCmd = new RelayCommand(SizingBtn);
            ExitBtnCmd = new RelayCommand(window.onExit);
            
            this.Window = window;
            UI_Popup_Menu_ClickCmd = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });
            UI_Popup_Menu = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });
            AddProject_ClickCmd = new RelayCommand(AddProject_Click);

            
            ColorSelectionContext = new ColorSelectionViewModel(ColorSelectionContext_PopupIsOpenChanged);

            ColorSelectionContext.ColorChanged += (brush) => AddProject.Color = brush;

            ProjectAddClick = new RelayCommand(ProjectAddClickCmd);
            UI_DragOut_Color = new SolidColorBrush(Colors.Orange);
            ChangeDirectory = new RelayCommand(ChangeDirectoryCmd);
            ProjectList = pList;
            pList.ActionEventChanged += new ProjectViewModelList.ActionEventChangedEventHandler(delegate (ActionEvent e)
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

        private void ColorSelectionContext_PopupIsOpenChanged(bool obj)
        {
            if(obj)
            {
                Window.PauseState(this);
            }
            else
            {
                Window.ResumeState(this);
            }
        }

        private ColorSelectionViewModel _ColorSelectionContext;
        public ColorSelectionViewModel ColorSelectionContext
        {
            get
            {
                return _ColorSelectionContext;
            }
            set
            {
                _ColorSelectionContext = value;
                RaisePropertyChanged();
            }
        }

        private IProjectViewModel _SelectedProject;
        public IProjectViewModel SelectedProject
        {
            get
            {
                return _SelectedProject;
            }
            set
            {
                _SelectedProject = value;
                RaisePropertyChanged();
            }
        }



        #region ProjectsOverviewListVM


        public RelayCommand ChangeDirectory { get; set; }
        private void ChangeDirectoryCmd()
        {
            Forms.FolderBrowserDialog userGeneratedPath = new Forms.FolderBrowserDialog();

            Window.PauseState(userGeneratedPath);// pause window while searching a directory
            userGeneratedPath.ShowDialog();
            Window.ResumeState(userGeneratedPath);

            // change current version
            if (!String.IsNullOrEmpty(userGeneratedPath.SelectedPath))
                AddProject.Path = userGeneratedPath.SelectedPath;
        }
        private void ProjectAddClickCmd()
        {
            if (popupToggle_IsChecked)
            {
                addP_Text = "+";
                Window.ResumeState(this);
            }
            else
            {
                addP_Text = "x";
                Window.PauseState(this);
            }
            popupToggle_IsChecked = !popupToggle_IsChecked;
        }

        private ProjectViewModelList _ProjectList;
        public ProjectViewModelList ProjectList
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

        

        private bool _popupToggle_IsChecked;
        public bool popupToggle_IsChecked
        {
            get
            {
                return _popupToggle_IsChecked;
            }
            set
            {
                _popupToggle_IsChecked = value;
                RaisePropertyChanged();
            }
        }


        private string _addP_Text = "+";
        public string addP_Text
        {
            get
            {
                return _addP_Text;
            }
            set
            {
                _addP_Text = value;
                RaisePropertyChanged();
            }
        }


        private void AddProject_Click()
        {
            var ProjectModel = new Model.Project(
                AddProject.Name,
                AddProject.Path,
                AddProject.Color);

            ProjectList.Add(ProjectModel);

            Window.ResumeState(this);

            popupToggle_IsChecked = false;
            addP_Text = "+"; 
            AddProject.Name = "";
            AddProject.Path = "";
            ColorSelectionContext.isSelectionPlaneOpen(false);

        }
        #endregion

        #region MenuitemviewModel
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


        bool SizingStatus = false;
        private void SizingBtn()
        {
            SizingStatus = !SizingStatus;
            if (SizingStatus)
            { // show expanded
                Window.WindowChangeState(WindowState.Normal);
            }
            else
            { // show minimized open
                Window.WindowChangeState(WindowState.MinimizedOpen);
            }
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
        #endregion


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

    }
}
