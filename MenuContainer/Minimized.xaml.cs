using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Minimized.xaml
    /// </summary>
    public partial class Minimized : UserControl
    {
        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(Minimized), new PropertyMetadata(new SolidColorBrush(Colors.Orange)));


        public Minimized()
        {
            DataContext = this;
            InitializeComponent();
        }

        

    }
}
