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
            if(Properties.Settings.Default.Projects != null)
            {
                foreach (Model.Project item in Properties.Settings.Default.Projects)
                {
                    addProject(item);
                } 
            }
            else
            {
                Properties.Settings.Default.Projects = new ArrayList();
                Properties.Settings.Default.Save();
            }
        }

        public string getCurrentProjectPath()
        {
            // TODO: get path
            //foreach(UIElement el in projectPanel.Children)
                //((TextBlock)((Grid)((RadioButton)el))).Text;
            return ""; // convert to list panel to select
        }

        private void addProject(Model.Project project)
        {
            ProjectItem item = new ProjectItem(project);
            // add xaml
            RadioButton RButton = new RadioButton();
            RButton.Template = (ControlTemplate)FindResource("StyledRadioButton");

            RButton.Content = item;

            item.Deleted += new ProjectItem.DeletedEventHandler(delegate (object o, EventArgs e)
            {
                projectPanel.Children.Remove(RButton);
            });
            projectPanel.Children.Add(RButton);
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

        private void popupToggle_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void UI_Btn_AddProject_Click(object sender, RoutedEventArgs e)
        {
            var ProjectModel = new Model.Project(
                UI_TxtBox_ProjectName.Text,
                UI_TxtBox_ProjectPath.Text.Replace('/', '\\'),
                UI_ColorPicker_ColorSelectionBox.FillColor);

            addProject(ProjectModel);
            Properties.Settings.Default.Projects.Add(ProjectModel);
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




