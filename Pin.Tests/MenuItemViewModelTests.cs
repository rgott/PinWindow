using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pin.MenuContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Media;

namespace Pin.Tests
{
    [TestClass]
    public class ProjectViewModelListTests
    {
        ProjectViewModelList model;
        [TestMethod]
        public void ProjectViewModelList()
        {
            model = new ProjectViewModelList(new TestSettings());

            Assert.AreEqual(1, model.Projects.Count);
        }
        




        class TestSettings : ISettings
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
}
