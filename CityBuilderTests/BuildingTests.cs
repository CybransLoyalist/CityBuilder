using System;
using CityBuilding;
using NUnit.Framework;

namespace CityBuilderTests
{
    [TestFixture]
    public class BuildingTests
    {
        [Test]
        public void ConstructorShallSetGuid()
        {
            var guid = Guid.NewGuid();
            var building = new Building(guid);
            Assert.AreEqual(guid, building.Guid);
        }

        [Test]
        public void ConstructorShallSetEmptyTilesList()
        {
            var building = new Building(Guid.NewGuid());
            Assert.NotNull(building.Tiles);
            Assert.AreEqual(0, building.Tiles.Count);
        }
    }
}