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

namespace Pin
{
    /// <summary>
    /// Interaction logic for SaveFileDialog.xaml
    /// </summary>
    public partial class SaveFileDialog : Window
    {
        public SaveFileDialog(int moveItems,string fromDest,string toDest,string filename)
        {
            InitializeComponent();
            items.Text = moveItems.ToString();
            itemsAddS.Text = (moveItems > 1) ? "s" : "";
            FromHyperLink.Inlines.Add(fromDest);
            ToHyperLink.Inlines.Add(toDest);
            fName.Text = filename;
        }

    }
}
