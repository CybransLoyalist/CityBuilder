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
            new TilePattern(new Point(0, 0), true),
            new TilePattern(new Point(-1,-1)),
            new TilePattern(new Point(-1,0)),
            new TilePattern(new Point(-1,1)),
            new TilePattern(new Point(0, 1)),
        };
    }
}