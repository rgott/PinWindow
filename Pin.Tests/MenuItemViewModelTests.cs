using Pin.MenuContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Media;
using NUnit.Framework;

namespace Pin.Tests
{
    [TestFixture]
    public class MenuItemViewModelTests
    {
        MenuItemViewModel Model;

        [SetUp]
        public void ProjectViewModelList()
        {
            ProjectViewModelList ProjModel = new ProjectViewModelList(new BlankTestWindow(), new BlankTestSettings());

            Model = new MenuItemViewModel(new BlankTestWindow(), ProjModel);
        }
    }
}
