using CityBuilder.Buildings;
using CityBuilder.MapModel;
using NUnit.Framework;

namespace CityBuilderTests.Buildings
{
    [TestFixture]
    public class TilePatternTests
    {
        [Test]
        public void Transformation_ShallBeSet()
        {
            var point = new Point(10,1);
            var result = new TilePattern(point);
            Assert.AreEqual(point, result.Transformation);
        }

        [Test]
        public void ShallNotBeDoor()
        {
            var result = new TilePattern(new Point(10, 1));
            Assert.False(result.IsDoor);
        }
    }
}