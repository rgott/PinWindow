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
        public event EventHandler ItemDropped;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _ProjectName;
        public string ProjectName
        {
            get
            {
                return _ProjectName;
            }
            set
            {
                _ProjectName = value;
                NotifyPropertyChanged();
            }
        }

        private string _ProjectPath;
        public string ProjectPath
        {
            get
            {
                return _ProjectPath;
            }
            set
            {
                _ProjectPath = value;
                NotifyPropertyChanged();
            }
        }

        private Brush _ProjectColor;
        public Brush ProjectColor
        {
            get
            {
                return _ProjectColor;
            }
            set
            {
                _ProjectColor = value;
                NotifyPropertyChanged();
            }
        }

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
        public Model.Project ProjectModel { get; set; }
        public PinContainerProjectItem(Model.Project project) : this()
        {
            ProjectModel = project;
            ProjectName = project.ProjectName;
            ProjectPath = project.ProjectPath;
            ProjectColor = project.Color;
        }

        private void UI_Btn_ProjectColor_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void UI_Btn_ProjectColor_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            isPopupOpen = true;
            MouseOverController.isMoveOverWindow = true;
            e.Handled = true;

        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            DropDataHandler.dropData(ProjectModel, e);

            isPopupOpen = false;
            if (ItemDropped != null) ItemDropped(sender, e);
        }

        private void UserControl_DragLeave(object sender, DragEventArgs e)
        {
            MouseOverController.isMoveOverWindow = false;
            isPopupOpen = false;
            e.Handled = true;
        }

    }
}
