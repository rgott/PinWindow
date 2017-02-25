using System.Windows.Media;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pin.Model
{
    public interface IProject
    {
        string Name { get; set; }
        string Path { get; set; }
        Brush Color { get; set; }
    }
}
