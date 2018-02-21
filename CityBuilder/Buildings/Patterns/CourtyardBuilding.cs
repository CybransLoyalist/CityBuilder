using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class CourtyardBuilding : Building
    {

        public CourtyardBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => new List<ITilePattern>
        {
            new DoorTilePattern(),
            new TilePattern(new Point(0,1)),
            new TilePattern(new Point(0,2)),
            new TilePattern(new Point(0,3)),
            new TilePattern(new Point(1,1)),
            new TilePattern(new Point(1,3)),
            new TilePattern(new Point(2,1)),
            new TilePattern(new Point(2,2)),
            new TilePattern(new Point(2,3)),
        };
    }
}