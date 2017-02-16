using GalaSoft.MvvmLight;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Pin
{
    public class ProjectViewModel : ViewModelBase
    {
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            RaisePropertyChanged(propertyName);
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
        private Model.Project _Project;
        public Model.Project Project
        {
            get
            {
                return _Project;
            }
            set
            {
                _Project = value;
                UpdateAllProperties();
            }
        }

        private delegate void Properties();
        Properties UpdateAllProperties;
        public ProjectViewModel(Model.Project project)
        {
            UpdateAllProperties = new Properties(
                                   () => NotifyPropertyChanged("ProjectName"));
            UpdateAllProperties += () => NotifyPropertyChanged("ProjectPath");
            UpdateAllProperties += () => NotifyPropertyChanged("FillColor");

            Project = project;
        }

        public void SaveProperties()
        {
            ProjectViewModelList.Instance.Update(this,Project);
        }

        public void DeleteProperties()
        {
            ProjectViewModelList.Instance.Delete(this, Project);
        }

        public string ProjectName
        {
            get
            {
                return _Project.Name;
            }
            set
            {
                _Project.Name = value;
                NotifyPropertyChanged();
            }
        }

        public string ProjectPath
        {
            get
            {
                return _Project.Path;
            }
            set
            {
                _Project.Path = value;
                NotifyPropertyChanged();
            }
        }
        public Brush FillColor
        {
            get
            {
                return _Project.Color;
            }
            set
            {
                _Project.Color = value;
                NotifyPropertyChanged();
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is ProjectViewModel)
            {
                return Project.Equals(((ProjectViewModel)obj).Project);
            }
            else
            {
                return Project.Equals(obj);
            }
        }
    }
}
