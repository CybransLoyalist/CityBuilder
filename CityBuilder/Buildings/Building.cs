using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public abstract class Building : IBuilding
    {
        protected Building(Guid guid, Angle angle)
        {
            Guid = guid;
            Angle = angle;
        }

        public Guid Guid { get; }
        public abstract IList<ITilePattern> TilePatterns { get; }
        public Angle Angle { get;  }

        public static IList<Type> Types { get; set; } = new List<Type>
        {
            typeof(SquareBuilding),
            typeof(LongOrthogonalBuilding),
            typeof(ShortOrthogonalBuilding),
            typeof(SingleTileBuilding),
        };
    }
}