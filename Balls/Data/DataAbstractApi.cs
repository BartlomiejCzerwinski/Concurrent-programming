using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract IBall CreateBall(int number);
        public abstract void StopLoggingTask();
        public abstract Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue);
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
    internal class DataApi : DataAbstractApi
    {
        private readonly Random random = new Random();
        private readonly Stopwatch stopWatch;
        private bool stop;
        private object locker = new object();

        public override int Width { get; }
        public override int Height { get; }
        public DataApi()
        {
            Height = Board.height;
            Width = Board.width;
            stopWatch = new Stopwatch();
        }
        public override IBall CreateBall(int number)
        {
            Random random = new Random();
                    
            int r = 15;
            int weight = random.Next(15, 30);
            int x0 = random.Next(r, Width - r);
            int y0 = random.Next(r, Height - r);
            int x1, y1;
            do
            {
                x1 = random.Next(-5, 5);
                y1 = random.Next(-5, 5);
            } while (x1 == 0 && y1 == 0);

            Ball ball = new Ball(number, x0, y0, x1, y1, r, weight);
                 
          return ball;
        }

        public override void StopLoggingTask()
        {
            stop = true;
        }

        public override Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue)
        {
            stop = false;
            return CallLogger(logQueue);
        }

        internal async Task CallLogger(ConcurrentQueue<IBall> logQueue)
        {
            while (!stop)
            {
                stopWatch.Reset();
                stopWatch.Start();
                logQueue.TryDequeue(out IBall logObject);
                if (logObject != null)
                {
                    string diagnostics = JsonSerializer.Serialize(logObject);
                    string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                    string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, diagnostics) + "}";

                    lock (locker)
                    {
                        File.AppendAllText("LogFile.json", log);
                    }
                }
                else
                {
                    return;
                }
                stopWatch.Stop();
                await Task.Delay((int)(stopWatch.ElapsedMilliseconds));
            }
        }



    }
}