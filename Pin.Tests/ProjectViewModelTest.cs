using System;
using NUnit.Framework;
using Pin.ProjectContainer;

namespace Pin.Tests
{
    [TestFixture]
    public class ProjectViewModelTest
    {
        ProjectViewModel Model;

        [SetUp]
        public void SetUp()
        {
            Model = PinFactory.ProjectVM();
        }

        [Test]
        public void AddProjectCmdTest()
        {
            Model.Project = PinFactory.Project("Test", "path");
            Model.ShowEditor = true;
            Model.ShowInfo = false;

            Assert.AreEqual(0, Model.Projects.Projects.Count);

            Model.AddProject.Execute(null);

            Assert.AreEqual("", Model.Project.Name);
            Assert.AreEqual("", Model.Project.Path);

            Assert.AreEqual(1, Model.Projects.Projects.Count);

            Assert.IsFalse(Model.ShowInfo);
            Assert.IsFalse(Model.ShowEditor);
        }
    }
}
