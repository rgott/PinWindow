using GalaSoft.MvvmLight.Command;
using Pin.Model;
using System.Linq;
using Forms = System.Windows.Forms;
using System;
using Pin.ColorPicker;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Media;

namespace Pin.ProjectContainer
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        public IApplicationWindow Window { get; set; }
        public ProjectViewModelList Projects { get; set; }
        public IProject OrigionalProject { get; set; }

        public ICommand AddProject { get; set; }

        public ProjectViewModel(ProjectViewModelList Projects, IApplicationWindow Window) 
            : this(Projects,Window,Model.Project.DefaultRed) { }

        public ProjectViewModel(ProjectViewModelList Projects, IApplicationWindow Window, IProject Project)
        {
            this.Window = Window;
            OrigionalProject = Project.Clone() as IProject;
            this.Project = Project;
            this.Projects = Projects;

            ColorSelectionContext = new ColorSelectionViewModel(ColorSelectionContext_PopupisOpenChanged);

            ColorSelectionContext.ColorChanged += (brush) => Project.Color = brush;

            AddProject = new RelayCommand(AddProjectCmd);

            OpenWithExplorer = new RelayCommand(OpenWithExplorerCmd);
            ChangeDirectory = new RelayCommand(ChangeDirectoryCmd);
            SaveEditorSettings = new RelayCommand(SaveEditorSettingsCmd);
            CancelEditorBtn = new RelayCommand(CancelEditorBtnCmd);

            DeleteProject = new RelayCommand(() => Projects.Remove(OrigionalProject));
            EditBtn = new RelayCommand(() => ShowEditor = true);
            PauseWindowChange = new RelayCommand(() => Window.PauseState(this));
            ResumeWindowChange = new RelayCommand(() => Window.ResumeState(this));
        }

        private void AddProjectCmd()
        {
            if(Projects.Find(Project) == null)
            {
                ShowEditor = false;

                Window.ResumeState(this);

                Projects.Add(Project.Clone() as IProject);
                Project.Name = "";
                Project.Path = "";
            }
            else
            {
                Error.ErrorBox.Show("Cannot add a two projects of the same name.");
            }
        }
        private void ColorSelectionContext_PopupisOpenChanged(bool obj)
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

        public RelayCommand DeleteProject { get; private set; }

        public ColorSelectionViewModel ColorSelectionContext { get; set; }

        public RelayCommand PauseWindowChange { get; private set; }
        public RelayCommand ResumeWindowChange { get; private set; }
        public RelayCommand EditBtn { get; private set; }

        public RelayCommand CancelEditorBtn { get; private set; }
        private void CancelEditorBtnCmd()
        {
            Project = OrigionalProject.Clone() as IProject; // reset any changes
            ShowEditor = false;
        }

        public RelayCommand SaveEditorSettings { get; private set; }
        private void SaveEditorSettingsCmd()
        {
            ColorSelectionContext.Close();

            Projects.Change(OrigionalProject, Project);

            OrigionalProject = Project;
            ShowEditor = false;
        }

        public RelayCommand ChangeDirectory { get; set; }
        internal void ChangeDirectoryCmd()
        {
            ColorSelectionContext.Close();

            Window.PauseState(this);

            using (var userGeneratedPath = new Forms.FolderBrowserDialog())
            {
                userGeneratedPath.ShowDialog();

                Window.ResumeState(this);

                // change current version
                if(!String.IsNullOrEmpty(userGeneratedPath.SelectedPath))
                    Project.Path = userGeneratedPath.SelectedPath;
            }
        }

        public RelayCommand OpenWithExplorer { get; set; }
        private void OpenWithExplorerCmd()
        {
            ColorSelectionContext.Close();

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", Project.Path));
        }

        private IProject _Project;
        public IProject Project
        {
            get
            {
                return _Project;
            }
            set
            {
                _Project = value;
                RaisePropertyChanged();
            }
        }

        private bool _ShowEditor = false;
        public bool ShowEditor
        {
            get
            {
                return _ShowEditor;
            }
            set
            {
                _ShowEditor = value;
                
                RaisePropertyChanged();
            }
        }

        private bool _ShowInfo;

        public bool ShowInfo
        {
            get
            {
                return _ShowInfo;
            }
            set
            {
                _ShowInfo = value;
                RaisePropertyChanged();
            }
        }

        public Queue<string[]> FileToDrop { get; set; } = new Queue<string[]>();


        public override bool Equals(object obj)
        {
            if(obj is IProjectViewModel)
            {
                return Equals(obj as IProjectViewModel);
            }
            return false;
        }

        public bool Equals(IProjectViewModel obj)
        {
            return OrigionalProject.Equals(obj.OrigionalProject);
        }

        public override int GetHashCode()
        {
            return OrigionalProject.GetHashCode();
        }
    }
}
