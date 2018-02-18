using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class LongOrthogonalBuilding : Building
    {
        public LongOrthogonalBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }
        
        public override IList<ITilePattern> TilePatterns => new List<ITilePattern>
        {
            new TilePattern(new Point(0, 0), Direction.Left, Direction.Top, Direction.Right),
            new TilePattern(new Point(0, 1), Direction.Left, Direction.Right),
            new TilePattern(new Point(1, 2), Direction.Left, Direction.Bottom),
            new TilePattern(new Point(1, 2), Direction.Bottom, Direction.Top, Direction.Right),
        };
    }
}