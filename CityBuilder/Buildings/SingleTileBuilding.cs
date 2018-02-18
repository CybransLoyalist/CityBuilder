using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class SingleTileBuilding : Building
    {
        public SingleTileBuilding(Guid guid) : base(guid)
        {
        }

        public override ITilePattern TilePattern => new TilePattern
        {
            Transformations = new List<Point>
            {
                new Point(0, 0)
            }
        };
    }
}