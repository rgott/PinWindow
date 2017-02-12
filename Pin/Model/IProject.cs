using System;
using System.Collections.Generic;
using System.Windows.Media;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pin.Model
{
    public interface IProject
    {
        int ID { get; set; }
        string Name { get; set; }
        string Path { get; set; }

        //[XmlElement(Type = typeof(XmlColor))]
        Brush Color { get; set; }
    }
}
