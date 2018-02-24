using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings.Patterns
{
    public class SquareBuilding : Building
    {

        public SquareBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => Pattern;

        public static IList<ITilePattern> Pattern => new List<ITilePattern>
        {
            new DoorTilePattern(),
            new TilePattern(new Point(0,1)),
            new TilePattern(new Point(0,2)),
            new TilePattern(new Point(1,1)),
            new TilePattern(new Point(1,2)),
        };
    }
}