using System;

namespace CityBuilder.Map
{
    public struct Point : IPoint
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }

        public static double Distance(IPoint start, IPoint end)
        {
            return Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y));
        }
    }
}