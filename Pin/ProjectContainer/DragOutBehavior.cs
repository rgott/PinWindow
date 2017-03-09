using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Pin.ProjectContainer
{
    public class DragOutBehavior : Behavior<FrameworkElement>
    {


        public IProjectViewModel ProjectVM
        {
            get { return (IProjectViewModel)GetValue(ProjectVMProperty); }
            set { SetValue(ProjectVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectVMProperty =
            DependencyProperty.Register("ProjectVM", typeof(IProjectViewModel), typeof(DragOutBehavior));


        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (ProjectVM.FileToDrop.Count == 0) return;

            var data = new DataObject(DataFormats.FileDrop, ProjectVM.FileToDrop.Dequeue());
            DragDrop.DoDragDrop(this.AssociatedObject, data, getEffects((ActionEvent)Properties.Settings.Default.ActionEvent));
        }


        public static void setEffects(DragEventArgs e)
        {
            if (e.Data.GetFormats().Contains(DataFormats.Html)
                && e.Data.GetFormats().Contains(DataFormats.FileDrop))
            {
                e.Effects = getEffects((ActionEvent)Properties.Settings.Default.ActionEvent);
            }
        }
        public static DragDropEffects getEffects(ActionEvent e)
        {
            switch (e)
            {
                case ActionEvent.Move:
                    return DragDropEffects.Move;
                case ActionEvent.Copy:
                    return DragDropEffects.Copy;
            }
            return DragDropEffects.None;
        }
    }
}
