using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LibraryImports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static LibraryImports.Win32;
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

        private Color ColorSelection(POINT screenPoint)
        {
            IntPtr hdc = Win32.GetDC(IntPtr.Zero);
            uint color = Win32.GetPixel(hdc, (int)screenPoint.X, (int)screenPoint.Y);
            Win32.ReleaseDC(IntPtr.Zero, hdc);

            var r = (byte)(color >> 0);
            var g = (byte)(color >> 8);
            var b = (byte)(color >> 16);

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

        private Brush _SelectionColor = new SolidColorBrush(Colors.Red);
        public Brush SelectionColor
        {
            get
            {
                return _SelectionColor;
            }
            set
            {
                _SelectionColor = value;
                RaisePropertyChanged();
            }
        }


        public ICommand Color_Click { get; set; }
        public ICommand Done_Click { get; set; }
        public ICommand MajorColorSelectorPlane_MouseMove { get; set; }
        public ICommand MajorColorSelectorPlane_MouseDown { get; set; }
        public ICommand ColorSelectionGrid_MouseMove { get; set; }
        public ICommand ColorSelectionGrid_MouseDown { get; set; }
        public Action<bool> PopupIsOpenChanged { get; set; }

        public delegate void ColorChangedEventHandler(Brush Color);
        public event ColorChangedEventHandler ColorChanged;
        /// <summary>
        /// The color box of the selected color
        /// </summary>
        /// <param name="PopupIsOpenChanged"></param>
        public ColorSelectionViewModel(Action<bool> PopupIsOpenChanged)
        {
            this.PopupIsOpenChanged = PopupIsOpenChanged;

            Color_Click = new RelayCommand(() => ColorSelection_isOpen = !ColorSelection_isOpen);
            Done_Click = new RelayCommand(Done_ClickCmd);
            ColorSelectionGrid_MouseMove = new RelayCommand(ColorSelectionGrid_MouseMoveCmd);
            ColorSelectionGrid_MouseDown = new RelayCommand(ColorSelectionGrid_MouseDownCmd);
            MajorColorSelectorPlane_MouseDown = new RelayCommand(MajorColorSelectorPlane_MouseDownCmd);
            MajorColorSelectorPlane_MouseMove = new RelayCommand(MajorColorSelectorPlane_MouseMoveCmd);
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
        public void Close()
        {
            ColorSelection_isOpen = false;
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

            SelectionColor = new SolidColorBrush(ColorSelection(tmpPoint));

            PrimaryColor = SelectionColor;
            MajorColorSelector = new Thickness(0, Mouse.GetPosition(Mouse.DirectlyOver).Y, 0, 0);
        }

        private void ColorSelectionGrid_MouseDownCmd()
        {
            Win32.POINT tmpPoint;
            Win32.GetCursorPos(out tmpPoint);
            
            ColorSelectionGridColorFinder = new Thickness(Mouse.GetPosition(Mouse.DirectlyOver).X - 5, Mouse.GetPosition(Mouse.DirectlyOver).Y - 5, 0, 0);
            SelectionColor = new SolidColorBrush(ColorSelection(tmpPoint));
        }

        private void ColorSelectionGrid_MouseMoveCmd()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Win32.POINT tmpPoint;
                Win32.GetCursorPos(out tmpPoint);

                Point mousPos = Mouse.GetPosition(Mouse.DirectlyOver);
                ColorSelectionGridColorFinder = new Thickness(mousPos.X - 5, mousPos.Y - 5, 0, 0);
                SelectionColor = new SolidColorBrush(ColorSelection(tmpPoint));
            }
        }

        private void Done_ClickCmd()
        {
            Color = SelectionColor;
            ColorChanged?.Invoke(Color);
            ColorSelection_isOpen = false;
        }

        public void isSelectionPlaneOpen(bool isOpen = false)
        {
            ColorSelection_isOpen = isOpen;
        }
    }
}
