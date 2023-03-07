using firstProject;

namespace firstProjectTest
{
    [TestClass]
    public class HelloTest
    {
        [TestMethod]
        public void returnHelloReturnsHello()
        {
            Hello hello = new Hello();
            Assert.AreEqual(hello.returnHello(true), "HELLO!");
        }

        [TestMethod]
        public void returnHelloReturnsBye()
        {
            Hello hello = new Hello();
            Assert.AreEqual(hello.returnHello(false), "BYE!");
        }

    }
}