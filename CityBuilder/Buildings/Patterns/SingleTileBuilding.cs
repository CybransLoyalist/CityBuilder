using System;
using System.Collections.Generic;
using CityBuilder.MapModel;
using CityBuilder.Util;

namespace CityBuilder.Buildings.Patterns
{
    public class SingleTileBuilding : Building
    {  
        
        public SingleTileBuilding(Guid guid, Angle angle) : base(guid, angle)
        {
        }

        public override IList<ITilePattern> TilePatterns => Pattern;

        public static IList<ITilePattern> Pattern => new List<ITilePattern>
        {
            new DoorTilePattern(),
            new TilePattern(new Point(0, 1)),
        };
    }
}