using System.Collections.Specialized;
using System.Linq;

namespace Pin
{
    public interface ISettings
    {
        string PrimaryProjectName { get; set; }
        void Save();
        int ActionEvent { get; set; }
        StringCollection Projects { get; set; }
    }
}
