using System;
using System.Xml.Serialization;
using System.Xml;
using System.Windows.Media;

namespace Pin.Model
{
    public interface IProject : ICloneable
    {
        string Name { get; set; }
        string Path { get; set; }

        [XmlElement(Type = typeof(XmlBrushConverter))]
        Brush Color { get; set; }

        string Serialize();
    }
}
