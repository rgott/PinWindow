using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pin.MenuContainer
{
    /// <summary>
    /// Interaction logic for ProjectList.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
            MouseOverController.MouseOverWindow += MouseOverController_handler;
        }

        private bool MouseOverController_handler()
        {
            return UIPOPUP.IsMouseOver || IsMouseOver;
        }

        private void UI_UserControl_DragLeave(object sender, DragEventArgs e)
        {
            MouseOverController.isMouseOverMenu = false;
        }

        private void UI_UserControl_DragEnter(object sender, DragEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;

        }

        private void UI_TextBlock_FirstProject_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }
    }
}
