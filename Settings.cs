using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin
{
    static class Settings
    {
        public static void saveProjects(StringCollection propertyCollection)
        {
            Properties.Settings.Default.Projects = propertyCollection;
            Properties.Settings.Default.Save();
        }




    }
}
