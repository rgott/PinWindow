using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for PickerSelectionPlane.xaml
    /// </summary>
    public partial class PickerSelectionPlane : UserControl
    {
        public PickerSelectionPlane()
        {
            InitializeComponent();
        }
         
        private void MajorColorSelectorPlane_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
        }
        
        private void MajorColorSelectorPlane_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void MajorColorSelectorPlane_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
            }
        }
    }
}
