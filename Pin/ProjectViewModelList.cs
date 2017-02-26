using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Linq;
using Pin.Properties;

namespace Pin
{
    public class ProjectViewModelList : ViewModelBase
    {
        public delegate void ProjectEventHandler(object sender, ProjectViewModel project);

        public event ProjectEventHandler OnAdd;
        public event ProjectEventHandler OnUpdate;
        public event ProjectEventHandler OnDelete;

        public delegate void ProjectChangedEventHandler(ProjectViewModel project);
        public event ProjectChangedEventHandler PrimaryProjectChanged;

        public delegate void ActionEventChangedEventHandler(ActionEvent actionevent);
        public event ActionEventChangedEventHandler ActionEventChanged;

        public event EventHandler OnLoad;

        #region Rubbish
        [Obsolete]
        public void Load()
        {
            if (_Projects == null) Properties.Settings.Default.Projects = new StringCollection();
            Projects = new ObservableCollection<ProjectViewModel>();
            foreach (string item in _Projects)
            {
                Projects.Add(new ProjectViewModel((Model.Project)item)); // deserialize all
            }

            if (PrimaryProjectChanged != null) PrimaryProjectChanged(PrimaryProject);

            if (OnLoad != null) OnLoad(this, EventArgs.Empty);
        }
        [Obsolete]
        private static ProjectViewModelList _Instance = new ProjectViewModelList(Properties.Settings.Default);

        [Obsolete]
        public static ProjectViewModelList Instance => _Instance;

        #endregion

        private StringCollection _Projects => Settings.Projects;
        public ObservableCollection<ProjectViewModel> Projects { get; set; }
        public ISettings Settings { get; set; }
        public ProjectViewModelList(ISettings Settings)
        {
            this.Settings = Settings;
            if(_Projects == null) Settings.Projects = new StringCollection();
            Projects = new ObservableCollection<ProjectViewModel>();

            foreach (string item in _Projects)
            {
                Projects.Add(new ProjectViewModel((Model.Project)item)); // deserialize all
            }
        }

        public bool Add(object sender, Model.Project Model)
        {
            if(Projects.FirstOrDefault(m => m.Project == Model) == null)
                return false;

            var ViewModel = new ProjectViewModel(Model);

            Projects.Add(ViewModel);
            _Projects.Add((string)Model);

            Settings.Save();
            if (OnAdd != null) OnAdd(sender, ViewModel);

            return true;
        }


        internal void Update(object sender, Model.Project Model)
        {
            var ViewModel = new ProjectViewModel(Model);

            var index = Projects.IndexOf(ViewModel);

            Projects[index] = ViewModel;
            _Projects[index] = (String)Model;

            if(Model.Equals(PrimaryProject))
            {
                PrimaryProject = ViewModel;
            }

            Settings.Save();
            if (OnUpdate != null) OnUpdate(sender, ViewModel);    
        }

        internal void Delete(object sender, Model.Project Model)
        {
            var ViewModel = new ProjectViewModel(Model);

            var index = Projects.IndexOf(ViewModel);

            Projects.RemoveAt(index);
            _Projects.RemoveAt(index);

            Settings.Save();
            if (OnDelete != null) OnDelete(sender, ViewModel);
        }

        public bool isPrimaryProject(Model.Project project)
        {
            return project.Equals(Settings.PrimaryProjectName);
        }

        public void setActionEvent(ActionEvent actionevent)
        {
            Settings.ActionEvent = (int)actionevent;
            Settings.Save();

            if (ActionEventChanged != null) ActionEventChanged(actionevent);
        }

        private ProjectViewModel DefaultProjectViewModel => new ProjectViewModel(new Model.Project("", "", new SolidColorBrush(Colors.Orange)));
        public ProjectViewModel PrimaryProject
        {
            get
            {
                var vm = Projects?.FirstOrDefault(Model => Model.ProjectName == Settings.PrimaryProjectName);
                if (vm == null)
                    vm = DefaultProjectViewModel;

                return vm;
            }
            set
            {
                Settings.PrimaryProjectName = value.Project.Name;

                if (PrimaryProjectChanged != null)
                    PrimaryProjectChanged(value);
            }
        }
    }
}
