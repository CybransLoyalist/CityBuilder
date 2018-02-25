using System;
using System.Collections.Generic;
using CityBuilder.Buildings;
using CityBuilder.Buildings.Patterns;
using CityBuilder.MapModel;
using CityBuilder.MapModel.Tiles;
using CityBuilder.Util;
using NUnit.Framework;

namespace CityBuilderTests.Buildings
{
    [TestFixture]
    public class BuildingLocationTests
    {
        [Test]
        public void ConstructorShallSetTile()
        {
            var tile = new Tile();
            var buildingLocation = new BuildingLocation(typeof(SingleTileBuilding), tile, Angle.Zero);
            Assert.AreEqual(tile, buildingLocation.Tile);
        }

        [Test]
        public void ConstructorShallSetType()
        {
            var type = typeof(SingleTileBuilding);
            var buildingLocation = new BuildingLocation(type, new Tile(), Angle.Zero);
            Assert.AreEqual(type, buildingLocation.Type);
        }

        [Test]
        public void ConstructorShallSetAngle()
        {
            var buildingLocation = new BuildingLocation(typeof(SingleTileBuilding), new Tile(), Angle.Zero);
            Assert.AreEqual(Angle.Zero, buildingLocation.Angle);
        }

        [Test]
        public void ConstructorShallSetExpection_IfTypeIsNotBuilding()
        {
            var type = typeof(List<IMap>);
            Assert.Throws<ArgumentException>(() => new BuildingLocation(type, new Tile(), Angle.Zero));
        }

        [Test]
        public void Instantiate_ShallCreateBuilding()
        {
            var buildingLocation = new BuildingLocation(typeof(SingleTileBuilding), new Tile(), Angle.Zero);
            var result = buildingLocation.Instantiate();
            Assert.True(result is SingleTileBuilding);
            Assert.AreEqual(Angle.Zero, result.Angle);
        }
    }
}