using System;
using System.Collections.Generic;
using Logic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

namespace BallTest
{
    [TestClass]
    public class BallTest
    {
        private DataAbstractApi data;

        [TestMethod]
        public void TestCreateBalls()
        {
            data = DataAbstractApi.CreateApi();
            IBall b = data.CreateBall(1);

            Assert.AreEqual(1, b.Identifier);
            Assert.IsTrue(b.X1 >= -5 && b.X1 <= 5);
            Assert.IsTrue(b.Y1 >= -5 && b.Y1 <= 5);
        }

        [TestMethod]
        public void TestMove()
        {
            data = DataAbstractApi.CreateApi();
            IBall b = data.CreateBall(1);
            ConcurrentQueue<IBall> queue = new ConcurrentQueue<IBall>();
            double x = b.X0;
            double y = b.Y0;
            b.Move(1, queue);
            Assert.AreNotEqual(x, b.X0);
            Assert.AreNotEqual(y, b.Y0);
        }

    }
}
