using Pin.MenuContainer;
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
        private Type dataType;
        private FrameworkElementAdorner adorner;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (dataType != null)
            {
                //if the data type can be dropped 
                if (e.Data.GetDataPresent(dataType))
                {
                    //drop the data
                    IDroppable target = this.AssociatedObject.DataContext as IDroppable;
                    target.Drop(e.Data.GetData(dataType));

                    //remove the data from the source
                    IDraggable source = e.Data.GetData(dataType) as IDraggable;
                    source.Remove(e.Data.GetData(dataType));
                }
            }
            if (this.adorner != null)
                this.adorner.Remove();

            e.Handled = true;
            return;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (this.adorner != null)
                this.adorner.Remove();
            e.Handled = true;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (dataType != null)
            {
                //if item can be dropped
                if (e.Data.GetDataPresent(dataType))
                {
                    //give mouse effect
                    this.SetDragDropEffects(e);
                    //draw the dots
                    if (this.adorner != null)
                        this.adorner.Update();
                }
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            //if the DataContext implements IDropable, record the data type that can be dropped
            if (this.dataType == null)
            {
                if (this.AssociatedObject.DataContext != null)
                {
                    IDroppable dropObject = this.AssociatedObject.DataContext as IDroppable;
                    if (dropObject != null)
                    {
                        this.dataType = dropObject.DataType;
                    }
                }
            }

            if (this.adorner == null)
                this.adorner = new FrameworkElementAdorner(sender as UIElement);
            e.Handled = true;
        }

        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;  //default to None

            //if the data type can be dropped 
            if (e.Data.GetDataPresent(dataType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
    }
}
