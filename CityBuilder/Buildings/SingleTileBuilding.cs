using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class SingleTileBuilding : Building
    {
        public SingleTileBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => new List<ITilePattern>
        {
            new TilePattern(new Point(0, 0), Direction.Left, Direction.Top, Direction.Right, Direction.Bottom),
        };
    }
}