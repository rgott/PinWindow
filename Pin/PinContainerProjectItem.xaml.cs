using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Pin
{
    /// <summary>
    /// Interaction logic for PinContainerProjectItem.xaml
    /// </summary>
    public partial class PinContainerProjectItem : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
        public Brush ProjectColor { get; set; }

        private bool _isPopupOpen = false;
        public bool isPopupOpen
        {
            get
            {
                return _isPopupOpen;
            }
            set
            {
                _isPopupOpen = value;
                NotifyPropertyChanged();
            }
        }
        public PinContainerProjectItem()
        {
            DataContext = this;
            InitializeComponent();
        }

        public PinContainerProjectItem(Model.Project project) : this()
        {
            ProjectName = project.ProjectName;
            ProjectPath = project.ProjectPath;
            ProjectColor = project.Color;
        }

        private void UI_Btn_ProjectColor_MouseEnter(object sender, MouseEventArgs e)
        {
            isPopupOpen = true;
        }

        private void UI_Btn_ProjectColor_MouseLeave(object sender, MouseEventArgs e)
        {
            isPopupOpen = false;
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("DROPPED");
        }
    }
}
