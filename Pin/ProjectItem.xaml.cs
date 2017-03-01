using System.Windows;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using Pin.Model;

namespace Pin
{
    /// <summary>
    /// Interaction logic for ProjectItem.xaml
    /// </summary>
    public partial class ProjectItem : UserControl
    {
        public IProject Project { get; set; }
        public ProjectItem(IProject project)
        {
            DataContext = Project = project;
            InitializeComponent();

            UI_ColorSelectionBox.FillColor = project.Color;
        }
        public ProjectItem()
        {
            InitializeComponent();

            UI_ColorSelectionBox.FillColor = new System.Windows.Media.SolidColorBrush(Colors.Red);
        }

        private void UI_OpenWithExplorer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", Project.Path));
        }

        private void UI_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            UI_Btn_Cancel_Click(sender, e); // revert to previous state
        }

        private void UI_Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog userGeneratedPath = new Forms.FolderBrowserDialog();
            userGeneratedPath.ShowDialog();

            // change current version
            Project.Path = userGeneratedPath.SelectedPath;
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
        }
    }
}
