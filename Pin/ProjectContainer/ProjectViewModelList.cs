using GalaSoft.MvvmLight;
using Pin.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Pin.ProjectContainer
{
    public class ProjectViewModelList : ViewModelBase
    {
        public ISettings Settings { get; set; }
        public IApplicationWindow Window { get; set; }

        public ObservableCollection<IProjectViewModel> Projects { get; set; } = new ObservableCollection<IProjectViewModel>();

        public IProjectViewModel Selected { get; set; }

        public ProjectViewModelList(IApplicationWindow Window, ISettings Settings)
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
                    Projects.Add(ProjectFactory.getViewModelFromModel(this, Window, Model.Project.Deserialize<Model.Project>(item)));
                }
            }
        }

        public bool Add(IProject Project)
        {
            if (Find(Project) == null)
            {
                Projects.Add(ProjectFactory.getViewModelFromModel(this, Window, Project));

                Settings.Projects.Add(Project.Serialize());
                Settings.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IProjectViewModel Find(IProject project)
        {
            return Projects.FirstOrDefault(m => m.Project.Equals(project));
        }

        public void Change(IProject OldProject, IProject NewProject)
        {
            var OldProjectVM = ProjectFactory.getViewModelFromModel(this, Window, OldProject);
            var NewProjectVM = ProjectFactory.getViewModelFromModel(this, Window, NewProject);

            Change(OldProjectVM, NewProjectVM);
        }

        public void Change(IProjectViewModel OldProjectVM, IProjectViewModel NewProjectVM)
        {
            var index = Projects.IndexOf(OldProjectVM);

            Projects[index] = NewProjectVM;
            Settings.Projects[index] = NewProjectVM.Project.Serialize();

            if (OldProjectVM.Project.Name.Equals(Settings.PrimaryProjectName))
            {
                Settings.PrimaryProjectName = NewProjectVM.Project.Name;
                Selected = NewProjectVM;
            }
            Settings.Save();
        }

        public void Remove(Model.IProject Model)
        {
            var index = Projects.IndexOf(ProjectFactory.getViewModelFromModel(this, Window, Model));

            Projects.RemoveAt(index);
            Settings.Projects.RemoveAt(index);

            Settings.Save();
        }
    }
}