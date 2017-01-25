using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pin.Model;
using System.Windows.Media;
using static Pin.MouseOverController;

namespace Pin
{
    public class ProjectSettings
    {
        public delegate void ProjectEventHandler(object sender, Model.Project project);

        public event EventHandler OnLoad;
        public void Load()
        {
            Projects = new List<Model.Project>();
            foreach (string item in _Projects)
            {
                Projects.Add(item); // deserialize all
            }

            if (PrimaryProjectChanged != null) PrimaryProjectChanged(PrimaryProject);

            if (OnLoad != null) OnLoad(this, EventArgs.Empty);
        }
        private int NextID; 

        private static ProjectSettings _Instance = new ProjectSettings();
        public static ProjectSettings Instance => _Instance;
        private ProjectSettings()
        {
            Projects = new List<Model.Project>();
            if (Properties.Settings.Default.Projects == null)
            {
                Properties.Settings.Default.Projects = new StringCollection();
            }
            NextID = _Projects.Count;
        }
        
        public StringCollection _Projects => Properties.Settings.Default.Projects;
        public List<Model.Project> Projects { get; set; }



        public event ProjectEventHandler OnAdd;
        public void Add(object sender, Model.Project project)
        {
            project.ID = NextID;

            Projects.Add(project);
            _Projects.Add(project);

            Save();
            if (OnAdd != null) OnAdd(sender, project);
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
        }

        public event ProjectEventHandler OnUpdate;
        internal void Update(object sender, Model.Project project)
        {
            var index = Projects.IndexOf(project);

            Projects[index] = project;
            _Projects[index] = project;

            if(project.Equals(PrimaryProject))
            {
                PrimaryProject = project;
            }

            Save();
            if (OnUpdate != null) OnUpdate(sender,project);    
        }

        public event ProjectEventHandler OnDelete;
        internal void Delete(object sender, Model.Project project)
        {
            var index = Projects.IndexOf(project);

            Projects.RemoveAt(index);
            _Projects.RemoveAt(index);

            Save();
            if (OnDelete != null) OnDelete(sender, project);
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

        public delegate void ProjectChangedEventHandler(Model.Project project);
        public event ProjectChangedEventHandler PrimaryProjectChanged;

        public Model.Project PrimaryProject
        {
            get
            {
                return Projects?.Find(m => m.ID == Properties.Settings.Default.PrimaryProjectId);
            }
            set
            {
                Properties.Settings.Default.PrimaryProjectId = value.ID;
                if (PrimaryProjectChanged != null) PrimaryProjectChanged(value);

            }
        }
    }
}
