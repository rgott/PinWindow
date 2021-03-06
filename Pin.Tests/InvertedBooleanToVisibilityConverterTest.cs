using System;
using System.Windows;
using System.Globalization;
using NUnit.Framework;
using Pin;

namespace Pin.Tests
{
    /// <summary>This class contains parameterized unit tests for InvertedBooleanToVisibilityConverter</summary>
    [TestFixture]
    public partial class InvertedBooleanToVisibilityConverterTest
    {
        InvertedBooleanToVisibilityConverter Model;

        [SetUp]
        public void setUp()
        {
            Model = new InvertedBooleanToVisibilityConverter();
        }

        [Test]
        public void ConvertTrueTest()
        {
            Assert.AreEqual(Visibility.Hidden, Model.Convert(true, typeof(bool), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertFalseTest()
        {
            Assert.AreEqual(Visibility.Visible, Model.Convert(false, typeof(bool), null, CultureInfo.CurrentCulture));
        }
        [Test]
        public void ConvertBackVisibleTest()
        {
            Assert.IsFalse((bool)Model.ConvertBack(Visibility.Visible, typeof(Visibility), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertBackHiddenTest()
        {
            Assert.IsTrue((bool)Model.ConvertBack(Visibility.Hidden, typeof(Visibility), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertBackCollapsedTest()
        {
            Assert.IsTrue((bool)Model.ConvertBack(Visibility.Collapsed, typeof(Visibility), null, CultureInfo.CurrentCulture));
        }


    }
}
