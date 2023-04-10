using System;
using System.Collections.Generic;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BallTest
{
    [TestClass]
    public class BallTest
    {
        private LogicAbstractApi L;
        private TimerApi timer;
        private readonly Ball ball = new Ball(1, 2, 3, 4);
        private List<Ball> balls = new List<Ball>();

        [TestMethod]
        public void TestApi()
        {
            L = LogicAbstractApi.CreateApi(100, 100, timer);
            Assert.AreEqual(L.Height, 100);
            Assert.AreEqual(L.Width, 100);
        }

        [TestMethod]
        public void TestCreateList()
        {
            L = LogicAbstractApi.CreateApi(100, 100, timer);
            L.CreateBallsList(2);
            Assert.AreEqual(L.balls.Count, 2);
        }

        [TestMethod]
        public void TestCords()
        {
            Assert.AreEqual(ball.X, 1);
            Assert.AreEqual(ball.Y, 2);
        }
        [TestMethod]
        public void TestMove()
        {
            ball.MoveBall(100, 100);
            Assert.AreNotEqual(ball.X, 1);
            Assert.AreNotEqual(ball.Y, 2);
        }

        [TestMethod]
        public void TestCoords()
        {
            L = LogicAbstractApi.CreateApi(100, 100, timer);
            L.CreateBallsList(1);
            Assert.IsTrue(L.balls[0].R == 20);
            Assert.IsTrue(L.balls[0].X < 100 - 20);
            Assert.IsTrue(L.balls[0].Y < 100 - 20);
        }



    }
}
