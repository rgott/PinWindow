using Microsoft.Pex.Framework;
using Pin;
using NUnit.Framework;
using Pin.Model;
using System;
using System.Windows.Media;

namespace Pin.Tests
{
    [TestFixture]
    public class ProjectViewModelListTest
    {
        Pin.ProjectContainer.ProjectViewModelList model;

        IProject project = new Model.Project("Name1", "Path1", new SolidColorBrush(Colors.Red));

        [SetUp]
        public void Init()
        {
            model = new Pin.ProjectContainer.ProjectViewModelList(null, new BlankTestSettings());
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

        [Test]
        public void DuplicateProject()
        {
            Assert.True(model.Add(project));

            Assert.False(model.Add((IProject)project.Clone()));
        }

        [Test]
        public void FindTest()
        {
            Assert.True(model.Add(project));

            Assert.AreEqual(project.Name,model.Find(project).Project.Name);
        }

        [Test]
        public void FindNoItemsTest()
        {
            Assert.IsNull(model.Find(project));
        }

        [Test]
        public void FindItemsNotFoundTest()
        {
            Assert.True(model.Add(project));

            var tmpProject = new Project("TmpRandomName", "randPath", new SolidColorBrush(Colors.Firebrick));

            Assert.IsNull(model.Find(tmpProject));
        }
    }
}
