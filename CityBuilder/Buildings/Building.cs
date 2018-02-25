using System;
using System.Collections.Generic;
using CityBuilder.Util;

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
    }
}