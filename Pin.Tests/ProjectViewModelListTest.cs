using NUnit.Framework;
using System;
using System.Windows.Media;

namespace Pin.Tests
{
    [TestFixture]
    public class ProjectViewModelListTest
    {
        ProjectViewModelList model;
        [SetUp]
        public void Init()
        {
            model = new ProjectViewModelList(null,new BlankTestSettings());
        }

        [Test]
        public void InitialState()
        {
            Assert.AreEqual(0, model.Projects.Count);
        }

        [Test]
        public void AddProject()
        {
            model.Add(new Model.Project("New", "newpath", new SolidColorBrush(Colors.Red)));

            Assert.AreEqual(1, model.Projects.Count);
        }

        [Test]
        public void ModifyProject()
        {
            var oldproject = new Model.Project("other", "newerPath", new SolidColorBrush(Colors.Blue));
            var NewProject = new Model.Project("New", "newpath", new SolidColorBrush(Colors.Red));

            model.Add(oldproject);
            Assert.AreEqual(1, model.Projects.Count);

            model.Change(oldproject, NewProject);
            Assert.AreEqual(1, model.Projects.Count);

            Assert.AreSame(NewProject.Color, model.Projects[0].Project.Color);
            Assert.AreSame(NewProject.Name, model.Projects[0].Project.Name);
            Assert.AreSame(NewProject.Path, model.Projects[0].Project.Path);
        }

    }
}
