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

        public static IList<IList<Type>> Types { get; set; } = new List<IList<Type>>
        {
            new List<Type>{typeof(CourtyardBuilding)},
            new List<Type>{typeof(LongOrthogonalBuilding),  typeof(SquareBuilding)},
            new List<Type>{typeof(ShortOrthogonalBuilding) },
            new List<Type>{typeof(SingleTileBuilding) },
        };
    }
}