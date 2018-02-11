
using CityBuilding;
using NUnit.Framework;

namespace CityBuilderTests
{
    [TestFixture]
    public class CityBuilderTests
    {
        private CityBuilder _cut = new CityBuilder();

        [Test]
        public void test1()
        {
            var map = new Map(4, 7);
            //first group
            map[1,0].TileState = TileState.Empty;
            map[2,0].TileState = TileState.Empty;
            map[3,0].TileState = TileState.Empty;


            map[0, 1].TileState = TileState.Empty;
            map[1, 1].TileState = TileState.Empty;
            map[2, 1].TileState = TileState.Empty;
            map[3, 1].TileState = TileState.Empty;


            map[0, 2].TileState = TileState.Empty;
            map[1, 2].TileState = TileState.Empty;
            map[2, 2].TileState = TileState.Empty;
            map[3, 2].TileState = TileState.Empty;

            map[1, 3].TileState = TileState.Empty;
            map[2, 3].TileState = TileState.Empty;

            //second group


            map[5, 0].TileState = TileState.Empty;
            map[6, 0].TileState = TileState.Empty;

            map[5, 1].TileState = TileState.Empty;
            map[6, 1].TileState = TileState.Empty;

            map[5, 2].TileState = TileState.Empty;
            map[6, 2].TileState = TileState.Empty;
            
            map[6, 3].TileState = TileState.Empty;

            var emptyAreas = _cut.GetEmptyAreas(map);
        }
    }
}
