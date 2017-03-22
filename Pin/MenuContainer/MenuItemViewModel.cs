using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.ColorPicker;
using Pin.Model;
using Pin.ProjectContainer;
using System;
using System.Windows.Input;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace Pin.MenuContainer
{
    public class MenuItemViewModel : ViewModelBase
    {
        public MenuItemViewModel(ISettings Settings, IApplicationWindow Window, ProjectViewModelList pList)
        {
            this.Window = Window;

            InitCommands();

            Settings.ClipboardActionChanged += PList_ActionEventChanged;
            PList_ActionEventChanged(Settings.ClipboardAction);

            
            ProjectList = pList;

            NewProject = new Pin.ProjectContainer.ProjectViewModel(pList, Window);
        }

        public ICommand MouseLeave { get; set; }
        public ICommand ResumeWindow { get; private set; }
        public ICommand PauseWindow { get; private set; }
        public ICommand ExitBtnCmd { get; set; }
        public ICommand SizingBtnCmd { get; set; }

        public IApplicationWindow Window { get; private set; }

       

        private void InitCommands()
        {
            ResumeWindow = new RelayCommand(() => Window.ResumeState(this));
            PauseWindow = new RelayCommand(() => Window.PauseState(this));

            ExitBtnCmd = new RelayCommand(Window.onExit);
            SizingBtnCmd = new RelayCommand(SizingBtn);

            UI_Popup_Menu_ClickCmd = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });
            UI_Popup_Menu = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });

            UI_DragOut_Color = new SolidColorBrush(Colors.Orange);
            ChangeDirectory = new RelayCommand(ChangeDirectoryCmd);


        }

        private void SizingBtn()
        {
            switch (Window.State)
            {
                case WindowState.Normal:
                    Window.WindowChangeState(WindowState.MinimizedOpen);
                    break;
                default:
                    Window.WindowChangeState(WindowState.Normal);
                    break;
            }
        }
        private string _ClipboardActionText;
        public string ClipboardActionText
        {
            get
            {
                return _ClipboardActionText;
            }
            set
            {
                _ClipboardActionText = value;
                RaisePropertyChanged();
            }
        }

        private IProjectViewModel _SelectedProject;
        /// <summary>
        /// Primary project that is selected is the default project for dropping files into the project
        /// </summary>
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

        private void PList_ActionEventChanged(ClipboardEvent actionevent)
        {
            switch (actionevent)
            {
                case ClipboardEvent.Move:
                    ClipboardActionText = "Move";
                    break;
                case ClipboardEvent.Copy:
                    ClipboardActionText = "Copy";
                    break;
                default:
                    break;
            }
        }
        public ICommand UI_Popup_Menu { get; set; }

        public ICommand UI_Popup_Menu_ClickCmd { get; set; }

        private bool _UI_Popup_Menu_IsOpen = false;
        private bool UI_Popup_Menu_IsOpen
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

        public Model.Project AddProject { get; set; } = Model.Project.DefaultRed;
        

        #region ProjectsOverviewListVM
        
        public RelayCommand ChangeDirectory { get; set; }
        private void ChangeDirectoryCmd()
        {

            var userGeneratedPath = new Forms.FolderBrowserDialog();

            Window.PauseState(userGeneratedPath);// pause window while searching a directory
            userGeneratedPath.ShowDialog();
            Window.ResumeState(userGeneratedPath);

            // change current version
            if (!String.IsNullOrEmpty(userGeneratedPath.SelectedPath))
                AddProject.Path = userGeneratedPath.SelectedPath;
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

        private ProjectViewModel _NewProject;
        public ProjectViewModel NewProject
        {
            get
            {
                return _NewProject;

            }
            set
            {
                _NewProject = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        // project color
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
