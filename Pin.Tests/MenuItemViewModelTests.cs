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
    public class MenuItemViewModelTests
    {
        MenuItemViewModel Model;

        [TestInitialize]
        public void ProjectViewModelList()
        {
            ProjectViewModelList ProjModel = new ProjectViewModelList(new BlankTestWindow(), new BlankTestSettings());

            Model = new MenuItemViewModel(new BlankTestWindow(), ProjModel);
        }
    }
}
