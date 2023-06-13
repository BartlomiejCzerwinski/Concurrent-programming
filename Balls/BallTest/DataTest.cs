using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DataTest
{
    [TestClass]
    public class DataTest
    {

        private DataAbstractApi data;

        [TestMethod]
        public void TestApiCreation()
        {
            data = DataAbstractApi.CreateApi();
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void TestWidthHeight()
        {
            data = DataAbstractApi.CreateApi();
            Assert.AreEqual(Board.width, data.Width);
            Assert.AreEqual(Board.height, data.Height);

        }
    }
}
