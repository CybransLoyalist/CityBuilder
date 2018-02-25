using System;
using System.Collections.Generic;
using CityBuilder.Map;
using CityBuilder.Util;

namespace CityBuilder.Buildings.Patterns
{
    public class ShortOrthogonalBuilding : Building
    {
        
        public ShortOrthogonalBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => Pattern;

        public static IList<ITilePattern> Pattern => new List<ITilePattern>
        {
            new DoorTilePattern(),
            new TilePattern(new Point(0, 1)),
            new TilePattern(new Point(1, 0)),
            new TilePattern(new Point(1, 1)),
        };
    }
}