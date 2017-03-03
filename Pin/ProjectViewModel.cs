using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.Model;
using System.Linq;
using Forms = System.Windows.Forms;
using System;

namespace Pin
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        public IMainWindow Window { get; set; }
        public ProjectViewModelList ProjectVM { get; set; }
        public IProject OrigionalProject { get; set; }
        public ProjectViewModel(ProjectViewModelList ProjectVM, IMainWindow Window, IProject Project)// isettings, iwindow
        {
            this.Window = Window;
            this.Project = Project;
            OrigionalProject = Project;
            this.ProjectVM = ProjectVM;

            DeleteProject = new RelayCommand(DeleteProjectCmd);
            OpenWithExplorer = new RelayCommand(OpenWithExplorerCmd);
            ChangeDirectory = new RelayCommand(ChangeDirectoryCmd);
            SaveEditorSettings = new RelayCommand(SaveEditorSettingsCmd);
            CancelEditorBtn = new RelayCommand(CancelEditorBtnCmd);
            EditBtn = new RelayCommand(EditBtnCmd);
            PauseWindowChange = new RelayCommand(PauseWindowChangeCmd);
            ResumeWindowChange = new RelayCommand(ResumeWindowChangeCmd);
        }

        

        public RelayCommand DeleteProject { get; private set; }
        private void DeleteProjectCmd()
        {
            ProjectVM.Delete(OrigionalProject);
        }

        public RelayCommand PauseWindowChange { get; private set; }
        private void PauseWindowChangeCmd()
        {
            // TODO: pause window change in iwindow
            // HOWDO: lock each pause with the this object to make sure the unpauser is the same as the pauser
        }
        public RelayCommand ResumeWindowChange { get; private set; }
        private void ResumeWindowChangeCmd()
        {
            // TODO: resume window change in iwindow
        }

        public RelayCommand EditBtn { get; private set; }
        private void EditBtnCmd()
        {
            ShowEditor = true;
        }

        public RelayCommand CancelEditorBtn { get; private set; }
        private void CancelEditorBtnCmd()
        {
            Project = OrigionalProject.Clone() as IProject; // reset any changes
            ShowEditor = false;
        }

        public RelayCommand SaveEditorSettings { get; private set; }
        private void SaveEditorSettingsCmd()
        {
            ProjectVM.Change(OrigionalProject, Project);

            OrigionalProject = Project;
            ShowEditor = false;
        }

        public RelayCommand ChangeDirectory { get; set; }
        private void ChangeDirectoryCmd()
        {
            Forms.FolderBrowserDialog userGeneratedPath = new Forms.FolderBrowserDialog();
            userGeneratedPath.ShowDialog();

            // change current version
            Project.Path = userGeneratedPath.SelectedPath;
        }
        public RelayCommand OpenWithExplorer { get; set; }
        private void OpenWithExplorerCmd()
        {
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

        public override bool Equals(object obj)
        {
            if(obj is IProjectViewModel)
            {
                return Equals(obj as IProjectViewModel);
            }
            return false;
        }
        public bool Equals(IProject project)
        {
            return OrigionalProject.Equals(Project);
        }
        public bool Equals(IProjectViewModel obj)
        {
            return OrigionalProject.Equals(obj.OrigionalProject);
        }
    }
}
