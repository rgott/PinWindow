using System.Windows;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pin
{
    /// <summary>
    /// Interaction logic for ProjectItem.xaml
    /// </summary>
    public partial class ProjectItem : UserControl
    {
        public ProjectViewModel DataModel { get; set; }
        public ProjectItem(ProjectViewModel project)
        {
            DataContext = DataModel = project;
            InitializeComponent();

            UI_ColorSelectionBox.FillColor = project.Project.Color;
        }
        public ProjectItem()
        {
            DataContext = DataModel = new ProjectViewModel(new Model.Project("Blah","Blah",new SolidColorBrush(Colors.Blue)));
            InitializeComponent();

            UI_ColorSelectionBox.FillColor = new System.Windows.Media.SolidColorBrush(Colors.Red);
        }

        private void UI_OpenWithExplorer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", DataModel.ProjectPath));
        }

        private void UI_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            DataModel.FillColor = UI_ColorSelectionBox.FillColor;
            UI_Btn_Cancel_Click(sender, e); // revert to previous state
        }

        private void UI_Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog path = new Forms.FolderBrowserDialog();
            path.ShowDialog();

            // change current version
            DataModel.ProjectPath = path.SelectedPath;
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
