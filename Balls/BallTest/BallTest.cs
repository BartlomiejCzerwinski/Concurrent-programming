using System;
using System.Collections.Generic;
using Logic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BallTest
{
    [TestClass]
    public class BallTest
    {
        private DataAbstractApi dataLayer;
        private LogicAbstractApi logicLayer;

        [TestMethod]
        public void TestDataApi()
        {
            dataLayer = DataAbstractApi.CreateApi();
            dataLayer.CreateBalls(1);
            Assert.IsNotNull(dataLayer.GetBall(0));
            Assert.AreEqual(0, dataLayer.GetBall(0).Identifier);
            Assert.AreEqual(10, dataLayer.GetBall(0).R);

            Assert.AreEqual(Board.width, dataLayer.Width);
            Assert.AreEqual(Board.height, dataLayer.Height);

        }

        [TestMethod]
        public void TestCords()
        {
            dataLayer = DataAbstractApi.CreateApi();
            dataLayer.CreateBalls(1);
            Assert.IsTrue(dataLayer.GetBall(0).X0 >= 0);
            Assert.IsTrue(dataLayer.GetBall(0).X0 <= Board.width);
            Assert.IsTrue(dataLayer.GetBall(0).Y0 >= 0);
            Assert.IsTrue(dataLayer.GetBall(0).Y0 <= Board.height);

        }
        [TestMethod]
        public void TestMove()
        {
            dataLayer = DataAbstractApi.CreateApi();
            dataLayer.CreateBalls(1);
            double x0 = dataLayer.GetBall(0).X0;
            double y0 = dataLayer.GetBall(0).Y0;
            dataLayer.GetBall(0).X1 = 1;
            dataLayer.GetBall(0).Y1 = 1;
            dataLayer.GetBall(0).Move();
            Assert.AreNotEqual(x0, dataLayer.GetBall(0).X0);
            Assert.AreNotEqual(y0, dataLayer.GetBall(0).Y0);
            Assert.AreEqual(dataLayer.GetBall(0).X0, x0 + dataLayer.GetBall(0).X1);
            Assert.AreEqual(dataLayer.GetBall(0).Y0, y0 + dataLayer.GetBall(0).Y1);
        }
        [TestMethod]
        public void TestLogicApi()
        {
            logicLayer = LogicAbstractApi.CreateApi();
            Assert.IsNotNull(logicLayer);
            Assert.IsNotNull(logicLayer.CreateBalls(1));
        }

        [TestMethod]
        public void TestBallsCollision()
        {
            logicLayer = LogicAbstractApi.CreateApi();
            logicLayer.CreateBalls(2);
            logicLayer.GetBall(0).X0 = 50;
            logicLayer.GetBall(0).Y0 = 50;
            logicLayer.GetBall(0).X1 = 5;
            logicLayer.GetBall(0).Y1 = 5;
            logicLayer.GetBall(1).X0 = 55;
            logicLayer.GetBall(1).Y0 = 55;
            logicLayer.GetBall(1).X1 = -5;
            logicLayer.GetBall(1).Y1 = -5;
            logicLayer.GetBall(0).Move();
            logicLayer.GetBall(1).Move();
            Assert.AreNotEqual(50, logicLayer.GetBall(0).X0);
            Assert.AreNotEqual(50, logicLayer.GetBall(0).Y0);
            
        }
        [TestMethod]
        public void TestBorderCollision()
        {
            logicLayer = LogicAbstractApi.CreateApi();
            logicLayer.CreateBalls(2);
            logicLayer.GetBall(0).X0 = 20;
            logicLayer.GetBall(0).Y0 = 20;
            logicLayer.GetBall(0).X1 = -10;
            logicLayer.GetBall(0).Y1 = 0;
            logicLayer.GetBall(1).X0 = 20;
            logicLayer.GetBall(1).Y0 = 470;
            logicLayer.GetBall(1).X1 = 0;
            logicLayer.GetBall(1).Y1 = 10;
            logicLayer.GetBall(0).Move();
            logicLayer.GetBall(1).Move();
            Assert.AreNotEqual(20, logicLayer.GetBall(0).X0);
            Assert.AreNotEqual(470, logicLayer.GetBall(1).Y0);
        }

       
    }
}
