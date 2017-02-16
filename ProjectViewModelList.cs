using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Linq;

namespace Pin
{
    public class ProjectViewModelList : ViewModelBase
    {
        public delegate void ProjectEventHandler(object sender, ProjectViewModel project);

        public event EventHandler OnLoad;
        
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
        private int NextID;

        [Obsolete]
        private static ProjectViewModelList _Instance = new ProjectViewModelList();
        [Obsolete]
        public static ProjectViewModelList Instance => _Instance;
        public ProjectViewModelList()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
            if (Properties.Settings.Default.Projects == null)
            {
                Properties.Settings.Default.Projects = new StringCollection();
            }
            NextID = _Projects.Count;
        }
        
        private StringCollection _Projects => Properties.Settings.Default.Projects;
        public IList<ProjectViewModel> Projects { get; set; }



        public event ProjectEventHandler OnAdd;
        public void Add(object sender, Model.Project Model)
        {
            Model.ID = NextID;

            var ViewModel = new ProjectViewModel(Model);

            Projects.Add(ViewModel);
            _Projects.Add((string)Model);

            Save();
            if (OnAdd != null) OnAdd(sender, ViewModel);
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
        }

        public event ProjectEventHandler OnUpdate;
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

            Save();
            if (OnUpdate != null) OnUpdate(sender, ViewModel);    
        }

        public event ProjectEventHandler OnDelete;
        internal void Delete(object sender, Model.Project Model)
        {
            var ViewModel = new ProjectViewModel(Model);

            var index = Projects.IndexOf(ViewModel);

            Projects.RemoveAt(index);
            _Projects.RemoveAt(index);

            Save();
            if (OnDelete != null) OnDelete(sender, ViewModel);
        }

        internal bool isPrimaryProject(Model.Project project)
        {
            return project.Equals(Properties.Settings.Default.PrimaryProjectId);
        }

        public delegate void ActionEventChangedEventHandler(ActionEvent actionevent);
        public event ActionEventChangedEventHandler ActionEventChanged;

        public void setActionEvent(ActionEvent actionevent)
        {
            Properties.Settings.Default.ActionEvent = (int)actionevent;
            Properties.Settings.Default.Save();

            if (ActionEventChanged != null) ActionEventChanged(actionevent);
        }

        public delegate void ProjectChangedEventHandler(ProjectViewModel project);
        public event ProjectChangedEventHandler PrimaryProjectChanged;

                
        private ProjectViewModel DefaultProjectViewModel => new ProjectViewModel(new Model.Project("", "", new SolidColorBrush(Colors.Orange)));
        public ProjectViewModel PrimaryProject
        {
            get
            {
                // never returns null
                var vm = Projects?.FirstOrDefault(Model => Model.Project.ID == Properties.Settings.Default.PrimaryProjectId);
                if (vm == null)
                    vm = DefaultProjectViewModel;

                return DefaultProjectViewModel;
            }
            set
            {
                Properties.Settings.Default.PrimaryProjectId = value.Project.ID;

                if (PrimaryProjectChanged != null)
                {

                    if (value != null)
                    {
                        PrimaryProjectChanged(value);
                    }
                    else
                    {
                    }
                }
            }
        }
        
    }
}
