using System.Windows.Media;

namespace Pin.Model
{
    public class XmlColor
    {
        private SolidColorBrush _Brush;

        public string BrushValue
        {
            get
            {
                return new System.Windows.Media.BrushConverter().ConvertToString(_Brush);
            }
            set
            {
                _Brush = (SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFromString(value);
            }
        }

        public XmlColor() { }
        public XmlColor(SolidColorBrush brush) { _Brush = brush; }


        public SolidColorBrush ToBrush()
        {
            return _Brush;
        }

        public void FromColor(SolidColorBrush brush)
        {
            _Brush = brush;
        }

        public static implicit operator SolidColorBrush(XmlColor xmlcolor)
        {
            return xmlcolor.ToBrush();
        }

        public static implicit operator XmlColor(SolidColorBrush brush)
        {
            return new XmlColor(brush);
        }

    }
}
