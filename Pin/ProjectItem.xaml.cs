using System.Windows;
using Forms = System.Windows.Forms;
using System.Windows.Input;

namespace Pin
{
    /// <summary>
    /// Interaction logic for ProjectItem.xaml
    /// </summary>
    public partial class ProjectItem : UIProjectProperties
    {
        public ProjectItem(Model.Project project) : base(project)
        {
            DataContext = this;
            InitializeComponent();

            UI_ColorSelectionBox.FillColor = project.Color;
        }

        private void UI_OpenWithExplorer_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", ProjectPath));
        }

        private void UI_Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            FillColor = UI_ColorSelectionBox.FillColor;
            SaveProperties();
            UI_Btn_Cancel_Click(sender, e); // revert to previous state
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
            DeleteProperties();
        }
    }
}
