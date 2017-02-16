using System.Windows;
using System.Windows.Controls;

namespace Pin.MenuContainer
{
    /// <summary>
    /// Interaction logic for Maximized.xaml
    /// </summary>
    public partial class Maximized : UserControl
    {
        public Maximized()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void UI_Popup_Menu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            MouseOverController.isMouseOverMenu = false;
        }
        bool popup = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UI_Popup_Menu.IsOpen = popup = !popup;
        }
    }
}
