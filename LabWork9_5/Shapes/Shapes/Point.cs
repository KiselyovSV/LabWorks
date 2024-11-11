using System;

namespace Shapes
{
    internal class Point
    {
        int X { get; set;}
        int Y { get; set;}
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        internal double CalculatingLength(Point another)
        {
            double length = Math.Sqrt(Math.Pow(another.X - X, 2) + Math.Pow(another.Y - Y, 2));
            return length;
        }

    }
}
