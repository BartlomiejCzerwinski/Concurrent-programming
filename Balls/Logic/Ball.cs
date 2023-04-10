using System;
using Data;

namespace Logic
{
    public class Ball
    {
        private int x, y, r;
        private double angle;
        public int X
        {
            get => x;
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
            }
        }

        public int R
        {
            get => r;
            set
            {
                r = value;
            }
        }

        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
            }
        }

        public Ball(int x, int y, int r, double angle)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.angle = angle;

        }

        public void MoveBall(int height, int width)
        {
            Random random = new Random();
            // Obliczenie nowej pozycji piłki na podstawie kierunku
            double dx = Math.Cos(angle) * 4;
            double dy = Math.Sin(angle) * 4;
            x += (int)dx;
            y += (int)dy;

            bool hitWall = false;
            // Sprawdzenie czy piłka nie wychodzi poza kwadrat
            if (x + r >= width)
            {
                x = width - r;
                angle = Math.PI - angle;
                hitWall = true;
            }
            else if (x - r <= 0)
            {
                x = r;
                angle = Math.PI - angle;
                hitWall = true;
            }

            if (y + r >= height)
            {
                y = height - r;
                angle = -angle;
                hitWall = true;
            }
            else if (y - r <= 0)
            {
                y = r;
                angle = -angle;
                hitWall = true;
            }
            if (hitWall)
            {
                angle += random.NextDouble() * Math.PI / 2 - Math.PI / 4;
            }

        }

    }
}