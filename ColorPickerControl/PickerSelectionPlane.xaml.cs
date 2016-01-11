using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]


        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        IntPtr hdc;
        public PickerSelectionPlane()
        {
            InitializeComponent();
            
        }
         
        private void MajorColorSelectorPlane_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MajorColorSelection(e.GetPosition(MajorColorSelectorPlane));
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
                MajorColorSelection(e.GetPosition(MajorColorSelectorPlane));
            }
        }

        private void MajorColorSelection(Point point)
        {
            MajorColorSelector.Margin = new Thickness(0, point.Y, 0, 0);
            Point screenPoint = MajorColorSelectorPlane.PointToScreen(point);
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint color = GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            PrimaryFillColor.Fill = new SolidColorBrush(Color.FromArgb(255, r, g, b));
        }

        private void ColorSelectionGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point mousPos = e.GetPosition(ColorSelectionGrid);
                ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
            }

        }

        private void ColorSelectionGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
                Point mousPos = e.GetPosition(ColorSelectionGrid);
                ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
        }
    }
}
