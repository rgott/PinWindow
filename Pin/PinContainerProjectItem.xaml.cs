using System;
using System.Windows;

namespace Pin
{
    /// <summary>
    /// Interaction logic for PinContainerProjectItem.xaml
    /// </summary>
    public partial class PinContainerProjectItem : UIProjectProperties
    {
        public event EventHandler ItemDropped;


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

        public Model.Project ProjectModel { get; set; }
        public PinContainerProjectItem(Model.Project project) : base(project)
        {
            DataContext = this;
            InitializeComponent();

            ProjectModel = project;
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            isPopupOpen = true;
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
            isPopupOpen = false;
            e.Handled = true;
        }

        private void UIProjectProperties_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.Handled = false;
        }
    }
}
