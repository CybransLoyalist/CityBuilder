
using System;
using System.Collections.Generic;
using CityBuilder.AreaWithBuildingFilling;
using CityBuilder.Buildings.Patterns;
using CityBuilder.MapModel.Tiles;
using CityBuilder.Util;
using Moq;
using NUnit.Framework;

namespace CityBuilderTests.AreaWithBuildingFilling
{
    [TestFixture]
    class RandomBuildingLocationGeneratorTests
    {
        private RandomBuildingLocationGenerator _cut;
        private Mock<TileAnglesCombinations> _tileAnglesCombinationsMock;
        private Mock<BuildingTypesOfEqualSize> _buildingTypesOfEqualSizeMock;

        [SetUp]
        public void SetUp()
        {
            _tileAnglesCombinationsMock = new Mock<TileAnglesCombinations>(new List<ITile>());

            _buildingTypesOfEqualSizeMock = new Mock<BuildingTypesOfEqualSize>(new List<Type>(), 0);
            _buildingTypesOfEqualSizeMock.Setup(a => a.GetRandomType()).Returns(typeof(SingleTileBuilding));

            _cut = new RandomBuildingLocationGenerator();
        }

        [Test]
        public void TypeShallBeRandomlyChosen_FromBuildingTypesOfEqualSize()
        {
            var type = typeof(SingleTileBuilding);
            _buildingTypesOfEqualSizeMock.Setup(a => a.GetRandomType()).Returns(type);

            var result = _cut.Generate(
                _tileAnglesCombinationsMock.Object,
                _buildingTypesOfEqualSizeMock.Object);

            Assert.AreEqual(type, result.Type);

            _buildingTypesOfEqualSizeMock.Verify(a => a.GetRandomType());
        }

        [Test]
        public void TileShallBeRandomlyChosen_FromTileAnglesCombinations()
        {
            var tile = new Tile();
            _tileAnglesCombinationsMock.Setup(a => a.GetRandomTile()).Returns(tile);

            var result = _cut.Generate(
                _tileAnglesCombinationsMock.Object,
                _buildingTypesOfEqualSizeMock.Object);

            Assert.AreEqual(tile, result.Tile);

            _tileAnglesCombinationsMock.Verify(a => a.GetRandomTile());
        }

        [Test]
        public void AngleShallBeRandomlyChosen_FromTileAnglesCombinations()
        {
            var angle = Angle.OneHundredEighty;
            var tile = new Tile();
            _tileAnglesCombinationsMock.Setup(a => a.GetRandomTile()).Returns(tile);
            _tileAnglesCombinationsMock.Setup(a => a.GetRandomAngleForTile(tile)).Returns(angle);

            var result = _cut.Generate(
                _tileAnglesCombinationsMock.Object,
                _buildingTypesOfEqualSizeMock.Object);

            Assert.AreEqual(angle, result.Angle);

            _tileAnglesCombinationsMock.Verify(a => a.GetRandomAngleForTile(tile));
        }
    }
}
