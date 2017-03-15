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
using System.Windows.Shapes;

namespace Pin.Error
{
    /// <summary>
    /// Interaction logic for CustomMessageWindow.xaml
    /// </summary>
    public partial class CustomMessageWindow : Window
    {
        public CustomMessageWindow(string Message)
        {
            DataContext = new CustomMessageViewModel(Close)
            {
                Message = Message
            };
            InitializeComponent();
        }
    }
}
