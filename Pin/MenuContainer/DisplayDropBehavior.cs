using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

namespace Pin.MenuContainer
{
    public class DisplayDropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += (s, e) => { if (DragEnter != null) DragEnter(s, e); };
            this.AssociatedObject.DragLeave += (s, e) => { if (DragLeave != null) DragLeave(s, e); };
            this.AssociatedObject.Drop += (s, e) => { if (Drop != null) Drop(s, e); };
        }

        public DragEventHandler DragEnter
        {
            get { return (DragEventHandler)GetValue(DragEnterProperty); }
            set { SetValue(DragEnterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DragEnter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragEnterProperty =
            DependencyProperty.Register("DragEnter", typeof(DragEventHandler), typeof(DisplayDropBehavior));

        public DragEventHandler DragLeave
        {
            get { return (DragEventHandler)GetValue(DragLeaveEnterProperty); }
            set { SetValue(DragLeaveEnterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DragEnter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragLeaveEnterProperty =
            DependencyProperty.Register("DragLeave", typeof(DragEventHandler), typeof(DisplayDropBehavior));


        public DragEventHandler Drop
        {
            get { return (DragEventHandler)GetValue(DropProperty); }
            set { SetValue(DropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Drop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropProperty =
            DependencyProperty.Register("Drop", typeof(DragEventHandler), typeof(DisplayDropBehavior));

    }
}
