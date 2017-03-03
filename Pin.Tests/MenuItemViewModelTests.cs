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
            model = new ProjectViewModelList(null,new TestSettings());

            Assert.AreEqual(1, model.Projects.Count);
        }
    }
}
