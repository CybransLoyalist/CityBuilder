using System.Collections.Generic;
using System.Linq;
using CityBuilding;
using NUnit.Framework;

namespace CityBuilderTests
{
    [TestFixture]
    public class MapTests
    {
        private const int Width = 5;
        private const int Height = 10;

        [Test]
        public void Width_ShallBeSet()
        {
            var map = new Map(Height, Width);
            Assert.AreEqual(5, map.Width);
        }

        [Test]
        public void Height_ShallBeSet()
        {
            var map = new Map(Height, Width);
            Assert.AreEqual(10, map.Height);
        }

        [Test]
        public void AllTilesShallBeInitiallySetToBlocked()
        {
            var map = new Map(Height, Width);
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Assert.AreEqual(TileState.Blocked, map[i, j].TileState);
                }
            }
        }

        [Test]
        public void TileLocation_ShallGetProperLocation()
        {
            var map = new Map(Height, Width);
            var tileLocation = map.GetLocationOf(map[4, 5]);
            Assert.AreEqual(tileLocation.X, 4);
            Assert.AreEqual(tileLocation.Y, 5);
        }

        [Test]
        public void LeftTopTile_ShallHaveTwoNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, 0], NeighbourMode.Orthogonal).ToList();
            Assert.AreEqual(2, neighbours.Count);

            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 1), map, neighbours));
        }

        [Test]
        public void RightTop_ShallHaveTwoNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[Width - 1, 0], NeighbourMode.Orthogonal).ToList();
            Assert.AreEqual(2, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(Width - 2, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(Width - 1, 1), map, neighbours));
        }

        [Test]
        public void LeftBottom_ShallHaveTwoNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, Height - 1], NeighbourMode.Orthogonal).ToList();
            Assert.AreEqual(2, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, Height - 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, Height - 1), map, neighbours));
        }

        [Test]
        public void TileByTheWall_ShallHaveThreeNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, 1], NeighbourMode.Orthogonal).ToList();
            Assert.AreEqual(3, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 1), map, neighbours));
        }

        [Test]
        public void InnerTile_ShallHaveFourNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[1, 1], NeighbourMode.Orthogonal).ToList();
            Assert.AreEqual(4, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(2, 1), map, neighbours));
        }

        [Test]
        public void LeftTopTile_ShallHaveThreeNeighbours_IfNeigbourMode_All()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, 0], NeighbourMode.All).ToList();
            Assert.AreEqual(3, neighbours.Count);

            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 1), map, neighbours));
        }

        [Test]
        public void RightTop_ShallHaveThreeNeighbours_IfNeigbourMode_All()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[Width - 1, 0], NeighbourMode.All).ToList();
            Assert.AreEqual(3, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(Width - 2, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(Width - 2, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(Width - 1, 1), map, neighbours));
        }

        [Test]
        public void LeftBottom_ShallHaveThreeNeighbours_IfNeigbourMode_All()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, Height - 1], NeighbourMode.All).ToList();
            Assert.AreEqual(3, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, Height - 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, Height - 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, Height - 2), map, neighbours));
        }

        [Test]
        public void TileByTheWall_ShallHaveFiveNeighbours_IfNeigbourMode_All()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[0, 1], NeighbourMode.All).ToList();
            Assert.AreEqual(5, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 2), map, neighbours));
        }

        [Test]
        public void InnerTile_ShallHaveEightNeighbours_IfNeigbourMode_Orthogonal()
        {
            var map = new Map(Height, Width);
            var neighbours = map.GetNeighboursOf(map[1, 1], NeighbourMode.All).ToList();
            Assert.AreEqual(8, neighbours.Count());

            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(1, 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(2, 1), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(2, 0), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(0, 2), map, neighbours));
            Assert.True(NeighboursContainsAnyWithLocation(new Point(2, 2), map, neighbours));
        }

        private static bool NeighboursContainsAnyWithLocation(Point point, Map map, IEnumerable<ITile> neighbours)
        {
            return neighbours.Any(a => map.GetLocationOf(a).X == point.X && map.GetLocationOf(a).Y == point.Y);
        }
    }
}