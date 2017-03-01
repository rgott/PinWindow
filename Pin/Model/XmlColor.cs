using System;
using System.Windows.Media;

namespace Pin.Model
{
    /// <summary>
    /// Used to store brush value as xml
    /// </summary>
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
                if (String.IsNullOrEmpty(value))
                {// TODO: better default color
                    _Brush = new SolidColorBrush(Colors.Orange);
                }
                else
                {
                    _Brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(value);
                }
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
