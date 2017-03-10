using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;

namespace Pin.Tests
{
    [TestClass]
    public class ProjectViewModelListTest
    {
        ProjectViewModelList model;
        [TestInitialize]
        public void Init()
        {
            model = new ProjectViewModelList(null,new BlankTestSettings());

            Assert.AreEqual(0, model.Projects.Count);
        }

        [TestMethod]
        public void AddProject()
        {
            model.Add(new Model.Project("New", "newpath", new SolidColorBrush(Colors.Red)));

            Assert.AreEqual(1, model.Projects.Count);
        }

        [TestMethod]
        public void ModifyProject()
        {
            var oldproject = new Model.Project("other", "newerPath", new SolidColorBrush(Colors.Blue));
            var newproject = new Model.Project("New", "newpath", new SolidColorBrush(Colors.Red));

            model.Add(oldproject);

            model.Change(oldproject, newproject);

            Assert.AreEqual(1, model.Projects.Count);

            Assert.AreSame(newproject.Color, model.Projects[0].Project.Color);
            Assert.AreSame(newproject.Name, model.Projects[0].Project.Name);
            Assert.AreSame(newproject.Path, model.Projects[0].Project.Path);
        }

    }
}
