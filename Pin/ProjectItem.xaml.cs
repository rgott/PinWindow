using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Forms = System.Windows.Forms;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pin.ColorPicker;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Pin
{
    /// <summary>
    /// Interaction logic for ProjectItem.xaml
    /// </summary>
    public partial class ProjectItem : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private SolidColorBrush _FillColor;
        public SolidColorBrush FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                _FillColor = value;
                NotifyPropertyChanged();
            }
        }
        public int ID { get; set; }
        public string ProjectName
        {
            get { return (string)GetValue(ProjectNameProperty); }
            set { SetValue(ProjectNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectNameProperty =
            DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectItem), new PropertyMetadata(default(string)));

        public string ProjectPath
        {
            get { return (string)GetValue(ProjectPathProperty); }
            set { SetValue(ProjectPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectPathProperty =
            DependencyProperty.Register("ProjectPath", typeof(string), typeof(ProjectItem), new PropertyMetadata(default(string)));


        private string _Cached_Project;
        public string Cached_Project
        {
            get
            {
                return _Cached_Project;
            }
            set
            {
                _Cached_Project = value;
            }
        } // serialized version

        public ProjectItem()
        {
            DataContext = this;
            InitializeComponent();
            UI_ColorSelectionBox.FillColor.Changed += FillColor_Changed;
        }
        public ProjectItem(Model.Project project) : this()
        {
            this.ID = project.ID;
            this.ProjectName = project.ProjectName;
            this.ProjectPath = project.ProjectPath;
            FillColor = project.Color;
            UI_ColorSelectionBox.FillColor = project.Color;
            Cached_Project = project.Serialize();
        }


        private void FillColor_Changed(object sender, EventArgs e)
        {
            FillColor = UI_ColorSelectionBox.FillColor;
            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_Project);
            if (settingIndex != -1)
            {
                var tempProject = Model.Project.Deserialize(Properties.Settings.Default.Projects[settingIndex]);
                tempProject.Color = FillColor;
                Properties.Settings.Default.Projects[settingIndex] = tempProject.Serialize();
                Properties.Settings.Default.Save();
            }
        }

       
        private void UI_Btn_OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", ProjectPath));
        }

        private void UI_MenuItem_OpenWithExplorer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", ProjectPath));
        }

        private void UI_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            // change saved version

            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_Project);

            if (settingIndex != -1)
            {
                var ProjectModel = new Model.Project(settingIndex, ProjectName, ProjectPath.Replace('/', '\\'), FillColor).Serialize();

                Properties.Settings.Default.Projects[settingIndex] = ProjectModel;

                Cached_Project = ProjectModel;
                FillColor = UI_ColorSelectionBox.FillColor;

                //if(settingIndex == Properties.Settings.Default.PrimaryProjectId)
                //{
                //    Properties.Settings.Default.PrimaryProjectId = -1; // initiate a change event
                //    Properties.Settings.Default.PrimaryProjectId = settingIndex;
                //}
                Properties.Settings.Default.Save();

            }
            UI_Btn_Cancel_Click(sender, e);

        }

        private void UI_Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog path = new Forms.FolderBrowserDialog();
            path.ShowDialog();

            // change current version
            ProjectPath = path.SelectedPath;
        }

        private void UI_Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            UI_Grid_View.Visibility = Visibility.Visible;
            UI_Grid_Edit.Visibility = Visibility.Hidden;
        }

        private void UI_MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            UI_Grid_Edit.Visibility = Visibility.Visible;
            UI_Grid_View.Visibility = Visibility.Hidden;
        }


        private void UI_ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }

        private void UI_ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            MouseOverController.isMouseOverMenu = false;
        }

        private void UI_ContextMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }

        private void UI_ContextMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverController.isMouseOverMenu = false;
        }

        private void UI_MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            // change saved version
            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_Project);
            if (settingIndex != -1)
            {
                Properties.Settings.Default.Projects.RemoveAt(settingIndex);
                Properties.Settings.Default.Save();
            }
            OnDeleted(EventArgs.Empty); // send to listener to be deleted by Project.cs
        }
        #region Delete Event
        public delegate void DeletedEventHandler(object sender, EventArgs e);

        public event DeletedEventHandler Deleted;

        protected virtual void OnDeleted(EventArgs e)
        {
            if (Deleted != null)
                Deleted(this, e);
        }
        #endregion

    }
}
