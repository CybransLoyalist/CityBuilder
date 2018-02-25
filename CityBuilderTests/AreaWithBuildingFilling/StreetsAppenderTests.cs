using System.Collections.Generic;
using System.Linq;
using CityBuilder.AreaWithBuildingFilling;
using CityBuilder.MapModel;
using CityBuilder.MapModel.Tiles;
using NUnit.Framework;

namespace CityBuilderTests.AreaWithBuildingFilling
{
    [TestFixture]
    public class StreetsAppenderTests
    {
        private readonly StreetsAppender _cut = new StreetsAppender();

        [Test]
        public void IfNoEmptyTilesArePresent_ResultShallBeEmptyListOfGroups()
        {
            var map = new CityBuilder.MapModel.Map(4, 7);
            var streetTiles = _cut.AppendStreets(map, new List<EmptyAreaGroup>());
            Assert.AreEqual(0, streetTiles.Count());
        }

        [Test]
        public void SingleEmptyGroup_ShallBeSurroundedWithStreetTiles()
        {
            var map = new CityBuilder.MapModel.Map(5, 7);
            var emptyGroup = new EmptyAreaGroup
                {Tiles = new List<ITile>
            {
                map[1, 1],
                map[1, 2],
                map[2, 1],
                map[2, 2],
            }};

            foreach (var tile in emptyGroup.Tiles)
            {
                tile.TileState = TileState.Empty;
            }

            _cut.AppendStreets(map, new List<EmptyAreaGroup>{emptyGroup});

            Assert.AreEqual(4, emptyGroup.Tiles.Count(a => a.TileState == TileState.Empty));
            Assert.AreEqual(12, map.AllTiles.Count(a => a.TileState == TileState.Street));
        }

        [Test]
        public void IfTwoGroupsTouchByCorners_OneCornerShallBeRemoved_ToCreatePassage()
        {
            var map = new CityBuilder.MapModel.Map(6,6);
            var firstEmptyGroup = new EmptyAreaGroup
            {
                Tiles = new List<ITile>
                {
                    map[1, 1],
                    map[1, 2],
                    map[2, 1],
                    map[2, 2],
                }
            };

            foreach (var tile in firstEmptyGroup.Tiles)
            {
                tile.TileState = TileState.Empty;
            }

            var secondEmptyGroup = new EmptyAreaGroup
            {
                Tiles = new List<ITile>
                {
                    map[3,3],
                    map[3,4],
                    map[4,3],
                    map[4,4],
                }
            };

            foreach (var tile in secondEmptyGroup.Tiles)
            {
                tile.TileState = TileState.Empty;
            }

            _cut.AppendStreets(map, new List<EmptyAreaGroup> { firstEmptyGroup, secondEmptyGroup });

            Assert.AreEqual(4, firstEmptyGroup.Tiles.Count(a => a.TileState == TileState.Empty));
            Assert.AreEqual(3, secondEmptyGroup.Tiles.Count(a => a.TileState == TileState.Empty));

            Assert.AreEqual(21, map.AllTiles.Count(a => a.TileState == TileState.Street));
        }

    }
}