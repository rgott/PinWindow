using System;
using System.Windows.Media;
using NUnit.Framework;

namespace Pin.Model.Tests
{
    [TestFixture]
    public partial class ProjectTest
    {
        string _Name = "nameTest";
        string _Path = "pathTest";
        Brush _ColorBrush = new SolidColorBrush(Colors.Blue);
        string _UniqueValue = "newValueTest";

        IProject Model;

        [SetUp]
        public void Setup()
        {
            Model = new Project(_Name, _Path, _ColorBrush);
        }

        /// <summary>Test stub for .ctor(String, String, Brush)</summary>
        [Test]
        public void ConstructorTest()
        {
            Project target = new Project(_Name, _Path, _ColorBrush);
            
        }

        /// <summary>Test stub for Deserialize(String)</summary>
        [Test]
        public void DeserializeTest()
        {
            IProject result = Project.Deserialize<Project>(Model.Serialize());

            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Path);
            Assert.IsNotNull(result.Color);

            // TODO: add assertions to method ProjectTest.DeserializeTest(String)
        }

        [Test]
        public void EqualsTest()
        {
            var project = Model.Clone();
            Assert.True(Model.Equals(project));
        }

        [Test]
        public void NotEqualsTest()
        {
            var project = new Project("randName1", "randpath1", new SolidColorBrush(Colors.Black));

            Assert.False(Model.Equals(project));
        }

        [Test]
        public void NullEqualsTest()
        {
            Assert.False(Model.Equals(null));
        }

        /// <summary>Test stub for GetHashCode()</summary>
        [Test]
        public void GetHashCodeTest()
        {
            Model.GetHashCode();
            // TODO: add assertions to method ProjectTest.GetHashCodeTest(Project)
        }

        /// <summary>Test stub for Serialize()</summary>
        [Test]
        public void SerializeTest()
        {
            var result = Model.Serialize();

            var deserializedProduct = Project.Deserialize<Project>(result);

            Assert.AreEqual(Model, deserializedProduct);
        }

        /// <summary>Test stub for get_Color()</summary>
        [Test]
        public void ColorGetTest()
        {
            Assert.AreEqual(_ColorBrush, Model.Color);
        }

        /// <summary>Test stub for get_Name()</summary>
        [Test]
        public void NameGetTest()
        {
            Assert.AreEqual(_Name, Model.Name);
        }

        /// <summary>Test stub for get_Path()</summary>
        [Test]
        public void PathGetTest()
        {
            Assert.AreEqual(_Path, Model.Path);
        }

        /// <summary>Test stub for op_Explicit(String)</summary>
        [Test]
        public void op_ExplicitProjectTest()
        {
            Assert.AreEqual(Model, (Project)Model.Serialize());
        }

        [Test]
        public void op_ExplicitStringTest()
        {
            Assert.AreEqual(Model.Serialize(), (string)(Project)Model);
        }

        /// <summary>Test stub for set_Color(Brush)</summary>
        [Test]
        public void ColorSetTest()
        {
            Brush newColor = new SolidColorBrush(Colors.BlanchedAlmond);
            Model.Color = newColor;

            Assert.AreEqual(newColor, Model.Color);
        }

        /// <summary>Test stub for set_Name(String)</summary>
        [Test]
        public void NameSetTest()
        {
            Model.Name = _UniqueValue;

            Assert.AreEqual(_UniqueValue, Model.Name);
        }

        /// <summary>Test stub for set_Path(String)</summary>
        [Test]
        public void PathSetTest()
        {
            Model.Path = _UniqueValue;

            Assert.AreEqual(_UniqueValue, Model.Path);
        }

        /// <summary>Test stub for set_Path(String)</summary>
        [Test]
        public void PathSetSlashTest()
        {
            Model.Path = @"\";

            Assert.AreEqual(@"\", Model.Path);
        }
    }
}
