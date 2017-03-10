using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pin.ColorPicker
{
    /// <summary>
    /// Interaction logic for PickerSelectionPlane.xaml
    /// </summary>
    public partial class PickerSelectionPlane : UserControl , INotifyPropertyChanged
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public SolidColorBrush FillColor
        {
            get
            {
                return (SolidColorBrush)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
                NotifyPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(SolidColorBrush), typeof(PickerSelectionPlane), new PropertyMetadata(new SolidColorBrush(Colors.Red)));



        System.Windows.Point previousPoint;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public PickerSelectionPlane()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void MajorColorSelectorPlane_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FillColor = new SolidColorBrush(ColorSelection(ColorSelectionGrid, previousPoint, 0));

            PrimaryFillColor.Fill = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane, e));
            MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
        }

        private void MajorColorSelectorPlane_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                PrimaryFillColor.Fill = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane, e));
                MajorColorSelector.Margin = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
            }
        }

        private Color ColorSelection(Point screenPoint)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint color = GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 0);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 16);
            return Color.FromArgb(255, r, g, b);
        }
        
        private Color ColorSelection(UIElement UIComponent, MouseEventArgs e,int offset = 0)
        {
            return ColorSelection(UIComponent, e.GetPosition(UIComponent),offset);
        }
        private Color ColorSelection(UIElement UIComponent, System.Windows.Point point, int offset = 0)
        {
            return ColorSelection(UIComponent.PointToScreen(new System.Windows.Point(point.X + offset, point.Y + offset)));
        }

        private void ColorSelectionGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point mousPos = e.GetPosition(ColorSelectionGrid);
                ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
                FillColor = new SolidColorBrush(ColorSelection(ColorSelectionGrid, e,0));
                previousPoint = mousPos;
            }
        }

        private void ColorSelectionGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousPos = e.GetPosition(ColorSelectionGrid);
            ColorSelectionGridColorFinder.Margin = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
            FillColor = new SolidColorBrush(ColorSelection(ColorSelectionGrid, e, 0));
            previousPoint = mousPos;
        }
    }
}
