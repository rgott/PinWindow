using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pin.MenuContainer;
using NUnit.Framework;
using Pin.ProjectContainer;

namespace Pin.Tests
{
    [TestFixture]
    public class MenuItemViewModelTests
    {
        MenuItemViewModel Model;

        [SetUp]
        public void ProjectViewModelList()
        {
            var ProjModel = new ProjectViewModelList(new BlankTestWindow(), new BlankTestSettings());

            Model = new MenuItemViewModel(new BlankTestSettings(),new BlankTestWindow(), ProjModel);
        }
    }
}
