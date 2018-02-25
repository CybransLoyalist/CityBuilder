using System;
using CityBuilder.Buildings.Patterns;
using CityBuilder.Util;
using NUnit.Framework;

namespace CityBuilderTests.Buildings
{
    [TestFixture]
    public class BuildingTests
    {
        [Test]
        public void ConstructorShallSetGuid()
        {
            var guid = Guid.NewGuid();
            var building = new SingleTileBuilding(guid, Angle.Zero);
            Assert.AreEqual(guid, building.Guid);
        }

        [Test]
        public void ConstructorShallSetAngle()
        {
            var building = new ShortOrthogonalBuilding(Guid.NewGuid(), Angle.Zero);
            Assert.AreEqual(Angle.Zero, building.Angle);
        }
    }
}