﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.Model;
using System;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace Pin.MenuContainer
{
    public class MenuItemViewModel : ViewModelBase
    {
        private bool PinStatus = false;

        public RelayCommand SizingBtnCmd { get; set; }
        public RelayCommand ExitBtnCmd { get; set; }
        public RelayCommand DragOutCmd { get; set; }
        public RelayCommand CheckedBtnCmd { get; set; }
        public RelayCommand UncheckedBtnCmd { get; set; }

        public RelayCommand AddProject_ClickCmd { get; set; }

        public RelayCommand MouseOverFalseCmd => new RelayCommand(() => MouseOverController.isMouseOverMenu = false);
        public RelayCommand MouseOverTrueCmd => new RelayCommand(() => MouseOverController.isMouseOverMenu = true);


        public RelayCommand UI_Popup_Menu { get; set; }

        public RelayCommand UI_Popup_Menu_ClickCmd { get; set; }

        private bool UI_Popup_Menu_IsOpen = false;
        public IMainWindow Window { get; set; }

        public RelayCommand DeleteProject { get; set; }

        public MenuItemViewModel(IMainWindow window, ProjectViewModelList pList)
        {
            UncheckedBtnCmd = new RelayCommand(UncheckedBtn);
            CheckedBtnCmd = new RelayCommand(CheckedBtn);
            SizingBtnCmd = new RelayCommand(SizingBtn);
            ExitBtnCmd = new RelayCommand(window.onExit);
            DragOutCmd = new RelayCommand(DragOut);
            this.Window = window;
            UI_Popup_Menu_ClickCmd = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });
            UI_Popup_Menu = new RelayCommand(() => { UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen; });
            AddProject_ClickCmd = new RelayCommand(AddProject_Click);
            UI_Btn_FolderBrowse_ClickCmd = new RelayCommand(UI_Btn_FolderBrowse_Click);
            //PinContainer
            UC_DragEnterCmd = new RelayCommand(() => UC_DragEnter());
            UC_DragLeaveCmd = new RelayCommand(() => UC_DragLeave());
            UC_DropCmd = new RelayCommand(() => UC_Drop());
            UC_MouseEnterCmd = new RelayCommand(() => UC_MouseEnter());

            UI_DragOut_Color = new SolidColorBrush(Colors.Orange);

            ProjectList = pList;
            DeleteProject = new RelayCommand(() => ProjectList.Delete(SelectedProject));
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

        


        #region Properties
        private IProject _SelectedProject;
        public IProject SelectedProject
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

       
        //public delegate void ProjectItemDropEventHandler(object sender, Model.Project project, string[] sourcePaths);
        //public event ProjectItemDropEventHandler ProjectItemDropped;
        public RelayCommand UI_Btn_FolderBrowse_ClickCmd { get; set; } 
        private void UI_Btn_FolderBrowse_Click()
        {
            Forms.FolderBrowserDialog path = new Forms.FolderBrowserDialog();
            path.ShowDialog();

            // change current version
            UI_TxtBox_ProjectPath = path.SelectedPath;
        }

        private string _UI_TxtBox_ProjectName;
        public string UI_TxtBox_ProjectName
        {
            get
            {
                return _UI_TxtBox_ProjectName;
            }
            set
            {
                _UI_TxtBox_ProjectName = value;
                RaisePropertyChanged();
            }
        }

        private string _UI_TxtBox_ProjectPath;
        public string UI_TxtBox_ProjectPath
        {
            get
            {
                return _UI_TxtBox_ProjectPath;
            }
            set
            {
                _UI_TxtBox_ProjectPath = value.Replace('/', '\\');
                RaisePropertyChanged();
            }
        }


        private Brush _UI_ColorPicker_ColorSelectionBox;
        public Brush UI_ColorPicker_ColorSelectionBox
        {
            get
            {
                return _UI_ColorPicker_ColorSelectionBox;
            }
            set
            {
                _UI_ColorPicker_ColorSelectionBox = value;
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
                UI_TxtBox_ProjectName,
                UI_TxtBox_ProjectPath,
                UI_ColorPicker_ColorSelectionBox);

            ProjectList.Add(ProjectModel);

            popupToggle_IsChecked = false;
            addP_Text = "+";
            UI_TxtBox_ProjectName = "";
            UI_TxtBox_ProjectPath = "";
            MouseOverController.isProjectOpen = false;
        }

        private void UncheckedBtn()
        {
            Window.WindowChangeState(WindowState.Pinned);
            PinStatus = !PinStatus;
        }

        private void CheckedBtn()
        {
            Window.WindowChangeState(WindowState.Pinned);
            PinStatus = !PinStatus;
        }

        string[] lastDraggedIn;
        private void DragOut()
        {
            // TODO: change null
            DropDataHandler.dragDataOut(null, lastDraggedIn);
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

        internal void RaiseWindowChangeState(WindowState minimizedDragging)
        {
            Window.WindowChangeState(minimizedDragging);
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
