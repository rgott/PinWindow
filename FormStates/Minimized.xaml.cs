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

namespace Pin.FormStates
{
    /// <summary>
    /// Interaction logic for Minimized.xaml
    /// </summary>
    public partial class Minimized : UserControl
    {

        //public string ItemsSource
        //{
        //    get { return (string)GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}
        //public static readonly DependencyProperty ItemsSourceProperty = Polygon.FillProperty.AddOwner(typeof(Minimized));

        public string Fill
        {
            get { return (string)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Colors), typeof(Minimized));

        public Minimized()
        {
            InitializeComponent();
        }
    }
}
