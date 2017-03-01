using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms = System.Windows.Forms;

namespace Pin
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        public ProjectViewModel(IProject Project)
        {
            this.Project = Project;
            OpenWithExplorer = new RelayCommand(OpenWithExplorerCmd);
        }
        public RelayCommand OpenWithExplorer { get; set; }
        private void OpenWithExplorerCmd()
        {
            Forms.FolderBrowserDialog userGeneratedPath = new Forms.FolderBrowserDialog();
            userGeneratedPath.ShowDialog();

            // change current version
            Project.Path = userGeneratedPath.SelectedPath;
        }


        private Model.IProject _Project;
        public Model.IProject Project
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
    }
}
