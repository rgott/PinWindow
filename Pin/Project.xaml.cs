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
            
        }

        private void RButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is RadioButton)
            {
                
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

        
    }
}




