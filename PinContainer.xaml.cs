using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pin
{
    /// <summary>
    /// Interaction logic for PinContainer.xaml
    /// </summary>
    public partial class PinContainer : UserControl, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ImageSource _UI_SizingSource;
        public ImageSource UI_SizingSource
        {
            get
            {
                return _UI_SizingSource;
            }
            set
            {
                _UI_SizingSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_ArrowSource;

        public ImageSource UI_ArrowSource
        {
            get
            {
                return _UI_ArrowSource;
            }
            set
            {
                _UI_ArrowSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_MenuSource;

        public ImageSource UI_MenuSource
        {
            get
            {
                return _UI_MenuSource;
            }
            set
            {
                _UI_MenuSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_ExitSource;
        public ImageSource UI_ExitSource
        {
            get
            {
                return _UI_ExitSource;
            }
            set
            {
                _UI_ExitSource = value;
                NotifyPropertyChanged();
            }
        }

        private ImageSource _UI_PinSource;
        public ImageSource UI_PinSource
        {
            get
            {
                return _UI_PinSource;
            }
            set
            {
                _UI_PinSource = value;
                NotifyPropertyChanged();
            }
        }

        private bool ArrowStatus = false;
        private bool PinStatus = false;
        private bool MenuStatus = false;

        #endregion

        public event EventHandler OnOpenPin;
        public event EventHandler OnClosePin;

        public event EventHandler OnOpenMenu;
        public event EventHandler OnCloseMenu;

        public event EventHandler OnExit;

        public event EventHandler OnOpenArrow;
        public event EventHandler OnCloseArrow;


        public event EventHandler OnDrop;

        public PinContainer()
        {
            UI_SizingSource = new BitmapImage(new Uri("images/CloseArrow.png", UriKind.Relative));
            UI_ArrowSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
            UI_ExitSource = new BitmapImage(new Uri("images/Exit.png", UriKind.Relative));
            UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            UI_MenuSource = new BitmapImage(new Uri("images/menu.png", UriKind.Relative));

            DataContext = this;
            InitializeComponent();
        }

        private void UI_Btn_Sizing_Click(object sender, RoutedEventArgs e)
        {
            if(ArrowStatus)
            {
                UI_SizingSource = new BitmapImage(new Uri("images/CloseArrow.png", UriKind.Relative));
                if (OnCloseArrow != null) OnCloseArrow(this, e);
            }
            else
            {
                UI_SizingSource = new BitmapImage(new Uri("images/OpenArrow.png", UriKind.Relative));
                if (OnOpenArrow != null) OnOpenArrow(this, e);
            }
            ArrowStatus = !ArrowStatus;
        }

        private void UI_Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            if(MenuStatus)
            {
                if (OnOpenMenu != null) OnOpenMenu(this, e);
            }
            else
            {
                if (OnCloseMenu != null) OnCloseMenu(this, e);
            }
            MenuStatus = !MenuStatus;
        }

        private void UI_Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (OnExit != null) OnExit(this, e);
        }

        private void UI_Btn_Pin_Click(object sender, RoutedEventArgs e)
        {
            if(PinStatus)
            {
                UI_PinSource = new BitmapImage(new Uri("images/pin.png", UriKind.Relative));
            }
            else
            {
                UI_PinSource = new BitmapImage(new Uri("images/pinned.png", UriKind.Relative));
            }
            PinStatus = !PinStatus;
        }

        private void UI_Border_Polygon_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void UI_Btn_Polygon_Drop(object sender, DragEventArgs e)
        {

        }



        
    }
}
