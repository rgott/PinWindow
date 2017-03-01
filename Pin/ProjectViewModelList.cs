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

        public ObservableCollection<IProjectViewModel> Projects { get; set; } = new ObservableCollection<IProjectViewModel>();
        public ProjectViewModelList(ISettings Settings)
        {
            this.Settings = Settings;
            if (Settings.Projects == null)
            {
                Settings.Projects = new StringCollection();
            }
            else
            {
                // load into viewable object
                foreach (string item in Settings.Projects)
                {
                    Projects.Add(ProjectFactory.getViewModelFromModel(CustomXmlSerializer.Deserialize<IProject>(item))); 
                }
            }
        }

        public bool Add(IProject Project)
        {
            // only unique projects
            if(Projects.Count(currentProject => currentProject == Project) != 0)
                return false;

            Projects.Add(ProjectFactory.getViewModelFromModel(Project));
            Settings.Projects.Add(Project.Serialize());

            Settings.Save();
            return true;
        }


        internal void Change(IProject Project)
        {
            var ProjectVM = ProjectFactory.getViewModelFromModel(Project);

            var index = Projects.IndexOf(ProjectVM);
            Projects[index] = ProjectVM;
            Settings.Projects[index] = Project.Serialize();

            if(Project.IsPrimary)
            {
                PrimaryProject = ProjectVM;
            }

            Settings.Save();
        }

        internal void Delete(Model.IProject Model)
        {
            var index = Projects.IndexOf(ProjectFactory.getViewModelFromModel(Model));

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
                return Projects?.FirstOrDefault(Model => Model.Project.IsPrimary) ?? ProjectFactory.getViewModelFromModel(ProjectFactory.DefaultProject);
            }
            set
            {
                Settings.PrimaryProjectName = value.Project.Name;

                PrimaryProjectChanged?.Invoke(value?.Project);
            }
        }
    }
}
