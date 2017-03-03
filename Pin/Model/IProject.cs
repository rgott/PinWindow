using System.Windows.Media;

using System.Linq;
using System;

namespace Pin.Model
{
    public interface IProject : ICloneable
    {
        string Name { get; set; }
        string Path { get; set; }
        Brush Color { get; set; }
        string Serialize();
    }
}
