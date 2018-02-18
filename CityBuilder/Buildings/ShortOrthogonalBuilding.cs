using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class ShortOrthogonalBuilding : Building
    {
        public ShortOrthogonalBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => new List<ITilePattern>
        {
            new TilePattern(new Point(0, 0), Direction.Left, Direction.Top, Direction.Right),
            new TilePattern(new Point(0, 1), Direction.Left,  Direction.Bottom),
            new TilePattern(new Point(1, 1), Direction.Bottom, Direction.Top, Direction.Right),
        };
    }
}