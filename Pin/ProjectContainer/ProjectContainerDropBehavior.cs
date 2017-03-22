using System.Windows;

namespace Pin.ProjectContainer
{
    public class ProjectContainerDropBehavior : ProjectDropBehavior
    {
        protected override void DragEnterCmd(object sender, DragEventArgs e)
        {
            base.DragEnterCmd(sender, e);

            ProjectVM.ShowInfo = true;
        }

        protected override void DragLeaveCmd(object sender, DragEventArgs e)
        {
            base.DragLeaveCmd(sender, e);

            ProjectVM.ShowInfo = false;
            e.Handled = true;
        }

        protected override void DropCmd(object sender, DragEventArgs e)
        {
            base.DropCmd(sender, e);

            ProjectVM.ShowInfo = false;
        }
    }
}