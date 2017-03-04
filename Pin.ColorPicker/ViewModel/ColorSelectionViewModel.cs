using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Drawing = System.Drawing;

namespace Pin.ColorPicker
{
    public class ColorSelectionViewModel : ViewModelBase
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);


        System.Windows.Point previousPoint;
        private Drawing.Color ColorSelection(UIElement UIComponent, MouseEventArgs e, int offset = 0)
        {
            return ColorSelection(UIComponent, e.GetPosition(UIComponent), offset);
        }
        private Drawing.Color ColorSelection(UIElement UIComponent, System.Windows.Point point, int offset = 0)
        {
            return ColorSelection(UIComponent.PointToScreen(new Point(point.X + offset, point.Y + offset)));
        }


        private Thickness _ColorSelectionGridColorFinder;
        public Thickness ColorSelectionGridColorFinder
        {
            get
            {
                // adjust for center
                _ColorSelectionGridColorFinder.Top -= 5;
                _ColorSelectionGridColorFinder.Left -= 5;

                return _ColorSelectionGridColorFinder; 
            }
            set
            {
                _ColorSelectionGridColorFinder = value;
                RaisePropertyChanged();
            }
        }


        private Thickness _MajorColorSelector;
        public Thickness MajorColorSelector
        {
            get
            {
                return _MajorColorSelector;
            }
            set
            {
                _MajorColorSelector = value;
                RaisePropertyChanged();
            }
        }

        private Drawing.Color ColorSelection(Point screenPoint)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint color = GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            ReleaseDC(IntPtr.Zero, hdc);
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 0);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 16);
            return Drawing.Color.FromArgb(255, r, g, b);
        }

        private Brush _Color;
        public Brush Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                RaisePropertyChanged();
            }
        }


        public ICommand Color_Click { get; set; }
        public ICommand Done_Click { get; set; }
        public ICommand MajorColorSelectorPlane_MouseMove { get; set; }
        public ICommand MajorColorSelectorPlane_MouseDown { get; set; }
        public ICommand ColorSelectionGrid_MouseMove { get; set; }
        public ICommand ColorSelectionGrid_MouseDown { get; set; }
        public ColorSelectionViewModel PickerSelectionPlaneContext { get; set; }

        public ColorSelectionViewModel()
        {
            Color_Click = new RelayCommand(Color_ClickCmd);
            Done_Click = new RelayCommand(Done_ClickCmd);
            //ColorSelectionGrid_MouseMove = new RelayCommand(ColorSelectionGrid_MouseMoveCmd);
            //ColorSelectionGrid_MouseDown = new RelayCommand(ColorSelectionGrid_MouseDownCmd);
            //MajorColorSelectorPlane_MouseDown = new RelayCommand(MajorColorSelectorPlane_MouseDownCmd);
            //MajorColorSelectorPlane_MouseMove = new RelayCommand(MajorColorSelectorPlane_MouseMoveCmd);
            PickerSelectionPlaneContext = this;
        }


        private Brush _PrimaryColor;
        public Brush PrimaryColor
        {
            get
            {
                return _PrimaryColor;
            }
            set
            {
                _PrimaryColor = value;
                RaisePropertyChanged();
            }
        }

        //private void MajorColorSelectorPlane_MouseMoveCmd()
        //{
        //    if (Mouse.LeftButton == MouseButtonState.Pressed)
        //    {
        //        PrimaryColor = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane, e));
        //        MajorColorSelector = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
        //    }
        //}

        //private void MajorColorSelectorPlane_MouseDownCmd()
        //{
        //    Color = new SolidColorBrush(ColorSelection(ColorSelectionGrid, previousPoint, 0));

        //    PrimaryColor = new SolidColorBrush(ColorSelection(MajorColorSelectorPlane, e));
        //    MajorColorSelector = new Thickness(0, e.GetPosition(MajorColorSelectorPlane).Y, 0, 0);
        //}

        //private void ColorSelectionGrid_MouseDownCmd()
        //{
        //    Point mousPos = e.GetPosition(ColorSelectionGrid);
        //    ColorSelectionGridColorFinder = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
        //    Color = new SolidColorBrush(ColorSelection(ColorSelectionGrid, e, 0));
        //    previousPoint = mousPos;
        //}

        //private void ColorSelectionGrid_MouseMoveCmd()
        //{
        //    if (Mouse.LeftButton == MouseButtonState.Pressed)
        //    {
        //        System.Windows.Point mousPos = e.GetPosition(ColorSelectionGrid);
        //        ColorSelectionGridColorFinder = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
        //        Color = new SolidColorBrush(ColorSelection(ColorSelectionGrid, e, 0));
        //        previousPoint = mousPos;
        //    }
        //}

        private void Done_ClickCmd()
        {
            throw new NotImplementedException();
        }

        private void Color_ClickCmd()
        {
            throw new NotImplementedException();
        }

        

    }
}
