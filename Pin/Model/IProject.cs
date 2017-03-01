using System.Windows.Media;

using System.Linq;

namespace Pin.Model
{
    public interface IProject
    {
        string Name { get; set; }
        string Path { get; set; }
        Brush Color { get; set; }
        bool IsPrimary { get; set; }
        string Serialize();
    }
}
