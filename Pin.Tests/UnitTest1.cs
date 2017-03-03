using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pin.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ProjectViewModelList model;
        [TestInitialize]
        public void T()
        {
            model = new ProjectViewModelList(null,new TestSettings());
        }

        [TestMethod]
        public void Add()
        {
            
        }
    }
}
