using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pin
{
    public class UIProjectProperties : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
        public UIProjectProperties(Model.Project project)
        {
            UpdateAllProperties = new Properties(
                        () => NotifyPropertyChanged("ProjectName"));
            UpdateAllProperties += () => NotifyPropertyChanged("ProjectPath");
            UpdateAllProperties += () => NotifyPropertyChanged("FillColor");

            Project = project;
        }


        public void SaveProperties()
        {
            ProjectSettings.Instance.Update(this,Project);
        }

        public void DeleteProperties()
        {
            ProjectSettings.Instance.Delete(this, Project);
        }

        public string ProjectName
        {
            get
            {
                return _Project.ProjectName;
            }
            set
            {
                _Project.ProjectName = value;
                NotifyPropertyChanged();
            }
        }

        public string ProjectPath
        {
            get
            {
                return _Project.ProjectPath;
            }
            set
            {
                _Project.ProjectPath = value;
                NotifyPropertyChanged();
            }
        }
        public SolidColorBrush FillColor
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
    }

}
