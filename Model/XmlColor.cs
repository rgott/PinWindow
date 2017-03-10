using System.Windows.Media;

namespace Pin.Model
{
    public class XmlColor
    {
        private Brush _Brush;

        public string BrushValue
        {
            get
            {
                return new System.Windows.Media.BrushConverter().ConvertToString(_Brush);
            }
            set
            {
                _Brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(value);
            }
        }

        public XmlColor() { }
        public XmlColor(Brush brush) { _Brush = brush; }


        public Brush ToBrush()
        {
            return _Brush;
        }

        public void FromColor(Brush brush)
        {
            _Brush = brush;
        }

        public static implicit operator Brush(XmlColor xmlcolor)
        {
            return xmlcolor.ToBrush();
        }

        public static implicit operator XmlColor(Brush brush)
        {
            return new XmlColor(brush);
        }

    }
}
