using System;
using CityBuilder.MapModel;
using NUnit.Framework;

namespace CityBuilderTests.MapModel
{
    [TestFixture]
    public class PointTests
    {
        public void ConstructorShallSetX()
        {
            var point = new Point(4,2);
            Assert.AreEqual(4, point.X);
        }

        public void ConstructorShallSetY()
        {
            var point = new Point(4,2);
            Assert.AreEqual(2, point.Y);
        }

        [TestCase(0, 0, 3, 4, 5)]
        [TestCase(3,2,5,1, 2.23607)]
        public void DistanceShallBeCalculatedCorrectly(
            int point1X, int point1Y,
            int point2X, int point2Y,
            decimal expectedDistance)
        {
            var point1 = new Point(point1X, point1Y);
            var point2 = new Point(point2X, point2Y);

            Assert.AreEqual(expectedDistance, Decimal.Round((decimal)Point.Distance(point1, point2), 5));
        }
    }
}