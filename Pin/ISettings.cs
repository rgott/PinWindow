using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
