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

namespace Pin.MenuContainer
{
    /// <summary>
    /// Interaction logic for MinimizedOpen.xaml
    /// </summary>
    public partial class MinimizedOpen : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set
            {
                SetValue(FillColorProperty, value);
                NotifyPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(MinimizedOpen), new PropertyMetadata(default(Brush)));


        public MinimizedOpen()
        {
            DataContext = this;
            InitializeComponent();
        }

        
        private void UI_Btn_MouseDown_DragOut(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SourcePaths != null)
                DropDataHandler.dragDataOut(this, SourcePaths);
        }


        public Model.Project LastMovedProject { get; set; }
        public string[] SourcePaths { get; set; }
        private void PinProjectItem_ProjectItemDropped(object sender, Model.Project Model, string[] sourcePaths)
        {
            LastMovedProject = Model;
            SourcePaths = sourcePaths;

            // set color and path information
            //UI_DragOut_Color = Model.Color;
        }
        public event RoutedEventHandler SizeChangeRequest;
        public event RoutedEventHandler ExitRequest;
        private void UI_Btn_Sizing_Click(object sender, RoutedEventArgs e)
        {
            if (SizeChangeRequest != null) SizeChangeRequest(this, e);
        }
        
        private void UI_Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (ExitRequest != null) ExitRequest(this, e);
        }
    }
}
