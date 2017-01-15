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
        private Brush _FillColor;
        public Brush FillColor
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

        public string Cached_ProjectName { get; set; }

        public ProjectItem()
        {
            DataContext = this;
            InitializeComponent();
            UI_ColorSelectionBox.FillColor.Changed += FillColor_Changed;
        }
        public ProjectItem(Model.Project project) : this()
        {
            this.ProjectName = project.ProjectName;
            this.ProjectPath = project.ProjectPath;
            FillColor = project.Color;
            UI_ColorSelectionBox.FillColor = project.Color;
            Cached_ProjectName = project.ProjectName;
        }


        private void FillColor_Changed(object sender, EventArgs e)
        {
            FillColor = UI_ColorSelectionBox.FillColor;
            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_ProjectName);
            if (settingIndex != -1)
            {
                ((Model.Project)Properties.Settings.Default.Projects[settingIndex]).Color = FillColor;
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
            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_ProjectName);
            if (settingIndex != -1)
            {
                Properties.Settings.Default.Projects[settingIndex] = new Model.Project(ProjectName, ProjectPath.Replace('/', '\\'), FillColor);
                Properties.Settings.Default.Save();
            }
            Cached_ProjectName = ProjectName;

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
            if (Cached_ProjectName == null)
            {
                Cached_ProjectName = ProjectName;
            }
            // change saved version
            int settingIndex = Properties.Settings.Default.Projects.IndexOf(Cached_ProjectName);
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
