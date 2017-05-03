using System.Windows;
using System.Windows.Interactivity;

namespace Pin
{
    public class ProjectDragoutBehavior : Behavior<FrameworkElement>
    {
        public IProjectViewModel ProjectVM
        {
            get { return (IProjectViewModel)GetValue(ProjectVMProperty); }
            set { SetValue(ProjectVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectVMProperty =
            DependencyProperty.Register(nameof(ProjectVM), typeof(IProjectViewModel), typeof(ProjectDragoutBehavior));

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProjectVM == null) return;

            if (ProjectVM.FileToDrop.Count == 0)
            {
                Error.ErrorBox.Show("No Files to drag out.");
                return;
            }
            var data = new DataObject(DataFormats.FileDrop, ProjectVM.FileToDrop.Dequeue());
            DragDrop.DoDragDrop(this.AssociatedObject, data, GetEffects(ProjectVM.Settings.ClipboardAction));
        }

        public static DragDropEffects GetEffects(ClipboardEvent e)
        {
            switch (e)
            {
                case ClipboardEvent.Move:
                    return DragDropEffects.Move;
                case ClipboardEvent.Copy:
                    return DragDropEffects.Copy;
            }
            return DragDropEffects.None;
        }
    }
}
