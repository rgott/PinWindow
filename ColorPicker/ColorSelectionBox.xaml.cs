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

namespace Pin.ColorPicker
{

    /// <summary>
    /// Interaction logic for ColorSelectionBox.xaml
    /// </summary>
    public partial class ColorSelectionBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Brush FillColor
        {
            get
            {
                return (Brush)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
                NotifyPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(SolidColorBrush), typeof(ColorSelectionBox), new PropertyMetadata(new SolidColorBrush(Colors.Red)));




        public ColorSelectionBox()
        {
            DataContext = this;
            InitializeComponent();
            MainWindow.MinimizedWindow += MainWindow_MinimizedWindow;
        }

        private void MainWindow_MinimizedWindow(EventArgs e)
        {
            Dispatcher.Invoke(() => { UI_Popup_PickerPlane.IsOpen = false; });
        }


        private void UI_Popup_PickerPlane_MouseLeave(object sender, MouseEventArgs e)
        {
        }
        private void UI_Btn_ProjectColor_Click(object sender, RoutedEventArgs e)
        {
            UI_Popup_PickerPlane.IsOpen = true;
            MouseOverController.isMouseOverMenu = true;
        }

        private void UI_Popup_PickerPlane_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UI_Popup_PickerPlane.IsOpen = false;
            FillColor = UI_PickerSelectionPlane.FillColor;
            MouseOverController.isMouseOverMenu = false;
        }

    }
}
