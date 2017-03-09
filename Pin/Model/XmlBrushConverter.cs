using System;
using System.Windows.Media;

namespace Pin.Model
{
    /// <summary>
    /// Used to store brush value as xml
    /// </summary>
    public class XmlBrushConverter
    {
        private Brush _Brush;

        public string BrushValue
        {
            get
            {
                return new BrushConverter().ConvertToString(_Brush);
            }
            set
            {
                _Brush = (Brush)new BrushConverter().ConvertFromString(value);
            }
        }

        protected XmlBrushConverter() { }
        public XmlBrushConverter(Brush brush) { _Brush = brush; }


        public static implicit operator Brush(XmlBrushConverter xmlcolor)
        {
            return xmlcolor._Brush;
        }

        public static implicit operator XmlBrushConverter(Brush brush)
        {
            return new XmlBrushConverter(brush);
        }
    }
}
