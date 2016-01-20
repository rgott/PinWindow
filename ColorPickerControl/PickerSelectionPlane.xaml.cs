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

        public PickerSelectionPlane()
        {
            InitializeComponent();
            
        }
         
        private void MajorColorSelectorPlane_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(canInitiate)
            {
                PrimaryFillColor.Fill = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane, e));
                MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
            }
        }
        
        private void MajorColorSelectorPlane_MouseUp(object sender, MouseButtonEventArgs e)
        {
            canInitiate = true;
        }

        protected bool canInitiate = true;
        protected bool isInitiated = false;
        private void MajorColorSelectorPlane_MouseMove(object sender, MouseEventArgs e)
        {
            if(canInitiate && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                PrimaryFillColor.Fill = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane,e));
                MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
            }
        }

        private Color ColorSelection(Point screenPoint)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint color = GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            Console.WriteLine(Color.FromArgb(255, r, g, b));
            return Color.FromArgb(255, r, g, b);

        }
        private Color ColorSelection(UIElement UIComponent, MouseEventArgs e)
        {
            return ColorSelection(UIComponent.PointToScreen(e.GetPosition(UIComponent)));
        }
        private void ColorSelectionGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //if (Mouse.LeftButton == MouseButtonState.Pressed)
            //{
            //    Point mousPos = e.GetPosition(ColorSelectionGrid);
            //    ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
            //    LastColor.Fill = new SolidColorBrush(ColorSelection(ColorSelectionGrid.PointFromScreen(new Point(5, e.GetPosition(ColorSelectionGrid).Y))));
            //}
        }

        private void ColorSelectionGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point mousPos = e.GetPosition(ColorSelectionGrid);
            //ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
        }

        private void MajorColorSelectorPlane_MouseEnter(object sender, MouseEventArgs e)
        {
            //if(e.LeftButton != MouseButtonState.Pressed || !isInitiated)
            //{
            //    canInitiate = false;
            //}
            //else
            //{
            //    canInitiate = true;
            //}
        }
    }
}
