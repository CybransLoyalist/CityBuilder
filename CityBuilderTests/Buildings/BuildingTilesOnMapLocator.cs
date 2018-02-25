using CityBuilder;
using CityBuilding;
using NUnit.Framework;

namespace CityBuilderTests.Buildings
{
    [TestFixture]
    public class BuildingTilesOnMapLocatorTests
    {
        private BuildingOnMapLocator _cut;

        [SetUp]
        public void SetUp()
        {
            _cut = new BuildingOnMapLocator();
        }

        [TestCase(Angle.Zero)]
        [TestCase(Angle.Ninety)]
        [TestCase(Angle.OneHundredEighty)]
        [TestCase(Angle.TwoHundredSeventy)]
        public void SingleTileBuilding_ShallBeLocatedOnProperTile(Angle angle)
        {
//            var map = new Map(2,2);
//           _cut.Locate(map, new SingleTileBuilding(Guid.NewGuid()), new Point(1, 1), angle);
//
//            Assert.AreEqual(1, result.Count());
//
//            var locationOfTileOnMap = map.GetLocationOf(result.First());
//            Assert.AreEqual(new Point(1,1), locationOfTileOnMap);
        }

//        [Test]
//        public void ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngleZero()
//        {
//            var expectedLocations = new List<IPoint>
//            {
//                new Point(1,1),
//                new Point(2,2),
//                new Point(1,2),
//            };
//
//            var angle = Angle.Zero;
//
//            ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngle(expectedLocations, angle);
//        }
//
//        [Test]
//        public void ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngleNinety()
//        {
//            var expectedLocations = new List<IPoint>
//            {
//                new Point(1,1),
//                new Point(2,0),
//                new Point(2,1),
//            };
//
//            var angle = Angle.Ninety;
//
//            ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngle(expectedLocations, angle);
//        }
//
//        [Test]
//        public void ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngleOneHundredEighty()
//        {
//            var expectedLocations = new List<IPoint>
//            {
//                new Point(1,1),
//                new Point(0,0),
//                new Point(1,0),
//            };
//
//            var angle = Angle.OneHundredEighty;
//
//            ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngle(expectedLocations, angle);
//        }
//
//        [Test]
//        public void ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngleTwoHundredSeventy()
//        {
//            var expectedLocations = new List<IPoint>
//            {
//                new Point(1,1),
//                new Point(0,1),
//                new Point(0,2),
//            };
//
//            var angle = Angle.TwoHundredSeventy;
//
//            ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngle(expectedLocations, angle);
//        }

//        private void ShortOrthogonalBuilding_ShallBeProperlyLocated_ForAngle(List<IPoint> expectedLocations, Angle angle)
//        {
//            var map = new Map(4, 4);
//            var result = _cut.Locate(map, new ShortOrthogonalBuilding(Guid.NewGuid()), new Point(1, 1), angle);
//
//            Assert.AreEqual(3, result.Count());
//
//            var buildingTilesLocations = result.Select(tile => map.GetLocationOf(tile));
//
//            foreach (var expectedLocation in expectedLocations)
//            {
//                Assert.True(buildingTilesLocations.Any(a => a.X == expectedLocation.X && a.Y == expectedLocation.Y));
//            }
//        }
    }
}