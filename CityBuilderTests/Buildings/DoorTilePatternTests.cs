using CityBuilder.Buildings;
using CityBuilder.MapModel;
using NUnit.Framework;

namespace CityBuilderTests.Buildings
{
    [TestFixture]
    public class DoorTilePatternTests
    {
        [Test]
        public void Transformation_ShallBe_0_0()
        {
            var result = new DoorTilePattern();
            Assert.AreEqual(new Point(0,0), result.Transformation);
        }

        [Test]
        public void ShallBeDoor()
        {
            var result = new DoorTilePattern();
            Assert.True(result.IsDoor);
        }
    }
}