using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace Pin.ColorPicker
{
    /// <summary>
    /// Stackoverflow: <see cref="http://stackoverflow.com/a/30054535"/>
    /// </summary>
    public class MouseButtonEventArgsToPointConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = (MouseEventArgs)value;
            var element = (FrameworkElement)parameter;
            var point = args.GetPosition(element);
            return point;
        }
    }
}
