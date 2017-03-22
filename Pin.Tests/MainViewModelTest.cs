using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using Pin;

namespace Pin.Tests
{
    /// <summary>This class contains parameterized unit tests for MainViewModel</summary>
    [TestFixture]
    public partial class MainViewModelTest
    {
        [Test]
        public void ConstructorTest()
        {
            MainViewModel model = ViewModelFactory.MainVM(new BlankTestSettings(), new BlankTestWindow());

        }
    }
}
