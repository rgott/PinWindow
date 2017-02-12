using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Maximized.xaml
    /// </summary>
    public partial class Maximized : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(Maximized), new PropertyMetadata(default(Brush)));


        public Maximized()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private bool _UI_Popup_Menu_IsOpen = false;
        public bool UI_Popup_Menu_IsOpen
        {
            get
            {
                return _UI_Popup_Menu_IsOpen;
            }
            set
            {
                _UI_Popup_Menu_IsOpen = value;
                NotifyPropertyChanged();
            }
        }

        private void UI_Popup_Menu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UI_Popup_Menu_IsOpen = false;
            MouseOverController.isMouseOverMenu = false;
        }
        public event RoutedEventHandler SizingClicked;
        public event RoutedEventHandler TackClicked;
        public event RoutedEventHandler ExitClicked;


        public delegate void WindowStateEventHandler(MouseOverController.WindowState requestState);
        public event WindowStateEventHandler RequestChangeWindowState;
        private void UI_Btn_Sizing_Click(object sender, RoutedEventArgs e)
        {
        }


        private void UI_Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            UI_Popup_Menu_IsOpen = !UI_Popup_Menu_IsOpen;
        }

        private void UI_Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (ExitClicked != null) ExitClicked(sender, e);
            
        }

        private void UI_ToggleBtn_Tack_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void UI_ToggleBtn_Tack_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
