using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pin.Tests
{
    [TestClass]
    public class CustomXmlSerializerTest
    {
        public class TestObjA : CustomXmlSerializer
        {
            public int MyProperty { get; set; }
        }

        public TestObjA test;

        public CustomXmlSerializerTest()
        {
            test = new TestObjA()
            {
                MyProperty = 10
            };
        }

        [TestMethod]
        public void Serialize()
        {
            test.Serialize();
        }

        [TestMethod]
        public void Deserialize()
        {
            var value = test.Serialize();

            TestObjA newObj = TestObjA.Deserialize<TestObjA>(value);

            Assert.AreEqual(10, newObj.MyProperty);
        }
    }
}
