using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int count);
        public abstract void Start();
        public abstract void Stop();
        public abstract IBall GetBall(int index);
        public abstract void WallCollision(IBall ball);
        public abstract void ChangeDirection(IBall ball);
        public abstract void BallPositionChanged(object sender, PropertyChangedEventArgs args);
        public static LogicAbstractApi CreateApi()
        {
            return new LogicApi();
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private int width;
        private int height;
        private readonly DataAbstractApi dataLayer;
        private readonly Mutex mutex = new Mutex();

        public LogicApi()
        {
            dataLayer = DataAbstractApi.CreateApi();
            this.width = dataLayer.Width;
            this.height = dataLayer.Height;

        }

        public override int Width { get; }
        public override int Height { get; }

        public override void Start()
        {
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                dataLayer.GetBall(i).CreateTask(20);

            }
        }

        public override void Stop()
        {
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                dataLayer.GetBall(i).Stop();

            }
        }


        public override void WallCollision(IBall ball)
        {

            int diameter = ball.R * 2;

            int rightBorder = this.width - diameter;

            int bottomBorder = this.height - diameter;


            if (ball.X0 <= 0)
            {
                ball.X0 = -ball.X0;
                ball.X1 = -ball.X1;
            }

            else if (ball.X0 >= rightBorder)
            {
                ball.X0 = rightBorder - (ball.X0 - rightBorder);
                ball.X1 = -ball.X1;
            }
            if (ball.Y0 <= 0)
            {
                ball.Y0 = -ball.Y0;
                ball.Y1 = -ball.Y1;
            }

            else if (ball.Y0 >= bottomBorder)
            {
                ball.Y0 = bottomBorder - (ball.Y0 - bottomBorder);
                ball.Y1 = -ball.Y1;
            }
        }

        public override void ChangeDirection(IBall ball)
        {
            for (int i = 0; i < dataLayer.GetCount; i++)
            {
                IBall secondBall = dataLayer.GetBall(i);
                if (ball.Identifier == secondBall.Identifier)
                {
                    continue;
                }

                if (DetectCollision(ball, secondBall))
                {

                    double m1 = ball.Weight;
                    double m2 = secondBall.Weight;
                    double v1x = ball.X1;
                    double v1y = ball.Y1;
                    double v2x = secondBall.X1;
                    double v2y = secondBall.Y1;



                    double u1x = (v1x * (m1 - m2) + (2 * m2 * v2x)) / (m1 + m2);
                    double u1y = (v1y * (m1 - m2) + (2 * m2 * v2y)) / (m1 + m2);

                    double u2x = (v2x * (m2 - m1) + (2 * m1 * v1x)) / (m1 + m2);
                    double u2y = (v2y * (m2 - m1) + (2 * m1 * v1y)) / (m1 + m2);

                    if (DistanceAfterChangeDirection(ball, secondBall, u1x, u1y, u2x, u2y) <= Distance(ball, secondBall)) 
                    {
                        return;
                    }

                    ball.X1 = u1x;
                    ball.Y1 = u1y;
                    secondBall.X1 = u2x;
                    secondBall.Y1 = u2y;

                    

                }



            }

        }

        internal bool DetectCollision(IBall a, IBall b)
        {
           
            bool flag = false;
            if (a == null || b == null)
            {
                return false;
            }
            if (Distance(a, b) <= (2 * a.R))
            {
                flag = true;
            }

            return flag;
        }

        internal double Distance(IBall a, IBall b)
        {
            double x1 = a.X0 + a.R + a.X1;
            double y1 = a.Y0 + a.R + a.Y1;
            double x2 = b.X0 + b.R + b.Y1;
            double y2 = b.Y0 + b.R + b.Y1;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        internal double DistanceAfterChangeDirection(IBall a, IBall b, double x1, double y1, double x2, double y2)
        {
            double dx1 = a.X0 + a.R + x1;
            double dy1 = a.Y0 + a.R + y1;
            double dx2 = b.X0 + b.R + x2;
            double dy2 = b.Y0 + b.R + y2;

            return Math.Sqrt((Math.Pow(dx1 - dx2, 2) + Math.Pow(dy1 - dy2, 2)));
        }


        public override IList CreateBalls(int number)
        {
            int previousCount = dataLayer.GetCount;
            IList temp = dataLayer.CreateBalls(number);
            for (int i = 0; i < dataLayer.GetCount - previousCount; i++)
            {
                dataLayer.GetBall(previousCount + i).PropertyChanged += BallPositionChanged;
            }
            return temp;
        }

        public override IBall GetBall(int index)
        {
            return dataLayer.GetBall(index);
        }

        public override int GetCount { get => dataLayer.GetCount; }

        public override void BallPositionChanged(object sender, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)sender;
            mutex.WaitOne();
            WallCollision(ball);
            ChangeDirection(ball);
            mutex.ReleaseMutex();
        }


    }
}