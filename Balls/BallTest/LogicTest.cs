using Logic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLogic
{
    [TestClass]

    public class LogicTest
    {
        private LogicAbstractApi logic;
        private DataAbstractApi data;

        [TestMethod]
        public void TestApiCreation()
        {
            logic = LogicAbstractApi.CreateApi();
            Assert.IsNotNull(logic);
        }

        [TestMethod]
        public void TestWidthHeight()
        {
            data = DataAbstractApi.CreateApi();
            logic = LogicAbstractApi.CreateApi();
            Assert.AreEqual(Board.width, data.Width);
            Assert.AreEqual(Board.height, data.Height);

        }

        [TestMethod]
        public void TestCreateBalls()
        {
            logic = LogicAbstractApi.CreateApi();
            Assert.AreEqual(5, logic.CreateBalls(5).Count);

        }
    }
}
