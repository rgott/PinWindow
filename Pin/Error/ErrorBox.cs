using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pin.Error
{
    public class ErrorBox
    {
        public static void Show(string error)
        {
            var window = new CustomMessageWindow(error)
            {
                Left = 10,
                Top = 20,
                Width = 600
            };
            window.ShowDialog();
        }
    }
}
