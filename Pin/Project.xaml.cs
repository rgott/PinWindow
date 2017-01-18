using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System;
using System.Collections;
using Pin.Model;

namespace Pin
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        public delegate void ProjectChangedEventHandler(object sender, int ProjectId);

        public event ProjectChangedEventHandler PrimaryProjectChanged;

        public Project()
        {
            InitializeComponent();
            // load projects in form 
            if(Properties.Settings.Default.Projects != null)
            {
                foreach (string item in Properties.Settings.Default.Projects)
                {
                    var project = Model.Project.Deserialize(item);
                    if (project.ID == Properties.Settings.Default.PrimaryProjectId)
                    {
                        addProject(project, true);
                    }
                    else
                    {
                        addProject(project, false);
                    }
                } 
            }
            else
            {
                Properties.Settings.Default.Projects = new StringCollection();
                Properties.Settings.Default.Save();
            }
        }

        private void addProject(Model.Project projectModel,bool isCheckedDefault = false)
        {
            ProjectItem projectItem = new ProjectItem(projectModel);

            RadioButton RButton = new RadioButton();
            RButton.Margin = new Thickness(0);
            RButton.Template = (ControlTemplate)FindResource("StyledRadioButton");
            RButton.GroupName = "Projects";
            RButton.Checked += RButton_Checked;

            RButton.Content = projectItem;
            projectItem.Deleted += new ProjectItem.DeletedEventHandler(delegate (object o, EventArgs e)
            {
                projectPanel.Children.Remove(RButton);
            });

            if (isCheckedDefault)
            {
                RButton.IsChecked = true;
            }

            projectPanel.Children.Add(RButton);
        }

        private void RButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton)
            {
                var RButtion = sender as RadioButton;
                if(RButtion.IsChecked == true)
                {
                    var id = (RButtion.Content as ProjectItem).ID;
                    Properties.Settings.Default.PrimaryProjectId = id;
                    Properties.Settings.Default.Save();
                    if (PrimaryProjectChanged != null) PrimaryProjectChanged(sender, id);
                }
            }
        }

        private void projects_Click(object sender, RoutedEventArgs e)
        {
            if(projects.IsChecked == true)
            {
                addP.Text = "x";
                popupToggle.IsOpen = true;
            }
            else
            {
                addP.Text = "+";
                popupToggle.IsOpen = false;
            }
        }

        private void popupToggle_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }

        private void UI_Btn_AddProject_Click(object sender, RoutedEventArgs e)
        {
            var ProjectModel = new Model.Project(
                Properties.Settings.Default.Projects.Count,
                UI_TxtBox_ProjectName.Text,
                UI_TxtBox_ProjectPath.Text.Replace('/', '\\'),
                UI_ColorPicker_ColorSelectionBox.FillColor);

            addProject(ProjectModel);
            Properties.Settings.Default.Projects.Add(ProjectModel.Serialize());
            Properties.Settings.Default.Save();
            popupToggle.IsOpen = false;
            addP.Text = "+";
            projects.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            UI_TxtBox_ProjectName.Text = "";
            UI_TxtBox_ProjectPath.Text = "";
            MouseOverController.isMouseOverMenu = false;
        }

       
        private void UI_Btn_FolderBrowse_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog path = new Forms.FolderBrowserDialog();
            path.ShowDialog();

            // change current version
            UI_TxtBox_ProjectPath.Text = path.SelectedPath;
        }
    }
}




