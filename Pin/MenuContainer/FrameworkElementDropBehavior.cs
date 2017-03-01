using Pin.MenuContainer;
using Pin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Pin.MenuContainer
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private FrameworkElementAdorner adorner;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        public IProject DroppableProject
        {
            get { return (IProject)GetValue(DroppableProjectProperty); }
            set { SetValue(DroppableProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DroppableProject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DroppableProjectProperty =
            DependencyProperty.Register("DroppableProject", typeof(IProject), typeof(FrameworkElementDropBehavior));

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetFormats().Contains(DataFormats.Html))
            {// if html then from web retrieve and save or content

            }
            else if (e.Data.GetFormats().Contains(DataFormats.FileDrop))
            {// file drop
                DropDataHandler.dropData(DroppableProject, e);
            }

            if (this.adorner != null)
                this.adorner.Remove();

            e.Handled = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            this.SetDragDropEffects(e);
            if (this.adorner != null)
                this.adorner.Remove();
            e.Handled = true;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            this.SetDragDropEffects(e);
            if (this.adorner == null)
                this.adorner = new FrameworkElementAdorner(sender as UIElement);

            e.Handled = true;
        }

        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;  //default to None

            if (e.Data.GetFormats().Contains(DataFormats.Html) || e.Data.GetFormats().Contains(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
    }
}
