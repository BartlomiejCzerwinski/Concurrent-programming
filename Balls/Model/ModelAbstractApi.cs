using Logic;
using System;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int width { get; }
        public abstract int height { get; }
        public abstract void StartMoving();
        public abstract IList Start(int ballVal);
        public abstract void Stop();


        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override int width { get; }
        public override int height { get; }
        private readonly LogicAbstractApi LogicLayer;

        public ModelApi()
        {
            LogicLayer = LogicAbstractApi.CreateApi();
            width = LogicLayer.Width;
            height = LogicLayer.Height;
        }

        public override void StartMoving()
        {
            LogicLayer.Start();
        }   


        public override void Stop()
        {
            LogicLayer.Stop();
        }

        public override IList Start(int ballVal) => LogicLayer.CreateBalls(ballVal);

    }

}
