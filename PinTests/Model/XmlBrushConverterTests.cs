using NUnit.Framework;
using Pin.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace Pin.Tests.Model
{
    [TestFixture]
    public class XmlBrushConverterTests
    {
        SolidColorBrush testBrushRed;

        XmlBrushConverter Model;

        [SetUp]
        public void Initialize()
        {
            testBrushRed = new SolidColorBrush(Colors.Red);
            Model = new XmlBrushConverter(testBrushRed);
        }

        /// <summary>
        /// Tests protected constructor
        /// </summary>
        [Test]
        public void BrushConverterTest()
        {
            var classConstructor = typeof(XmlBrushConverter).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

            classConstructor.Invoke(null);
        }

        /// <summary>
        /// ensure brush is properly represented
        /// </summary>
        [Test]
        public void BrushConverterToBrushTest()
        {
            Assert.AreEqual(testBrushRed, (Brush)Model);
        }

        /// <summary>
        /// brush converts to xmlbrushconverter (explicit)
        /// </summary>
        [Test]
        public void BrushConverterToStringTest()
        {
            Assert.AreEqual(Model.BrushValue, ((XmlBrushConverter)testBrushRed).BrushValue);
        }


        [Test]
        public void BrushConverterSetBrushValueTest()
        {
            var testBrushGreen = new XmlBrushConverter(new SolidColorBrush(Colors.Black));

            Model.BrushValue = testBrushGreen.BrushValue;

            Assert.AreEqual(testBrushGreen.BrushValue, Model.BrushValue);
        }
    }
}