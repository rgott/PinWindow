using System.Collections.Specialized;

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
