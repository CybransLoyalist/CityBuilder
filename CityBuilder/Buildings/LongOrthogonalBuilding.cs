using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class LongOrthogonalBuilding : Building
    {
        public LongOrthogonalBuilding(Guid guid) : base(guid)
        {
        }

        public override ITilePattern TilePattern => new TilePattern
        {
            Transformations = new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 2),
                new Point(1, 2),
            }
        };
    }
}