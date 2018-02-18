using System;
using CityBuilder.Buildings;
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
            var building = new SingleTileBuilding(guid);
            Assert.AreEqual(guid, building.Guid);
        }

        [Test]
        public void ConstructorShallSetEmptyTilesList()
        {
            var building = new ShortOrthogonalBuilding(Guid.NewGuid());
            Assert.NotNull(building.Tiles);
            Assert.AreEqual(0, building.Tiles.Count);
        }
    }
}