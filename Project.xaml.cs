using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;
using System.Windows.Input;

namespace Pin
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        public Project()
        {
            InitializeComponent();
            
            // load projects in form 
            ProjectSettings.Instance.OnLoad += new System.EventHandler(delegate (object sender, System.EventArgs e)
            {
                foreach (ProjectViewModel item in ProjectSettings.Instance.Projects)
                {
                    projectPanel.Children.Add(newProject(item));
                }
            });

            ProjectSettings.Instance.OnAdd += new ProjectSettings.ProjectEventHandler(delegate (object sender, ProjectViewModel project)
            {
                projectPanel.Children.Add(newProject(project));
            });

            ProjectSettings.Instance.OnUpdate += new ProjectSettings.ProjectEventHandler(delegate (object sender, ProjectViewModel project)
            {
            });

            ProjectSettings.Instance.OnDelete += new ProjectSettings.ProjectEventHandler(delegate (object sender, ProjectViewModel project)
            {
                projectPanel.Children.Remove((sender as UserControl).Parent as UIElement);
            });
        }

        private RadioButton newProject(ProjectViewModel ViewModel)
        {
            ProjectItem projectItem = new ProjectItem(ViewModel);

            RadioButton RButton = new RadioButton();
            
            RButton.Template = (ControlTemplate)FindResource("StyledRadioButton");
            RButton.GroupName = "Projects";
            RButton.Checked += RButton_Checked;

            RButton.Content = projectItem;

            if (ProjectSettings.Instance.isPrimaryProject(ViewModel.Project))
            {
                RButton.IsChecked = true;
            }

            return RButton;
        }


        private void RButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton)
            {
                var RButtion = sender as RadioButton;
                if(RButtion.IsChecked == true)
                {
                    var id = (RButtion.Content as ProjectItem);
                    ProjectSettings.Instance.PrimaryProject = id.DataModel;
                }
            }
        }

        private void projects_Click(object sender, RoutedEventArgs e)
        {
            if(projects.IsChecked == true)
            {
                addP.Text = "x";
                popupToggle.IsOpen = true;
                MouseOverController.isProjectOpen = true;
            }
            else
            {
                addP.Text = "+";
                popupToggle.IsOpen = false;
                MouseOverController.isProjectOpen = false;
            }
        }

        private void popupToggle_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void UI_Btn_AddProject_Click(object sender, RoutedEventArgs e)
        {
            var ProjectModel = new Model.Project(
                UI_TxtBox_ProjectName.Text,
                UI_TxtBox_ProjectPath.Text.Replace('/', '\\'),
                UI_ColorPicker_ColorSelectionBox.FillColor);

            ProjectSettings.Instance.Add(this,ProjectModel);


            popupToggle.IsOpen = false;
            addP.Text = "+";
            projects.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            UI_TxtBox_ProjectName.Text = "";
            UI_TxtBox_ProjectPath.Text = "";
            MouseOverController.isProjectOpen = false;

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




