using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Drawing = System.Drawing;

namespace Pin.ColorPicker
{
    public class ColorSelectionViewModel : ViewModelBase
    {
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

        private Color ColorSelection(Point screenPoint)
        {
            IntPtr hdc = Win32.GetDC(IntPtr.Zero);
            uint color = Win32.GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            Win32.ReleaseDC(IntPtr.Zero, hdc);

            byte r = (byte)(color >> 0);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 16);

            // ignore any alpha channel (want non transparent colors)
            return System.Windows.Media.Color.FromArgb(255, r, g, b);
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

        

        private Brush _Color = new SolidColorBrush(Colors.Red);
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
        public Action<Brush> ColorChanged { get; set; }
        public Action<bool> PopupIsOpenChanged { get; set; }

        /// <summary>
        /// The color box of the selected color
        /// </summary>
        /// <param name="ColorChanged">Fired when the user has finished selecting a color (pressed done)</param>
        /// <param name="PopupIsOpenChanged"></param>
        public ColorSelectionViewModel(Action<Brush> ColorChanged,Action<bool> PopupIsOpenChanged)
        {
            this.ColorChanged = ColorChanged;
            this.PopupIsOpenChanged = PopupIsOpenChanged;

            Color_Click = new RelayCommand(Color_ClickCmd);
            Done_Click = new RelayCommand(Done_ClickCmd);
            ColorSelectionGrid_MouseMove = new RelayCommand(ColorSelectionGrid_MouseMoveCmd);
            ColorSelectionGrid_MouseDown = new RelayCommand(ColorSelectionGrid_MouseDownCmd);
            MajorColorSelectorPlane_MouseDown = new RelayCommand(MajorColorSelectorPlane_MouseDownCmd);
            MajorColorSelectorPlane_MouseMove = new RelayCommand(MajorColorSelectorPlane_MouseMoveCmd);
            PickerSelectionPlaneContext = this;
        }


        private Brush _PrimaryColor = new SolidColorBrush(Colors.Red);
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


        private bool _ColorSelection_isOpen = false;
        public bool ColorSelection_isOpen
        {
            get
            {
                return _ColorSelection_isOpen;
            }
            set
            {
                _ColorSelection_isOpen = value;
                PopupIsOpenChanged(value);
                RaisePropertyChanged();
            }
        }

        private void MajorColorSelectorPlane_MouseMoveCmd()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Win32.POINT tmpPoint;
                Win32.GetCursorPos(out tmpPoint);

                PrimaryColor = new SolidColorBrush(ColorSelection(tmpPoint));
                MajorColorSelector = new Thickness(0, Mouse.GetPosition(Mouse.DirectlyOver).Y, 0, 0);
            }
        }

        private void MajorColorSelectorPlane_MouseDownCmd()
        {
            Win32.POINT tmpPoint;
            Win32.GetCursorPos(out tmpPoint);

            Color = new SolidColorBrush(ColorSelection(tmpPoint));

            PrimaryColor = Color;
            MajorColorSelector = new Thickness(0, Mouse.GetPosition(Mouse.DirectlyOver).Y, 0, 0);
        }

        private void ColorSelectionGrid_MouseDownCmd()
        {
            Win32.POINT tmpPoint;
            Win32.GetCursorPos(out tmpPoint);
            
            ColorSelectionGridColorFinder = new Thickness(Mouse.GetPosition(Mouse.DirectlyOver).X - 5, Mouse.GetPosition(Mouse.DirectlyOver).Y - 5, 0, 0);
            Color = new SolidColorBrush(ColorSelection(tmpPoint));
        }

        private void ColorSelectionGrid_MouseMoveCmd()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Win32.POINT tmpPoint;
                Win32.GetCursorPos(out tmpPoint);

                Point mousPos = Mouse.GetPosition(Mouse.DirectlyOver);
                ColorSelectionGridColorFinder = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
                Color = new SolidColorBrush(ColorSelection(tmpPoint));
            }
        }

        private void Done_ClickCmd()
        {
            ColorChanged(Color);
            ColorSelection_isOpen = false;
        }

        private void Color_ClickCmd()
        {
            ColorSelection_isOpen = !ColorSelection_isOpen;
        }
    }
}
