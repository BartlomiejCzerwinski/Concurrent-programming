using Logic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int height { get; }
        public abstract int width { get; }
        public abstract Canvas Canvas { get; set; }
        public abstract List<Ellipse> ellipseCollection { get; }
        public abstract void CreateEllipses(int ballVal);
        public abstract void Move();

        public abstract void Stop();


        public static ModelAbstractApi CreateApi(int Height, int Width)
        {
            return new ModelApi(Height, Width);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override int width { get; }
        public override int height { get; }

        private LogicAbstractApi LogicLayer;
        public override List<Ellipse> ellipseCollection { get; }
        public override Canvas Canvas { get; set; }
        public ModelApi(int Height, int Width)
        {

            width = Width;
            height = Height;
            LogicLayer = LogicAbstractApi.CreateApi(width, height);
            Canvas = new Canvas();
            ellipseCollection = new List<Ellipse>();
            Canvas.HorizontalAlignment = HorizontalAlignment.Left;
            Canvas.VerticalAlignment = VerticalAlignment.Bottom;
            Canvas.Width = width;
            Canvas.Height = height;
            LogicLayer.Update += (sender, args) => Move();
        }
        public override void CreateEllipses(int numberOfBalls)
        {
            Random random = new Random();
            LogicLayer.CreateBallsList(numberOfBalls);

            for (int i = LogicLayer.GetCount - numberOfBalls; i < LogicLayer.GetCount; i++)
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 128), (byte)random.Next(128, 256), (byte)random.Next(128, 256)));
                Ellipse ellipse = new Ellipse
                {
                    Width = LogicLayer.GetDiagonal(i),
                    Height = LogicLayer.GetDiagonal(i),
                    Fill = brush
                };
                Canvas.SetLeft(ellipse, LogicLayer.GetX(i));
                Canvas.SetTop(ellipse, LogicLayer.GetY(i));

                ellipseCollection.Add(ellipse);
                Canvas.Children.Add(ellipse);
            }
            LogicLayer.Start();

        }

        public override void Move()
        {
            for (int i = 0; i < LogicLayer.GetCount; i++)
            {
                Canvas.SetLeft(ellipseCollection[i], LogicLayer.GetX(i));
                Canvas.SetTop(ellipseCollection[i], LogicLayer.GetY(i));
            }
            for (int i = LogicLayer.balls.Count; i < ellipseCollection.Count; i++)
            {
                Canvas.Children.Remove(ellipseCollection[ellipseCollection.Count - 1]);
                ellipseCollection.Remove(ellipseCollection[ellipseCollection.Count - 1]);
            }
        }

        public override void Stop()
        {
            LogicLayer.Stop();
        }
    }

}
