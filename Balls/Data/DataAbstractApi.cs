using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int number);

        public abstract IBall GetBall(int index);
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }
        private readonly Mutex mutex = new Mutex();

        private readonly Random random = new Random();

        public override int Width { get; }
        public override int Height { get; }
        public ObservableCollection<IBall> Balls => balls;
        public DataApi()
        {
            balls = new ObservableCollection<IBall>();
            Height = Board.height;
            Width = Board.width;
        }
        public override IList CreateBalls(int number)
        {
            Random random = new Random();
            if (number > 0)
            {
                int ballsCount = balls.Count;
                for (int i = 0; i < number; i++)
                {
                    mutex.WaitOne();
                    int r = 20;
                    int weight = 30;
                    int x0 = random.Next(r, Width - r);
                    int y0 = random.Next(r, Height - r);
                    int x1 = random.Next(-5, 5);
                    int y1 = random.Next(-5, 5);
                    
                    Ball ball = new Ball(i + ballsCount, x0, y0, x1, y1, r, weight);
                    balls.Add(ball);
                    mutex.ReleaseMutex();
                }
            }
                return balls;
        }
        public override int GetCount { get => balls.Count; }

        public override IBall GetBall(int index)
        {
            return balls[index];
        }

    }
}