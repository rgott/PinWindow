using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Linq;
using Pin.Model;

namespace Pin
{
    public class ProjectViewModelList : ViewModelBase
    {
        public ISettings Settings { get; set; }
        public IMainWindow Window { get; set; }

        public ObservableCollection<IProjectViewModel> Projects { get; set; } = new ObservableCollection<IProjectViewModel>();
        public ProjectViewModelList(IMainWindow Window,ISettings Settings)
        {
            this.Settings = Settings;
            this.Window = Window;

            if (Settings.Projects == null)
            {
                Settings.Projects = new StringCollection();
            }
            else
            {
                // load into viewable object
                foreach (string item in Settings.Projects)
                {
                    Projects.Add(ProjectFactory.getViewModelFromModel(this, Window, CustomXmlSerializer.Deserialize<Model.Project>(item))); 
                }
            }
        }

        public bool Add(IProject Project)
        {
            // only unique projects
            if(Projects.Count(currentProject => currentProject == Project) != 0)
                return false;

            Projects.Add(ProjectFactory.getViewModelFromModel(this, Window, Project));
            Settings.Projects.Add(Project.Serialize());

            Settings.Save();
            return true;
        }

        internal IProjectViewModel find(IProject project)
        {
            return Projects.FirstOrDefault(m => m.Project == project);
        }


        internal void Change(IProject OldProject, IProject NewProject)
        {
            var OldProjectVM = ProjectFactory.getViewModelFromModel(this, Window, OldProject);
            var NewProjectVM = ProjectFactory.getViewModelFromModel(this, Window, NewProject);

            Change(OldProjectVM, NewProjectVM);
        }

        internal void Change(IProjectViewModel OldProjectVM, IProjectViewModel NewProjectVM)
        {
            var index = Projects.IndexOf(OldProjectVM);
            Projects[index] = NewProjectVM;
            Settings.Projects[index] = NewProjectVM.Project.Serialize();

            if (NewProjectVM.Project.Name.Equals(Settings.PrimaryProjectName))
            {
                PrimaryProject = NewProjectVM;
            }

            Settings.Save();
        }

        internal void Delete(Model.IProject Model)
        {
            var index = Projects.IndexOf(ProjectFactory.getViewModelFromModel(this, Window, Model));

            Projects.RemoveAt(index);
            Settings.Projects.RemoveAt(index);

            Settings.Save();
        }


        public delegate void ActionEventChangedEventHandler(ActionEvent actionevent);
        public event ActionEventChangedEventHandler ActionEventChanged;
        public void setActionEvent(ActionEvent actionevent)
        {
            Settings.ActionEvent = (int)actionevent;
            Settings.Save();

            ActionEventChanged?.Invoke(actionevent);
        }


        public delegate void ProjectChangedEventHandler(IProject project);
        public event ProjectChangedEventHandler PrimaryProjectChanged;
        public IProjectViewModel PrimaryProject
        {
            get
            {
                return Projects?.FirstOrDefault(Model => Model.Project.Name == Settings.PrimaryProjectName) ?? ProjectFactory.getViewModelFromModel(this, Window, ProjectFactory.DefaultProject);
            }
            set
            {
                Settings.PrimaryProjectName = value.Project.Name;

                PrimaryProjectChanged?.Invoke(value?.Project);
            }
        }
    }
}
