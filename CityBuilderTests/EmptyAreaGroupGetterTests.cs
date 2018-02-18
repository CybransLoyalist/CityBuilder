using System.Collections.Generic;
using CityBuilding;
using NUnit.Framework;

namespace CityBuilderTests
{
    [TestFixture]
    public class EmptyAreaGroupGetterTests
    {
        private readonly EmptyAreaGroupGetter _cut = new EmptyAreaGroupGetter();

        [Test]
        public void IfNoEmptyTilesArePresent_ResultShallBeEmptyListOfGroups()
        {
            var map = new Map(4, 7);
            var emptyAreas = _cut.Get(map);
            Assert.AreEqual(0, emptyAreas.Count);
        }

        [Test]
        public void IfMapContainsTwoGroupsOfTiles_TheyShallBeFound()
        {
            var map = new Map(4, 7);

            var firstExcpectedGroup = new List<ITile>
            {
                map[1, 0],
                map[2, 0],
                map[3, 0],


                map[0, 1],
                map[1, 1],
                map[2, 1],
                map[3, 1],


                map[0, 2],
                map[1, 2],
                map[2, 2],
                map[3, 2],


                map[1, 3],
                map[2, 3],
            };

            foreach (var tile in firstExcpectedGroup)
            {
                tile.TileState = TileState.Empty;
            }

            var secondExpectedGroup = new List<ITile>
            {
                map[5, 0],
                map[6, 0],


                map[5, 1],
                map[6, 1],


                map[5, 2],
                map[6, 2],


                map[6, 3],
            };

            foreach (var tile in secondExpectedGroup)
            {
                tile.TileState = TileState.Empty;
            }

            var emptyAreas = _cut.Get(map);

            Assert.AreEqual(2, emptyAreas.Count);


            foreach (var expectedTile in firstExcpectedGroup)
            {
                Assert.True(emptyAreas[0].Contains(expectedTile));
            }

            foreach (var expectedTile in secondExpectedGroup)
            {
                Assert.True(emptyAreas[1].Contains(expectedTile));
            }
        }
    }
}