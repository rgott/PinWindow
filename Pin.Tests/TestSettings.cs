using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin.Tests
{
    public class TestSettings : ISettings
    {
        public TestSettings()
        {
            ActionEvent = (int)Pin.ActionEvent.Move;
            var projectCollection = new StringCollection();
            var p = new Model.Project("NAME", "PATH", new SolidColorBrush(Colors.Purple));

            projectCollection.Add(p.Serialize());
            Projects = projectCollection;
        }

        public int ActionEvent { get; set; }
        public string PrimaryProjectName { get; set; }

        public StringCollection Projects { get; set; }



        public void Save()
        {
            // stub only do nothing
        }
    }
}
