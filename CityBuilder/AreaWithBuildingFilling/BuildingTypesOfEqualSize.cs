using System;
using System.Collections.Generic;
using CityBuilder.Extensions;

namespace CityBuilder.AreaWithBuildingFilling
{
    public class BuildingTypesOfEqualSize
    {
        public BuildingTypesOfEqualSize( IList<Type> types, int tilesCount)
        {
            TilesCount = tilesCount;
            Types = types;
        }

        public int TilesCount { get; set; }
        public IList<Type> Types { get; set; }

        public virtual Type GetRandomType()
        {
            return Types.Random();
        }
    }
}