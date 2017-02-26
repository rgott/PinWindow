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
                RaisePropertyChanged();
            }
        }

        public ProjectViewModel(Model.Project project)
        {
            Project = project;
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
