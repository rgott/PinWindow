using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Pin.MenuContainer
{
    public class MenuContainerDropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += DragEnterCmd;
            this.AssociatedObject.DragLeave += DragLeaveCmd;
            this.AssociatedObject.Drop += DropCmd;
        }

        private void DropCmd(object sender, DragEventArgs e)
        {

        }

        protected virtual void DragEnterCmd(object sender, DragEventArgs e)
        {
            
        }

        protected virtual void DragLeaveCmd(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
